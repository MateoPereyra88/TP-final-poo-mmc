using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Cl;
using ValoresConstantes;
using Ser;
using System.Data;


namespace TrabajoFinalKrauseCord_MateoCamilaMatias_Parte2
{
    public partial class Form1 : Form
    {

        Cliente cln;
        string rutaDeDescarga = "Descargas";
        //hacer una propiedad para el puerto y la ip en modo de IPAddress

        private delegate void DAddItem(String s);
        public Form1()
        {
            InitializeComponent();
        }
        private void AddItem(String s)
        {
            lbListaMensajes.Items.Add(s);
        }
        private void registrar_Click(object sender, EventArgs e)
        {

            VisibilizarMensajes();
            
            
            cln = new Cliente("192.168.3.112", 1234, txbNick.Text, txbPass.Text, "sign");

            this.Invoke(new DAddItem(AddItem), "Te has conectado, cargando mensajes (si es que hay)...");

            Thread recib = new Thread(AgregarMensaje);
            recib.Start();
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            VisibilizarMensajes();


            cln = new Cliente("192.168.3.112", 1234, txbNick.Text, txbPass.Text, "log");

            string confirmacion = cln.RecibirMensaje();
            if (confirmacion != "usuario inexistente o contrasea incorrecta")
            {
                this.Invoke(new DAddItem(AddItem), "Te has conectado, cargando mensajes...");



                Thread recib = new Thread(AgregarMensaje);
                recib.Start();
            }
            else
            {
                this.Invoke(new DAddItem(AddItem), confirmacion);
            }
               

        }

        private void VisibilizarMensajes()
        {
            if (txbNick.Text == "")
                txbNick.Text = "desconocido";
            lblUsuario.Visible = false;
            txbNick.Visible = false;
            txbPass.Visible = false;
            lblNombreReg.Visible = false;
            lblContrasenia.Visible = false;

            btnRegistrar.Visible = false;
            btnLogin.Visible = false;

            lbListaMensajes.Visible = true;
            tbMensaje.Visible = true;
            Enviar.Visible = true;
            adj.Visible = true;
            lbNick.Text = txbNick.Text;
            lbNick.Visible = true;
            ruta.Visible = true;
            btnEnviarArch.Visible = true;
        }

        private void AgregarMensaje()
        {
            bool desconectado = false;
            while (!desconectado)
            {
                string msg = cln.RecibirMensaje();
                this.Invoke(new DAddItem(AddItem), msg);
                if (msg.Contains(ClaseConstante.Desconectado))
                    desconectado = true;
            }

            
        }
        private void Enviar_Click(object sender, EventArgs e)
        {
            if((tbMensaje.Text != null) && (tbMensaje.Text != "") && (tbMensaje.Text != string.Empty))
            {
                this.Invoke(new DAddItem(AddItem), lbNick.Text + " (tu): " + tbMensaje.Text);
                cln.EnviarMensajes("1", tbMensaje.Text, "---");
                tbMensaje.Clear();
            }
        }
        private void adj_Click(object sender, EventArgs e)
        {
            string rutaArchivo = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                rutaArchivo = openFileDialog.FileName;
            }
            ruta.Text = rutaArchivo;
        }




        /* mejorar traspaso para poder pasar otros tipos de archivos, aparte de los de textos*/
        private void BtnEnviarArch_Click(object sender, EventArgs e)
        {
            if (ruta.Text != "" && ruta.Text != null)
            {
                string contenido = "";
                string nombre = "";
                using (Stream ArchivoAdj = new FileStream(ruta.Text, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(ArchivoAdj))
                    {
                        contenido = sr.ReadToEnd();

                        int i = ruta.Text.Length;
                        while ((i >= 0) && (ruta.Text[i - 1] != '/') && (ruta.Text[i - 1] != '\\'))
                        {
                            nombre = ruta.Text[i - 1] + nombre;
                            i--;
                        }


                    }
                }

                this.Invoke(new DAddItem(AddItem), "has enviado un archivo");
                cln.EnviarMensajes("2", contenido, nombre);

                ruta.Text = "";

            }
        }
        private void LbListaMensajes_Click(object sender, EventArgs e)
        {
            ListBox lbox = (ListBox)sender;
            if (lbox.SelectedItem != null)
            {
                if ((string)lbListaMensajes.SelectedItem == ClaseConstante.MensajeDeArchivo)
                {
                    int mensajeSeleccionado = lbox.SelectedIndex + 1;
                    int indArchivo = -1;

                    for (int i = 0; i < mensajeSeleccionado; i++)
                    {
                        lbox.SelectedIndex = i;
                        if ((string)lbox.SelectedItem == ClaseConstante.MensajeDeArchivo)
                        {
                            indArchivo++;
                        }

                    }

                    using (Stream ArchivoNuevo = new FileStream(rutaDeDescarga + cln.ListaDeArchivos[indArchivo].Nombre, FileMode.Create, FileAccess.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(ArchivoNuevo))
                        {
                            sw.WriteLine(cln.ListaDeArchivos[indArchivo].Ccontenido);

                        }
                    }
                }
            }
        }




        private void tbMensaje_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
        private void tbMensaje_DragDrop(object sender, DragEventArgs e)
        {
            ruta.Text = ((string[])e.Data.GetData(DataFormats.FileDrop, false))[0];
        }

        
    }
}
