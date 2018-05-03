using System.Net;
using System.Xml.Linq;
using LINQPad.Extensibility.DataContext;

namespace AzureDocumentDbDriver.Common
{

    public class Properties
    {
        readonly IConnectionInfo _cxInfo;
        readonly XElement _driverData;

        public Properties(IConnectionInfo cxInfo)
        {
            _cxInfo = cxInfo;
            _driverData = cxInfo.DriverData;
        }

        public ICustomTypeInfo CustomTypeInfo => _cxInfo.CustomTypeInfo;

        public bool Persist
        {
            get => _cxInfo.Persist;
            set => _cxInfo.Persist = value;
        }

        public string Uri
        {
            get => (string)_driverData.Element("Uri") ?? string.Empty;
            set => _driverData.SetElementValue("Uri", value);
        }

        public string AccountKey
        {
            get => (string)_driverData.Element("AccountKey") ?? string.Empty;
            set => _driverData.SetElementValue("AccountKey", value);
        }

        public string Database
        {
            get => (string)_driverData.Element("Database") ?? string.Empty;
            set => _driverData.SetElementValue("Database", value);
        }
    }

}
