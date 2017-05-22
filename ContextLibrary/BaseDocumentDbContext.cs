using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;

namespace ContextLibrary
{
    public abstract class BaseDocumentDbContext : IDisposable
    {
        private readonly DocumentClient client;
        private readonly string database;

        public BaseDocumentDbContext(string uri, string authKey, string database)
        {
            client = new DocumentClient(new Uri(uri), authKey);
            this.database = database;
        }

        protected IQueryable<T> CreateDocumentQuery<T>(string collectionName)
        {
            return client.CreateDocumentQuery<T>(UriFactory.CreateDocumentCollectionUri(database, collectionName));
        }

        protected IQueryable<T> CreateStoredProcedureQuery<T>(string storedProcName, string collectionName, params dynamic[] parameters)
        {
            var r = client.ExecuteStoredProcedureAsync<string>(UriFactory.CreateStoredProcedureUri(database, collectionName, storedProcName), parameters).Result.Response;
            using (JsonTextReader reader = new JsonTextReader(new StringReader(r)))
            {
                if (!reader.Read())
                {
                    return new List<T>().AsQueryable();
                }

                if (reader.TokenType == JsonToken.Null || reader.TokenType == JsonToken.None || reader.TokenType == JsonToken.Undefined)
                {
                    return new List<T>().AsQueryable();
                }
                else if (reader.TokenType == JsonToken.StartObject)
                {
                    T instance = JsonConvert.DeserializeObject<T>(r);
                    return new List<T>() { instance }.AsQueryable();
                }
                else if (reader.TokenType == JsonToken.StartArray)
                {
                    return JsonConvert.DeserializeObject<List<T>>(r).AsQueryable();
                }
                else if (reader.TokenType == JsonToken.Boolean || reader.TokenType == JsonToken.Date || reader.TokenType == JsonToken.Float || reader.TokenType == JsonToken.Integer || reader.TokenType == JsonToken.String)
                {
                    T instance = JsonConvert.DeserializeObject<T>(r);
                    return new List<T>() { instance }.AsQueryable();
                }
                throw new InvalidOperationException($"Token {reader.TokenType} cannot be the first token in the result");
            }
        }

        private IQueryable<dynamic> DeserializeAs<T>(string r)
        {
            T instance = JsonConvert.DeserializeObject<T>(r);
            return new List<dynamic>() { instance }.AsQueryable();
        }

        protected IQueryable<dynamic> CreateDynamicStoredProcedureQuery(string storedProcName, string collectionName, params dynamic[] parameters)
        {
            var r = client.ExecuteStoredProcedureAsync<string>(UriFactory.CreateStoredProcedureUri(database, collectionName, storedProcName), parameters).Result.Response;
            return JsonExtensions.ReadJsonAsDynamicQueryable(r);        
        }


        public void Dispose()
        {
            client.Dispose();
        }
    }
}
