using CapaDatos.Interfaz;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
namespace Sistema.Interfaz
{
    public static class RepositoryFactory
    {
        public static IGenericRepository<T> CreateRepository<T>(string connectionString) where T : class, new()
        {
            if (typeof(T) == typeof(Productos))
                return (IGenericRepository<T>)new ProductoRepository(connectionString);

            if (typeof(T) == typeof(Usuarios))
                return (IGenericRepository<T>)new UsuarioRepository(connectionString);

            if (typeof(T) == typeof(Clientes))
                return (IGenericRepository<T>)new ClienteRepository(connectionString);

            // Otros tipos...

            throw new ArgumentException($"No hay repositorio definido para el tipo {typeof(T).Name}");
        }
    }
}
*/