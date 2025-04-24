using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Usuarios
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string GmailUsuario { get; set; }
        public string Clave { get; set; }
        public string Rol { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public DateTime FechaContratado { get; set; }
        public bool Estado { get; set; }
        public string NombreCompleto => $"{Nombre} {Apellido}";
        public string EstadoTexto => Estado ? "Activo" : "Inactivo";

        public Usuarios()
        {
            FechaContratado = DateTime.Now;
            Estado = true;
        }

        public Usuarios(int idUsuario, string nombre, string apellido, string gmailUsuario, string clave, string rol, DateTime? fechaNacimiento)
        {
            IdUsuario = idUsuario;
            Nombre = nombre;
            Apellido = apellido;
            GmailUsuario = gmailUsuario;
            Clave = clave;
            Rol = rol;
            FechaNacimiento = fechaNacimiento;
            FechaContratado = DateTime.Now;
            Estado = true;
        }
    }
}