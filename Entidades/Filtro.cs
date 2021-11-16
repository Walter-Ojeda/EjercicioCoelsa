using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class Filtro<T> where T : EntidadBase
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public T Map { get; set; }
    }
}
