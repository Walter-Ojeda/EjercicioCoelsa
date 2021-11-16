using Datos.Interfaces;
using Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Servicio.Interfaces
{
    public interface IServicioBase<T> where T : EntidadBase
    {
        public int Insert(T entidad);
        public T Update(T entidad);
        public void Delete(int id);
        public List<T> GetListByFilter(Filtro<T> filtro, out int totalRows);
        public List<T> GetAll();
        public T GetById(int id);
    }
}
