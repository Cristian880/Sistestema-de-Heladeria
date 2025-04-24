using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaNegocio
{
    public class CategoriasBL
    {
        private readonly string _connectionString;
        private readonly GenericRepository<Categorias> _repository;

        public CategoriasBL()
        {
            _connectionString = ConexionDA.CadenaConexion;
            _repository = new GenericRepository<Categorias>(_connectionString);
        }

        public List<Categorias> ObtenerTodos()
        {
            return _repository.ObtenerTodos();
        }

        public Categorias ObtenerPorId(int id)
        {
            return _repository.ObtenerPorId(id);
        }

        public void Agregar(Categorias categoria, int usuarioModificacion = 0)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("AgregarCategoria", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                        cmd.Parameters.AddWithValue("@Descripcion", categoria.Descripcion ?? (object)DBNull.Value);

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
                throw new Exception("Error al agregar categoría: " + ex.Message);
            }
        }

        public void Actualizar(Categorias categoria, int usuarioModificacion = 0)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("EditarCategoria", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdCategoria", categoria.IdCategoria);
                        cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                        cmd.Parameters.AddWithValue("@Descripcion", categoria.Descripcion ?? (object)DBNull.Value);

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
                throw new Exception("Error al actualizar categoría: " + ex.Message);
            }
        }

        public void Eliminar(int id)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("EliminarCategoria", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdCategoria", id);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar categoría: " + ex.Message);
            }
        }

        public List<Categorias> Buscar(string datoBusqueda)
        {
            List<Categorias> categorias = new List<Categorias>();

            try
            {
                using (SqlConnection conexion = new SqlConnection(_connectionString))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("BuscarCategoria", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DatoBusqueda", datoBusqueda);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Categorias categoria = new Categorias
                                {
                                    IdCategoria = Convert.ToInt32(reader["IdCategoria"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Descripcion = reader["Descripcion"].ToString(),
                                    FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"])
                                };

                                if (reader["FechaModificacion"] != DBNull.Value)
                                    categoria.FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);

                                if (reader["UsuarioModificacion"] != DBNull.Value)
                                    categoria.UsuarioModificacion = Convert.ToInt32(reader["UsuarioModificacion"]);

                                categorias.Add(categoria);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar categorías: " + ex.Message);
            }

            return categorias;
        }
    }
}