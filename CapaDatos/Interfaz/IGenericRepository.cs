using System;
using System.Collections.Generic;

namespace CapaDatos.Interfaz
{
    public interface IGenericRepository<T> where T : class
    {
        void Agregar(T entidad);
        List<T> ObtenerTodos();
        T ObtenerPorId(int id);
        void Actualizar(T entidad);
        void Eliminar(int id);
    }
}
