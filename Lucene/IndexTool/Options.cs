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
            ReindexGraphVerb = new ReindexGraphSubOptions();
            AddGraphDocumentVerb = new AddGraphDocumentSubOptions();
            AddDocumentVerb = new AddDocumentSubOptions();

            RemoveDocumentVerb = new RemoveDocumentSubOptions();
            QueryVerb = new QuerySubOptions();
        }

        [VerbOption("reindex", HelpText = "Reindex whole catalogue")]
        public ReindexSubOptions ReindexVerb { get; set; }

        [VerbOption("addgraphdoc", HelpText = "Add or update document in graph index")]
        public AddGraphDocumentSubOptions AddGraphDocumentVerb { get; set; }

        [VerbOption("adddoc", HelpText = "Add or update document in index")]
        public AddDocumentSubOptions AddDocumentVerb { get; set; }

        [VerbOption("removedoc", HelpText = "Remove document from index")]
        public RemoveDocumentSubOptions RemoveDocumentVerb { get; set; }

        [VerbOption("query", HelpText = "Query document index")]
        public QuerySubOptions QueryVerb { get; set; }

        [VerbOption("reindexgraph", HelpText = "Reindex whole catalogue to graph db (Neo4j)")]
        public ReindexGraphSubOptions ReindexGraphVerb { get; set; }


    }

    class QuerySubOptions : CommonLuceneSubOptions
    {
        [Option('q', "queryText", HelpText = "Query text. For more complex queries leave this out for query line entering.")]
        public string QueryText { get; set; }
    }

    abstract class CommonGraphSubOptions
    {
        [Option('r', "repositoryRoot", HelpText = "Catalogue repository root folder location.", Required = true)]
        public string CatalogueRepositoryRoot { get; set; }

        protected virtual void validateSelf()
        {

        }
        public void Validate()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(CatalogueRepositoryRoot);
            if (dirInfo.Exists == false)
                throw new ArgumentException("Invalid CatalogueRepositoryRoot value (directory not found): " + CatalogueRepositoryRoot);
            validateSelf();
        }
    }

    abstract class CommonLuceneSubOptions
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

        protected virtual void validateSelf()
        {
            
        }
        public void Validate()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(CatalogueRepositoryRoot);
            if(dirInfo.Exists == false)
                throw new ArgumentException("Invalid CatalogueRepositoryRoot value (directory not found): " + CatalogueRepositoryRoot);
            validateSelf();
        }
    }

    class ReindexSubOptions : CommonLuceneSubOptions
    {
        [Option('d', "documentFilter", HelpText = "Document file filter (applied from Repository Root)", DefaultValue = "*.xml")]
        public string DocumentFilter { get; set; }

        public FileInfo[] GetDocumentFiles()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(CatalogueRepositoryRoot);
            return dirInfo.GetFiles(DocumentFilter, SearchOption.AllDirectories);
        }
    }

    class ReindexGraphSubOptions : CommonGraphSubOptions
    {
        [Option('d', "documentFilter", HelpText = "Document file filter (applied from Repository Root)", DefaultValue = "*.xml")]
        public string DocumentFilter { get; set; }

        public FileInfo[] GetDocumentFiles()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(CatalogueRepositoryRoot);
            return dirInfo.GetFiles(DocumentFilter, SearchOption.AllDirectories);
        }
    }

    class AddDocumentSubOptions : CommonLuceneSubOptions
    {
        [Option('d', "documentFilter", HelpText = "Document file filter (applied from Repository Root)", Required = true)]
        public string DocumentFilter { get; set; }

        public FileInfo[] GetDocumentFiles()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(CatalogueRepositoryRoot);
            return dirInfo.GetFiles(DocumentFilter, SearchOption.AllDirectories);
        }
    }

    class AddGraphDocumentSubOptions : CommonGraphSubOptions
    {
        [Option('d', "documentFilter", HelpText = "Document file filter (applied from Repository Root)", Required = true)]
        public string DocumentFilter { get; set; }

        public FileInfo[] GetDocumentFiles()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(CatalogueRepositoryRoot);
            return dirInfo.GetFiles(DocumentFilter, SearchOption.AllDirectories);
        }
    }

    class RemoveDocumentSubOptions : CommonLuceneSubOptions
    {
        [Option('i', "documentId", HelpText = "Document ID to remove from index", Required = true)]
        public string DocumentID { get; set; }
    }
}

