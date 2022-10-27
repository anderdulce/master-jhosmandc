﻿using System;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Modelss;

namespace WebApplication1.Repository
{
    public class ADO_Venta
    {

        public static List<Venta> DevolverVenta()
        {
            var ventas = new List<Venta>();
            string connectionString = General.connectionString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd2 = connection.CreateCommand();
                cmd2.CommandText = "select Id,Comentarios,IdUsuario FROM Venta";
                var reader2 = cmd2.ExecuteReader();

                while (reader2.Read())
                {
                    var venta = new Venta();
                    venta.Id = Convert.ToInt32(reader2.GetValue(0));
                    venta.Comentarios = reader2.GetValue(1).ToString();
                    venta.IdUsuario = Convert.ToInt32(reader2.GetValue(2));

                    ventas.Add(venta);

                }
                reader2.Close();
                connection.Close();

            }
            return ventas;
        }

        public static void CargarVenta(VentaProducto ventaProducto)
        {
            int idVenta;
            string connectionString = General.connectionString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //INSERT en tabla venta y obtener el id de la venta
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Venta (Comentarios, IdUsuario) VALUES (@Comentarios, @IdUsuario); Select scope_identity();", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("Comentarios", SqlDbType.VarChar)).Value = ventaProducto.Comentarios;
                cmd.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt)).Value = ventaProducto.IdUsuario;
                idVenta = Convert.ToInt32(cmd.ExecuteScalar());

                //INSERT en tabla producto vendido con lista de productos enviados
                foreach (ProductoVendido producto in ventaProducto.Producto)
                {
                    //Agregar Venta
                    cmd = new SqlCommand("insert into ProductoVendido (Stock,IdProducto,IdVenta)  values   (@Stock,@IdProducto,@IdVenta) ", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int)).Value = producto.Stock;
                    cmd.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.BigInt)).Value = producto.IdProducto;
                    cmd.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.BigInt)).Value = idVenta;
                    cmd.ExecuteNonQuery();


                    //Actualizar Stock en Productos

                    cmd = new SqlCommand("update Producto SET Stock = Stock - @Stock where IdProducto  = @IdProducto", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int)).Value = producto.Stock;
                    cmd.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.BigInt)).Value = producto.IdProducto;
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }

        }

    }
}
