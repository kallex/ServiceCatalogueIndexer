using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Console.WriteLine(doc.GetField("ServiceName"));
            }
        }

        private static void DoRemoveDocument(RemoveDocumentSubOptions verbSubOptions)
        {
            throw new NotImplementedException();
        }

        private static void DoAddDocument(AddDocumentSubOptions verbSubOptions)
        {
            Console.WriteLine(verbSubOptions.LuceneIndexRoot);
        }

        private static void DoReindex(ReindexSubOptions verbSubOptions)
        {
            Console.WriteLine(verbSubOptions.CatalogueRepositoryRoot);
        }
    }
}
