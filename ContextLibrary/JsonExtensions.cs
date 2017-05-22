using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextLibrary
{
    public static class JsonExtensions
    {
        public static IQueryable<object> ReadJsonAsDynamicQueryable(string json, JsonSerializerSettings settings = null)
        {
            var serializer = JsonSerializer.CreateDefault(settings);
            using (StringReader sr = new StringReader(json))
            using (JsonTextReader reader = new JsonTextReader(sr))
            {
                var root = JsonExtensions.ReadJsonAsDynamicQueryable(reader, serializer);
                return root;
            }
        }

        public static IQueryable<dynamic> ReadJsonAsDynamicQueryable(JsonReader reader, JsonSerializer serializer)
        {
            dynamic obj;

            if (!TryReadJsonAsDynamic(reader, serializer, out obj) || obj == null)
                return Enumerable.Empty<dynamic>().AsQueryable();

            var list = obj as IList<dynamic> ?? new[] { obj };

            return list.AsQueryable();
        }

        public static bool TryReadJsonAsDynamic(JsonReader reader, JsonSerializer serializer, out dynamic obj)
        {
            // Adapted from:
            // https://github.com/JamesNK/Newtonsoft.Json/blob/master/Src/Newtonsoft.Json/Converters/ExpandoObjectConverter.cs
            // License:
            // https://github.com/JamesNK/Newtonsoft.Json/blob/master/LICENSE.md
            // Stackoverflow question:
            // http://stackoverflow.com/questions/43214424/converting-arbitrary-json-response-to-list-of-things

            if (reader.TokenType == JsonToken.None)
                if (!reader.Read())
                {
                    obj = null;
                    return false;
                }

            switch (reader.TokenType)
            {
                case JsonToken.StartArray:
                    var list = new List<dynamic>();
                    ReadList(reader,
                        (r) =>
                        {
                            dynamic item;
                            if (TryReadJsonAsDynamic(reader, serializer, out item))
                                list.Add(item);
                        });
                    obj = list;
                    return true;

                case JsonToken.StartObject:
                    obj = serializer.Deserialize<ExpandoObject>(reader);
                    return true;

                default:
                    if (reader.TokenType.IsPrimitiveToken())
                    {
                        obj = reader.Value;
                        return true;
                    }
                    else
                    {
                        throw new JsonSerializationException("Unknown token: " + reader.TokenType);
                    }
            }
        }

        static void ReadList(this JsonReader reader, Action<JsonReader> readValue)
        {
            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonToken.Comment:
                        break;
                    default:
                        readValue(reader);
                        break;
                    case JsonToken.EndArray:
                        return;
                }
            }
            throw new JsonSerializationException("Unexpected end when reading List.");
        }

        public static bool IsPrimitiveToken(this JsonToken token)
        {
            switch (token)
            {
                case JsonToken.Integer:
                case JsonToken.Float:
                case JsonToken.String:
                case JsonToken.Boolean:
                case JsonToken.Undefined:
                case JsonToken.Null:
                case JsonToken.Date:
                case JsonToken.Bytes:
                    return true;
                default:
                    return false;
            }
        }
    }
}
