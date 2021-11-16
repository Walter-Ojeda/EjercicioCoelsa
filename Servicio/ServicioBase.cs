using Datos.Interfaces;
using Servicio.Interfaces;
using System;
using Entidades;

namespace Servicio
{
    public class ServicioBase<T, TD>
        where T : EntidadBase
        where TD : IDatosBase<T>
    {
        protected readonly TD _datos;
        public ServicioBase(TD datos)
        {
            _datos = datos;
        }
    }
}
