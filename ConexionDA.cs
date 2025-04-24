using System;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class ConexionDA
    {
        private static string _cadenaConexion;

        public static string CadenaConexion
        {
            get
            {
                if (string.IsNullOrEmpty(_cadenaConexion))
                {
                    _cadenaConexion = "Server=DESKTOP-CUUOMB2;Database=Proyecto_FinalP2_Invetario_Ventas_SitemaClientes_FacturazionDB;Integrated Security=True;";
                }
                return _cadenaConexion;
            }
        }

        public static bool ProbarConexion()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(CadenaConexion))
                {
                    conexion.Open();
                    Console.WriteLine("Conectado con la bases de datos");
                    return true;
                }
            }
            catch
            {
                Console.WriteLine("Error al conectar con la bases de datos");
                return false;
            }
        }
    }
}