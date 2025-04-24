using CapaDatos.Interfaz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace CapaDatos
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        private readonly string _connectionString;
        private readonly string _tableName;

        public GenericRepository(string connectionString)
        {
            _connectionString = connectionString;
            _tableName = typeof(T).Name; // Asume que el nombre de la clase es igual al nombre de la tabla
        }

        public void Agregar(T entidad)
        {
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                try
                {
                    conexion.Open();

                    // Obtener propiedades de la entidad
                    PropertyInfo[] propiedades = typeof(T).GetProperties();

                    // Construir la consulta SQL
                    string columnas = string.Join(", ", propiedades.Where(p => !p.Name.Equals("ID") && !p.Name.Contains("Texto") && !p.PropertyType.Name.Contains("Collection")).Select(p => p.Name));
                    string parametros = string.Join(", ", propiedades.Where(p => !p.Name.Equals("ID") && !p.Name.Contains("Texto") && !p.PropertyType.Name.Contains("Collection")).Select(p => "@" + p.Name));

                    string query = $"INSERT INTO {_tableName} ({columnas}) VALUES ({parametros})";

                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        // Agregar parámetros
                        foreach (PropertyInfo propiedad in propiedades.Where(p => !p.Name.Equals("ID") && !p.Name.Contains("Texto") && !p.PropertyType.Name.Contains("Collection")))
                        {
                            cmd.Parameters.AddWithValue("@" + propiedad.Name, propiedad.GetValue(entidad) ?? DBNull.Value);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error al agregar {_tableName}: " + ex.Message);
                }
            }
        }

        public List<T> ObtenerTodos()
        {
            List<T> lista = new List<T>();

            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                try
                {
                    conexion.Open();
                    string query = $"SELECT * FROM {_tableName}";

                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                T entidad = new T();

                                // Mapear propiedades
                                foreach (PropertyInfo propiedad in typeof(T).GetProperties().Where(p => !p.Name.Contains("Texto") && !p.PropertyType.Name.Contains("Collection")))
                                {
                                    try
                                    {
                                        if (!reader.IsDBNull(reader.GetOrdinal(propiedad.Name)))
                                        {
                                            object valor = reader[propiedad.Name];
                                            propiedad.SetValue(entidad, valor);
                                        }
                                    }
                                    catch
                                    {
                                        // Ignorar propiedades que no existen en la tabla
                                    }
                                }

                                lista.Add(entidad);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error al obtener {_tableName}: " + ex.Message);
                }
            }

            return lista;
        }

        public T ObtenerPorId(int id)
        {
            T entidad = new T();

            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                try
                {
                    conexion.Open();

                    // Determinar el nombre de la columna ID
                    string idColumnName = "Id" + _tableName.TrimEnd('s');
                    if (_tableName == "Usuarios")
                        idColumnName = "idUsuario";
                    else if (_tableName == "Clientes")
                        idColumnName = "idCliente";
                    else if (_tableName == "Vendedores")
                        idColumnName = "idVendedor";

                    string query = $"SELECT * FROM {_tableName} WHERE {idColumnName} = @ID";

                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Mapear propiedades
                                foreach (PropertyInfo propiedad in typeof(T).GetProperties().Where(p => !p.Name.Contains("Texto") && !p.PropertyType.Name.Contains("Collection")))
                                {
                                    try
                                    {
                                        if (!reader.IsDBNull(reader.GetOrdinal(propiedad.Name)))
                                        {
                                            object valor = reader[propiedad.Name];
                                            propiedad.SetValue(entidad, valor);
                                        }
                                    }
                                    catch
                                    {
                                        // Ignorar propiedades que no existen en la tabla
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error al obtener {_tableName} por ID: " + ex.Message);
                }
            }

            return entidad;
        }

        public void Actualizar(T entidad)
        {
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                try
                {
                    conexion.Open();

                    // Obtener propiedades de la entidad
                    PropertyInfo[] propiedades = typeof(T).GetProperties().Where(p => !p.Name.Contains("Texto") && !p.PropertyType.Name.Contains("Collection")).ToArray();

                    // Determinar el nombre de la columna ID
                    string idColumnName = "Id" + _tableName.TrimEnd('s');
                    if (_tableName == "Usuarios")
                        idColumnName = "idUsuario";
                    else if (_tableName == "Clientes")
                        idColumnName = "idCliente";
                    else if (_tableName == "Vendedores")
                        idColumnName = "idVendedor";

                    PropertyInfo idPropiedad = propiedades.First(p => p.Name.Equals(idColumnName));

                    // Construir la consulta SQL
                    string setClause = string.Join(", ", propiedades.Where(p => !p.Name.Equals(idColumnName)).Select(p => $"{p.Name} = @{p.Name}"));

                    string query = $"UPDATE {_tableName} SET {setClause} WHERE {idColumnName} = @{idColumnName}";

                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        // Agregar parámetros
                        foreach (PropertyInfo propiedad in propiedades)
                        {
                            cmd.Parameters.AddWithValue("@" + propiedad.Name, propiedad.GetValue(entidad) ?? DBNull.Value);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error al actualizar {_tableName}: " + ex.Message);
                }
            }
        }

        public void Eliminar(int id)
        {
            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                try
                {
                    conexion.Open();

                    // Determinar el nombre de la columna ID
                    string idColumnName = "Id" + _tableName.TrimEnd('s');
                    if (_tableName == "Usuarios")
                        idColumnName = "idUsuario";
                    else if (_tableName == "Clientes")
                        idColumnName = "idCliente";
                    else if (_tableName == "Vendedores")
                        idColumnName = "idVendedor";

                    string query = $"DELETE FROM {_tableName} WHERE {idColumnName} = @ID";

                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error al eliminar {_tableName}: " + ex.Message);
                }
            }
        }

        // Método para ejecutar procedimientos almacenados
        public DataTable EjecutarSP(string nombreSP, Dictionary<string, object> parametros = null)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand(nombreSP, conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros si existen
                        if (parametros != null)
                        {
                            foreach (var param in parametros)
                            {
                                cmd.Parameters.AddWithValue("@" + param.Key, param.Value ?? DBNull.Value);
                            }
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error al ejecutar SP {nombreSP}: " + ex.Message);
                }
            }

            return dt;
        }
    }
}