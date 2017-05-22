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

        public ICustomTypeInfo CustomTypeInfo
        {
            get { return _cxInfo.CustomTypeInfo; }
        }

        public bool Persist
        {
            get { return _cxInfo.Persist; }
            set { _cxInfo.Persist = value; }
        }

        public string Uri
        {
            get { return (string)_driverData.Element("Uri") ?? ""; }
            set { _driverData.SetElementValue("Uri", value); }
        }

        public string AccountKey
        {
            get { return (string)_driverData.Element("AccountKey") ?? ""; }
            set { _driverData.SetElementValue("AccountKey", value); }
        }

        public string Database
        {
            get { return (string)_driverData.Element("Database") ?? ""; }
            set { _driverData.SetElementValue("Database", value); }
        }
    }

}
