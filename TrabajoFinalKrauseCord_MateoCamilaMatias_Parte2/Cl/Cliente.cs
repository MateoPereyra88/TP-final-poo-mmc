using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using ValoresConstantes;

namespace Cl
{
    public class Cliente
    {
        private static Socket servidor;
        private IPEndPoint miDireccion;
        private List<Archivo> listArchivos = new List<Archivo> { };

        public Cliente(string ip, int puerto, string nick, string password, string log_o_sign)
        {
            servidor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.miDireccion = new IPEndPoint(IPAddress.Parse(ip), puerto);
            try
            {
                servidor.Connect(miDireccion);

                servidor.Send(Encoding.ASCII.GetBytes(nick));
                Thread.Sleep(ClaseConstante.TiempoDeEspera);
                servidor.Send(Encoding.ASCII.GetBytes(password));
                Thread.Sleep(ClaseConstante.TiempoDeEspera);
                servidor.Send(Encoding.ASCII.GetBytes(log_o_sign));


            }
            catch (Exception error)
            {
                MessageBox.Show("Servidor no encontrado");
            }
        }

        public List<Archivo> ListaDeArchivos => listArchivos;
        public Socket ServidorConectado => servidor;

        public void EnviarMensajes(string tipo, string msg, string nombre)
        {
            if(msg != "" && msg != null && msg != string.Empty)
            {
                byte[] bytesTi = Encoding.ASCII.GetBytes(tipo);
                byte[] bytesMs = Encoding.ASCII.GetBytes(msg);
                byte[] bytesNom = Encoding.ASCII.GetBytes(nombre);

                servidor.Send(bytesTi);
                Thread.Sleep(ClaseConstante.TiempoDeEspera);//++++
                servidor.Send(bytesMs);
                Thread.Sleep(ClaseConstante.TiempoDeEspera);
                servidor.Send(bytesNom);
            }
            
        }
        public string RecibirMensaje()
        {
            string devolucion = "";
            try
            {
                byte[] bTi = new byte[1];
                byte[] bMs = new byte[10000];
                byte[] bNo = new byte[100];
                int tTi = servidor.Receive(bTi);
                int tMs = servidor.Receive(bMs);
                int tNo = servidor.Receive(bNo);

                string tipo = Encoding.ASCII.GetString(bTi, 0, tTi);
                string msg = Encoding.ASCII.GetString(bMs, 0, tMs);
                string nombre = Encoding.ASCII.GetString(bNo, 0, tNo);

                if (tipo == "1")
                {
                    devolucion = msg;
                }
                else
                {
                    devolucion = ClaseConstante.MensajeDeArchivo;
                    listArchivos.Add(new Archivo(msg, nombre));
                }
            }
            catch (Exception error)
            {
                devolucion = "te has " + ClaseConstante.Desconectado + " del servidor";
            }
            return devolucion;
        }
    }
}
