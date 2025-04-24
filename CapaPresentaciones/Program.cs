using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaPresentacion.Formularios.Clientes;
using CapaPresentaciones.Formularios.Seguridad_Principales;
using SIS_Heladeria.CapaPresentacion.Formularios.Inventario;
using SIS_Heladeria.CapaPresentacion.Formularios.Reportes;
using SIS_Heladeria.CapaPresentacion.Formularios.Seguridad_Principales;
using SIS_Heladeria.CapaPresentacion.Formularios.Ventas;

namespace CapaPresentaciones
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmLogin());
        }
    }
}
