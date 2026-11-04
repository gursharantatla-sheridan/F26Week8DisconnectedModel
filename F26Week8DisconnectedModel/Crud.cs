using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml.Linq;


namespace F26Week8DisconnectedModel
{
    public class Crud
    {
        private SqlConnection _conn;
        private SqlDataAdapter _adp;
        private SqlCommandBuilder _cmdBuilder;
        private DataSet _ds;
        private DataTable _tbl;

        public Crud()
        {
            _conn = new SqlConnection(Data.GetConnectionString());
            _ds = new DataSet();

            InitProductsTable();
        }

        private void InitProductsTable()
        {
            string query = "select ProductID, ProductName, UnitPrice, UnitsInStock from Products";

            _adp = new SqlDataAdapter(query, _conn);
            _cmdBuilder = new SqlCommandBuilder(_adp);

            _adp.Fill(_ds, "Products");
            _tbl = _ds.Tables["Products"]!;

            // define primary key
            DataColumn[] pk = new DataColumn[1];
            pk[0] = _tbl.Columns["ProductID"]!;
            pk[0].AutoIncrement = true;
            _tbl.PrimaryKey = pk;
        }

        public DataTable GetAllProducts()
        {
            InitProductsTable();
            return _tbl;
        }

        public DataRow? GetProductById(int id)
        {
            var row = _tbl.Rows.Find(id);
            return row;
        }

        public void InsertProduct(string name, decimal price, int quantity)
        {
            var row = _tbl.NewRow();
            row["ProductName"] = name;
            row["UnitPrice"] = price;
            row["UnitsInStock"] = quantity;

            _tbl.Rows.Add(row);  // required

            _adp.InsertCommand = _cmdBuilder.GetInsertCommand();
            _adp.Update(_tbl);
        }

        public void UpdateProduct(int id, string name, decimal price, int quantity)
        {
            var row = _tbl.Rows.Find(id);

            row["ProductName"] = name;
            row["UnitPrice"] = price;
            row["UnitsInStock"] = quantity;

            _adp.UpdateCommand = _cmdBuilder.GetUpdateCommand();
            _adp.Update(_tbl);
        }

        public void DeleteProduct(int id)
        {
            var row = _tbl.Rows.Find(id);
            row.Delete();

            _adp.DeleteCommand = _cmdBuilder.GetDeleteCommand();
            _adp.Update(_tbl);
        }
    }
}
