// CapaPresentacion/Formularios/Seguridad_Principales/FrmUsuarios.cs
using CapaEntidades;
using CapaNegocio;
using CapaPresentaciones.Formularios;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SIS_Heladeria.CapaPresentacion.Formularios.Seguridad_Principales
{
    public partial class FrmUsuarios : Form
    {
        private readonly UsuariosBL _usuariosBL;
        private bool _esEdicion = false;
        private int _idUsuarioSeleccionado = 0;

        public FrmUsuarios()
        {
            InitializeComponent();
            _usuariosBL = new UsuariosBL();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de Usuarios - Sistema de Heladería";
            this.WindowState = FormWindowState.Maximized;
            this.ResumeLayout(false);

            ConfigurarFormulario();
            CargarUsuarios();
        }

        private void ConfigurarFormulario()
        {
            // Panel principal
            Panel panelPrincipal = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            this.Controls.Add(panelPrincipal);

            // Panel superior
            Panel panelSuperior = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(41, 128, 185)
            };
            panelPrincipal.Controls.Add(panelSuperior);

            // Título
            Label lblTitulo = new Label
            {
                Text = "GESTIÓN DE USUARIOS",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            panelSuperior.Controls.Add(lblTitulo);

            // Panel de navegación (botón volver)
            Panel panelNavegacion = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            panelPrincipal.Controls.Add(panelNavegacion);

            // Botón volver
            Button btnVolver = new Button
            {
                Text = "← Volver",
                Font = new Font("Segoe UI", 9),
                Size = new Size(100, 30),
                Location = new Point(10, 5),
                BackColor = Color.FromArgb(41, 128, 185),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.Tag = "Administradores";
            btnVolver.Click += (sender, e) => {
                Form formulario = FormularioFactory.CrearFormulario(btnVolver.Tag.ToString());
                this.Hide();
                formulario.FormClosed += (s, args) => this.Close();
                formulario.Show();
            };
            panelNavegacion.Controls.Add(btnVolver);

            // Panel izquierdo (formulario)
            Panel panelFormulario = new Panel
            {
                Dock = DockStyle.Left,
                Width = 400,
                Padding = new Padding(0, 20, 20, 0)
            };
            panelPrincipal.Controls.Add(panelFormulario);

            // Título del formulario
            Label lblTituloFormulario = new Label
            {
                Text = "Datos del Usuario",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 128, 185),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Top,
                Height = 30
            };
            panelFormulario.Controls.Add(lblTituloFormulario);

            // ID (oculto)
            TextBox txtID = new TextBox
            {
                Name = "txtID",
                Visible = false
            };
            panelFormulario.Controls.Add(txtID);

            // Campos del formulario
            int fieldY = 50;
            int fieldHeight = 60;

            // Nombre
            Label lblNombre = new Label
            {
                Text = "Nombre:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(0, fieldY),
                AutoSize = true
            };
            panelFormulario.Controls.Add(lblNombre);

            TextBox txtNombre = new TextBox
            {
                Name = "txtNombre",
                Font = new Font("Segoe UI", 10),
                Location = new Point(0, fieldY + 25),
                Size = new Size(180, 30),
                BorderStyle = BorderStyle.FixedSingle
            };
            panelFormulario.Controls.Add(txtNombre);

            // Apellido
            Label lblApellido = new Label
            {
                Text = "Apellido:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(200, fieldY),
                AutoSize = true
            };
            panelFormulario.Controls.Add(lblApellido);

            TextBox txtApellido = new TextBox
            {
                Name = "txtApellido",
                Font = new Font("Segoe UI", 10),
                Location = new Point(200, fieldY + 25),
                Size = new Size(180, 30),
                BorderStyle = BorderStyle.FixedSingle
            };
            panelFormulario.Controls.Add(txtApellido);

            fieldY += fieldHeight;

            // Email
            Label lblEmail = new Label
            {
                Text = "Email:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(0, fieldY),
                AutoSize = true
            };
            panelFormulario.Controls.Add(lblEmail);

            TextBox txtEmail = new TextBox
            {
                Name = "txtEmail",
                Font = new Font("Segoe UI", 10),
                Location = new Point(0, fieldY + 25),
                Size = new Size(380, 30),
                BorderStyle = BorderStyle.FixedSingle
            };
            panelFormulario.Controls.Add(txtEmail);

            fieldY += fieldHeight;

            // Clave
            Label lblClave = new Label
            {
                Text = "Clave:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(0, fieldY),
                AutoSize = true
            };
            panelFormulario.Controls.Add(lblClave);

            TextBox txtClave = new TextBox
            {
                Name = "txtClave",
                Font = new Font("Segoe UI", 10),
                Location = new Point(0, fieldY + 25),
                Size = new Size(380, 30),
                BorderStyle = BorderStyle.FixedSingle,
                PasswordChar = '•'
            };
            panelFormulario.Controls.Add(txtClave);

            fieldY += fieldHeight;

            // Rol
            Label lblRol = new Label
            {
                Text = "Rol:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(0, fieldY),
                AutoSize = true
            };
            panelFormulario.Controls.Add(lblRol);

            ComboBox cmbRol = new ComboBox
            {
                Name = "cmbRol",
                Font = new Font("Segoe UI", 10),
                Location = new Point(0, fieldY + 25),
                Size = new Size(180, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbRol.Items.AddRange(new object[] { "Administrador", "Vendedor" });
            cmbRol.SelectedIndex = 1;
            panelFormulario.Controls.Add(cmbRol);

            // Fecha de Nacimiento
            Label lblFechaNacimiento = new Label
            {
                Text = "Fecha de Nacimiento:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(200, fieldY),
                AutoSize = true
            };
            panelFormulario.Controls.Add(lblFechaNacimiento);

            DateTimePicker dtpFechaNacimiento = new DateTimePicker
            {
                Name = "dtpFechaNacimiento",
                Font = new Font("Segoe UI", 10),
                Location = new Point(200, fieldY + 25),
                Size = new Size(180, 30),
                Format = DateTimePickerFormat.Short
            };
            panelFormulario.Controls.Add(dtpFechaNacimiento);

            fieldY += fieldHeight;

            // Estado
            CheckBox chkEstado = new CheckBox
            {
                Name = "chkEstado",
                Text = "Activo",
                Font = new Font("Segoe UI", 10),
                Location = new Point(0, fieldY),
                AutoSize = true,
                Checked = true
            };
            panelFormulario.Controls.Add(chkEstado);

            fieldY += 50;

            // Botones
            Button btnGuardar = new Button
            {
                Name = "btnGuardar",
                Text = "Guardar",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(180, 40),
                Location = new Point(0, fieldY),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.Click += BtnGuardar_Click;
            panelFormulario.Controls.Add(btnGuardar);

            Button btnCancelar = new Button
            {
                Name = "btnCancelar",
                Text = "Cancelar",
                Font = new Font("Segoe UI", 10),
                Size = new Size(180, 40),
                Location = new Point(200, fieldY),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Click += (sender, e) => {
                LimpiarFormulario();
            };
            panelFormulario.Controls.Add(btnCancelar);

            // Panel de listado
            Panel panelListado = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20, 20, 0, 0)
            };
            panelPrincipal.Controls.Add(panelListado);

            // DataGridView para mostrar usuarios
            DataGridView dgvUsuarios = new DataGridView
            {
                Name = "dgvUsuarios",
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            panelListado.Controls.Add(dgvUsuarios);

            // Configurar columnas del DataGridView
            dgvUsuarios.Columns.Add("IdUsuario", "ID");
            dgvUsuarios.Columns.Add("Nombre", "Nombre");
            dgvUsuarios.Columns.Add("Apellido", "Apellido");
            dgvUsuarios.Columns.Add("Email", "Email");
            dgvUsuarios.Columns.Add("Rol", "Rol");
            dgvUsuarios.Columns.Add("Estado", "Estado");

            // Columna de acciones
            DataGridViewButtonColumn colEditar = new DataGridViewButtonColumn
            {
                HeaderText = "Editar",
                Text = "Editar",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat
            };
            dgvUsuarios.Columns.Add(colEditar);

            DataGridViewButtonColumn colEliminar = new DataGridViewButtonColumn
            {
                HeaderText = "Eliminar",
                Text = "Eliminar",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat
            };
            dgvUsuarios.Columns.Add(colEliminar);

            // Estilo de las columnas
            dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvUsuarios.ColumnHeadersHeight = 40;
            dgvUsuarios.RowTemplate.Height = 35;

            // Manejar eventos de clic en las celdas
            dgvUsuarios.CellClick += (sender, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == dgvUsuarios.Columns["colEditar"].Index)
                    {
                        // Código para editar
                        DataGridViewRow row = dgvUsuarios.Rows[e.RowIndex];
                        CargarUsuarioParaEdicion(Convert.ToInt32(row.Cells["IdUsuario"].Value));
                    }
                    else if (e.ColumnIndex == dgvUsuarios.Columns["colEliminar"].Index)
                    {
                        // Código para eliminar
                        DataGridViewRow row = dgvUsuarios.Rows[e.RowIndex];
                        int idUsuario = Convert.ToInt32(row.Cells["IdUsuario"].Value);
                        string nombre = row.Cells["Nombre"].Value.ToString() + " " + row.Cells["Apellido"].Value.ToString();

                        DialogResult result = MessageBox.Show(
                            $"¿Está seguro que desea eliminar al usuario {nombre}?",
                            "Confirmar eliminación",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question
                        );

                        if (result == DialogResult.Yes)
                        {
                            try
                            {
                                _usuariosBL.Eliminar(idUsuario);
                                MessageBox.Show(
                                    "Usuario eliminado correctamente.",
                                    "Eliminación exitosa",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information
                                );
                                CargarUsuarios();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(
                                    "Error al eliminar usuario: " + ex.Message,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error
                                );
                            }
                        }
                    }
                }
            };
        }

        private void CargarUsuarios()
        {
            try
            {
                DataGridView dgvUsuarios = (DataGridView)Controls.Find("dgvUsuarios", true)[0];
                dgvUsuarios.Rows.Clear();

                foreach (var usuario in _usuariosBL.ObtenerTodos())
                {
                    dgvUsuarios.Rows.Add(
                        usuario.IdUsuario,
                        usuario.Nombre,
                        usuario.Apellido,
                        usuario.GmailUsuario,
                        usuario.Rol,
                        usuario.Estado
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuscarUsuarios(string textoBusqueda)
        {
            try
            {
                DataGridView dgvUsuarios = (DataGridView)Controls.Find("dgvUsuarios", true)[0];
                dgvUsuarios.Rows.Clear();

                foreach (var usuario in _usuariosBL.Buscar(textoBusqueda))
                {
                    dgvUsuarios.Rows.Add(
                        usuario.IdUsuario,
                        usuario.Nombre,
                        usuario.Apellido,
                        usuario.GmailUsuario,
                        usuario.Rol,
                        usuario.Estado
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar usuarios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarUsuarioParaEdicion(int idUsuario)
        {
            try
            {
                var usuario = _usuariosBL.ObtenerPorId(idUsuario);

                if (usuario != null)
                {
                    TextBox txtID = (TextBox)Controls.Find("txtID", true)[0];
                    TextBox txtNombre = (TextBox)Controls.Find("txtNombre", true)[0];
                    TextBox txtApellido = (TextBox)Controls.Find("txtApellido", true)[0];
                    TextBox txtEmail = (TextBox)Controls.Find("txtEmail", true)[0];
                    TextBox txtClave = (TextBox)Controls.Find("txtClave", true)[0];
                    ComboBox cmbRol = (ComboBox)Controls.Find("cmbRol", true)[0];
                    DateTimePicker dtpFechaNacimiento = (DateTimePicker)Controls.Find("dtpFechaNacimiento", true)[0];
                    CheckBox chkEstado = (CheckBox)Controls.Find("chkEstado", true)[0];

                    txtID.Text = usuario.IdUsuario.ToString();
                    txtNombre.Text = usuario.Nombre;
                    txtApellido.Text = usuario.Apellido;
                    txtEmail.Text = usuario.GmailUsuario;
                    txtClave.Text = usuario.Clave;
                    cmbRol.SelectedItem = usuario.Rol;
                    dtpFechaNacimiento.Value = usuario.FechaNacimiento ?? DateTime.Now;
                    chkEstado.Checked = usuario.Estado;

                    _esEdicion = true;
                    _idUsuarioSeleccionado = idUsuario;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuario para edición: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox txtID = (TextBox)Controls.Find("txtID", true)[0];
                TextBox txtNombre = (TextBox)Controls.Find("txtNombre", true)[0];
                TextBox txtApellido = (TextBox)Controls.Find("txtApellido", true)[0];
                TextBox txtEmail = (TextBox)Controls.Find("txtEmail", true)[0];
                TextBox txtClave = (TextBox)Controls.Find("txtClave", true)[0];
                ComboBox cmbRol = (ComboBox)Controls.Find("cmbRol", true)[0];
                DateTimePicker dtpFechaNacimiento = (DateTimePicker)Controls.Find("dtpFechaNacimiento", true)[0];
                CheckBox chkEstado = (CheckBox)Controls.Find("chkEstado", true)[0];

                // Validar campos
                if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApellido.Text) ||
                    string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtClave.Text))
                {
                    MessageBox.Show("Por favor complete todos los campos obligatorios", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear objeto usuario
                Usuarios usuario = new Usuarios
                {
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    GmailUsuario = txtEmail.Text,
                    Clave = txtClave.Text,
                    Rol = cmbRol.SelectedItem.ToString(),
                    FechaNacimiento = dtpFechaNacimiento.Value,
                    Estado = chkEstado.Checked
                };

                if (_esEdicion)
                {
                    // Actualizar usuario existente
                    usuario.IdUsuario = Convert.ToInt32(txtID.Text);
                    _usuariosBL.Actualizar(usuario);
                    MessageBox.Show("Usuario actualizado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Agregar nuevo usuario
                    _usuariosBL.Agregar(usuario);
                    MessageBox.Show("Usuario agregado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Limpiar formulario y recargar usuarios
                LimpiarFormulario();
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            TextBox txtID = (TextBox)Controls.Find("txtID", true)[0];
            TextBox txtNombre = (TextBox)Controls.Find("txtNombre", true)[0];
            TextBox txtApellido = (TextBox)Controls.Find("txtApellido", true)[0];
            TextBox txtEmail = (TextBox)Controls.Find("txtEmail", true)[0];
            TextBox txtClave = (TextBox)Controls.Find("txtClave", true)[0];
            ComboBox cmbRol = (ComboBox)Controls.Find("cmbRol", true)[0];
            DateTimePicker dtpFechaNacimiento = (DateTimePicker)Controls.Find("dtpFechaNacimiento", true)[0];
            CheckBox chkEstado = (CheckBox)Controls.Find("chkEstado", true)[0];

            txtID.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEmail.Text = "";
            txtClave.Text = "";
            cmbRol.SelectedIndex = 1;
            dtpFechaNacimiento.Value = DateTime.Now;
            chkEstado.Checked = true;

            _esEdicion = false;
            _idUsuarioSeleccionado = 0;
        }
    }
}