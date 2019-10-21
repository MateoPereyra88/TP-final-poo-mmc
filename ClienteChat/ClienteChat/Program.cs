using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ClienteChat
{
    class Program
    {


        static void Main(string[] args)
        {
            Cliente cliente = new Cliente("192.168.3.116", 1234);
            cliente.enviarMensajes();
        }

    }
}
