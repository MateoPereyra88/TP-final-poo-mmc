using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ValoresConstantes;
using MySql.Data.MySqlClient;
using System.Data;

namespace Ser
{
    public class Servidor
    {
        private Socket listener;
        private IPEndPoint miDireccion;
        private static List<Usuario> clientes;// innecesario??
        private static MySqlConnection conexionBD;

        public Servidor(string ip, int puerto)
        {
            this.listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.miDireccion = new IPEndPoint(IPAddress.Parse(ip), puerto);
            clientes = new List<Usuario> { };
            listener.Bind(miDireccion);
            listener.Listen(10);
            Console.WriteLine("servidor funcionando...");//--------------

            conexionBD = new MySqlConnection("server = localhost; user id = root; database = KrauseCord_BD");
            conexionBD.Open();
            Console.WriteLine("conexión con la base de datos exitosa...");//--------------

        }
        public void AceptarConexiones()
        {
            while (true)
            {
                try
                {
                    Socket nuevoCliente = listener.Accept();

                    byte[] nomBytes = new byte[100];
                    int nomTamanio = nuevoCliente.Receive(nomBytes);
                    string nombre = Encoding.ASCII.GetString(nomBytes, 0, nomTamanio);

                    byte[] passBytes = new byte[100];
                    int passTamanio = nuevoCliente.Receive(passBytes);
                    string password = Encoding.ASCII.GetString(passBytes, 0, passTamanio);


                    byte[] lsBytes = new byte[100];
                    int lsTamanio = nuevoCliente.Receive(lsBytes);
                    string log_o_sign = Encoding.ASCII.GetString(lsBytes, 0, lsTamanio);


                    Console.WriteLine(nombre);
                    Console.WriteLine(password);
                    Console.WriteLine(log_o_sign);
                    
                    Usuario nuevoUsuario = new Usuario(nuevoCliente, nombre, password);

                    

                    if (log_o_sign == "sign")
                    { 
                        
                        nuevoUsuario.RegistrarEn(conexionBD);
                        
                        
                        nuevoUsuario.CargarMensajes(conexionBD);
                        
                        foreach (Usuario u in clientes)
                        {
                            u.Conexion().Send(Encoding.ASCII.GetBytes("1"));
                            Thread.Sleep(ClaseConstante.TiempoDeEspera);
                            u.Conexion().Send(Encoding.ASCII.GetBytes(nombre + " se ha conectado"));
                            Thread.Sleep(ClaseConstante.TiempoDeEspera);
                            u.Conexion().Send(Encoding.ASCII.GetBytes("---"));
                        }

                        clientes.Add(nuevoUsuario);
                        





                        Thread thr = new Thread(Servidor.EscucharCliente);
                        thr.Start(nuevoUsuario);

                        

                        Console.WriteLine(nombre + " se ha conectado con exito");


                    }
                    else
                    {

                        if(nuevoUsuario.IniciarSesion(conexionBD) > 0)
                        {
                            Thread.Sleep(ClaseConstante.TiempoDeEspera);//++++
                            nuevoUsuario.Conexion().Send(Encoding.ASCII.GetBytes("1"));
                            Thread.Sleep(ClaseConstante.TiempoDeEspera);//++++
                            nuevoUsuario.Conexion().Send(Encoding.ASCII.GetBytes("te has conectado"));
                            Thread.Sleep(ClaseConstante.TiempoDeEspera);//++++
                            nuevoUsuario.Conexion().Send(Encoding.ASCII.GetBytes("---"));



                            nuevoUsuario.CargarMensajes(conexionBD);


                            foreach (Usuario u in clientes)
                            {
                                u.Conexion().Send(Encoding.ASCII.GetBytes("1"));
                                Thread.Sleep(ClaseConstante.TiempoDeEspera);//++++
                                u.Conexion().Send(Encoding.ASCII.GetBytes(nombre + " se ha conectado"));
                                Thread.Sleep(ClaseConstante.TiempoDeEspera);//++++
                                u.Conexion().Send(Encoding.ASCII.GetBytes("---"));
                            }


                            clientes.Add(nuevoUsuario);

                            Thread thr = new Thread(Servidor.EscucharCliente);
                            thr.Start(nuevoUsuario);
                            Console.WriteLine(nombre + " se ha conectado con exito");//--------------
                        }
                        else
                        {
                            Console.WriteLine("usuario inexistente o contraseña incorrecta");

                            Thread.Sleep(ClaseConstante.TiempoDeEspera);//++++
                            nuevoUsuario.Conexion().Send(Encoding.ASCII.GetBytes("1"));
                            Thread.Sleep(ClaseConstante.TiempoDeEspera);//++++
                            nuevoUsuario.Conexion().Send(Encoding.ASCII.GetBytes("usuario inexistente o contrasea incorrecta"));
                            Thread.Sleep(ClaseConstante.TiempoDeEspera);//++++
                            nuevoUsuario.Conexion().Send(Encoding.ASCII.GetBytes("---"));

                        }

                    }


                    
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


                    byte[] bMs = new byte[10000];
                    Console.WriteLine(clien.Nick() + ": esperando mensaje...");//--------------
                    int tMs = clien.Conexion().Receive(bMs);
                    string msg = Encoding.ASCII.GetString(bMs, 0, tMs);
                    Console.WriteLine(clien.Nick() + ": " + msg);//--------------


                    byte[] bNo = new byte[100];
                    Console.WriteLine(clien.Nick() + ": esperando nombre...");//--------------
                    int tNo = clien.Conexion().Receive(bNo);
                    string nombre = Encoding.ASCII.GetString(bNo, 0, tNo);
                    Console.WriteLine(nombre);//--------------

                    if (tipo != "" && tipo != string.Empty && tipo != null)
                    {

                        MySqlCommand comando = new MySqlCommand(String.Format("insert into Mensaje(nom_usuario, mensaje) values ('{0}', '{1}') ", clien.Nick(), msg), conexionBD);
                        comando.ExecuteNonQuery();
                        Console.WriteLine("-_-_-_-_-");

                        foreach (Usuario u in clientes)
                        {
                            if (u != clien)
                            {
                                if (tipo == "1")
                                {
                                    u.Conexion().Send(Encoding.ASCII.GetBytes(tipo));
                                    Thread.Sleep(ClaseConstante.TiempoDeEspera);//++++
                                    u.Conexion().Send(Encoding.ASCII.GetBytes(clien.Nick() + ": " + msg));
                                    Thread.Sleep(ClaseConstante.TiempoDeEspera);//++++
                                    u.Conexion().Send(Encoding.ASCII.GetBytes(nombre));


                                }
                                else
                                {
                                    u.Conexion().Send(Encoding.ASCII.GetBytes(tipo));
                                    Thread.Sleep(ClaseConstante.TiempoDeEspera);//++++
                                    u.Conexion().Send(Encoding.ASCII.GetBytes(msg));
                                    Thread.Sleep(ClaseConstante.TiempoDeEspera);//++++
                                    u.Conexion().Send(Encoding.ASCII.GetBytes(nombre));


                                    // hacer algo para el registro de archivos

                                }

                            }

                        }




                    }
                    else
                        throw new Exception("ututv");




                }
                catch (Exception error)
                {
                    Console.WriteLine(clien.Nick() + " se ha desconectado");
                    clientes.Remove(clien);
                    foreach (Usuario u in clientes)
                    {
                        u.Conexion().Send(Encoding.ASCII.GetBytes("1"));
                        Thread.Sleep(ClaseConstante.TiempoDeEspera);//++++
                        u.Conexion().Send(Encoding.ASCII.GetBytes(clien.Nick() + " se ha desconectado"));
                        Thread.Sleep(ClaseConstante.TiempoDeEspera);//++++
                        u.Conexion().Send(Encoding.ASCII.GetBytes("---"));
                    }
                    clien.Conexion().Close();
                    break;
                }
            }
        }
    }
}
