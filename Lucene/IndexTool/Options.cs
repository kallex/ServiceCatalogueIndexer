using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace IndexTool
{
    class Options
    {
        [HelpVerbOption]
        public string GetUsage(string verb)
        {
            return HelpText.AutoBuild(this, verb);
        }

        public Options()
        {
            ReindexVerb = new ReindexSubOptions();
        }

        [VerbOption("reindex", HelpText = "Reindex whole catalogue")]
        public ReindexSubOptions ReindexVerb { get; set; }
    }

    abstract class CommonSubOptions
    {
        [Option('r', "repositoryRoot", HelpText = "Catalogue repository root folder location.", Required = true)]
        public string CatalogueRepositoryRoot { get; set; }
    }

    class ReindexSubOptions : CommonSubOptions
    {
        
    }
}

