using CapaEntidades;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class UsuarioDA : GenericRepository<Usuarios>
    {
        public UsuarioDA() : base(ConexionDA.CadenaConexion) { }

        public Usuarios AutenticarUsuario(string userMail, string password)
        {
            using (SqlConnection conexion = new SqlConnection(ConexionDA.CadenaConexion))
            {
                try
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("AutenticarUsuario", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@GmailUsuario", userMail);
                        cmd.Parameters.AddWithValue("@Clave", password);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Usuarios usuario = new Usuarios();

                                // Mapear propiedades
                                usuario.IdUsuario = Convert.ToInt32(reader["idUsuario"]);
                                usuario.Nombre = reader["Nombre"].ToString();
                                usuario.Apellido = reader["Apellido"].ToString();
                                usuario.GmailUsuario = reader["GmailUsuario"].ToString();
                                usuario.Rol = reader["Rol"].ToString();
                                usuario.Estado = Convert.ToBoolean(reader["Estado"]);

                                return usuario;
                            }
                        }
                    }

                    return null; // Usuario no encontrado
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al autenticar usuario: " + ex.Message);
                }
            }
        }
    }
}