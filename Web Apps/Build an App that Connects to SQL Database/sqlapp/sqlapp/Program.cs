
using sqlapp.Models;
using System.Data.SqlClient;

namespace sqlapp.Services
{
    public class ProductService
    {
        private static string db_source = "";
        private static string db_user = "";
        private static string db_password = "";
        private static string db_database = "";

        private SqlConnection GetConnection()
        {
            var _builder = new SqlConnectionStringBuilder(db_source);
            _builder.DataSource = db_source;
            _builder.UserID = db_user;
            _builder.Password = db_password;
            _builder.InitialCatalog = db_database;
            return new SqlConnection(_builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            string statement = "select ProductID, ProductName, Quantity from Products";
            SqlConnection conn = GetConnection();

            SqlCommand sqlCommand = new SqlCommand(statement, conn);
            using (SqlDataReader reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product() {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2),
                    };
                    products.Add(product);
                }
            }
            conn.Close();
            return products;
        }
    }
    
}