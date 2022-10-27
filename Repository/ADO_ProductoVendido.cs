
using System;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Modelss;


namespace WebApplication1.Repository
{
    public class ADO_ProductoVendido
    {
        public static List<ProductoVendido> DevolverProductosVendidos()
        {
            var listaProductosVendido = new List<ProductoVendido>();
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                SqlCommand cmd2 = connection.CreateCommand();
                cmd2.CommandText = "SELECT Id,Stock,IdProducto,IdVenta FROM dbo.ProductoVendido";
                var reader2 = cmd2.ExecuteReader();

                while (reader2.Read())
                {
                    var productoven = new ProductoVendido();
                    productoven.Id = Convert.ToInt32(reader2.GetValue(0));
                    productoven.Stock = Convert.ToInt32(reader2.GetValue(1).ToString());
                    productoven.IdProducto = Convert.ToInt32(reader2.GetValue(2));
                    productoven.IdVenta = Convert.ToInt32(reader2.GetValue(3));

                    listaProductosVendido.Add(productoven);

                }
                reader2.Close();
                connection.Close();

            }
            return listaProductosVendido;
        }
    }
}
