using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticDriverDataContext
{
    public class VehicleContext : ContextLibrary.BaseDocumentDbContext
    {
        public VehicleContext(string uri, string authKey, string database) : base(uri, authKey, database)
        {
        }

        // An example for defining a collection; specify the collection name and the type into which you want to deserialize the results
        // Json.NET is used to deserialize the results, so everything that it can deserialize will be deserialized
        public IQueryable<Vehicle> Vehicles => CreateDocumentQuery<Vehicle>("vehicles");

        // An example for defining a stored procedure with parameters
        public IQueryable<Vehicle> MyStoredProcedure(string param1, string param2)
        {
            return base.CreateStoredProcedureQuery<Vehicle>(nameof(MyStoredProcedure), "vehicle", param1, param2);
        }

    }
}
