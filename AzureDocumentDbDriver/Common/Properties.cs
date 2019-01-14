using System.Net;
using System.Xml.Linq;
using LINQPad.Extensibility.DataContext;

namespace AzureCosmosDbDriver.Common
{

    public class Properties
    {
        private readonly IConnectionInfo cxInfo;
        private readonly XElement driverData;

        public Properties(IConnectionInfo cxInfo)
        {
            this.cxInfo = cxInfo;
            driverData = cxInfo.DriverData;
        }

        public ICustomTypeInfo CustomTypeInfo => cxInfo.CustomTypeInfo;

        public bool Persist
        {
            get => cxInfo.Persist;
            set => cxInfo.Persist = value;
        }

        public string Uri
        {
            get => (string)driverData.Element(nameof(Uri)) ?? string.Empty;
            set => driverData.SetElementValue(nameof(Uri), value);
        }

        public string AccountKey
        {
            get => (string)driverData.Element(nameof(AccountKey)) ?? string.Empty;
            set => driverData.SetElementValue(nameof(AccountKey), value);
        }

        public string Database
        {
            get => (string)driverData.Element(nameof(Database)) ?? string.Empty;
            set => driverData.SetElementValue(nameof(Database), value);
        }
    }

}
