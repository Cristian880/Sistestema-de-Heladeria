using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Ventas
    {
        public int IdVenta { get; set; }
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal Total { get; set; }
        public string NombreCliente { get; set; }
        public string NombreUsuario { get; set; }
        public string MetodoPago { get; set; }
        public bool Estado { get; set; }
    }
}
