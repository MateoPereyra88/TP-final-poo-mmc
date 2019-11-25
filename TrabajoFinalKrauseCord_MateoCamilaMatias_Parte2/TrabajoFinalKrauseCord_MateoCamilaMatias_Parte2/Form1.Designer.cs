namespace TrabajoFinalKrauseCord_MateoCamilaMatias_Parte2
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.adj = new System.Windows.Forms.Button();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lbListaMensajes = new System.Windows.Forms.ListBox();
            this.Enviar = new System.Windows.Forms.Button();
            this.tbMensaje = new System.Windows.Forms.TextBox();
            this.txbNick = new System.Windows.Forms.TextBox();
            this.lbNick = new System.Windows.Forms.Label();
            this.ruta = new System.Windows.Forms.Label();
            this.btnEnviarArch = new System.Windows.Forms.Button();
            this.txbPass = new System.Windows.Forms.TextBox();
            this.lblNombreReg = new System.Windows.Forms.Label();
            this.lblContrasenia = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // adj
            // 
            this.adj.Location = new System.Drawing.Point(205, 318);
            this.adj.Name = "adj";
            this.adj.Size = new System.Drawing.Size(56, 23);
            this.adj.TabIndex = 14;
            this.adj.Text = "Adjuntar";
            this.adj.UseVisualStyleBackColor = true;
            this.adj.Visible = false;
            this.adj.Click += new System.EventHandler(this.adj_Click);
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Location = new System.Drawing.Point(397, 185);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(75, 23);
            this.btnRegistrar.TabIndex = 13;
            this.btnRegistrar.Text = "Registrarse";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            this.btnRegistrar.Click += new System.EventHandler(this.registrar_Click);
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(274, 83);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(43, 13);
            this.lblUsuario.TabIndex = 11;
            this.lblUsuario.Text = "Usuario";
            // 
            // lbListaMensajes
            // 
            this.lbListaMensajes.FormattingEnabled = true;
            this.lbListaMensajes.Location = new System.Drawing.Point(205, 22);
            this.lbListaMensajes.Name = "lbListaMensajes";
            this.lbListaMensajes.Size = new System.Drawing.Size(327, 212);
            this.lbListaMensajes.TabIndex = 10;
            this.lbListaMensajes.Visible = false;
            this.lbListaMensajes.Click += new System.EventHandler(this.LbListaMensajes_Click);
            // 
            // Enviar
            // 
            this.Enviar.Location = new System.Drawing.Point(473, 247);
            this.Enviar.Name = "Enviar";
            this.Enviar.Size = new System.Drawing.Size(59, 23);
            this.Enviar.TabIndex = 8;
            this.Enviar.Text = "Enviar";
            this.Enviar.UseVisualStyleBackColor = true;
            this.Enviar.Visible = false;
            this.Enviar.Click += new System.EventHandler(this.Enviar_Click);
            // 
            // tbMensaje
            // 
            this.tbMensaje.AllowDrop = true;
            this.tbMensaje.Location = new System.Drawing.Point(205, 247);
            this.tbMensaje.Name = "tbMensaje";
            this.tbMensaje.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbMensaje.Size = new System.Drawing.Size(242, 20);
            this.tbMensaje.TabIndex = 15;
            this.tbMensaje.Visible = false;
            this.tbMensaje.DragDrop += new System.Windows.Forms.DragEventHandler(this.tbMensaje_DragDrop);
            this.tbMensaje.DragEnter += new System.Windows.Forms.DragEventHandler(this.tbMensaje_DragEnter);
            // 
            // txbNick
            // 
            this.txbNick.Location = new System.Drawing.Point(350, 119);
            this.txbNick.Name = "txbNick";
            this.txbNick.Size = new System.Drawing.Size(122, 20);
            this.txbNick.TabIndex = 16;
            // 
            // lbNick
            // 
            this.lbNick.AutoSize = true;
            this.lbNick.Location = new System.Drawing.Point(202, 6);
            this.lbNick.Name = "lbNick";
            this.lbNick.Size = new System.Drawing.Size(27, 13);
            this.lbNick.TabIndex = 17;
            this.lbNick.Text = "nick";
            this.lbNick.Visible = false;
            // 
            // ruta
            // 
            this.ruta.AutoSize = true;
            this.ruta.Location = new System.Drawing.Point(202, 288);
            this.ruta.Name = "ruta";
            this.ruta.Size = new System.Drawing.Size(86, 13);
            this.ruta.TabIndex = 18;
            this.ruta.Text = "(ruta o direccion)";
            this.ruta.Visible = false;
            // 
            // btnEnviarArch
            // 
            this.btnEnviarArch.Location = new System.Drawing.Point(288, 318);
            this.btnEnviarArch.Name = "btnEnviarArch";
            this.btnEnviarArch.Size = new System.Drawing.Size(103, 23);
            this.btnEnviarArch.TabIndex = 19;
            this.btnEnviarArch.Text = "Enviar archivo";
            this.btnEnviarArch.UseVisualStyleBackColor = true;
            this.btnEnviarArch.Visible = false;
            this.btnEnviarArch.Click += new System.EventHandler(this.BtnEnviarArch_Click);
            // 
            // txbPass
            // 
            this.txbPass.Location = new System.Drawing.Point(350, 145);
            this.txbPass.Name = "txbPass";
            this.txbPass.Size = new System.Drawing.Size(122, 20);
            this.txbPass.TabIndex = 20;
            // 
            // lblNombreReg
            // 
            this.lblNombreReg.AutoSize = true;
            this.lblNombreReg.Location = new System.Drawing.Point(273, 119);
            this.lblNombreReg.Name = "lblNombreReg";
            this.lblNombreReg.Size = new System.Drawing.Size(47, 13);
            this.lblNombreReg.TabIndex = 21;
            this.lblNombreReg.Text = "Nombre:";
            // 
            // lblContrasenia
            // 
            this.lblContrasenia.AutoSize = true;
            this.lblContrasenia.Location = new System.Drawing.Point(274, 148);
            this.lblContrasenia.Name = "lblContrasenia";
            this.lblContrasenia.Size = new System.Drawing.Size(64, 13);
            this.lblContrasenia.TabIndex = 22;
            this.lblContrasenia.Text = "Contraseña:";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(249, 185);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(102, 23);
            this.btnLogin.TabIndex = 23;
            this.btnLogin.Text = "Iniciar sesión";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.lblContrasenia);
            this.Controls.Add(this.lblNombreReg);
            this.Controls.Add(this.txbPass);
            this.Controls.Add(this.btnEnviarArch);
            this.Controls.Add(this.ruta);
            this.Controls.Add(this.lbNick);
            this.Controls.Add(this.txbNick);
            this.Controls.Add(this.tbMensaje);
            this.Controls.Add(this.adj);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.lbListaMensajes);
            this.Controls.Add(this.Enviar);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button adj;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.ListBox lbListaMensajes;
        private System.Windows.Forms.Button Enviar;
        private System.Windows.Forms.TextBox tbMensaje;
        private System.Windows.Forms.TextBox txbNick;
        private System.Windows.Forms.Label lbNick;
        private System.Windows.Forms.Label ruta;
        private System.Windows.Forms.Button btnEnviarArch;
        private System.Windows.Forms.TextBox txbPass;
        private System.Windows.Forms.Label lblNombreReg;
        private System.Windows.Forms.Label lblContrasenia;
        private System.Windows.Forms.Button btnLogin;
    }
}

