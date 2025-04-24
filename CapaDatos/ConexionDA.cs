using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class ConexionDA
    {

        private static string _cadenaConexion = "Data Source=.;Initial Catalog=Proyecto_FinalP2_Invetario_Ventas_SitemaClientes_FacturazionDB1;Integrated Security=True";

        public static string CadenaConexion => _cadenaConexion;
        // Método para obtener la conexión

        public static bool ProbarConexion()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(CadenaConexion))
                {
                    conexion.Open();
                    Console.WriteLine("Conexión exitosa a la base de datos.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al conectar con la base de datos: " + ex.Message);
                return false;
            }
        }

        // Método para cerrar la conexión
        public void CerrarConexion(SqlConnection conexion)
        {
            try
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cerrar la conexión: " + ex.Message);
            }
        }
    }
}