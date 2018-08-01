using Elasticsearch.Net;
using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoeSj.KnowledgeBase.Infrastructure.Search
{
   public class ElasticSearchContext
    {
        public IElasticClient  _client;
        public ElasticSearchContext(IOptions<ElasticSearchOptions> options)
        {
           string connString = options.Value.connString;
            if (string.IsNullOrWhiteSpace(connString))
            {
                throw new Exception("ElasticSearch ConnnectString could not be empty.");
            }

            var uris = connString.Split(',').ToList().ConvertAll(x => new Uri(x));
            var connectionPool = new StaticConnectionPool(uris);
            var settings = new ConnectionSettings(connectionPool, sourceSerializer: (builtin, serializerSettings) => new CustomJsonNetSerializer(builtin, serializerSettings));
            var Index = options.Value.index;
            //var Type = typeof(TEntity).Name.ToLower();
            //DefaultIndex("knowledgebase")
            // .DefaultTypeName("post")
            settings = settings.DefaultIndex(Index.ToLower()).
                        DefaultFieldNameInferrer(s => s); // By default, NEST camelcases all field names, this make no changes to the name
            _client = new ElasticClient(settings);

        }
    }
}
