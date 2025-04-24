using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaNegocio
{
    public class VendedoresBL
    {
        private readonly string _connectionString;
        private readonly GenericRepository<Vendedores> _repository;

        public VendedoresBL()
        {
            _connectionString = ConexionDA.CadenaConexion;
            _repository = new GenericRepository<Vendedores>(_connectionString);
        }

        public List<Vendedores> ObtenerTodos()
        {
            return _repository.ObtenerTodos();
        }

        public Vendedores ObtenerPorId(int id)
        {
            return _repository.ObtenerPorId(id);
        }

        public void Agregar(Vendedores vendedor, int usuarioModificacion = 0)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("AgregarVendedor", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", vendedor.Nombre);
                        cmd.Parameters.AddWithValue("@Correo", vendedor.Correo ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Telefono", vendedor.Telefono ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Ubicacion", vendedor.Ubicacion ?? (object)DBNull.Value);

                        if (usuarioModificacion > 0)
                            cmd.Parameters.AddWithValue("@UsuarioModificacion", usuarioModificacion);
                        else
                            cmd.Parameters.AddWithValue("@UsuarioModificacion", DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar vendedor: " + ex.Message);
            }
        }

        public void Actualizar(Vendedores vendedor, int usuarioModificacion = 0)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("EditarVendedor", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idVendedor", vendedor.IdVendedor);
                        cmd.Parameters.AddWithValue("@Nombre", vendedor.Nombre);
                        cmd.Parameters.AddWithValue("@Correo", vendedor.Correo ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Telefono", vendedor.Telefono ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Ubicacion", vendedor.Ubicacion ?? (object)DBNull.Value);

                        if (usuarioModificacion > 0)
                            cmd.Parameters.AddWithValue("@UsuarioModificacion", usuarioModificacion);
                        else
                            cmd.Parameters.AddWithValue("@UsuarioModificacion", DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar vendedor: " + ex.Message);
            }
        }

        public void Eliminar(int id)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("EliminarVendedor", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idVendedor", id);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar vendedor: " + ex.Message);
            }
        }

        public List<Vendedores> Buscar(string datoBusqueda)
        {
            List<Vendedores> vendedores = new List<Vendedores>();

            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("BuscarVendedor", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DatoBusqueda", datoBusqueda);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Vendedores vendedor = new Vendedores
                                {
                                    IdVendedor = Convert.ToInt32(reader["idVendedor"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"])
                                };

                                if (reader["Correo"] != DBNull.Value)
                                    vendedor.Correo = reader["Correo"].ToString();

                                if (reader["Telefono"] != DBNull.Value)
                                    vendedor.Telefono = reader["Telefono"].ToString();

                                if (reader["Ubicacion"] != DBNull.Value)
                                    vendedor.Ubicacion = reader["Ubicacion"].ToString();

                                if (reader["FechaModificacion"] != DBNull.Value)
                                    vendedor.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);

                                if (reader["UsuarioModificacion"] != DBNull.Value)
                                    vendedor.UsuarioModificacion = Convert.ToInt32(reader["UsuarioModificacion"]);

                                vendedores.Add(vendedor);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar vendedores: " + ex.Message);
            }

            return vendedores;
        }
    }
}