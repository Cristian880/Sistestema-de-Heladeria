// CapaPresentacion/Formularios/Seguridad_Principales/FrmAdministradores.cs
using CapaEntidades;
using CapaNegocio;
using CapaPresentaciones.Formularios;
using SIS_Heladeria.CapaPresentacion.Formularios.Inventario;
using SIS_Heladeria.CapaPresentacion.Formularios.Reportes;
using SIS_Heladeria.CapaPresentacion.Formularios.Ventas;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SIS_Heladeria.CapaPresentacion.Formularios.Seguridad_Principales
{
    public partial class FrmAdministradores : Form
    {
        private readonly AdministradoresBL _administradoresBL;
        private List<Administradores> _administradores;
        private bool _esEdicion = false;

        public FrmAdministradores()
        {
            InitializeComponent();
            _administradoresBL = new AdministradoresBL();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Panel de Administración - Sistema de Heladería";
            this.WindowState = FormWindowState.Maximized;
            this.ResumeLayout(false);

            ConfigurarFormulario();
            CargarAdministradores();
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
                Text = "PANEL DE ADMINISTRACIÓN",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            panelSuperior.Controls.Add(lblTitulo);

            // Panel de usuario
            Panel panelUsuario = new Panel
            {
                Dock = DockStyle.Right,
                Width = 200,
                Height = 60
            };
            panelSuperior.Controls.Add(panelUsuario);

            // Nombre de usuario
            Label lblUsuario = new Label
            {
                Text = UsuarioActual.Nombre + " " + UsuarioActual.Apellido,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.White,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Top,
                Height = 30,
                Padding = new Padding(0, 5, 10, 0)
            };
            panelUsuario.Controls.Add(lblUsuario);

            // Rol de usuario
            Label lblRol = new Label
            {
                Text = UsuarioActual.Rol,
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.White,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Top,
                Height = 20,
                Padding = new Padding(0, 0, 10, 0)
            };
            panelUsuario.Controls.Add(lblRol);

            // Panel de menú lateral
            Panel panelMenu = new Panel
            {
                Dock = DockStyle.Left,
                Width = 250,
                BackColor = Color.FromArgb(52, 73, 94)
            };
            panelPrincipal.Controls.Add(panelMenu);

            // Logo o nombre del sistema
            Panel panelLogo = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.FromArgb(44, 62, 80)
            };
            panelMenu.Controls.Add(panelLogo);

            // Nombre del sistema
            Label lblSistema = new Label
            {
                Text = "SISTEMA DE HELADERÍA",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            panelLogo.Controls.Add(lblSistema);

            // Botones del menú
            int buttonHeight = 50;
            int buttonMargin = 5;
            int y = 120;

            // Botón Dashboard
            Button btnDashboard = new Button
            {
                Text = "Dashboard",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(52, 73, 94),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(250, buttonHeight),
                Location = new Point(0, y),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                Image = null, // Aquí puedes agregar un ícono
                ImageAlign = ContentAlignment.MiddleLeft
            };
            btnDashboard.FlatAppearance.BorderSize = 0;
            btnDashboard.Click += (sender, e) => {
                // Mostrar dashboard
            };
            panelMenu.Controls.Add(btnDashboard);

            y += buttonHeight + buttonMargin;

            // Botón Ventas
            Button btnVentas = new Button
            {
                Text = "Ventas",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(52, 73, 94),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(250, buttonHeight),
                Location = new Point(0, y),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                Image = null, // Aquí puedes agregar un ícono
                ImageAlign = ContentAlignment.MiddleLeft
            };
            btnVentas.FlatAppearance.BorderSize = 0;
            btnVentas.Click += (sender, e) => {
                Form frmVentas = new FrmVentas();
                this.Hide();
                frmVentas.FormClosed += (s, args) => this.Close();
                frmVentas.Show();
            };
            panelMenu.Controls.Add(btnVentas);

            y += buttonHeight + buttonMargin;

            // Botón Productos
            Button btnProductos = new Button
            {
                Text = "Productos",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(52, 73, 94),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(250, buttonHeight),
                Location = new Point(0, y),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                Image = null, // Aquí puedes agregar un ícono
                ImageAlign = ContentAlignment.MiddleLeft
            };
            btnProductos.FlatAppearance.BorderSize = 0;
            btnProductos.Click += (sender, e) => {
                Form frmProductos = new FrmProductos();
                this.Hide();
                frmProductos.FormClosed += (s, args) => this.Close();
                frmProductos.Show();
            };
            panelMenu.Controls.Add(btnProductos);

            y += buttonHeight + buttonMargin;

            // Botón Clientes
            Button btnClientes = new Button
            {
                Text = "Clientes",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(52, 73, 94),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(250, buttonHeight),
                Location = new Point(0, y),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                Image = null, // Aquí puedes agregar un ícono
                ImageAlign = ContentAlignment.MiddleLeft
            };
            btnClientes.FlatAppearance.BorderSize = 0;
            btnClientes.Click += (sender, e) => {
                Form frmClientes = FormularioFactory.CrearFormulario("clientes");
                this.Hide();
                frmClientes.FormClosed += (s, args) => this.Close();
                frmClientes.Show();
            };
            panelMenu.Controls.Add(btnClientes);

            y += buttonHeight + buttonMargin;

            // Botón Reportes
            Button btnReportes = new Button
            {
                Text = "Reportes",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(52, 73, 94),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(250, buttonHeight),
                Location = new Point(0, y),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                Image = null, // Aquí puedes agregar un ícono
                ImageAlign = ContentAlignment.MiddleLeft
            };
            btnReportes.FlatAppearance.BorderSize = 0;
            btnReportes.Click += (sender, e) => {
                Form frmReportes = new FrmReportes();
                this.Hide();
                frmReportes.FormClosed += (s, args) => this.Close();
                frmReportes.Show();
            };
            panelMenu.Controls.Add(btnReportes);

            y += buttonHeight + buttonMargin;

            // Botón Usuarios
            Button btnUsuarios = new Button
            {
                Text = "Usuarios",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(52, 73, 94),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(250, buttonHeight),
                Location = new Point(0, y),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                Image = null, // Aquí puedes agregar un ícono
                ImageAlign = ContentAlignment.MiddleLeft
            };
            btnUsuarios.FlatAppearance.BorderSize = 0;
            btnUsuarios.Click += (sender, e) => {
                Form frmUsuarios = FormularioFactory.CrearFormulario("usuarios");
                this.Hide();
                frmUsuarios.FormClosed += (s, args) => this.Close();
                frmUsuarios.Show();
            };
            panelMenu.Controls.Add(btnUsuarios);

            y += buttonHeight + buttonMargin;

            // Botón Cerrar Sesión
            Button btnCerrarSesion = new Button
            {
                Text = "Cerrar Sesión",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(231, 76, 60),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(250, buttonHeight),
                Location = new Point(0, y),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                Image = null, // Aquí puedes agregar un ícono
                ImageAlign = ContentAlignment.MiddleLeft
            };
            btnCerrarSesion.FlatAppearance.BorderSize = 0;
            btnCerrarSesion.Click += (sender, e) => {
                DialogResult result = MessageBox.Show("¿Está seguro que desea cerrar sesión?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    UsuarioActual.LimpiarDatos();
                    Form frmLogin = FormularioFactory.CrearFormulario("login");
                    this.Hide();
                    frmLogin.FormClosed += (s, args) => this.Close();
                    frmLogin.Show();
                }
            };
            panelMenu.Controls.Add(btnCerrarSesion);

            // Panel de contenido
            Panel panelContenido = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };
            panelPrincipal.Controls.Add(panelContenido);

            // Título del panel de contenido
            Label lblTituloContenido = new Label
            {
                Text = "Administradores del Sistema",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 128, 185),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Top,
                Height = 40
            };
            panelContenido.Controls.Add(lblTituloContenido);

            // Panel de formulario
            Panel panelFormulario = new Panel
            {
                Dock = DockStyle.Left,
                Width = 400,
                Padding = new Padding(0, 20, 20, 0)
            };
            panelContenido.Controls.Add(panelFormulario);

            // Título del formulario
            Label lblTituloFormulario = new Label
            {
                Text = "Datos del Administrador",
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

            // Nivel de Acceso
            Label lblNivelAcceso = new Label
            {
                Text = "Nivel de Acceso:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(0, fieldY),
                AutoSize = true
            };
            panelFormulario.Controls.Add(lblNivelAcceso);

            ComboBox cmbNivelAcceso = new ComboBox
            {
                Name = "cmbNivelAcceso",
                Font = new Font("Segoe UI", 10),
                Location = new Point(0, fieldY + 25),
                Size = new Size(180, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbNivelAcceso.Items.AddRange(new object[] { "Básico", "Intermedio", "Avanzado" });
            cmbNivelAcceso.SelectedIndex = 0;
            panelFormulario.Controls.Add(cmbNivelAcceso);

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

            // Permisos
            Label lblPermisos = new Label
            {
                Text = "Permisos:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(0, fieldY),
                AutoSize = true
            };
            panelFormulario.Controls.Add(lblPermisos);

            fieldY += 25;

            // CheckBox para permisos
            CheckBox chkGestionUsuarios = new CheckBox
            {
                Name = "chkGestionUsuarios",
                Text = "Gestión de Usuarios",
                Font = new Font("Segoe UI", 10),
                Location = new Point(0, fieldY),
                AutoSize = true
            };
            panelFormulario.Controls.Add(chkGestionUsuarios);

            CheckBox chkReportes = new CheckBox
            {
                Name = "chkReportes",
                Text = "Reportes",
                Font = new Font("Segoe UI", 10),
                Location = new Point(200, fieldY),
                AutoSize = true
            };
            panelFormulario.Controls.Add(chkReportes);

            fieldY += 30;

            CheckBox chkConfiguracion = new CheckBox
            {
                Name = "chkConfiguracion",
                Text = "Configuración",
                Font = new Font("Segoe UI", 10),
                Location = new Point(0, fieldY),
                AutoSize = true
            };
            panelFormulario.Controls.Add(chkConfiguracion);

            CheckBox chkGestionProductos = new CheckBox
            {
                Name = "chkGestionProductos",
                Text = "Gestión de Productos",
                Font = new Font("Segoe UI", 10),
                Location = new Point(200, fieldY),
                AutoSize = true
            };
            panelFormulario.Controls.Add(chkGestionProductos);

            fieldY += 30;

            CheckBox chkGestionVentas = new CheckBox
            {
                Name = "chkGestionVentas",
                Text = "Gestión de Ventas",
                Font = new Font("Segoe UI", 10),
                Location = new Point(0, fieldY),
                AutoSize = true
            };
            panelFormulario.Controls.Add(chkGestionVentas);

            CheckBox chkGestionClientes = new CheckBox
            {
                Name = "chkGestionClientes",
                Text = "Gestión de Clientes",
                Font = new Font("Segoe UI", 10),
                Location = new Point(200, fieldY),
                AutoSize = true
            };
            panelFormulario.Controls.Add(chkGestionClientes);

            fieldY += 50;

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
            panelContenido.Controls.Add(panelListado);

            // DataGridView para mostrar administradores
            DataGridView dgvAdministradores = new DataGridView
            {
                Name = "dgvAdministradores",
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
            panelListado.Controls.Add(dgvAdministradores);

            // Configurar columnas del DataGridView
            dgvAdministradores.Columns.Add("IdAdmin", "ID");
            dgvAdministradores.Columns.Add("Nombre", "Nombre");
            dgvAdministradores.Columns.Add("Apellido", "Apellido");
            dgvAdministradores.Columns.Add("Email", "Email");
            dgvAdministradores.Columns.Add("NivelAcceso", "Nivel de Acceso");
            dgvAdministradores.Columns.Add("Estado", "Estado");

            // Columna de acciones
            DataGridViewButtonColumn colEditar = new DataGridViewButtonColumn
            {
                HeaderText = "Editar",
                Text = "Editar",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat
            };
            dgvAdministradores.Columns.Add(colEditar);

            DataGridViewButtonColumn colEliminar = new DataGridViewButtonColumn
            {
                HeaderText = "Eliminar",
                Text = "Eliminar",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat
            };
            dgvAdministradores.Columns.Add(colEliminar);

            // Estilo de las columnas
            dgvAdministradores.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            dgvAdministradores.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvAdministradores.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvAdministradores.ColumnHeadersHeight = 40;
            dgvAdministradores.RowTemplate.Height = 35;

            // Manejar eventos de clic en las celdas
            dgvAdministradores.CellClick += (sender, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == colEditar.Index)
                    {
                        // Código para editar
                        DataGridViewRow row = dgvAdministradores.Rows[e.RowIndex];
                        CargarAdministradorParaEdicion(Convert.ToInt32(row.Cells["IdAdmin"].Value));
                    }
                    else if (e.ColumnIndex == colEliminar.Index)
                    {
                        // Código para eliminar
                        DataGridViewRow row = dgvAdministradores.Rows[e.RowIndex];
                        int idAdmin = Convert.ToInt32(row.Cells["IdAdmin"].Value);
                        string nombre = row.Cells["Nombre"].Value.ToString() + " " + row.Cells["Apellido"].Value.ToString();

                        DialogResult result = MessageBox.Show(
                            $"¿Está seguro que desea eliminar al administrador {nombre}?",
                            "Confirmar eliminación",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question
                        );

                        if (result == DialogResult.Yes)
                        {
                            try
                            {
                                _administradoresBL.Eliminar(idAdmin);
                                MessageBox.Show(
                                    "Administrador eliminado correctamente.",
                                    "Eliminación exitosa",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information
                                );
                                CargarAdministradores();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(
                                    "Error al eliminar administrador: " + ex.Message,
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

        private void CargarAdministradores()
        {
            try
            {
                _administradores = _administradoresBL.ObtenerTodos();

                DataGridView dgvAdministradores = (DataGridView)Controls.Find("dgvAdministradores", true)[0];
                dgvAdministradores.Rows.Clear();

                foreach (var admin in _administradores)
                {
                    string estado = admin.Estado ? "Activo" : "Inactivo";

                    dgvAdministradores.Rows.Add(
                        admin.IdAdmin,
                        admin.Nombre,
                        admin.Apellido,
                        admin.Email,
                        admin.NivelAcceso,
                        estado
                    );
                }

                // Colorear filas según el estado
                foreach (DataGridViewRow row in dgvAdministradores.Rows)
                {
                    string estado = row.Cells["Estado"].Value.ToString();
                    if (estado == "Inactivo")
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
                        row.DefaultCellStyle.ForeColor = Color.Gray;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar administradores: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarAdministradorParaEdicion(int idAdmin)
        {
            try
            {
                Administradores admin = _administradoresBL.ObtenerPorId(idAdmin);

                if (admin != null)
                {
                    TextBox txtID = (TextBox)Controls.Find("txtID", true)[0];
                    TextBox txtNombre = (TextBox)Controls.Find("txtNombre", true)[0];
                    TextBox txtApellido = (TextBox)Controls.Find("txtApellido", true)[0];
                    TextBox txtEmail = (TextBox)Controls.Find("txtEmail", true)[0];
                    TextBox txtClave = (TextBox)Controls.Find("txtClave", true)[0];
                    ComboBox cmbNivelAcceso = (ComboBox)Controls.Find("cmbNivelAcceso", true)[0];
                    DateTimePicker dtpFechaNacimiento = (DateTimePicker)Controls.Find("dtpFechaNacimiento", true)[0];
                    CheckBox chkGestionUsuarios = (CheckBox)Controls.Find("chkGestionUsuarios", true)[0];
                    CheckBox chkReportes = (CheckBox)Controls.Find("chkReportes", true)[0];
                    CheckBox chkConfiguracion = (CheckBox)Controls.Find("chkConfiguracion", true)[0];
                    CheckBox chkGestionProductos = (CheckBox)Controls.Find("chkGestionProductos", true)[0];
                    CheckBox chkGestionVentas = (CheckBox)Controls.Find("chkGestionVentas", true)[0];
                    CheckBox chkGestionClientes = (CheckBox)Controls.Find("chkGestionClientes", true)[0];
                    CheckBox chkEstado = (CheckBox)Controls.Find("chkEstado", true)[0];

                    txtID.Text = admin.IdAdmin.ToString();
                    txtNombre.Text = admin.Nombre;
                    txtApellido.Text = admin.Apellido;
                    txtEmail.Text = admin.Email;
                    txtClave.Text = admin.Clave;

                    // Seleccionar nivel de acceso
                    switch (admin.NivelAcceso)
                    {
                        case "Básico":
                            cmbNivelAcceso.SelectedIndex = 0;
                            break;
                        case "Intermedio":
                            cmbNivelAcceso.SelectedIndex = 1;
                            break;
                        case "Avanzado":
                            cmbNivelAcceso.SelectedIndex = 2;
                            break;
                        default:
                            cmbNivelAcceso.SelectedIndex = 0;
                            break;
                    }

                    dtpFechaNacimiento.Value = admin.FechaNacimiento;
                    chkGestionUsuarios.Checked = admin.GestionUsuarios;
                    chkReportes.Checked = admin.Reportes;
                    chkConfiguracion.Checked = admin.Configuracion;
                    chkGestionProductos.Checked = admin.GestionProductos;
                    chkGestionVentas.Checked = admin.GestionVentas;
                    chkGestionClientes.Checked = admin.GestionClientes;
                    chkEstado.Checked = admin.Estado;

                    _esEdicion = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar administrador para edición: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                ComboBox cmbNivelAcceso = (ComboBox)Controls.Find("cmbNivelAcceso", true)[0];
                DateTimePicker dtpFechaNacimiento = (DateTimePicker)Controls.Find("dtpFechaNacimiento", true)[0];
                CheckBox chkGestionUsuarios = (CheckBox)Controls.Find("chkGestionUsuarios", true)[0];
                CheckBox chkReportes = (CheckBox)Controls.Find("chkReportes", true)[0];
                CheckBox chkConfiguracion = (CheckBox)Controls.Find("chkConfiguracion", true)[0];
                CheckBox chkGestionProductos = (CheckBox)Controls.Find("chkGestionProductos", true)[0];
                CheckBox chkGestionVentas = (CheckBox)Controls.Find("chkGestionVentas", true)[0];
                CheckBox chkGestionClientes = (CheckBox)Controls.Find("chkGestionClientes", true)[0];
                CheckBox chkEstado = (CheckBox)Controls.Find("chkEstado", true)[0];

                // Validar campos
                if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApellido.Text) ||
                    string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtClave.Text))
                {
                    MessageBox.Show("Por favor complete todos los campos obligatorios", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear objeto administrador
                Administradores admin = new Administradores
                {
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Email = txtEmail.Text,
                    Clave = txtClave.Text,
                    NivelAcceso = cmbNivelAcceso.SelectedItem.ToString(),
                    FechaNacimiento = dtpFechaNacimiento.Value,
                    GestionUsuarios = chkGestionUsuarios.Checked,
                    Reportes = chkReportes.Checked,
                    Configuracion = chkConfiguracion.Checked,
                    GestionProductos = chkGestionProductos.Checked,
                    GestionVentas = chkGestionVentas.Checked,
                    GestionClientes = chkGestionClientes.Checked,
                    Estado = chkEstado.Checked
                };

                if (_esEdicion)
                {
                    // Actualizar administrador existente
                    admin.IdAdmin = Convert.ToInt32(txtID.Text);
                    _administradoresBL.Actualizar(admin);
                    MessageBox.Show("Administrador actualizado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Agregar nuevo administrador
                    _administradoresBL.Agregar(admin);
                    MessageBox.Show("Administrador agregado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Limpiar formulario y recargar administradores
                LimpiarFormulario();
                CargarAdministradores();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar administrador: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            TextBox txtID = (TextBox)Controls.Find("txtID", true)[0];
            TextBox txtNombre = (TextBox)Controls.Find("txtNombre", true)[0];
            TextBox txtApellido = (TextBox)Controls.Find("txtApellido", true)[0];
            TextBox txtEmail = (TextBox)Controls.Find("txtEmail", true)[0];
            TextBox txtClave = (TextBox)Controls.Find("txtClave", true)[0];
            ComboBox cmbNivelAcceso = (ComboBox)Controls.Find("cmbNivelAcceso", true)[0];
            DateTimePicker dtpFechaNacimiento = (DateTimePicker)Controls.Find("dtpFechaNacimiento", true)[0];
            CheckBox chkGestionUsuarios = (CheckBox)Controls.Find("chkGestionUsuarios", true)[0];
            CheckBox chkReportes = (CheckBox)Controls.Find("chkReportes", true)[0];
            CheckBox chkConfiguracion = (CheckBox)Controls.Find("chkConfiguracion", true)[0];
            CheckBox chkGestionProductos = (CheckBox)Controls.Find("chkGestionProductos", true)[0];
            CheckBox chkGestionVentas = (CheckBox)Controls.Find("chkGestionVentas", true)[0];
            CheckBox chkGestionClientes = (CheckBox)Controls.Find("chkGestionClientes", true)[0];
            CheckBox chkEstado = (CheckBox)Controls.Find("chkEstado", true)[0];

            txtID.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEmail.Text = "";
            txtClave.Text = "";
            cmbNivelAcceso.SelectedIndex = 0;
            dtpFechaNacimiento.Value = DateTime.Now;
            chkGestionUsuarios.Checked = false;
            chkReportes.Checked = false;
            chkConfiguracion.Checked = false;
            chkGestionProductos.Checked = false;
            chkGestionVentas.Checked = false;
            chkGestionClientes.Checked = false;
            chkEstado.Checked = true;

            _esEdicion = false;
        }
    }
}