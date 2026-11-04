using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Data;
using Microsoft.Data.SqlClient;


namespace F26Week8DisconnectedModel
{
    /// <summary>
    /// Interaction logic for DataSetWithMultipleTables.xaml
    /// </summary>
    public partial class DataSetWithMultipleTables : Window
    {
        public DataSetWithMultipleTables()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection(Data.GetConnectionString());

            string query = "select * from Categories; select * from Products";
            SqlDataAdapter adp = new SqlDataAdapter(query, conn);

            DataSet ds = new DataSet();
            adp.Fill(ds);

            DataTable tblCats = ds.Tables[0];
            DataTable tblProds = ds.Tables[1];

            grdCategories.ItemsSource = tblCats.DefaultView;
            grdProducts.ItemsSource = tblProds.DefaultView;
        }
    }
}
