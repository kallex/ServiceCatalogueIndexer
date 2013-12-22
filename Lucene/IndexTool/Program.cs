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
            switch (verb)
            {
                case "reindex":
                    DoReindex((ReindexSubOptions) verbSubOptions);
                    break;
                case "adddoc":
                    AddDocument((AddDocumentSubOptions) verbSubOptions);
                    break;
//                case "help":
//                    DoHelp((string)verbSubOptions);
//                    break;
            }
        }

        private static void AddDocument(AddDocumentSubOptions verbSubOptions)
        {
            Console.WriteLine(verbSubOptions.LuceneIndexRoot);
        }

        private static void DoReindex(ReindexSubOptions verbSubOptions)
        {
            Console.WriteLine(verbSubOptions.CatalogueRepositoryRoot);
        }
    }
}
