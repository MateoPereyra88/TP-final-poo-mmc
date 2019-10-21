using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;      //required
using System.Net.Sockets;    //required

namespace ServidorChat
{
    class Program
    {
        static void Main(string[] args)
        {
            Servidor servidor = new Servidor("192.168.3.116", 1234);
            servidor.AceptarConexiones();
        }
    }
}
