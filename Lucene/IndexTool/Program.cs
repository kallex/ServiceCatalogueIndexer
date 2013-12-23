using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
                        string id = file.Name; //.Replace("-", "").Replace(".", "");
                        //id = Guid.NewGuid().ToString();
                        string serviceNameSpace = operation.semanticTypeName;
                        Document doc = new Document();
                        doc.Add(new Field("ID", id, Field.Store.YES, Field.Index.NOT_ANALYZED));
                        doc.Add(new Field("SemanticTypeName", serviceNameSpace, Field.Store.YES, Field.Index.NOT_ANALYZED));
                        doc.Add(new Field("OperationName", operation.name, Field.Store.YES, Field.Index.NOT_ANALYZED));
                        foreach (var usesOperation in operation.UsesOperation ?? new UsesOperationType[0])
                        {
                            Console.WriteLine("Adding usesoperation: " + operation.semanticTypeName + " " + usesOperation.semanticTypeName);
                            Field field = new Field("UsesOperation", usesOperation.semanticTypeName, Field.Store.YES,
                                                    Field.Index.NOT_ANALYZED, Field.TermVector.YES);
                            doc.Add(field);
                        }
                        docs.Add(doc);
                    }
                }
            }
            LuceneSupport.LuceneSupport.AddDocuments(verbSubOptions.LuceneIndexRoot, docs.ToArray(), new WhitespaceAnalyzer());
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
