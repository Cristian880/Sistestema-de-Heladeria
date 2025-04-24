using CapaEntidades;
using SIS_Heladeria.CapaPresentacion.Formularios.Seguridad_Principales;
using System;
using CapaDatos;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentaciones.Formularios.Seguridad_Principales
{
    public partial class NuevoUsuario : Form
    {
        private Panel panelPrincipal;
        private Panel panelLateral;
        private Label lblTitulo;

        // Campos del formulario
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblApellido;
        private TextBox txtApellido;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblClave;
        private TextBox txtClave;
        private Label lblConfirmarClave;
        private TextBox txtConfirmarClave;
        private Label lblFechaNacimiento;
        private DateTimePicker dtpFechaNacimiento;

        // Botones
        private Button btnGuardar;
        private Button btnCancelar;
        private Button btnVolver;

        public NuevoUsuario()
        {
            ConfigurarFormulario();
            ConfigurarEventos();
        }

        private void ConfigurarFormulario()
        {
            // Configuración básica del formulario
            this.Text = "Crear Usuario Empleado";
            this.Size = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Panel principal
            panelPrincipal = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            this.Controls.Add(panelPrincipal);

            // Panel lateral
            panelLateral = new Panel
            {
                Dock = DockStyle.Left,
                Width = 300,
                BackColor = Color.FromArgb(41, 128, 185)
            };
            panelPrincipal.Controls.Add(panelLateral);

            // Título en el panel lateral
            Label lblSistema = new Label
            {
                Text = "SIS Heladería",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(75, 50)
            };
            panelLateral.Controls.Add(lblSistema);

            // Subtítulo en el panel lateral
            Label lblSubtitulo = new Label
            {
                Text = "Creación de Empleados",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12),
                AutoSize = true,
                Location = new Point(65, 90)
            };
            panelLateral.Controls.Add(lblSubtitulo);

            // Información en el panel lateral
            Label lblInfo = new Label
            {
                Text = "Complete el formulario para crear un nuevo usuario con rol de empleado. Los empleados tendrán acceso limitado al sistema según sus permisos.",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9),
                Size = new Size(250, 100),
                Location = new Point(25, 150)
            };
            panelLateral.Controls.Add(lblInfo);

            // Botón para volver
            btnVolver = new Button
            {
                Text = "← Volver",
                Font = new Font("Segoe UI", 9),
                Size = new Size(100, 30),
                Location = new Point(25, 400),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnVolver.FlatAppearance.BorderSize = 0;
            panelLateral.Controls.Add(btnVolver);

            // Título del formulario
            lblTitulo = new Label
            {
                Text = "Nuevo Usuario Empleado",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(350, 30)
            };
            panelPrincipal.Controls.Add(lblTitulo);

            // Campos del formulario
            int posY = 80;
            int separacion = 50;

            // Nombre
            lblNombre = new Label
            {
                Text = "Nombre:",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(350, posY)
            };
            panelPrincipal.Controls.Add(lblNombre);

            txtNombre = new TextBox
            {
                Font = new Font("Segoe UI", 10),
                Size = new Size(250, 27),
                Location = new Point(350, posY + 25)
            };
            panelPrincipal.Controls.Add(txtNombre);

            // Apellido
            posY += separacion;
            lblApellido = new Label
            {
                Text = "Apellido:",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(350, posY)
            };
            panelPrincipal.Controls.Add(lblApellido);

            txtApellido = new TextBox
            {
                Font = new Font("Segoe UI", 10),
                Size = new Size(250, 27),
                Location = new Point(350, posY + 25)
            };
            panelPrincipal.Controls.Add(txtApellido);

            // Email
            posY += separacion;
            lblEmail = new Label
            {
                Text = "Email:",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(350, posY)
            };
            panelPrincipal.Controls.Add(lblEmail);

            txtEmail = new TextBox
            {
                Font = new Font("Segoe UI", 10),
                Size = new Size(250, 27),
                Location = new Point(350, posY + 25)
            };
            panelPrincipal.Controls.Add(txtEmail);

            // Contraseña
            posY += separacion;
            lblClave = new Label
            {
                Text = "Contraseña:",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(350, posY)
            };
            panelPrincipal.Controls.Add(lblClave);

            txtClave = new TextBox
            {
                Font = new Font("Segoe UI", 10),
                Size = new Size(250, 27),
                Location = new Point(350, posY + 25),
                UseSystemPasswordChar = true
            };
            panelPrincipal.Controls.Add(txtClave);

            // Confirmar Contraseña
            posY += separacion;
            lblConfirmarClave = new Label
            {
                Text = "Confirmar Contraseña:",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(350, posY)
            };
            panelPrincipal.Controls.Add(lblConfirmarClave);

            txtConfirmarClave = new TextBox
            {
                Font = new Font("Segoe UI", 10),
                Size = new Size(250, 27),
                Location = new Point(350, posY + 25),
                UseSystemPasswordChar = true
            };
            panelPrincipal.Controls.Add(txtConfirmarClave);

            // Fecha de Nacimiento
            posY += separacion;
            lblFechaNacimiento = new Label
            {
                Text = "Fecha de Nacimiento:",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(350, posY)
            };
            panelPrincipal.Controls.Add(lblFechaNacimiento);

            dtpFechaNacimiento = new DateTimePicker
            {
                Font = new Font("Segoe UI", 10),
                Size = new Size(250, 27),
                Location = new Point(350, posY + 25),
                Format = DateTimePickerFormat.Short
            };
            panelPrincipal.Controls.Add(dtpFechaNacimiento);

            // Botones
            btnGuardar = new Button
            {
                Text = "Guardar",
                Font = new Font("Segoe UI", 10),
                Size = new Size(120, 35),
                Location = new Point(350, 400),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnGuardar.FlatAppearance.BorderSize = 0;
            panelPrincipal.Controls.Add(btnGuardar);

            btnCancelar = new Button
            {
                Text = "Cancelar",
                Font = new Font("Segoe UI", 10),
                Size = new Size(120, 35),
                Location = new Point(480, 400),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCancelar.FlatAppearance.BorderSize = 0;
            panelPrincipal.Controls.Add(btnCancelar);
        }

        private void ConfigurarEventos()
        {
            btnGuardar.Click += new EventHandler(BtnGuardar_Click);
            btnCancelar.Click += new EventHandler(BtnCancelar_Click);
            btnVolver.Click += new EventHandler(BtnVolver_Click);

            // Validación de email
            txtEmail.TextChanged += (s, e) =>
            {
                if (!string.IsNullOrEmpty(txtEmail.Text) && !txtEmail.Text.Contains("@"))
                {
                    txtEmail.BackColor = Color.MistyRose;
                }
                else
                {
                    txtEmail.BackColor = SystemColors.Window;
                }
            };

            // Validación de contraseñas
            txtConfirmarClave.TextChanged += (s, e) =>
            {
                if (!string.IsNullOrEmpty(txtConfirmarClave.Text) && txtConfirmarClave.Text != txtClave.Text)
                {
                    txtConfirmarClave.BackColor = Color.MistyRose;
                }
                else
                {
                    txtConfirmarClave.BackColor = SystemColors.Window;
                }
            };
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar campos
                if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApellido.Text) ||
                    string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtClave.Text))
                {
                    MessageBox.Show("Por favor complete todos los campos obligatorios", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!txtEmail.Text.Contains("@"))
                {
                    MessageBox.Show("Por favor ingrese un email válido", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return;
                }

                if (txtClave.Text != txtConfirmarClave.Text)
                {
                    MessageBox.Show("Las contraseñas no coinciden", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtConfirmarClave.Focus();
                    return;
                }

                // Crear el objeto Usuario
                Usuarios nuevoUsuario = new Usuarios
                {
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    GmailUsuario = txtEmail.Text,
                    Clave = txtClave.Text, 
                    Rol = "Empleado",
                    FechaNacimiento = dtpFechaNacimiento.Value,
                    Estado = true
                };

                if (!CapaDatos.ConexionDA.ProbarConexion())
                {
                    MessageBox.Show("No se pudo conectar a la base de datos. Verifique su conexión.", "Error de conexión",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                using (SqlConnection conexion = new SqlConnection(CapaDatos.ConexionDA.CadenaConexion))
                {
                    conexion.Open();

                    using (SqlCommand cmd = new SqlCommand("AgregarUsuario", conexion))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", nuevoUsuario.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", nuevoUsuario.Apellido);
                        cmd.Parameters.AddWithValue("@GmailUsuario", nuevoUsuario.GmailUsuario);
                        cmd.Parameters.AddWithValue("@Clave", nuevoUsuario.Clave);
                        cmd.Parameters.AddWithValue("@Rol", nuevoUsuario.Rol);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", nuevoUsuario.FechaNacimiento);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Usuario empleado creado exitosamente", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar campos
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear el usuario: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void BtnVolver_Click(object sender, EventArgs e)
        {
            // Volver al formulario de administradores
            FrmAdministradores frmAdmin = new FrmAdministradores();
            this.Close ();
            frmAdmin.FormClosed += (s, args) => this.Close();
            frmAdmin.Show();
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtEmail.Clear();
            txtClave.Clear();
            txtConfirmarClave.Clear();
            dtpFechaNacimiento.Value = DateTime.Now;
            txtNombre.Focus();
        }
    }
}
