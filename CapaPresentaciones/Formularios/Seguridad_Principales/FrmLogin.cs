using CapaEntidades;
using CapaNegocio;
using SIS_Heladeria.CapaPresentacion.Formularios.Seguridad_Principales;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentaciones.Formularios.Seguridad_Principales
{
    public partial class FrmLogin : Form
    {
        private readonly UsuariosBL _usuariosBL;

        public FrmLogin()
        {
            InitializeComponent();
            _usuariosBL = new UsuariosBL();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(400, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Inicio de Sesión - Sistema de Heladería";
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;
            this.ResumeLayout(false);

            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            // Panel principal
            Panel panelPrincipal = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(20)
            };
            this.Controls.Add(panelPrincipal);

            // Título
            Label lblTitulo = new Label
            {
                Text = "Bienvenido",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 128, 185),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 60
            };
            panelPrincipal.Controls.Add(lblTitulo);

            // Subtítulo
            Label lblSubtitulo = new Label
            {
                Text = "Sistema de Heladería",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.Gray,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 30
            };
            panelPrincipal.Controls.Add(lblSubtitulo);

            // Panel de contenido
            Panel panelContenido = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };
            panelPrincipal.Controls.Add(panelContenido);

            // Label para Email
            Label lblEmail = new Label
            {
                Text = "Correo Electrónico:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 128, 185),
                Location = new Point(50, 50),
                AutoSize = true
            };
            panelContenido.Controls.Add(lblEmail);

            // TextBox para Email
            TextBox txtEmail = new TextBox
            {
                Font = new Font("Segoe UI", 12),
                Size = new Size(300, 30),
                Location = new Point(50, 80),
                BorderStyle = BorderStyle.FixedSingle
            };
            panelContenido.Controls.Add(txtEmail);

            // Label para Contraseña
            Label lblClave = new Label
            {
                Text = "Contraseña:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 128, 185),
                Location = new Point(50, 130),
                AutoSize = true
            };
            panelContenido.Controls.Add(lblClave);

            // TextBox para Contraseña
            TextBox txtClave = new TextBox
            {
                Font = new Font("Segoe UI", 12),
                Size = new Size(300, 30),
                Location = new Point(50, 160),
                BorderStyle = BorderStyle.FixedSingle,
                UseSystemPasswordChar = true
            };
            panelContenido.Controls.Add(txtClave);

            // Botón de inicio de sesión
            Button btnIngresar = new Button
            {
                Text = "Iniciar Sesión",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(300, 40),
                Location = new Point(50, 220),
                BackColor = Color.FromArgb(41, 128, 185),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnIngresar.FlatAppearance.BorderSize = 0;
            btnIngresar.Click += (sender, e) =>
            {
                try
                {
                    if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtClave.Text))
                    {
                        MessageBox.Show("Por favor ingrese su correo y contraseña", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    Usuarios usuario = _usuariosBL.Autenticar(txtEmail.Text, txtClave.Text);

                    if (usuario != null)
                    {
                        UsuarioActual.EstablecerUsuario(usuario);

                        Form frmAdministradores = new FrmAdministradores();
                        this.Hide();
                        frmAdministradores.FormClosed += (s, args) => this.Close();
                        frmAdministradores.Show();
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrectos", "Error de autenticación",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al iniciar sesión: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            panelContenido.Controls.Add(btnIngresar);

            // Botón de cierre
            Button btnCerrar = new Button
            {
                Text = "X",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(40, 40),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(this.Width - 50, 10)
            };
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.Click += (sender, e) => this.Close();
            panelPrincipal.Controls.Add(btnCerrar);
        }
    }
}