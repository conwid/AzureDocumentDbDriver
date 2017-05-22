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
using ContextLibrary.DocumentDbProvider;
using AzureDocumentDbDriver.Common;

namespace AzureDocumentDbDriver.Dynamic
{
	/// <summary>
	/// Interaction logic for ConnectionDialog.xaml
	/// </summary>
	public partial class ConnectionDialog : Window
	{
		private Properties properties;

		public ConnectionDialog(IConnectionInfo cxInfo)
		{
            cxInfo.DatabaseInfo.Provider = DocumentDbProviderFactory.ProviderName;                   
			DataContext = properties = new Properties (cxInfo);
			Background = SystemColors.ControlBrush;
			InitializeComponent ();            
		}	

		void btnOK_Click (object sender, RoutedEventArgs e)
		{
			DialogResult = true;            
		}
	}
}
