using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Compras
    {
        public int IdCompra { get; set; }
        public int IdVendedor { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal Total { get; set; }
        public string NombreVendedor { get; set; }
        public string NombreUsuario { get; set; }
    }
}
