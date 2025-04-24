using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class UsuarioDA
    {
        private readonly string _connectionString;

        /// <summary>
        /// Constructor de la clase UsuarioDA
        /// </summary>
        public UsuarioDA()
        {
            // Usa la propiedad estática CadenaConexion de ConexionDA
            _connectionString = ConexionDA.CadenaConexion;
        }

        /// <summary>
        /// Autentica un usuario por su correo y contraseña
        /// </summary>
        /// <param name="gmail">Correo electrónico del usuario</param>
        /// <param name="clave">Contraseña del usuario</param>
        /// <returns>DataTable con la información del usuario si la autenticación es exitosa, tabla vacía en caso contrario</returns>
        public DataTable AutenticarUsuario(string gmail, string clave)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("AutenticarUsuario", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@GmailUsuario", gmail);
                        cmd.Parameters.AddWithValue("@Clave", clave);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al autenticar usuario: " + ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// Agrega un nuevo usuario al sistema
        /// </summary>
        /// <param name="nombre">Nombre del usuario</param>
        /// <param name="apellido">Apellido del usuario</param>
        /// <param name="gmail">Correo electrónico del usuario</param>
        /// <param name="clave">Contraseña del usuario</param>
        /// <param name="rol">Rol del usuario</param>
        /// <param name="fechaNacimiento">Fecha de nacimiento del usuario (opcional)</param>
        /// <returns>True si el usuario fue agregado correctamente, False en caso contrario</returns>
        public bool AgregarUsuario(string nombre, string apellido, string gmail, string clave, string rol, DateTime? fechaNacimiento = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("AgregarUsuario", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", nombre);
                        cmd.Parameters.AddWithValue("@Apellido", apellido);
                        cmd.Parameters.AddWithValue("@GmailUsuario", gmail);
                        cmd.Parameters.AddWithValue("@Clave", clave);
                        cmd.Parameters.AddWithValue("@Rol", rol);

                        if (fechaNacimiento.HasValue)
                            cmd.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento.Value);
                        else
                            cmd.Parameters.AddWithValue("@FechaNacimiento", DBNull.Value);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar usuario: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Edita la información de un usuario existente
        /// </summary>
        /// <param name="idUsuario">ID del usuario a editar</param>
        /// <param name="nombre">Nuevo nombre del usuario</param>
        /// <param name="apellido">Nuevo apellido del usuario</param>
        /// <param name="gmail">Nuevo correo electrónico del usuario</param>
        /// <param name="clave">Nueva contraseña del usuario</param>
        /// <param name="rol">Nuevo rol del usuario</param>
        /// <param name="fechaNacimiento">Nueva fecha de nacimiento del usuario (opcional)</param>
        /// <param name="estado">Nuevo estado del usuario</param>
        /// <returns>True si el usuario fue editado correctamente, False en caso contrario</returns>
        public bool EditarUsuario(int idUsuario, string nombre, string apellido, string gmail, string clave, string rol, DateTime? fechaNacimiento = null, bool estado = true)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("EditarUsuario", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                        cmd.Parameters.AddWithValue("@Nombre", nombre);
                        cmd.Parameters.AddWithValue("@Apellido", apellido);
                        cmd.Parameters.AddWithValue("@GmailUsuario", gmail);
                        cmd.Parameters.AddWithValue("@Clave", clave);
                        cmd.Parameters.AddWithValue("@Rol", rol);

                        if (fechaNacimiento.HasValue)
                            cmd.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento.Value);
                        else
                            cmd.Parameters.AddWithValue("@FechaNacimiento", DBNull.Value);

                        cmd.Parameters.AddWithValue("@Estado", estado);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al editar usuario: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Elimina (desactiva) un usuario del sistema
        /// </summary>
        /// <param name="idUsuario">ID del usuario a eliminar</param>
        /// <returns>True si el usuario fue eliminado correctamente, False en caso contrario</returns>
        public bool EliminarUsuario(int idUsuario)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("EliminarUsuario", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar usuario: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Busca usuarios según un criterio de búsqueda
        /// </summary>
        /// <param name="datoBusqueda">Criterio de búsqueda (ID, nombre, apellido o correo)</param>
        /// <returns>DataTable con los usuarios que coinciden con el criterio de búsqueda</returns>
        public DataTable BuscarUsuario(string datoBusqueda)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("BuscarUsuario", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DatoBusqueda", datoBusqueda);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al buscar usuario: " + ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// Obtiene información de todos los usuarios del sistema
        /// </summary>
        /// <returns>DataTable con la información de todos los usuarios</returns>
        public DataTable ObtenerInformacionUsuarios()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("ObtenerInformacionUsuarios", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener información de usuarios: " + ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// Obtiene un usuario por su ID
        /// </summary>
        /// <param name="idUsuario">ID del usuario</param>
        /// <returns>DataTable con la información del usuario</returns>
        public DataTable ObtenerUsuarioPorId(int idUsuario)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Usuarios WHERE idUsuario = @idUsuario", conn))
                    {
                        cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener usuario por ID: " + ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// Verifica si un correo electrónico ya está registrado
        /// </summary>
        /// <param name="gmail">Correo electrónico a verificar</param>
        /// <returns>True si el correo ya existe, False en caso contrario</returns>
        public bool VerificarCorreoExistente(string gmail)
        {
            bool existe = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Usuarios WHERE GmailUsuario = @gmail", conn))
                    {
                        cmd.Parameters.AddWithValue("@gmail", gmail);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        existe = count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al verificar correo: " + ex.Message);
            }
            return existe;
        }

        /// <summary>
        /// Cambia la contraseña de un usuario
        /// </summary>
        /// <param name="idUsuario">ID del usuario</param>
        /// <param name="nuevaClave">Nueva contraseña</param>
        /// <returns>True si el cambio fue exitoso, False en caso contrario</returns>
        public bool CambiarClave(int idUsuario, string nuevaClave)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Usuarios SET Clave = @nuevaClave WHERE idUsuario = @idUsuario", conn))
                    {
                        cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                        cmd.Parameters.AddWithValue("@nuevaClave", nuevaClave);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cambiar clave: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Obtiene todos los usuarios con un rol específico
        /// </summary>
        /// <param name="rol">Rol a buscar</param>
        /// <returns>DataTable con los usuarios que tienen el rol especificado</returns>
        public DataTable ObtenerUsuariosPorRol(string rol)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Usuarios WHERE Rol = @rol AND Estado = 1", conn))
                    {
                        cmd.Parameters.AddWithValue("@rol", rol);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener usuarios por rol: " + ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// Actualiza el estado de un usuario (activo/inactivo)
        /// </summary>
        /// <param name="idUsuario">ID del usuario</param>
        /// <param name="estado">Nuevo estado (true = activo, false = inactivo)</param>
        /// <returns>True si la actualización fue exitosa, False en caso contrario</returns>
        public bool ActualizarEstadoUsuario(int idUsuario, bool estado)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Usuarios SET Estado = @estado WHERE idUsuario = @idUsuario", conn))
                    {
                        cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                        cmd.Parameters.AddWithValue("@estado", estado);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar estado de usuario: " + ex.Message);
                return false;
            }
        }
    }
}