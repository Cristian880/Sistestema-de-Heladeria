using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Productos
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public bool Estado { get; set; }
        public int? IdCategoria { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? UsuarioModificacion { get; set; }
        public string NombreCategoria { get; set; }


        // Propiedad de navegación
        public Categorias Categoria { get; set; }

        public Productos()
        {
            FechaCreacion = DateTime.Now;
            Estado = true;
        }

        public Productos(int idProducto, string nombre, decimal precio, int stock, bool estado, int? idCategoria, string nombreCategoria)
        {
            IdProducto = idProducto;
            Nombre = nombre;
            Precio = precio;
            Stock = stock;
            Estado = estado;
            IdCategoria = idCategoria;
            FechaCreacion = DateTime.Now;
            NombreCategoria = nombreCategoria;
        }
    }
}
