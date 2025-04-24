using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class DetalleCompra
    {
        public int IdDetalleCompra { get; set; }
        public int IdCompra { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public string NombreProducto { get; set; }
        public decimal Precio { get; set; }
        public decimal Subtotal => Precio * Cantidad;

        // Propiedades de navegación
        public Compras Compra { get; set; }
        public Productos Producto { get; set; }

        public DetalleCompra()
        {
        }

        public DetalleCompra(int idDetalleCompra, int idCompra, int idProducto, int cantidad, string nombreProducto, decimal precio)
        {
            IdDetalleCompra = idDetalleCompra;
            IdCompra = idCompra;
            IdProducto = idProducto;
            Cantidad = cantidad;
            NombreProducto = nombreProducto;
            Precio = precio;
        }
    }
}
