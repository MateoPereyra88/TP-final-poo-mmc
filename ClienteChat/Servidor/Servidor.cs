using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;      //required
using System.Net.Sockets;    //required
using System.Threading;

namespace ServidorChat
{
    public class Servidor
    {
        private Socket listener;
        private IPEndPoint miDireccion;
        private static List<Socket> clientes;

        public Servidor(string ip, int puerto)
        {
            this.listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.miDireccion = new IPEndPoint(IPAddress.Any, puerto);
            clientes = new List<Socket>();
            listener.Bind(miDireccion);
            listener.Listen(10);

        }


        public void AceptarConexiones()
        {
            while (true)
            {
                try
                {
                    Socket nuevoCliente = listener.Accept(); //Acepto la nueva conexion
                    Console.WriteLine("Conectado con exito");
                    clientes.Add(nuevoCliente); //Agrego el socket a la lista de clientes
                    Thread thr = new Thread(Servidor.EscucharCliente);
                    thr.Start(nuevoCliente);//
                }
                catch (Exception error)
                {
                    Console.WriteLine("Error: {0}", error.ToString());
                }

            }
        }

        private static void EscucharCliente(object param)
        {
            Socket cliente = ((Socket)param);
            while (true)
            {
                try
                {
                    byte[] b = new byte[100];
                    cliente.Receive(b);
                    foreach (Socket c in clientes)
                    {
                        if (c != cliente)
                            c.Send(b);
                    }

                    /*byte[] b = new byte[100];
                    int k = cliente.Receive(b);
                    string msg = Encoding.ASCII.GetString(b, 0, k);
                    Console.WriteLine(msg);*/
                }
                catch (Exception error)
                {
                    Console.WriteLine("El cliente se ha desconectado");
                    clientes.Remove(cliente);
                    cliente.Close();
                    break;
                }
            }
        }

    }
}
