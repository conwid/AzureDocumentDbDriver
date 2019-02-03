using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LINQPad.Extensibility.DataContext;
using AzureCosmosDbDriver.Common;
using CosmosDbAdoNetProvider;

namespace AzureCosmosDbDriver.Dynamic
{
    /// <summary>
    /// Interaction logic for ConnectionDialog.xaml
    /// </summary>
    public partial class ConnectionDialog : Window
    {        
        public ConnectionDialog(IConnectionInfo cxInfo)
        {
            cxInfo.DatabaseInfo.Provider = CosmosDbSqlProviderFactory.ProviderName;            
            DataContext = new Properties(cxInfo);
            Background = SystemColors.ControlBrush;
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e) => DialogResult = true;

    }
}
