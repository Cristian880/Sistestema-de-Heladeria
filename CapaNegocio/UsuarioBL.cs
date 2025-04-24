// CapaNegocio/UsuariosBL.cs
using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaNegocio
{
    public class UsuariosBL
    {
        private readonly string _connectionString;
        private readonly GenericRepository<Usuarios> _repository;

        public UsuariosBL()
        {
            _connectionString = ConexionDA.CadenaConexion;
            _repository = new GenericRepository<Usuarios>(_connectionString);
        }

        public List<Usuarios> ObtenerTodos()
        {
            return _repository.ObtenerTodos();
        }

        public Usuarios ObtenerPorId(int id)
        {
            return _repository.ObtenerPorId(id);
        }

        public void Agregar(Usuarios usuario)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("AgregarUsuario", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                        cmd.Parameters.AddWithValue("@GmailUsuario", usuario.GmailUsuario);
                        cmd.Parameters.AddWithValue("@Clave", usuario.Clave);
                        cmd.Parameters.AddWithValue("@Rol", usuario.Rol);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento ?? (object)DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar usuario: " + ex.Message);
            }
        }

        public void Actualizar(Usuarios usuario)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("EditarUsuario", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idUsuario", usuario.IdUsuario);
                        cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                        cmd.Parameters.AddWithValue("@GmailUsuario", usuario.GmailUsuario);
                        cmd.Parameters.AddWithValue("@Clave", usuario.Clave);
                        cmd.Parameters.AddWithValue("@Rol", usuario.Rol);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Estado", usuario.Estado);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar usuario: " + ex.Message);
            }
        }

        public void Eliminar(int id)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("EliminarUsuario", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idUsuario", id);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar usuario: " + ex.Message);
            }
        }

        public List<Usuarios> Buscar(string datoBusqueda)
        {
            List<Usuarios> usuarios = new List<Usuarios>();

            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("BuscarUsuario", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DatoBusqueda", datoBusqueda);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Usuarios usuario = new Usuarios
                                {
                                    IdUsuario = Convert.ToInt32(reader["idUsuario"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Apellido = reader["Apellido"].ToString(),
                                    GmailUsuario = reader["GmailUsuario"].ToString(),
                                    Clave = reader["Clave"].ToString(),
                                    Rol = reader["Rol"].ToString(),
                                    Estado = Convert.ToBoolean(reader["Estado"])
                                };

                                if (reader["FechaNacimiento"] != DBNull.Value)
                                    usuario.FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]);

                                usuarios.Add(usuario);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar usuarios: " + ex.Message);
            }

            return usuarios;
        }

        public Usuarios Autenticar(string email, string clave)
        {
            Usuarios usuario = null;

            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("AutenticarUsuario", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@GmailUsuario", email);
                        cmd.Parameters.AddWithValue("@Clave", clave);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                usuario = new Usuarios
                                {
                                    IdUsuario = Convert.ToInt32(reader["idUsuario"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Apellido = reader["Apellido"].ToString(),
                                    GmailUsuario = reader["GmailUsuario"].ToString(),
                                    Rol = reader["Rol"].ToString(),
                                    Estado = Convert.ToBoolean(reader["Estado"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al autenticar usuario: " + ex.Message);
            }

            return usuario;
        }
    }
}