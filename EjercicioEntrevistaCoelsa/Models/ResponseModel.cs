namespace EjercicioEntrevistaCoelsa.Models
{
    public class ResponseModel
    {
        public ResponseModel()
        {
            Codigo = CodigosDeEstado.Error;
            Descripcion = "Error interno";
        }
        public CodigosDeEstado Codigo { get; set; }
        public string Descripcion { get; set; }
        public dynamic Data { get; set; }

        public enum CodigosDeEstado
        {
            Ok = 0,
            Error = -1,
            InvalidRequest = -2
        }
    }
}
