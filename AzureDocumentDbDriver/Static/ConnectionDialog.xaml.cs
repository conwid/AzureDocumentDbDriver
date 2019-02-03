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
using System.IO;
using AzureCosmosDbDriver.Common;
using CosmosDbContext;
using CosmosDbAdoNetProvider;

namespace AzureCosmosDbDriver.Static
{
    /// <summary>
    /// Interaction logic for ConnectionDialog.xaml
    /// </summary>
    public partial class ConnectionDialog : Window
    {
        private readonly IConnectionInfo cxInfo;
        public ConnectionDialog(IConnectionInfo cxInfo)
        {
            cxInfo.DatabaseInfo.Provider = CosmosDbSqlProviderFactory.ProviderName;
            DataContext = new Properties(cxInfo);
            Background = SystemColors.ControlBrush;
            this.cxInfo = cxInfo;
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e) => DialogResult = true;

        void BrowseAssembly(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog() { Title = "Choose custom assembly", DefaultExt = ".dll" };
            if ((dialog.ShowDialog() ?? false) == true)
            {
                cxInfo.CustomTypeInfo.CustomAssemblyPath = dialog.FileName;
            }
        }

        private void ChooseType(object sender, RoutedEventArgs e)
        {
            string assemblyPath = cxInfo.CustomTypeInfo.CustomAssemblyPath;
            if (assemblyPath.Length == 0)
            {
                MessageBox.Show("First enter a path to an assembly.");
                return;
            }

            if (!File.Exists(assemblyPath))
            {
                MessageBox.Show("File '" + assemblyPath + "' does not exist.");
                return;
            }

            string[] customTypes;
            try
            {
                customTypes = cxInfo.CustomTypeInfo.GetCustomTypesInAssembly(typeof(CosmosDbContext.CosmosDbContext).FullName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error obtaining custom types: " + ex.Message);
                return;
            }
            if (customTypes.Length == 0)
            {
                MessageBox.Show($"There are no public types in that assembly based on {typeof(CosmosDbContext.CosmosDbContext).FullName}.");
                return;
            }

            var result = (string)LINQPad.Extensibility.DataContext.UI.Dialogs.PickFromList("Choose Custom Type", customTypes);
            if (result != null)
            {
                cxInfo.CustomTypeInfo.CustomTypeName = result;
            }
        }
    }
}
