    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ser
{
    class Program
    {
        static void Main(string[] args)
        {
            Servidor servidor = new Servidor("192.168.3.111", 1234);
            servidor.AceptarConexiones();
        }
    }
}
