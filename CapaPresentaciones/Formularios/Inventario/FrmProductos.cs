// CapaPresentacion/Formularios/Inventario/FrmProductos.cs
using CapaEntidades;
using CapaNegocio;
using CapaPresentaciones.Formularios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SIS_Heladeria.CapaPresentacion.Formularios.Inventario
{
    public partial class FrmProductos : Form
    {
        private ClientesBL _clientesBL = new ClientesBL();
        private int _idClienteSeleccionado = 0;
        private ProductosBL _productosBL = new ProductosBL();
        private int _idProductoSeleccionado = 0;
        private readonly CategoriasBL _categoriasBL = new CategoriasBL();
        private List<Productos> _productos;
        private List<Categorias> _categorias;
        private bool _esEdicion = false;

        public FrmProductos()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de Clientes - Sistema de Heladería";
            this.WindowState = FormWindowState.Maximized;
            this.ResumeLayout(false);

            ConfigurarFormulario();
            CargarClientes();
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
                Text = "GESTIÓN DE CLIENTES",
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
                Width = 350,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(240, 240, 240)
            };
            panelPrincipal.Controls.Add(panelFormulario);

            // Título del formulario
            Label lblFormulario = new Label
            {
                Text = "Datos del Cliente",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 128, 185),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 40
            };
            panelFormulario.Controls.Add(lblFormulario);

            // ID (oculto)
            TextBox txtID = new TextBox
            {
                Name = "txtID",
                Visible = false
            };
            panelFormulario.Controls.Add(txtID);

            // Campos del formulario
            int y = 60;
            int altoCampo = 60;

            // Nombre
            Label lblNombre = new Label
            {
                Text = "Nombre:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(20, y),
                AutoSize = true
            };
            panelFormulario.Controls.Add(lblNombre);

            TextBox txtNombre = new TextBox
            {
                Name = "txtNombre",
                Font = new Font("Segoe UI", 10),
                Location = new Point(20, y + 25),
                Size = new Size(panelFormulario.Width - 40, 30),
                BorderStyle = BorderStyle.FixedSingle
            };
            panelFormulario.Controls.Add(txtNombre);

            y += altoCampo;

            // Precio
            Label lblPrecio = new Label
            {
                Text = "Precio:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(20, y),
                AutoSize = true
            };
            panelFormulario.Controls.Add(lblPrecio);

            NumericUpDown nudPrecio = new NumericUpDown
            {
                Name = "nudPrecio",
                Font = new Font("Segoe UI", 10),
                Location = new Point(20, y + 25),
                Size = new Size(150, 30),
                Minimum = 0,
                Maximum = 9999,
                DecimalPlaces = 2,
                Value = 0
            };
            panelFormulario.Controls.Add(nudPrecio);

            y += altoCampo;

            // Stock
            Label lblStock = new Label
            {
                Text = "Stock:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(20, y),
                AutoSize = true
            };
            panelFormulario.Controls.Add(lblStock);

            NumericUpDown nudStock = new NumericUpDown
            {
                Name = "nudStock",
                Font = new Font("Segoe UI", 10),
                Location = new Point(20, y + 25),
                Size = new Size(150, 30),
                Minimum = 0,
                Maximum = 9999,
                Value = 0
            };
            panelFormulario.Controls.Add(nudStock);

            y += altoCampo;

            // Categoría
            Label lblCategoria = new Label
            {
                Text = "Categoría:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(20, y),
                AutoSize = true
            };
            panelFormulario.Controls.Add(lblCategoria);

            ComboBox cmbCategoria = new ComboBox
            {
                Name = "cmbCategoria",
                Font = new Font("Segoe UI", 10),
                Location = new Point(20, y + 25),
                Size = new Size(panelFormulario.Width - 40, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            panelFormulario.Controls.Add(cmbCategoria);

            y += altoCampo;

            // Estado
            CheckBox chkEstado = new CheckBox
            {
                Name = "chkEstado",
                Text = "Activo",
                Font = new Font("Segoe UI", 10),
                Location = new Point(20, y + 25),
                AutoSize = true,
                Checked = true
            };
            panelFormulario.Controls.Add(chkEstado);

            y += altoCampo;

            // Botones
            Button btnGuardar = new Button
            {
                Name = "btnGuardar",
                Text = "Guardar",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Size = new Size(150, 40),
                Location = new Point(20, y),
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
                Size = new Size(150, 40),
                Location = new Point(180, y),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Click += (sender, e) => {
                LimpiarFormulario();
            };
            panelFormulario.Controls.Add(btnCancelar);

            // Panel derecho (listado)
            Panel panelListado = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };
            panelPrincipal.Controls.Add(panelListado);

            // Panel de búsqueda
            Panel panelBusqueda = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                Padding = new Padding(10, 10, 10, 0)
            };
            panelListado.Controls.Add(panelBusqueda);

            // TextBox de búsqueda
            TextBox txtBuscar = new TextBox
            {
                Name = "txtBuscar",
                Font = new Font("Segoe UI", 10),
                Width = 300,
                Height = 30,
                Location = new Point(10, 10),
                BorderStyle = BorderStyle.FixedSingle
            };
            txtBuscar.KeyPress += (sender, e) => {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    BuscarClientes(txtBuscar.Text);
                    e.Handled = true;
                }
            };
            panelBusqueda.Controls.Add(txtBuscar);

            // Botón de búsqueda
            Button btnBuscar = new Button
            {
                Text = "Buscar",
                Font = new Font("Segoe UI", 10),
                Size = new Size(100, 30),
                Location = new Point(320, 10),
                BackColor = Color.FromArgb(41, 128, 185),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnBuscar.FlatAppearance.BorderSize = 0;
            btnBuscar.Click += (sender, e) => {
                BuscarClientes(txtBuscar.Text);
            };
            panelBusqueda.Controls.Add(btnBuscar);

            // DataGridView para mostrar clientes
            DataGridView dgvClientes = new DataGridView
            {
                Name = "dgvClientes",
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersVisible = true
            };
            panelListado.Controls.Add(dgvClientes);

            // Configurar columnas del DataGridView
            dgvClientes.Columns.Add("IdCliente", "ID");
            dgvClientes.Columns.Add("Nombre", "Nombre");
            dgvClientes.Columns.Add("Correo", "Correo");
            dgvClientes.Columns.Add("Telefono", "Teléfono");

            // Columna de acciones
            DataGridViewButtonColumn colEditar = new DataGridViewButtonColumn
            {
                Name = "colEditar",
                HeaderText = "Editar",
                Text = "Editar",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat
            };
            dgvClientes.Columns.Add(colEditar);

            DataGridViewButtonColumn colEliminar = new DataGridViewButtonColumn
            {
                Name = "colEliminar",
                HeaderText = "Eliminar",
                Text = "Eliminar",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat
            };
            dgvClientes.Columns.Add(colEliminar);

            // Estilo de las columnas
            dgvClientes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            dgvClientes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvClientes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvClientes.ColumnHeadersHeight = 40;
            dgvClientes.RowTemplate.Height = 35;

            // Manejar eventos de clic en las celdas
            dgvClientes.CellClick += (sender, e) =>
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == dgvClientes.Columns["colEditar"].Index)
                    {
                        // Código para editar
                        DataGridViewRow row = dgvClientes.Rows[e.RowIndex];
                        CargarClienteParaEdicion(Convert.ToInt32(row.Cells["IdCliente"].Value));
                    }
                    else if (e.ColumnIndex == dgvClientes.Columns["colEliminar"].Index)
                    {
                        // Código para eliminar
                        DataGridViewRow row = dgvClientes.Rows[e.RowIndex];
                        int idCliente = Convert.ToInt32(row.Cells["IdCliente"].Value);
                        string nombre = row.Cells["Nombre"].Value.ToString();

                        DialogResult result = MessageBox.Show(
                            $"¿Está seguro que desea eliminar el cliente {nombre}?",
                            "Confirmar eliminación",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question
                        );

                        if (result == DialogResult.Yes)
                        {
                            try
                            {
                                _clientesBL.Eliminar(idCliente);
                                MessageBox.Show(
                                    "Cliente eliminado correctamente.",
                                    "Eliminación exitosa",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information
                                );
                                CargarClientes();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(
                                    "Error al eliminar cliente: " + ex.Message,
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

        private void CargarClientes()
        {
            try
            {
                DataGridView dgvClientes = (DataGridView)Controls.Find("dgvClientes", true)[0];
                dgvClientes.Rows.Clear();

                foreach (var cliente in _clientesBL.ObtenerTodos())
                {
                    dgvClientes.Rows.Add(
                        cliente.IdCliente,
                        cliente.Nombre,
                        cliente.Correo,
                        cliente.Telefono
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuscarClientes(string textoBusqueda)
        {
            try
            {
                DataGridView dgvClientes = (DataGridView)Controls.Find("dgvClientes", true)[0];
                dgvClientes.Rows.Clear();

                foreach (var cliente in _clientesBL.Buscar(textoBusqueda))
                {
                    dgvClientes.Rows.Add(
                        cliente.IdCliente,
                        cliente.Nombre,
                        cliente.Correo,
                        cliente.Telefono
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar clientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarClienteParaEdicion(int idCliente)
        {
            try
            {
                var cliente = _clientesBL.ObtenerPorId(idCliente);

                if (cliente != null)
                {
                    TextBox txtID = (TextBox)Controls.Find("txtID", true)[0];
                    TextBox txtNombre = (TextBox)Controls.Find("txtNombre", true)[0];
                    TextBox txtCorreo = (TextBox)Controls.Find("txtCorreo", true)[0];
                    TextBox txtTelefono = (TextBox)Controls.Find("txtTelefono", true)[0];

                    txtID.Text = cliente.IdCliente.ToString();
                    txtNombre.Text = cliente.Nombre;
                    txtCorreo.Text = cliente.Correo;
                    txtTelefono.Text = cliente.Telefono;

                    _esEdicion = true;
                    _idClienteSeleccionado = idCliente;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar cliente para edición: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox txtID = (TextBox)Controls.Find("txtID", true)[0];
                TextBox txtNombre = (TextBox)Controls.Find("txtNombre", true)[0];
                TextBox txtCorreo = (TextBox)Controls.Find("txtCorreo", true)[0];
                TextBox txtTelefono = (TextBox)Controls.Find("txtTelefono", true)[0];

                // Validar campos
                if (string.IsNullOrEmpty(txtNombre.Text))
                {
                    MessageBox.Show("Por favor ingrese el nombre del cliente", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                // Crear objeto cliente
                Clientes cliente = new Clientes
                {
                    Nombre = txtNombre.Text,
                    Correo = txtCorreo.Text,
                    Telefono = txtTelefono.Text
                };

                if (_esEdicion)
                {
                    // Actualizar cliente existente
                    cliente.IdCliente = _idClienteSeleccionado;
                    _clientesBL.Actualizar(cliente);
                    MessageBox.Show("Cliente actualizado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Agregar nuevo cliente
                    _clientesBL.Agregar(cliente);
                    MessageBox.Show("Cliente agregado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Limpiar formulario y recargar clientes
                LimpiarFormulario();
                CargarClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormulario()
        {
            TextBox txtID = (TextBox)Controls.Find("txtID", true)[0];
            TextBox txtNombre = (TextBox)Controls.Find("txtNombre", true)[0];
            TextBox txtCorreo = (TextBox)Controls.Find("txtCorreo", true)[0];
            TextBox txtTelefono = (TextBox)Controls.Find("txtTelefono", true)[0];

            txtID.Text = "";
            txtNombre.Text = "";
            txtCorreo.Text = "";
            txtTelefono.Text = "";

            _esEdicion = false;
            _idClienteSeleccionado = 0;
        }
    }
}