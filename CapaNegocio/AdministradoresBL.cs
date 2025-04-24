using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaNegocio
{
    public class AdministradoresBL
    {
        private readonly string _connectionString;
        private readonly GenericRepository<Administradores> _repository;

        public AdministradoresBL()
        {
            _connectionString = ConexionDA.CadenaConexion;
            _repository = new GenericRepository<Administradores>(_connectionString);
        }

        public List<Administradores> ObtenerTodos()
        {
            return _repository.ObtenerTodos();
        }

        public Administradores ObtenerPorId(int id)
        {
            return _repository.ObtenerPorId(id);
        }

        public void Agregar(Administradores admin)
        {
            try
            {
                ConexionDA conexionDA = new ConexionDA();
                using (SqlConnection conexion = new SqlConnection(ConexionDA.CadenaConexion))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("AgregarAdministrador", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", admin.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", admin.Apellido);
                        cmd.Parameters.AddWithValue("@Email", admin.Email);
                        cmd.Parameters.AddWithValue("@Clave", admin.Clave);
                        cmd.Parameters.AddWithValue("@NivelAcceso", admin.NivelAcceso);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", admin.FechaNacimiento);
                        cmd.Parameters.AddWithValue("@GestionUsuarios", admin.GestionUsuarios);
                        cmd.Parameters.AddWithValue("@Reportes", admin.Reportes);
                        cmd.Parameters.AddWithValue("@Configuracion", admin.Configuracion);
                        cmd.Parameters.AddWithValue("@GestionProductos", admin.GestionProductos);
                        cmd.Parameters.AddWithValue("@GestionVentas", admin.GestionVentas);
                        cmd.Parameters.AddWithValue("@GestionClientes", admin.GestionClientes);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar administrador: " + ex.Message);
            }
        }

        public void Actualizar(Administradores admin)
        {
            _repository.Actualizar(admin);
        }

        public void Eliminar(int id)
        {
            _repository.Eliminar(id);
        }
    }
}