using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class DetalleVenta
    {
        public int IdDetalleVenta { get; set; }
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public string NombreProducto { get; set; }
        public string Categoria { get; set; }
        public decimal Precio { get; set; }
        private decimal _subtotal;
        public decimal Subtotal
        {
            get => Precio * Cantidad;
            set => _subtotal = value; // Esta asignación no afectará al valor devuelto por get
        }
        public decimal PrecioUnitario { get; set; }

        // Propiedades de navegación
        public Ventas Venta { get; set; }
        public Productos Producto { get; set; }

        public DetalleVenta()
        {
        }

        public DetalleVenta(int idDetalleVenta, int idVenta, int idProducto, int cantidad, string nombreProducto, string categoria, decimal precio, decimal precioUnitario)
        {
            IdDetalleVenta = idDetalleVenta;
            IdVenta = idVenta;
            IdProducto = idProducto;
            Cantidad = cantidad;
            NombreProducto = nombreProducto;
            Categoria = categoria;
            Precio = precio;
            PrecioUnitario = precioUnitario;
        }
    }
}
