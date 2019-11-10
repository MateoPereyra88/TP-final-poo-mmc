using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Ser
{
    public class Usuario
    {
        private Socket conexion;
        private string nick;
        public Usuario(Socket conexion, string nick)
        {
            this.conexion = conexion;
            this.nick = nick;
        }
        public Socket Conexion() => conexion;
        public string Nick() => nick;
    }
}
