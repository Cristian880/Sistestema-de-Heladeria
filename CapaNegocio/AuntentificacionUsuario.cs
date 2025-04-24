using CapaDatos;
using CapaDatos.Interfaz;
using CapaEntidades;
using System;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CapaNegocio
{
    public class AuntentificacionUsuario
    {
        private readonly UsuarioDA _usuarioRepository = new UsuarioDA();

        public AuntentificacionUsuario()
        {
            _usuarioRepository = new UsuarioDA();
        }

        public AuntentificacionUsuario(string connectionString)
        {
            _usuarioRepository = new UsuarioDA();
        }

        public Usuarios AutenticarUsuario(string userMail, string password)
        {
            DataTable dt = _usuarioRepository.AutenticarUsuario(userMail, password);

            if (dt.Rows.Count > 0)
            {
                return new Usuarios
                {
                    IdUsuario = Convert.ToInt32(dt.Rows[0]["IdUsuario"]),
                    Nombre = dt.Rows[0]["Nombre"].ToString(),
                    Apellido = dt.Rows[0]["Apellido"].ToString(),
                    GmailUsuario = dt.Rows[0]["GmailUsuario"].ToString(),
                    Clave = dt.Rows[0]["Clave"].ToString(),
                    Rol = dt.Rows[0]["Rol"].ToString(),
                    FechaNacimiento = dt.Rows[0]["FechaNacimiento"] == DBNull.Value? (DateTime?)null: Convert.ToDateTime(dt.Rows[0]["FechaNacimiento"]),
                    FechaContratado = Convert.ToDateTime(dt.Rows[0]["FechaContratado"]),
                    Estado = Convert.ToBoolean(dt.Rows[0]["Estado"])
                };
            }
            else
            {
                return null; // Si no existe el usuario
            }
        }
        public string EncriptarPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}