using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using MySql.Data.MySqlClient;
using System.Data;
using ValoresConstantes;

namespace Ser
{
    public class Usuario
    {
        private Socket conexionSer;
        private string nick;
        private string pass;
        public Usuario(Socket conexionSer, string nick, string pass)
        {
            this.conexionSer = conexionSer;
            this.nick = nick;
            this.pass = pass;
        }
        public Socket Conexion() => conexionSer;
        public string Nick() => nick;

        public void RegistrarEn(MySqlConnection baseDeDatos)
        {
            MySqlCommand comando = new MySqlCommand(String.Format("insert into Usuario(nombre, pass) values ('{0}', '{1}')", nick, pass), baseDeDatos);
            int result = comando.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("el registro de " + nick + " fue exitoso");
            }
            else
            {
                Console.WriteLine("hubo unproblema con el registro de " + nick);
            }
        }

        public int IniciarSesion(MySqlConnection baseDeDatos)
        {
            string comando = String.Format("select * from Usuario where nombre = '{0}' and pass = '{1}' ", nick, pass);
            MySqlDataAdapter consul = new MySqlDataAdapter(comando, baseDeDatos);
            DataTable resul = new DataTable();
            consul.Fill(resul);

            return resul.Rows.Count;
        }


        public void CargarMensajes(MySqlConnection baseDeDatos)
        {
            string cmdMensajes = "select * from Mensaje";
            MySqlDataAdapter consMensaje = new MySqlDataAdapter(cmdMensajes, baseDeDatos);
            DataTable rslMensaje = new DataTable();
            consMensaje.Fill(rslMensaje);

            foreach (DataRow r in rslMensaje.Rows)
            {
                string msg;
                if ((string)r[0] == nick)
                    msg = ((string)r[0]) + " (tu): " + ((string)r[1]);
                else
                    msg = ((string)r[0]) + " : " + ((string)r[1]);

                conexionSer.Send(Encoding.ASCII.GetBytes("1"));
                Thread.Sleep(ClaseConstante.TiempoDeEspera);
                conexionSer.Send(Encoding.ASCII.GetBytes(msg));
                Thread.Sleep(ClaseConstante.TiempoDeEspera);
                conexionSer.Send(Encoding.ASCII.GetBytes("---"));
            }

        }
    }
}
