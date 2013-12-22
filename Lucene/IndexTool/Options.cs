using System;
using System.Collections.Generic;
using System.IO;
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
            AddDocumentVerb = new AddDocumentSubOptions();
        }

        [VerbOption("reindex", HelpText = "Reindex whole catalogue")]
        public ReindexSubOptions ReindexVerb { get; set; }

        [VerbOption("adddoc", HelpText = "Add or update document in index")]
        public AddDocumentSubOptions AddDocumentVerb { get; set; }
    }

    abstract class CommonSubOptions
    {
        [Option('r', "repositoryRoot", HelpText = "Catalogue repository root folder location.", Required = true)]
        public string CatalogueRepositoryRoot { get; set; }

        [Option("ixName", DefaultValue = "Default", HelpText = "Lucene Index Storage Folder")]
        public string IndexName { get; set; }

        // Common auto-calculated values
        public string LuceneIndexRoot
        {
            get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ServiceCatalogueIndexTool" ,IndexName); }
        }
    }

    class ReindexSubOptions : CommonSubOptions
    {
        
    }

    class AddDocumentSubOptions : CommonSubOptions
    {
        [Option('d', "documentFile", HelpText = "Document file name.", Required = true)]
        public string DocumentFileName { get; set; }
    }
}

