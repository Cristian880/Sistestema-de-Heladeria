using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaNegocio
{
    public class ComprasBL
    {
        private readonly string _connectionString;

        public ComprasBL()
        {
            _connectionString = ConexionDA.CadenaConexion;
        }

        public int AgregarCompra(Compras compra)
        {
            int idCompra = 0;

            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("AgregarCompra", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdVendedor", compra.IdVendedor);
                        cmd.Parameters.AddWithValue("@IdUsuario", compra.IdUsuario);
                        cmd.Parameters.AddWithValue("@Total", compra.Total);

                        // Obtener el ID de la compra creada
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idCompra = Convert.ToInt32(reader["IdCompra"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar compra: " + ex.Message);
            }

            return idCompra;
        }

        public void AgregarDetalleCompra(DetalleCompra detalle, int usuarioModificacion = 0, int? idCategoria = null)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("AgregarDetalleCompra", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdCompra", detalle.IdCompra);
                        cmd.Parameters.AddWithValue("@IdProducto", detalle.IdProducto);
                        cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);

                        if (usuarioModificacion > 0)
                            cmd.Parameters.AddWithValue("@UsuarioModificacion", usuarioModificacion);
                        else
                            cmd.Parameters.AddWithValue("@UsuarioModificacion", DBNull.Value);

                        if (idCategoria.HasValue)
                            cmd.Parameters.AddWithValue("@IdCategoria", idCategoria.Value);
                        else
                            cmd.Parameters.AddWithValue("@IdCategoria", DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar detalle de compra: " + ex.Message);
            }
        }
    }
}