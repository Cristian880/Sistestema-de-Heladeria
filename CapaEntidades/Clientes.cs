using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Clientes
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? UsuarioModificacion { get; set; }

        public Clientes()
        {
            FechaCreacion = DateTime.Now;
        }

        public Clientes(int idCliente, string nombre, string correo, string telefono)
        {
            IdCliente = idCliente;
            Nombre = nombre;
            Correo = correo;
            Telefono = telefono;
            FechaCreacion = DateTime.Now;
        }
    }
}
