﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;

namespace IndexTool
{
    class Program
    {
        private static Options options = new Options();
        static void Main(string[] args)
        {
            //Debugger.Launch();
            bool success = CommandLine.Parser.Default.ParseArguments(args, options, OnVerbCommand);
            if (!success)
            {
                Environment.Exit(CommandLine.Parser.DefaultExitCodeFail);
            }
        }

        private static void OnVerbCommand(string verb, object verbSubOptions)
        {
            if (verbSubOptions == null)
                return;
            CommonSubOptions commonOptions = (CommonSubOptions) verbSubOptions;
            try
            {
                commonOptions.Validate();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            switch (verb)
            {
                case "reindex":
                    DoReindex((ReindexSubOptions) verbSubOptions);
                    break;
                case "adddoc":
                    DoAddDocument((AddDocumentSubOptions) verbSubOptions);
                    break;
                case "removedoc":
                    DoRemoveDocument((RemoveDocumentSubOptions) verbSubOptions);
                    break;
                case "query":
                    DoQuery((QuerySubOptions) verbSubOptions);
                    break;
//                case "help":
//                    DoHelp((string)verbSubOptions);
//                    break;
            }
        }

        private static void DoQuery(QuerySubOptions verbSubOptions)
        {
            string queryText = verbSubOptions.QueryText;
            if (String.IsNullOrEmpty(queryText))
            {
                Console.Write("Enter query text: ");
                queryText = Console.ReadLine();
            }
            var result = LuceneSupport.LuceneSupport.PerformQuery(verbSubOptions.LuceneIndexRoot, queryText, "ServiceDomainName",
                new WhitespaceAnalyzer());
            Console.WriteLine("Results ("+ result.Length + "):");
            foreach (var doc in result)
            {
                string[] lines = doc.GetFields().Where(field => field.Name != "ID").Select(field => field.Name + "\t" + field.StringValue).ToArray();
                Console.WriteLine(doc.Get("ID") + ":");
                Array.ForEach(lines, Console.WriteLine);
                Console.WriteLine("----------------------");
            }
        }

        private static void DoRemoveDocument(RemoveDocumentSubOptions verbSubOptions)
        {
            string documentID = verbSubOptions.DocumentID;
            LuceneSupport.LuceneSupport.RemoveDocuments(verbSubOptions.LuceneIndexRoot, documentID);
        }

        private static void DoAddDocument(AddDocumentSubOptions verbSubOptions)
        {
            FileInfo[] files = verbSubOptions.GetDocumentFiles();
            XmlSerializer serializer = new XmlSerializer(typeof(ServiceModelAbstractionType));
            List<Document> docs = new List<Document>();
            foreach (var file in files)
            {
                ServiceModelAbstractionType serviceModelAbs = (ServiceModelAbstractionType)serializer.Deserialize(file.OpenRead());
                foreach (var serviceModel in serviceModelAbs.ServiceModel)
                {
                    foreach (var operation in serviceModel.Service.SelectMany(ser => ser.Operation))
                    {
                        string id = file.Name + "_" + operation.name; //.Replace("-", "").Replace(".", "");
                        //id = Guid.NewGuid().ToString();
                        string serviceNameSpace = operation.semanticTypeName;
                        Document doc = new Document();
                        doc.Add(new Field("ID", id, Field.Store.YES, Field.Index.NOT_ANALYZED));
                        doc.Add(new Field("FileName", file.Name, Field.Store.YES, Field.Index.NOT_ANALYZED));
                        doc.Add(new Field("SemanticTypeName", serviceNameSpace, Field.Store.YES, Field.Index.NOT_ANALYZED));
                        doc.Add(new Field("OperationName", operation.name, Field.Store.YES, Field.Index.NOT_ANALYZED));
                        string hashValue = GetCombinedSemanticHashValue(operation);
                        doc.Add(new Field("SemanticCombinedString", hashValue, Field.Store.YES, Field.Index.NOT_ANALYZED));
                        string semanticThumbprintSHA256 = CalculateHexThumbprintSha256(hashValue);
                        doc.Add(new Field("SemanticThumbprint", semanticThumbprintSHA256, Field.Store.YES, Field.Index.NOT_ANALYZED));
                        if (operation.UsesOperation != null)
                        {
                            /*
                            string[] semanticTypeFields =
                                operation.UsesOperation.Select(usesOperation => usesOperation.semanticTypeName)
                                         .ToArray();
                            string singleField = String.Join(" ", semanticTypeFields);
                            Field field = new Field("UsesOperation", singleField, Field.Store.YES,
                                                    Field.Index.ANALYZED);

                            doc.Add(field);
                             */
                            var fields = operation.UsesOperation.Select(usesOperation =>
                                                                        new Field("UsesOperation",
                                                                                  usesOperation.semanticTypeName,
                                                                                  Field.Store.YES,
                                                                                  Field.Index.NOT_ANALYZED)).ToArray();
                            Array.ForEach(fields, doc.Add);
                        }
                        docs.Add(doc);
                    }
                }
            }
            LuceneSupport.LuceneSupport.AddDocuments(verbSubOptions.LuceneIndexRoot, docs.ToArray(), new WhitespaceAnalyzer());
        }

        private static string GetCombinedSemanticHashValue(OperationType operation)
        {
            var returnValueString = getTechSemanticConcatenation(operation.ReturnValue);
            string parameterString = getTechSemanticConcatenation(operation.Parameter);
            string nameString = operation.semanticTypeName;
            string hashSource = string.Format("RETURN{0}-NAME{1}-PARAMETERS{2}",
                          returnValueString, nameString, parameterString);
            return hashSource;
        }

        private static string CalculateHexThumbprintSha256(string hashSource)
        {
            SHA256 sha256 = new SHA256Managed();
            byte[] hashSourceBytes = Encoding.UTF8.GetBytes(hashSource);
            byte[] hashResult = sha256.ComputeHash(hashSourceBytes);
            string hexValue = BitConverter.ToString(hashResult).Replace("-", "");
            return hexValue;
        }

        private static string getTechSemanticConcatenation(SemanticDataType[] semanticTypes)
        {
            if (semanticTypes == null)
                return "";
            var strArray = semanticTypes.OrderBy(semType => semType.semanticTypeName)
                                         .ThenBy(semType => semType.isArray)
                                         .Select(semType => semType.dataType + (semType.isArray ? "[]" : "") +
                                                            ":" + semType.semanticTypeName).ToArray();
            return string.Join(";", strArray);
        }

        private static void DoReindex(ReindexSubOptions verbSubOptions)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(verbSubOptions.LuceneIndexRoot);
            dirInfo.Delete(true);
            LuceneSupport.LuceneSupport.CreateIndex(verbSubOptions.LuceneIndexRoot);
            DoAddDocument(new AddDocumentSubOptions
                {
                    CatalogueRepositoryRoot = verbSubOptions.CatalogueRepositoryRoot,
                    DocumentFilter = verbSubOptions.DocumentFilter,
                    IndexName = verbSubOptions.IndexName,
                });
        }
    }
}
