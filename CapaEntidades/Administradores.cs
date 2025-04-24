using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Administradores
    {
        public int IdAdmin { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
        public string NivelAcceso { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool GestionUsuarios { get; set; }
        public bool Reportes { get; set; }
        public bool Configuracion { get; set; }
        public bool GestionProductos { get; set; }
        public bool GestionVentas { get; set; }
        public bool GestionClientes { get; set; }
        public bool Estado { get; set; }
    }
}
