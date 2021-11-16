using FrameWork;

namespace Datos
{
    public class Datos
    {
        protected SqlServerConnector connection = null;

        public Datos()
        {
            this.connection = new SqlServerConnector();
        }
    }
}
