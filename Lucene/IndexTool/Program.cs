using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
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
            var result = LuceneSupport.LuceneSupport.PerformQuery(verbSubOptions.LuceneIndexRoot, queryText, "ServiceDomainName");
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
            throw new NotImplementedException();
        }

        private static void DoAddDocument(AddDocumentSubOptions verbSubOptions)
        {
            FileInfo[] files = verbSubOptions.GetDocumentFiles();
            XmlSerializer serializer = new XmlSerializer(typeof(ServiceModelType));
            List<Document> docs = new List<Document>();
            foreach (var file in files)
            {
                ServiceModelType serviceModel = (ServiceModelType) serializer.Deserialize(file.OpenRead());
                foreach (var service in serviceModel.Services)
                {
                    foreach (var serviceMethod in service.Service.SelectMany(ser => ser.Method))
                    {
                        string id = file.Name;
                        string serviceNameSpace = String.IsNullOrEmpty(serviceMethod.semanticName)
                                                      ? service.contractNamespaceName
                                                      : serviceMethod.semanticName;
                        Document doc = new Document();
                        doc.Add(new Field("ID", id, Field.Store.YES, Field.Index.ANALYZED));
                        doc.Add(new Field("ServiceDomainName", serviceNameSpace, Field.Store.YES, Field.Index.ANALYZED));
                        doc.Add(new Field("ServiceName", serviceMethod.name, Field.Store.YES, Field.Index.ANALYZED));
                        Field field;
                        docs.Add(doc);
                    }
                }
            }
            LuceneSupport.LuceneSupport.AddDocuments(verbSubOptions.LuceneIndexRoot, docs.ToArray());
        }

        private static void DoReindex(ReindexSubOptions verbSubOptions)
        {
            Console.WriteLine(verbSubOptions.CatalogueRepositoryRoot);
        }
    }
}
