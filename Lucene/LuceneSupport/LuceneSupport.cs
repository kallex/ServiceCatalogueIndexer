using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Version = Lucene.Net.Util.Version;

namespace LuceneSupport
{
    public class LuceneSupport
    {
        private string IndexRoot;
        private IndexWriter Writer;


        public static void AddDocuments(string indexRoot, Document[] docs)
        {
            var indexDirectory = FSDirectory.Open(indexRoot);
            Analyzer analyzer = new StandardAnalyzer(Version.LUCENE_30);
            var writer = new IndexWriter(indexDirectory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED);

            foreach(var doc in docs)
                writer.AddDocument(doc);
            writer.Optimize();
            writer.Flush(true, true, true);
            writer.Dispose();
        }

        public static Document[] PerformQuery(string indexPath, string queryText, string defaultFieldName, int resultAmount = 100)
        {
            Directory searchDirectory = FSDirectory.Open(indexPath);
            IndexSearcher searcher = new IndexSearcher(searchDirectory);
            Analyzer analyzer = new StandardAnalyzer(Version.LUCENE_30);
            QueryParser parser = new QueryParser(Version.LUCENE_30, defaultFieldName, analyzer);
            Query query = parser.Parse(queryText);
            TopDocs topDocs = searcher.Search(query, resultAmount);
            return topDocs.ScoreDocs.Select(sDoc => searcher.Doc(sDoc.Doc)).ToArray();
        }

    }
}
