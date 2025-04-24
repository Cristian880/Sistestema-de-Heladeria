using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaNegocio
{
    public class VentasBL
    {
        private readonly string _connectionString;

        public VentasBL()
        {
            _connectionString = ConexionDA.CadenaConexion;
        }

        public int AgregarVenta(Ventas venta)
        {
            int idVenta = 0;

            try
            {
                ConexionDA conexionDA = new ConexionDA();
                using (SqlConnection conexion = new SqlConnection(ConexionDA.CadenaConexion))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("AgregarVenta", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdCliente", venta.IdCliente);
                        cmd.Parameters.AddWithValue("@IdUsuario", venta.IdUsuario);
                        cmd.Parameters.AddWithValue("@Total", venta.Total);

                        // Obtener el ID de la venta creada
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idVenta = Convert.ToInt32(reader["IdVenta"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar venta: " + ex.Message);
            }

            return idVenta;
        }

        public void AgregarDetalleVenta(DetalleVenta detalle)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("AgregarDetalleVenta", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdVenta", detalle.IdVenta);
                        cmd.Parameters.AddWithValue("@IdProducto", detalle.IdProducto);
                        cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar detalle de venta: " + ex.Message);
            }
        }

        public List<Ventas> ObtenerVentasPorPeriodo(DateTime fechaInicio, DateTime fechaFin)
        {
            List<Ventas> ventas = new List<Ventas>();

            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("ObtenerVentasPorPeriodo", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                        cmd.Parameters.AddWithValue("@FechaFin", fechaFin);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Ventas venta = new Ventas
                                {
                                    IdVenta = Convert.ToInt32(reader["IdVenta"]),
                                    NombreCliente = reader["Cliente"].ToString(),
                                    NombreUsuario = reader["NombreUsuario"].ToString(),
                                    FechaVenta = Convert.ToDateTime(reader["FechaVenta"]),
                                    Total = Convert.ToDecimal(reader["Total"])
                                };

                                ventas.Add(venta);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener ventas por período: " + ex.Message);
            }

            return ventas;
        }

        public List<DetalleVenta> ObtenerDetalleVenta(int idVenta)
        {
            List<DetalleVenta> detalles = new List<DetalleVenta>();

            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("ObtenerDetalleVentaCompleto", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdVenta", idVenta);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DetalleVenta detalle = new DetalleVenta
                                {
                                    IdDetalleVenta = Convert.ToInt32(reader["idDetalleVenta"]),
                                    IdVenta = idVenta,
                                    NombreProducto = reader["Producto"].ToString(),
                                    Cantidad = Convert.ToInt32(reader["Cantidad"]),
                                    Precio = Convert.ToDecimal(reader["Precio"])
                                };

                                if (reader["Categoria"] != DBNull.Value)
                                    detalle.Categoria = reader["Categoria"].ToString();

                                detalles.Add(detalle);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener detalle de venta: " + ex.Message);
            }

            return detalles;
        }

        public DataSet ObtenerEstadisticasGenerales()
        {
            DataSet ds = new DataSet();

            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("ObtenerEstadisticasGenerales", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener estadísticas: " + ex.Message);
            }

            return ds;
        }

        public DataTable ObtenerVentasPorCategoria(DateTime fechaInicio, DateTime fechaFin)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("ObtenerVentasPorCategoria", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                        cmd.Parameters.AddWithValue("@FechaFin", fechaFin);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener ventas por categoría: " + ex.Message);
            }

            return dt;
        }
    }
}