using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Ser
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

            Console.WriteLine("servidor funcionando...");//--------------
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
                        u.Conexion().Send(Encoding.ASCII.GetBytes("1"));
                        Thread.Sleep(200);//++++
                        u.Conexion().Send(Encoding.ASCII.GetBytes(nombre + " se ha conectado"));
                        Thread.Sleep(200);//++++
                        u.Conexion().Send(Encoding.ASCII.GetBytes("---"));
                    }


                    Usuario nuevoUsuario = new Usuario(nuevoCliente, nombre);
                    clientes.Add(nuevoUsuario);
                    Thread thr = new Thread(Servidor.EscucharCliente);
                    thr.Start(nuevoUsuario);


                    Console.WriteLine(nombre + " se ha conectado con exito");//--------------
                }
                catch (Exception error)
                {
                    Console.WriteLine("Error: {0}", error.ToString());
                }
            }
        }
        private static void EscucharCliente(object param)
        {
            Usuario clien = ((Usuario)param);
            while (true)
            {
                try
                {
                    
                    

                    byte[] bTi = new byte[1];
                    Console.WriteLine(clien.Nick() + ": esperando tipo...");//--------------
                    int tTi = clien.Conexion().Receive(bTi);
                    string tipo = Encoding.ASCII.GetString(bTi, 0, tTi);
                    Console.WriteLine(tipo);//--------------


                    byte[] bMs = new byte[100];
                    Console.WriteLine(clien.Nick() + ": esperando mensaje...");//--------------
                    int tMs = clien.Conexion().Receive(bMs);
                    string msg = Encoding.ASCII.GetString(bMs, 0, tMs);
                    Console.WriteLine(clien.Nick() + ": " + msg);//--------------


                    byte[] bNo = new byte[100];
                    Console.WriteLine(clien.Nick() + ": esperando nombre...");//--------------
                    int tNo = clien.Conexion().Receive(bNo);
                    string nombre = Encoding.ASCII.GetString(bNo, 0, tNo);
                    Console.WriteLine(nombre);//--------------










                    foreach (Usuario u in clientes)
                    {
                        if (u != clien)
                        {
                            if(tipo == "1")
                            {
                                u.Conexion().Send(Encoding.ASCII.GetBytes(tipo));
                                Thread.Sleep(200);//++++
                                u.Conexion().Send(Encoding.ASCII.GetBytes(clien.Nick() + ": " + msg));
                                Thread.Sleep(200);//++++
                                u.Conexion().Send(Encoding.ASCII.GetBytes(nombre));
                            }
                            else
                            {
                                u.Conexion().Send(Encoding.ASCII.GetBytes(tipo));
                                u.Conexion().Send(Encoding.ASCII.GetBytes(msg));
                                u.Conexion().Send(Encoding.ASCII.GetBytes(nombre));
                            }
                            
                        }
                            
                    }
                    

                }
                catch (Exception error)
                {
                    clientes.Remove(clien);
                    foreach (Usuario u in clientes)
                    {
                        u.Conexion().Send(Encoding.ASCII.GetBytes(clien.Nick() + " se ha desconectado"));
                    }
                    clien.Conexion().Close();
                    break;
                }
            }
        }

        /*private static void DireccionarArchivo(Usuario cliente)
        {
            byte[] bNombre = new byte[100];
            cliente.Conexion().Receive(bNombre);
            byte[] bContenido = new byte[100];
            cliente.Conexion().Receive(bContenido);

            foreach (Usuario u in clientes)
            {
                if (u != cliente)
                {
                    u.Conexion().Send(bNombre);
                    u.Conexion().Send(bContenido);
                }
            }
        }*/

    }
}
