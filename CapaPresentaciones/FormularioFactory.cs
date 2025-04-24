// CapaPresentacion/FormularioFactory.cs
using CapaPresentacion.Formularios.Clientes;
using CapaPresentaciones.Formularios.Seguridad_Principales;
using SIS_Heladeria.CapaPresentacion.Formularios.Inventario;
using SIS_Heladeria.CapaPresentacion.Formularios.Reportes;
using SIS_Heladeria.CapaPresentacion.Formularios.Seguridad_Principales;
using SIS_Heladeria.CapaPresentacion.Formularios.Ventas;
using System;
using System.Windows.Forms;

namespace CapaPresentaciones.Formularios
{
    public static class FormularioFactory
    {
        public static Form CrearFormulario(string tipo)
        {
            switch (tipo.ToLower())
            {
                case "login":
                    return new FrmLogin();
                case "administradores":
                    return new FrmAdministradores();
                case "usuarios":
                    return new FrmUsuarios();
                case "productos":
                    return new FrmProductos();
                case "clientes":
                    return new FrmClientes();
                case "facturas":
                    return new FrmFacturas();
                case "reportes":
                    return new FrmReportes();
                case "bajostock":
                    return new FrmBajoStock();
                case "buscarproducto":
                    return new FrmBuscarProducto();
                case "ventas":
                    return new FrmVentas();
                default:
                    throw new ArgumentException($"Tipo de formulario no reconocido: {tipo}");
            }
        }
    }
}