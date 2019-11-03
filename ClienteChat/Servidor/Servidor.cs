using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ServidorChat
{
    public class Servidor
    {
        private Socket listener;
        private IPEndPoint miDireccion;
        private static List<Usuario> clientes;
        public Servidor(string ip, int puerto)
        {
            this.listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.miDireccion = new IPEndPoint(IPAddress.Parse(ip), puerto);
            clientes = new List<Usuario> { };
            listener.Bind(miDireccion);
            listener.Listen(10);
        }
        public void AceptarConexiones()
        {
            while (true)
            {
                try
                {
                    Socket nuevoCliente = listener.Accept();
                    byte[] bytes = new byte[100];
                    int tamanio = nuevoCliente.Receive(bytes);
                    string nombre = Encoding.ASCII.GetString(bytes, 0, tamanio);
                    foreach (Usuario u in clientes)
                    {
                        u.Conexion.Send(Encoding.ASCII.GetBytes(nombre + " se ha conectado"));
                    }
                    Usuario nuevoUsuario = new Usuario(nuevoCliente, nombre);
                    clientes.Add(nuevoUsuario); 
                    Thread thr = new Thread(Servidor.EscucharCliente);
                    thr.Start(nuevoUsuario);
                }
                catch (Exception error)
                {
                    Console.WriteLine("Error: {0}", error.ToString());
                }
            }
        }
        private static void EscucharCliente(object param)
        {
            Usuario cliente = ((Usuario)param);
            while (true)
            {
                try
                {
                    byte[] b = new byte[100];
                    cliente.Conexion.Receive(b);
                    foreach (Usuario u in clientes)
                    {
                        if (u != cliente)
                            u.Conexion.Send(b);
                    }
                }
                catch (Exception error)
                {
                    clientes.Remove(cliente);
                    foreach (Usuario u in clientes)
                    {
                        u.Conexion.Send(Encoding.ASCII.GetBytes(cliente.Nick + " se ha desconectado"));
                    }
                    cliente.Conexion.Close();
                    break;
                }
            }
        }
    }
}