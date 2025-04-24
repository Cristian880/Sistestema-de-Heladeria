using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaNegocio
{
    public class ClientesBL
    {
        private readonly string _connectionString;
        private readonly GenericRepository<Clientes> _repository;

        public ClientesBL()
        {
            _connectionString = ConexionDA.CadenaConexion;
            _repository = new GenericRepository<Clientes>(_connectionString);
        }

        public List<Clientes> ObtenerTodos()
        {
            return _repository.ObtenerTodos();
        }

        public Clientes ObtenerPorId(int id)
        {
            return _repository.ObtenerPorId(id);
        }

        public void Agregar(Clientes cliente, int usuarioModificacion = 0)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("AgregarCliente", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                        cmd.Parameters.AddWithValue("@Correo", cliente.Correo ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono ?? (object)DBNull.Value);

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
                throw new Exception("Error al agregar cliente: " + ex.Message);
            }
        }

        public void Actualizar(Clientes cliente, int usuarioModificacion = 0)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("EditarCliente", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdCliente", cliente.IdCliente);
                        cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                        cmd.Parameters.AddWithValue("@Correo", cliente.Correo ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono ?? (object)DBNull.Value);

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
                throw new Exception("Error al actualizar cliente: " + ex.Message);
            }
        }

        public void Eliminar(int id)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("EliminarCliente", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idCliente", id);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar cliente: " + ex.Message);
            }
        }

        public List<Clientes> Buscar(string datoBusqueda)
        {
            List<Clientes> clientes = new List<Clientes>();

            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("BuscarCliente", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DatoBusqueda", datoBusqueda);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Clientes cliente = new Clientes
                                {
                                    IdCliente = Convert.ToInt32(reader["idCliente"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"])
                                };

                                if (reader["Correo"] != DBNull.Value)
                                    cliente.Correo = reader["Correo"].ToString();

                                if (reader["Telefono"] != DBNull.Value)
                                    cliente.Telefono = reader["Telefono"].ToString();

                                if (reader["FechaModificacion"] != DBNull.Value)
                                    cliente.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);

                                if (reader["UsuarioModificacion"] != DBNull.Value)
                                    cliente.UsuarioModificacion = Convert.ToInt32(reader["UsuarioModificacion"]);

                                clientes.Add(cliente);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar clientes: " + ex.Message);
            }

            return clientes;
        }
    }
}