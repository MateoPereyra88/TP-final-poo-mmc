using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Cl;

namespace TrabajoFinalKrauseCord_MateoCamilaMatias_Parte2
{
    public partial class Form1 : Form
    {

        Cliente cln;
        string rutaDeDescarga = "C:\\Users\\escuela\\Downloads\\";
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
            label1.Visible = false;
            txbNick.Visible = false;
            registrar.Visible = false;
            lbListaMensajes.Visible = true;
            tbMensaje.Visible = true;
            Enviar.Visible = true;
            adj.Visible = true;
            lbNick.Text = txbNick.Text;
            lbNick.Visible = true;
            ruta.Visible = true;
            btnEnviarArch.Visible = true;
            btnDescArc.Visible = true;

            cln = new Cliente("192.168.3.111", 1234, txbNick.Text);

            

            Thread recib = new Thread(AgregarMensaje);
            recib.Start();
            

        }

        void AgregarMensaje()
        {
            bool error = false;
            while (!error)
            {
                try
                {
                    string msg = cln.RecibirMensaje();

                    this.Invoke(new DAddItem(AddItem), msg); 

                }
                catch
                {
                    error = true;
                    MessageBox.Show("No se ha podido conectar al servidor");
                    Application.Exit();
                }
            }
        }

        private void Enviar_Click(object sender, EventArgs e)
        {
            this.Invoke(new DAddItem(AddItem), lbNick.Text + " (tu): " + tbMensaje.Text);

            cln.EnviarMensajes("1", tbMensaje.Text, "---");

            tbMensaje.Clear();



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
                cln.EnviarMensajes("2", tbMensaje.Text, nombre);

            }
        }

        private void BtnDescArc_Click(object sender, EventArgs e)
        {
            using (Stream ArchivoNuevo = new FileStream(rutaDeDescarga + cln.UltimoArchivoEnviado.Nombre, FileMode.Open, FileAccess.Read))
            {
                using (StreamWriter sw = new StreamWriter(ArchivoNuevo))
                {
                    sw.WriteLine(cln.UltimoArchivoEnviado.Ccontenido);
                    
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
