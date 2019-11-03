using System.Net.Sockets;

namespace ServidorChat
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
        public Socket Conexion => conexion;
        public string Nick => nick;
    }
}