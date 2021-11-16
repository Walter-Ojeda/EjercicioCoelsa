using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Entidades;

namespace Datos.Interfaces
{
    public interface IDatosBase<T> where T : EntidadBase
    {
        public int Insert(T entidad);
        public T Update(T entidad);
        public void Delete(int id);
        public List<T> GetListByFilter(Filtro<T> filtro, out int totalRows);
        public List<T> GetAll();
        public T GetById(int id);
    }
}
