using CapaEntidades;
using System;

namespace CapaNegocio
{
    public static class UsuarioActual
    {
        public static int IdUsuario { get; private set; }
        public static string Nombre { get; private set; }
        public static string Apellido { get; private set; }
        public static string Email { get; private set; }
        public static string Rol { get; private set; }
        public static bool EstaAutenticado { get; private set; }

        public static void EstablecerUsuario(Usuarios usuario)
        {
            if (usuario != null)
            {
                IdUsuario = usuario.IdUsuario;
                Nombre = usuario.Nombre;
                Apellido = usuario.Apellido;
                Email = usuario.GmailUsuario;
                Rol = usuario.Rol;
                EstaAutenticado = true;
            }
        }

        public static void LimpiarDatos()
        {
            IdUsuario = 0;
            Nombre = string.Empty;
            Apellido = string.Empty;
            Email = string.Empty;
            Rol = string.Empty;
            EstaAutenticado = false;
        }

        public static bool EsAdministrador()
        {
            return Rol.Equals("Administrador", StringComparison.OrdinalIgnoreCase);
        }
    }
}