﻿using System;
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

        private static void doWithWriter(string indexRoot, Action<IndexWriter> actionWithWriter, Analyzer analyzer, bool recreateIndex = false)
        {
            var indexDirectory = FSDirectory.Open(indexRoot);
            if(analyzer == null)
                analyzer = new StandardAnalyzer(Version.LUCENE_30);
            var writer = new IndexWriter(indexDirectory, analyzer, recreateIndex, IndexWriter.MaxFieldLength.UNLIMITED);
            actionWithWriter(writer);
            //writer.Commit();
            writer.Optimize();
            writer.Flush(true, true, true);
            writer.Dispose();
        }

        public static void AddDocuments(string indexRoot, Document[] docs, Analyzer analyzer = null, bool recreateIndex = false)
        {
            doWithWriter(indexRoot, writer => {
                    foreach (var doc in docs)
                    {
                        string id = doc.GetField("ID").StringValue;
                        writer.UpdateDocument(new Term("ID", id), doc);
                    }
                }, analyzer, recreateIndex);
        }

        public static Document[] PerformQuery(string indexPath, string queryText, string defaultFieldName, Analyzer analyzer, int resultAmount = 100)
        {
            Directory searchDirectory = FSDirectory.Open(indexPath);
            IndexSearcher searcher = new IndexSearcher(searchDirectory);
            QueryParser parser = new QueryParser(Version.LUCENE_30, defaultFieldName, analyzer);
            Query query = parser.Parse(queryText);
            TopDocs topDocs = searcher.Search(query, resultAmount);
            return topDocs.ScoreDocs.Select(sDoc => searcher.Doc(sDoc.Doc)).ToArray();
        }

        public static void RemoveDocuments(string indexRoot, params string[] documentIds)
        {
            doWithWriter(indexRoot, writer =>
                {
                    Term[] terms = documentIds.Select(docId => new Term("ID", docId)).ToArray();
                    writer.DeleteDocuments(terms);
                }, new KeywordAnalyzer());
        }

        public static void CreateIndex(string indexRoot)
        {
            doWithWriter(indexRoot, writer => {}, null, true);
        }
    }
}
