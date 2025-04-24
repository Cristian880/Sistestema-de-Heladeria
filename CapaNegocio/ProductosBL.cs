using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaNegocio
{
    public class ProductosBL
    {
        private readonly string _connectionString;
        private readonly GenericRepository<Productos> _repository;

        public ProductosBL()
        {
            _connectionString = ConexionDA.CadenaConexion;
            _repository = new GenericRepository<Productos>(_connectionString);
        }

        public List<Productos> ObtenerTodos()
        {
            List<Productos> productos = new List<Productos>();

            try
            {
                DataTable dt = _repository.EjecutarSP("SELECT p.*, c.Nombre AS NombreCategoria FROM Productos p LEFT JOIN Categorias c ON p.id_Categoria = c.IdCategoria");

                foreach (DataRow row in dt.Rows)
                {
                    Productos producto = new Productos
                    {
                        IdProducto = Convert.ToInt32(row["IdProducto"]),
                        Nombre = row["Nombre"].ToString(),
                        Precio = Convert.ToDecimal(row["Precio"]),
                        Stock = Convert.ToInt32(row["Stock"]),
                        Estado = Convert.ToBoolean(row["Estado"]),
                        FechaCreacion = Convert.ToDateTime(row["FechaCreacion"])
                    };

                    if (row["id_Categoria"] != DBNull.Value)
                        producto.IdCategoria = Convert.ToInt32(row["id_Categoria"]);

                    if (row["NombreCategoria"] != DBNull.Value)
                        producto.NombreCategoria = row["NombreCategoria"].ToString();

                    if (row["FechaModificacion"] != DBNull.Value)
                        producto.FechaModificacion = Convert.ToDateTime(row["FechaModificacion"]);

                    if (row["UsuarioModificacion"] != DBNull.Value)
                        producto.UsuarioModificacion = Convert.ToInt32(row["UsuarioModificacion"]);

                    productos.Add(producto);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener productos: " + ex.Message);
            }

            return productos;
        }

        public Productos ObtenerPorId(int id)
        {
            Productos producto = null;

            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    string query = "SELECT p.*, c.Nombre AS NombreCategoria FROM Productos p LEFT JOIN Categorias c ON p.id_Categoria = c.IdCategoria WHERE p.IdProducto = @IdProducto";

                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@IdProducto", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                producto = new Productos
                                {
                                    IdProducto = Convert.ToInt32(reader["IdProducto"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Precio = Convert.ToDecimal(reader["Precio"]),
                                    Stock = Convert.ToInt32(reader["Stock"]),
                                    Estado = Convert.ToBoolean(reader["Estado"]),
                                    FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"])
                                };

                                if (reader["id_Categoria"] != DBNull.Value)
                                    producto.IdCategoria = Convert.ToInt32(reader["id_Categoria"]);

                                if (reader["NombreCategoria"] != DBNull.Value)
                                    producto.NombreCategoria = reader["NombreCategoria"].ToString();

                                if (reader["FechaModificacion"] != DBNull.Value)
                                    producto.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);

                                if (reader["UsuarioModificacion"] != DBNull.Value)
                                    producto.UsuarioModificacion = Convert.ToInt32(reader["UsuarioModificacion"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener producto: " + ex.Message);
            }

            return producto;
        }

        public void Agregar(Productos producto, int usuarioModificacion = 0)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("AgregarProducto", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                        cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                        cmd.Parameters.AddWithValue("@Stock", producto.Stock);

                        if (producto.IdCategoria.HasValue)
                            cmd.Parameters.AddWithValue("@IdCategoria", producto.IdCategoria.Value);
                        else
                            cmd.Parameters.AddWithValue("@IdCategoria", DBNull.Value);

                        if (usuarioModificacion > 0)
                            cmd.Parameters.AddWithValue("@UsuarioModificacion", usuarioModificacion);
                        else
                            cmd.Parameters.AddWithValue("@UsuarioModificacion", DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar producto: " + ex.Message);
            }
        }

        public void Actualizar(Productos producto, int usuarioModificacion = 0)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("EditarProducto", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                        cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                        cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                        cmd.Parameters.AddWithValue("@Stock", producto.Stock);

                        if (producto.IdCategoria.HasValue)
                            cmd.Parameters.AddWithValue("@IdCategoria", producto.IdCategoria.Value);
                        else
                            cmd.Parameters.AddWithValue("@IdCategoria", DBNull.Value);

                        if (usuarioModificacion > 0)
                            cmd.Parameters.AddWithValue("@UsuarioModificacion", usuarioModificacion);
                        else
                            cmd.Parameters.AddWithValue("@UsuarioModificacion", DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar producto: " + ex.Message);
            }
        }

        public void Eliminar(int id, int usuarioModificacion = 0)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("EliminarProducto", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdProducto", id);

                        if (usuarioModificacion > 0)
                            cmd.Parameters.AddWithValue("@UsuarioModificacion", usuarioModificacion);
                        else
                            cmd.Parameters.AddWithValue("@UsuarioModificacion", DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar producto: " + ex.Message);
            }
        }

        public List<Productos> Buscar(string datoBusqueda, int? idCategoria = null)
        {
            List<Productos> productos = new List<Productos>();

            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("BuscarProducto", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DatoBusqueda", datoBusqueda);

                        if (idCategoria.HasValue)
                            cmd.Parameters.AddWithValue("@IdCategoria", idCategoria.Value);
                        else
                            cmd.Parameters.AddWithValue("@IdCategoria", DBNull.Value);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Productos producto = new Productos
                                {
                                    IdProducto = Convert.ToInt32(reader["IdProducto"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Precio = Convert.ToDecimal(reader["Precio"]),
                                    Stock = Convert.ToInt32(reader["Stock"]),
                                    Estado = Convert.ToBoolean(reader["Estado"]),
                                    FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"])
                                };

                                if (reader["id_Categoria"] != DBNull.Value)
                                    producto.IdCategoria = Convert.ToInt32(reader["id_Categoria"]);

                                if (reader["NombreCategoria"] != DBNull.Value)
                                    producto.NombreCategoria = reader["NombreCategoria"].ToString();

                                if (reader["FechaModificacion"] != DBNull.Value)
                                    producto.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);

                                if (reader["UsuarioModificacion"] != DBNull.Value)
                                    producto.UsuarioModificacion = Convert.ToInt32(reader["UsuarioModificacion"]);

                                productos.Add(producto);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar productos: " + ex.Message);
            }

            return productos;
        }

        public List<Productos> ObtenerProductosBajoStock(int limiteStock = 10, int? idCategoria = null)
        {
            List<Productos> productos = new List<Productos>();

            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("ObtenerProductosBajoStock", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@LimiteStock", limiteStock);

                        if (idCategoria.HasValue)
                            cmd.Parameters.AddWithValue("@IdCategoria", idCategoria.Value);
                        else
                            cmd.Parameters.AddWithValue("@IdCategoria", DBNull.Value);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Productos producto = new Productos
                                {
                                    IdProducto = Convert.ToInt32(reader["IdProducto"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Precio = Convert.ToDecimal(reader["Precio"]),
                                    Stock = Convert.ToInt32(reader["Stock"]),
                                    NombreCategoria = reader["Categoria"].ToString()
                                };

                                productos.Add(producto);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener productos con bajo stock: " + ex.Message);
            }

            return productos;
        }

        public List<Productos> ObtenerProductosPorCategoria(int idCategoria)
        {
            List<Productos> productos = new List<Productos>();

            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("ObtenerProductosPorCategoria", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Productos producto = new Productos
                                {
                                    IdProducto = Convert.ToInt32(reader["IdProducto"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Precio = Convert.ToDecimal(reader["Precio"]),
                                    Stock = Convert.ToInt32(reader["Stock"]),
                                    NombreCategoria = reader["Categoria"].ToString()
                                };

                                productos.Add(producto);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener productos por categoría: " + ex.Message);
            }

            return productos;
        }
    }
}