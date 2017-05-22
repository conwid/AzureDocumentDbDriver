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
using ContextLibrary;
using ContextLibrary.DocumentDbProvider;
using AzureDocumentDbDriver.Common;

namespace AzureDocumentDbDriver.Static
{
    /// <summary>
    /// Interaction logic for ConnectionDialog.xaml
    /// </summary>
    public partial class ConnectionDialog : Window
    {
        IConnectionInfo cxInfo;

        public ConnectionDialog(IConnectionInfo cxInfo)
        {
            this.cxInfo = cxInfo;
            DataContext = new Properties(cxInfo);
            cxInfo.DatabaseInfo.Provider = DocumentDbProviderFactory.ProviderName;
            InitializeComponent();
        }

        void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        void BrowseAssembly(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog()
            {
                Title = "Choose custom assembly",
                DefaultExt = ".dll",
            };

            if (dialog.ShowDialog() == true)
                cxInfo.CustomTypeInfo.CustomAssemblyPath = dialog.FileName;
        }

        void ChooseType(object sender, RoutedEventArgs e)
        {
            string assemPath = cxInfo.CustomTypeInfo.CustomAssemblyPath;
            if (assemPath.Length == 0)
            {
                MessageBox.Show("First enter a path to an assembly.");
                return;
            }

            if (!File.Exists(assemPath))
            {
                MessageBox.Show("File '" + assemPath + "' does not exist.");
                return;
            }

            string[] customTypes;
            try
            {
                customTypes = cxInfo.CustomTypeInfo.GetCustomTypesInAssembly(typeof(BaseDocumentDbContext).FullName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error obtaining custom types: " + ex.Message);
                return;
            }
            if (customTypes.Length == 0)
            {
                MessageBox.Show($"There are no public types in that assembly based on {typeof(BaseDocumentDbContext).FullName}.");
                return;
            }

            string result = (string)LINQPad.Extensibility.DataContext.UI.Dialogs.PickFromList("Choose Custom Type", customTypes);
            if (result != null) cxInfo.CustomTypeInfo.CustomTypeName = result;
        }
    }
}
