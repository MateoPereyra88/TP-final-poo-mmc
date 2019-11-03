using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ClienteChat
{
    public class Cliente
    {
        private static Socket servidor;
        private IPEndPoint miDireccion;
        private static bool salir = false;
        private static string nombre;
        public Cliente(string ip, int puerto, string nick)
        {
            nombre = nick;
            servidor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.miDireccion = new IPEndPoint(IPAddress.Parse(ip), puerto);
            try
            {
                servidor.Connect(miDireccion);
                servidor.Send(Encoding.ASCII.GetBytes(nombre));
                Console.WriteLine("Conexion con servidor exitosa");
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error.ToString());
            }
        }
        public void Interactuar()
        {
            Thread thr1 = new Thread(Cliente.EnviarMensajes);
            Thread thr2 = new Thread(Cliente.RecibirMensaje);
            thr1.Start();
            thr2.Start();
        }
        private static void EnviarMensajes()
        {
            string msg = Console.ReadLine();
            while (msg != "salir")
            {
                byte[] bytes = Encoding.ASCII.GetBytes(nombre + ": " + msg);
                servidor.Send(bytes);
                msg = Console.ReadLine();
            }
            Desconectar();
        }
        private static void RecibirMensaje()
        {
            while (!salir)
            {
                try
                {
                    byte[] bytes = new byte[100];
                    int tamanio = servidor.Receive(bytes);
                    string msg = Encoding.ASCII.GetString(bytes, 0, tamanio);
                    Console.WriteLine(msg);
                }
                catch (Exception error)
                {
                    Console.WriteLine("te has desconectado del servidor");
                    salir = true;
                }
            }
        }
        private static void Desconectar() => servidor.Close();
    }
}