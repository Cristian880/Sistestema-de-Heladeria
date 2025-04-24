// CapaPresentacion/Formularios/Ventas/FrmVentas.cs
using CapaEntidades;
using CapaNegocio;
using CapaPresentaciones.Formularios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SIS_Heladeria.CapaPresentacion.Formularios.Ventas
{
    public partial class FrmVentas : Form
    {
        private readonly ProductosBL _productosBL;
        private readonly ClientesBL _clientesBL;
        private readonly VentasBL _ventasBL;
        private List<Productos> _productos;
        private List<Clientes> _clientes;
        private List<DetalleVenta> _detallesVenta = new List<DetalleVenta>();
        private Clientes _clienteSeleccionado;
        private Productos _productoSeleccionado;
        private decimal _total = 0;

        public FrmVentas()
        {
            InitializeComponent();
            _productosBL = new ProductosBL();
            _clientesBL = new ClientesBL();
            _ventasBL = new VentasBL();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Punto de Venta";
            this.WindowState = FormWindowState.Maximized;
            this.ResumeLayout(false);

            ConfigurarFormulario();
            CargarClientes();
            CargarProductos();
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
                Text = "PUNTO DE VENTA",
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

            // Panel izquierdo (carrito)
            Panel panelCarrito = new Panel
            {
                Dock = DockStyle.Left,
                Width = 800,
                Padding = new Padding(10),
                BackColor = Color.White
            };
            panelPrincipal.Controls.Add(panelCarrito);

            // Panel de cliente
            Panel panelCliente = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                Padding = new Padding(10),
                BackColor = Color.FromArgb(240, 240, 240)
            };
            panelCarrito.Controls.Add(panelCliente);

            // Label para cliente
            Label lblCliente = new Label
            {
                Text = "Cliente:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(10, 15),
                AutoSize = true
            };
            panelCliente.Controls.Add(lblCliente);

            // TextBox para cliente
            TextBox txtCliente = new TextBox
            {
                Name = "txtCliente",
                Font = new Font("Segoe UI", 10),
                Width = 300,
                Height = 30,
                Location = new Point(70, 12),
                BorderStyle = BorderStyle.FixedSingle,
                ReadOnly = true
            };
            panelCliente.Controls.Add(txtCliente);

            // Botón buscar cliente
            Button btnBuscarCliente = new Button
            {
                Text = "Buscar",
                Font = new Font("Segoe UI", 10),
                Size = new Size(100, 30),
                Location = new Point(380, 12),
                BackColor = Color.FromArgb(41, 128, 185),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnBuscarCliente.FlatAppearance.BorderSize = 0;
            btnBuscarCliente.Click += (sender, e) => {
                MostrarDialogoBuscarCliente();
            };
            panelCliente.Controls.Add(btnBuscarCliente);

            // Label para documento
            Label lblDocumento = new Label
            {
                Text = "Documento:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(10, 50),
                AutoSize = true
            };
            panelCliente.Controls.Add(lblDocumento);

            // TextBox para documento
            TextBox txtDocumento = new TextBox
            {
                Name = "txtDocumento",
                Font = new Font("Segoe UI", 10),
                Width = 200,
                Height = 30,
                Location = new Point(100, 47),
                BorderStyle = BorderStyle.FixedSingle,
                ReadOnly = true
            };
            panelCliente.Controls.Add(txtDocumento);

            // Label para teléfono
            Label lblTelefono = new Label
            {
                Text = "Teléfono:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(320, 50),
                AutoSize = true
            };
            panelCliente.Controls.Add(lblTelefono);

            // TextBox para teléfono
            TextBox txtTelefono = new TextBox
            {
                Name = "txtTelefono",
                Font = new Font("Segoe UI", 10),
                Width = 150,
                Height = 30,
                Location = new Point(380, 47),
                BorderStyle = BorderStyle.FixedSingle,
                ReadOnly = true
            };
            panelCliente.Controls.Add(txtTelefono);

            // Panel de productos
            Panel panelProductos = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                Padding = new Padding(10),
                BackColor = Color.FromArgb(240, 240, 240),
                Margin = new Padding(0, 10, 0, 0)
            };
            panelCarrito.Controls.Add(panelProductos);

            // Label para producto
            Label lblProducto = new Label
            {
                Text = "Producto:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(10, 20),
                AutoSize = true
            };
            panelProductos.Controls.Add(lblProducto);

            // TextBox para producto
            TextBox txtProducto = new TextBox
            {
                Name = "txtProducto",
                Font = new Font("Segoe UI", 10),
                Width = 300,
                Height = 30,
                Location = new Point(80, 17),
                BorderStyle = BorderStyle.FixedSingle,
                ReadOnly = true
            };
            panelProductos.Controls.Add(txtProducto);

            // Botón buscar producto
            Button btnBuscarProducto = new Button
            {
                Text = "Buscar",
                Font = new Font("Segoe UI", 10),
                Size = new Size(100, 30),
                Location = new Point(390, 17),
                BackColor = Color.FromArgb(41, 128, 185),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnBuscarProducto.FlatAppearance.BorderSize = 0;
            btnBuscarProducto.Click += (sender, e) => {
                MostrarDialogoBuscarProducto();
            };
            panelProductos.Controls.Add(btnBuscarProducto);

            // Label para cantidad
            Label lblCantidad = new Label
            {
                Text = "Cantidad:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(500, 20),
                AutoSize = true
            };
            panelProductos.Controls.Add(lblCantidad);

            // NumericUpDown para cantidad
            NumericUpDown nudCantidad = new NumericUpDown
            {
                Name = "nudCantidad",
                Font = new Font("Segoe UI", 10),
                Location = new Point(570, 17),
                Size = new Size(70, 30),
                Minimum = 1,
                Maximum = 100,
                Value = 1
            };
            panelProductos.Controls.Add(nudCantidad);

            // Botón agregar producto
            Button btnAgregar = new Button
            {
                Text = "Agregar",
                Font = new Font("Segoe UI", 10),
                Size = new Size(100, 30),
                Location = new Point(650, 17),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnAgregar.FlatAppearance.BorderSize = 0;
            btnAgregar.Click += (sender, e) => {
                AgregarProductoAlCarrito();
            };
            panelProductos.Controls.Add(btnAgregar);

            // DataGridView para mostrar productos en el carrito
            DataGridView dgvCarrito = new DataGridView
            {
                Name = "dgvCarrito",
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
            panelCarrito.Controls.Add(dgvCarrito);

            // Configurar columnas del DataGridView
            dgvCarrito.Columns.Add("IdProducto", "ID");
            dgvCarrito.Columns.Add("Nombre", "Producto");
            dgvCarrito.Columns.Add("Precio", "Precio");
            dgvCarrito.Columns.Add("Cantidad", "Cantidad");
            dgvCarrito.Columns.Add("Subtotal", "Subtotal");

            // Columna de acciones
            DataGridViewButtonColumn colEliminar = new DataGridViewButtonColumn
            {
                HeaderText = "Acción",
                Text = "Eliminar",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat
            };
            dgvCarrito.Columns.Add(colEliminar);

            // Estilo de las columnas
            dgvCarrito.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            dgvCarrito.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCarrito.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvCarrito.ColumnHeadersHeight = 40;
            dgvCarrito.RowTemplate.Height = 35;

            // Manejar eventos de clic en las celdas
            dgvCarrito.CellClick += (sender, e) =>
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == colEliminar.Index)
                {
                    EliminarProductoDelCarrito(e.RowIndex);
                }
            };

            // Panel derecho (totales y pago)
            Panel panelPago = new Panel
            {
                Dock = DockStyle.Right,
                Width = 400,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(240, 240, 240)
            };
            panelPrincipal.Controls.Add(panelPago);

            // Título del panel de pago
            Label lblTituloPago = new Label
            {
                Text = "RESUMEN DE VENTA",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 128, 185),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 40
            };
            panelPago.Controls.Add(lblTituloPago);

            // Panel de totales
            Panel panelTotales = new Panel
            {
                Dock = DockStyle.Top,
                Height = 200,
                Margin = new Padding(0, 20, 0, 0)
            };
            panelPago.Controls.Add(panelTotales);

            // Subtotal
            Label lblSubtotal = new Label
            {
                Text = "Subtotal:",
                Font = new Font("Segoe UI", 12),
                Location = new Point(0, 20),
                AutoSize = true
            };
            panelTotales.Controls.Add(lblSubtotal);

            Label lblSubtotalValor = new Label
            {
                Name = "lblSubtotalValor",
                Text = "RD$ 0.00",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(250, 20),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleRight
            };
            panelTotales.Controls.Add(lblSubtotalValor);

            // Impuestos
            Label lblImpuestos = new Label
            {
                Text = "Impuestos (18%):",
                Font = new Font("Segoe UI", 12),
                Location = new Point(0, 60),
                AutoSize = true
            };
            panelTotales.Controls.Add(lblImpuestos);

            Label lblImpuestosValor = new Label
            {
                Name = "lblImpuestosValor",
                Text = "RD$ 0.00",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(250, 60),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleRight
            };
            panelTotales.Controls.Add(lblImpuestosValor);

            // Descuento
            Label lblDescuento = new Label
            {
                Text = "Descuento:",
                Font = new Font("Segoe UI", 12),
                Location = new Point(0, 100),
                AutoSize = true
            };
            panelTotales.Controls.Add(lblDescuento);

            TextBox txtDescuento = new TextBox
            {
                Name = "txtDescuento",
                Font = new Font("Segoe UI", 12),
                Location = new Point(250, 97),
                Size = new Size(100, 30),
                TextAlign = HorizontalAlignment.Right,
                Text = "0.00",
                BorderStyle = BorderStyle.FixedSingle
            };
            txtDescuento.TextChanged += (sender, e) => {
                ActualizarTotal();
            };
            panelTotales.Controls.Add(txtDescuento);

            // Total
            Label lblTotal = new Label
            {
                Text = "TOTAL:",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(0, 150),
                AutoSize = true
            };
            panelTotales.Controls.Add(lblTotal);

            Label lblTotalValor = new Label
            {
                Name = "lblTotalValor",
                Text = "RD$ 0.00",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 128, 185),
                Location = new Point(220, 150),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleRight
            };
            panelTotales.Controls.Add(lblTotalValor);

            // Panel de método de pago
            Panel panelMetodoPago = new Panel
            {
                Dock = DockStyle.Top,
                Height = 150,
                Margin = new Padding(0, 20, 0, 0)
            };
            panelPago.Controls.Add(panelMetodoPago);

            // Título método de pago
            Label lblMetodoPago = new Label
            {
                Text = "Método de Pago",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(0, 0),
                AutoSize = true
            };
            panelMetodoPago.Controls.Add(lblMetodoPago);

            // RadioButton para efectivo
            RadioButton rbEfectivo = new RadioButton
            {
                Name = "rbEfectivo",
                Text = "Efectivo",
                Font = new Font("Segoe UI", 10),
                Location = new Point(0, 30),
                AutoSize = true,
                Checked = true
            };
            rbEfectivo.CheckedChanged += (sender, e) => {
                if (rbEfectivo.Checked)
                {
                    MostrarCamposMontoCambio(true);
                }
            };
            panelMetodoPago.Controls.Add(rbEfectivo);

            // RadioButton para tarjeta
            RadioButton rbTarjeta = new RadioButton
            {
                Name = "rbTarjeta",
                Text = "Tarjeta",
                Font = new Font("Segoe UI", 10),
                Location = new Point(100, 30),
                AutoSize = true
            };
            rbTarjeta.CheckedChanged += (sender, e) => {
                if (rbTarjeta.Checked)
                {
                    MostrarCamposMontoCambio(false);
                }
            };
            panelMetodoPago.Controls.Add(rbTarjeta);

            // RadioButton para transferencia
            RadioButton rbTransferencia = new RadioButton
            {
                Name = "rbTransferencia",
                Text = "Transferencia",
                Font = new Font("Segoe UI", 10),
                Location = new Point(200, 30),
                AutoSize = true
            };
            rbTransferencia.CheckedChanged += (sender, e) => {
                if (rbTransferencia.Checked)
                {
                    MostrarCamposMontoCambio(false);
                }
            };
            panelMetodoPago.Controls.Add(rbTransferencia);

            // Label para monto recibido
            Label lblMontoRecibido = new Label
            {
                Name = "lblMontoRecibido",
                Text = "Monto Recibido:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(0, 70),
                AutoSize = true
            };
            panelMetodoPago.Controls.Add(lblMontoRecibido);

            // TextBox para monto recibido
            TextBox txtMontoRecibido = new TextBox
            {
                Name = "txtMontoRecibido",
                Font = new Font("Segoe UI", 12),
                Location = new Point(120, 65),
                Size = new Size(150, 30),
                TextAlign = HorizontalAlignment.Right,
                BorderStyle = BorderStyle.FixedSingle
            };
            txtMontoRecibido.TextChanged += (sender, e) => {
                CalcularCambio();
            };
            panelMetodoPago.Controls.Add(txtMontoRecibido);

            // Label para cambio
            Label lblCambio = new Label
            {
                Name = "lblCambio",
                Text = "Cambio:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(0, 110),
                AutoSize = true
            };
            panelMetodoPago.Controls.Add(lblCambio);

            // Label para valor del cambio
            Label lblCambioValor = new Label
            {
                Name = "lblCambioValor",
                Text = "RD$ 0.00",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(120, 108),
                AutoSize = true
            };
            panelMetodoPago.Controls.Add(lblCambioValor);

            // Panel de botones
            Panel panelBotones = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 200,
                Padding = new Padding(0, 20, 0, 0)
            };
            panelPago.Controls.Add(panelBotones);

            // Botón procesar venta
            Button btnProcesar = new Button
            {
                Text = "PROCESAR VENTA",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(360, 50),
                Location = new Point(0, 20),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnProcesar.FlatAppearance.BorderSize = 0;
            btnProcesar.Click += (sender, e) => {
                ProcesarVenta();
            };
            panelBotones.Controls.Add(btnProcesar);

            // Botón cancelar
            Button btnCancelar = new Button
            {
                Text = "CANCELAR",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(360, 50),
                Location = new Point(0, 80),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Click += (sender, e) => {
                CancelarVenta();
            };
            panelBotones.Controls.Add(btnCancelar);

            // Botón nueva venta
            Button btnNueva = new Button
            {
                Text = "NUEVA VENTA",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(360, 50),
                Location = new Point(0, 140),
                BackColor = Color.FromArgb(41, 128, 185),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnNueva.FlatAppearance.BorderSize = 0;
            btnNueva.Click += (sender, e) => {
                NuevaVenta();
            };
            panelBotones.Controls.Add(btnNueva);
        }

        private void CargarClientes()
        {
            try
            {
                _clientes = _clientesBL.ObtenerTodos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarProductos()
        {
            try
            {
                _productos = _productosBL.ObtenerTodos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarDialogoBuscarCliente()
        {
            Form frmBuscarCliente = new Form
            {
                Text = "Buscar Cliente",
                Size = new Size(600, 400),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            // Panel de búsqueda
            Panel panelBusqueda = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                Padding = new Padding(10)
            };
            frmBuscarCliente.Controls.Add(panelBusqueda);

            // TextBox de búsqueda
            TextBox txtBuscar = new TextBox
            {
                Font = new Font("Segoe UI", 10),
                Width = 300,
                Height = 30,
                Location = new Point(10, 10),
                BorderStyle = BorderStyle.FixedSingle
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
            panelBusqueda.Controls.Add(btnBuscar);

            // DataGridView para mostrar clientes
            DataGridView dgvClientes = new DataGridView
            {
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
            frmBuscarCliente.Controls.Add(dgvClientes);

            // Configurar columnas del DataGridView
            dgvClientes.Columns.Add("IdCliente", "ID");
            dgvClientes.Columns.Add("Nombre", "Nombre");
            dgvClientes.Columns.Add("Correo", "Correo");
            dgvClientes.Columns.Add("Telefono", "Teléfono");

            // Estilo de las columnas
            dgvClientes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            dgvClientes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvClientes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvClientes.ColumnHeadersHeight = 40;
            dgvClientes.RowTemplate.Height = 35;

            // Cargar clientes
            foreach (var cliente in _clientes)
            {
                dgvClientes.Rows.Add(
                    cliente.IdCliente,
                    cliente.Nombre,
                    cliente.Correo,
                    cliente.Telefono
                );
            }

            // Evento de búsqueda
            btnBuscar.Click += (sender, e) => {
                string textoBusqueda = txtBuscar.Text.Trim();
                if (!string.IsNullOrEmpty(textoBusqueda))
                {
                    List<Clientes> clientesFiltrados = _clientesBL.Buscar(textoBusqueda);
                    dgvClientes.Rows.Clear();
                    foreach (var cliente in clientesFiltrados)
                    {
                        dgvClientes.Rows.Add(
                            cliente.IdCliente,
                            cliente.Nombre,
                            cliente.Correo,
                            cliente.Telefono
                        );
                    }
                }
                else
                {
                    dgvClientes.Rows.Clear();
                    foreach (var cliente in _clientes)
                    {
                        dgvClientes.Rows.Add(
                            cliente.IdCliente,
                            cliente.Nombre,
                            cliente.Correo,
                            cliente.Telefono
                        );
                    }
                }
            };

            // Evento de doble clic en una fila
            dgvClientes.CellDoubleClick += (sender, e) => {
                if (e.RowIndex >= 0)
                {
                    int idCliente = Convert.ToInt32(dgvClientes.Rows[e.RowIndex].Cells["IdCliente"].Value);
                    _clienteSeleccionado = _clientes.Find(c => c.IdCliente == idCliente);

                    if (_clienteSeleccionado != null)
                    {
                        TextBox txtCliente = (TextBox)Controls.Find("txtCliente", true)[0];
                        TextBox txtDocumento = (TextBox)Controls.Find("txtDocumento", true)[0];
                        TextBox txtTelefono = (TextBox)Controls.Find("txtTelefono", true)[0];

                        txtCliente.Text = _clienteSeleccionado.Nombre;
                        txtDocumento.Text = _clienteSeleccionado.Correo;
                        txtTelefono.Text = _clienteSeleccionado.Telefono;

                        frmBuscarCliente.Close();
                    }
                }
            };

            frmBuscarCliente.ShowDialog();
        }

        private void MostrarDialogoBuscarProducto()
        {
            Form frmBuscarProducto = new Form
            {
                Text = "Buscar Producto",
                Size = new Size(800, 500),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            // Panel de búsqueda
            Panel panelBusqueda = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                Padding = new Padding(10)
            };
            frmBuscarProducto.Controls.Add(panelBusqueda);

            // TextBox de búsqueda
            TextBox txtBuscar = new TextBox
            {
                Font = new Font("Segoe UI", 10),
                Width = 300,
                Height = 30,
                Location = new Point(10, 10),
                BorderStyle = BorderStyle.FixedSingle
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
            panelBusqueda.Controls.Add(btnBuscar);

            // DataGridView para mostrar productos
            DataGridView dgvProductos = new DataGridView
            {
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
            frmBuscarProducto.Controls.Add(dgvProductos);

            // Configurar columnas del DataGridView
            dgvProductos.Columns.Add("IdProducto", "ID");
            dgvProductos.Columns.Add("Nombre", "Nombre");
            dgvProductos.Columns.Add("Precio", "Precio");
            dgvProductos.Columns.Add("Stock", "Stock");
            dgvProductos.Columns.Add("Categoria", "Categoría");

            // Estilo de las columnas
            dgvProductos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            dgvProductos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProductos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvProductos.ColumnHeadersHeight = 40;
            dgvProductos.RowTemplate.Height = 35;

            // Cargar productos
            foreach (var producto in _productos)
            {
                dgvProductos.Rows.Add(
                    producto.IdProducto,
                    producto.Nombre,
                    producto.Precio.ToString("C"),
                    producto.Stock,
                    producto.NombreCategoria
                );
            }

            // Evento de búsqueda
            btnBuscar.Click += (sender, e) => {
                string textoBusqueda = txtBuscar.Text.Trim();
                if (!string.IsNullOrEmpty(textoBusqueda))
                {
                    List<Productos> productosFiltrados = _productosBL.Buscar(textoBusqueda);
                    dgvProductos.Rows.Clear();
                    foreach (var producto in productosFiltrados)
                    {
                        dgvProductos.Rows.Add(
                            producto.IdProducto,
                            producto.Nombre,
                            producto.Precio.ToString("C"),
                            producto.Stock,
                            producto.NombreCategoria
                        );
                    }
                }
                else
                {
                    dgvProductos.Rows.Clear();
                    foreach (var producto in _productos)
                    {
                        dgvProductos.Rows.Add(
                            producto.IdProducto,
                            producto.Nombre,
                            producto.Precio.ToString("C"),
                            producto.Stock,
                            producto.NombreCategoria
                        );
                    }
                }
            };

            // Evento de doble clic en una fila
            dgvProductos.CellDoubleClick += (sender, e) => {
                if (e.RowIndex >= 0)
                {
                    int idProducto = Convert.ToInt32(dgvProductos.Rows[e.RowIndex].Cells["IdProducto"].Value);
                    _productoSeleccionado = _productos.Find(p => p.IdProducto == idProducto);

                    if (_productoSeleccionado != null)
                    {
                        TextBox txtProducto = (TextBox)Controls.Find("txtProducto", true)[0];
                        txtProducto.Text = _productoSeleccionado.Nombre;

                        frmBuscarProducto.Close();
                    }
                }
            };

            frmBuscarProducto.ShowDialog();
        }

        private void AgregarProductoAlCarrito()
        {
            try
            {
                if (_productoSeleccionado == null)
                {
                    MessageBox.Show("Por favor seleccione un producto", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                NumericUpDown nudCantidad = (NumericUpDown)Controls.Find("nudCantidad", true)[0];
                int cantidad = Convert.ToInt32(nudCantidad.Value);

                if (_productoSeleccionado.Stock < cantidad)
                {
                    MessageBox.Show("No hay suficiente stock disponible", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Verificar si el producto ya está en el carrito
                DetalleVenta detalleExistente = _detallesVenta.Find(d => d.IdProducto == _productoSeleccionado.IdProducto);

                if (detalleExistente != null)
                {
                    // Actualizar cantidad
                    detalleExistente.Cantidad += cantidad;
                    detalleExistente.Subtotal = detalleExistente.Cantidad * detalleExistente.PrecioUnitario;
                }
                else
                {
                    // Agregar nuevo detalle
                    DetalleVenta detalle = new DetalleVenta
                    {
                        IdProducto = _productoSeleccionado.IdProducto,
                        NombreProducto = _productoSeleccionado.Nombre,
                        PrecioUnitario = _productoSeleccionado.Precio,
                        Cantidad = cantidad,
                        Subtotal = _productoSeleccionado.Precio * cantidad
                    };

                    _detallesVenta.Add(detalle);
                }

                // Actualizar DataGridView
                ActualizarCarrito();

                // Actualizar total
                ActualizarTotal();

                // Limpiar selección de producto
                TextBox txtProducto = (TextBox)Controls.Find("txtProducto", true)[0];
                txtProducto.Text = "";
                nudCantidad.Value = 1;
                _productoSeleccionado = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar producto al carrito: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EliminarProductoDelCarrito(int rowIndex)
        {
            try
            {
                DataGridView dgvCarrito = (DataGridView)Controls.Find("dgvCarrito", true)[0];
                int idProducto = Convert.ToInt32(dgvCarrito.Rows[rowIndex].Cells["IdProducto"].Value);

                // Eliminar detalle
                _detallesVenta.RemoveAll(d => d.IdProducto == idProducto);

                // Actualizar DataGridView
                ActualizarCarrito();

                // Actualizar total
                ActualizarTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar producto del carrito: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarCarrito()
        {
            try
            {
                DataGridView dgvCarrito = (DataGridView)Controls.Find("dgvCarrito", true)[0];
                dgvCarrito.Rows.Clear();

                foreach (var detalle in _detallesVenta)
                {
                    dgvCarrito.Rows.Add(
                        detalle.IdProducto,
                        detalle.NombreProducto,
                        detalle.PrecioUnitario.ToString("C"),
                        detalle.Cantidad,
                        detalle.Subtotal.ToString("C")
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar carrito: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarTotal()
        {
            try
            {
                // Calcular subtotal
                decimal subtotal = 0;
                foreach (var detalle in _detallesVenta)
                {
                    subtotal += detalle.Subtotal;
                }

                // Calcular impuestos (18%)
                decimal impuestos = subtotal * 0.18m;

                // Obtener descuento
                TextBox txtDescuento = (TextBox)Controls.Find("txtDescuento", true)[0];
                decimal descuento = 0;
                decimal.TryParse(txtDescuento.Text, out descuento);

                // Calcular total
                _total = subtotal + impuestos - descuento;

                // Actualizar etiquetas
                Label lblSubtotalValor = (Label)Controls.Find("lblSubtotalValor", true)[0];
                Label lblImpuestosValor = (Label)Controls.Find("lblImpuestosValor", true)[0];
                Label lblTotalValor = (Label)Controls.Find("lblTotalValor", true)[0];

                lblSubtotalValor.Text = "RD$ " + subtotal.ToString("N2");
                lblImpuestosValor.Text = "RD$ " + impuestos.ToString("N2");
                lblTotalValor.Text = "RD$ " + _total.ToString("N2");

                // Recalcular cambio
                CalcularCambio();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar total: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarCamposMontoCambio(bool mostrar)
        {
            Label lblMontoRecibido = (Label)Controls.Find("lblMontoRecibido", true)[0];
            TextBox txtMontoRecibido = (TextBox)Controls.Find("txtMontoRecibido", true)[0];
            Label lblCambio = (Label)Controls.Find("lblCambio", true)[0];
            Label lblCambioValor = (Label)Controls.Find("lblCambioValor", true)[0];

            lblMontoRecibido.Visible = mostrar;
            txtMontoRecibido.Visible = mostrar;
            lblCambio.Visible = mostrar;
            lblCambioValor.Visible = mostrar;
        }

        private void CalcularCambio()
        {
            try
            {
                TextBox txtMontoRecibido = (TextBox)Controls.Find("txtMontoRecibido", true)[0];
                Label lblCambioValor = (Label)Controls.Find("lblCambioValor", true)[0];

                decimal montoRecibido = 0;
                decimal.TryParse(txtMontoRecibido.Text, out montoRecibido);

                decimal cambio = montoRecibido - _total;
                if (cambio < 0)
                {
                    cambio = 0;
                }

                lblCambioValor.Text = "RD$ " + cambio.ToString("N2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al calcular cambio: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProcesarVenta()
        {
            try
            {
                // Validar que haya productos en el carrito
                if (_detallesVenta.Count == 0)
                {
                    MessageBox.Show("No hay productos en el carrito", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar que haya un cliente seleccionado
                if (_clienteSeleccionado == null)
                {
                    MessageBox.Show("Por favor seleccione un cliente", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar método de pago
                RadioButton rbEfectivo = (RadioButton)Controls.Find("rbEfectivo", true)[0];
                RadioButton rbTarjeta = (RadioButton)Controls.Find("rbTarjeta", true)[0];
                RadioButton rbTransferencia = (RadioButton)Controls.Find("rbTransferencia", true)[0];

                string metodoPago = "";
                if (rbEfectivo.Checked)
                {
                    metodoPago = "Efectivo";

                    // Validar monto recibido
                    TextBox txtMontoRecibido = (TextBox)Controls.Find("txtMontoRecibido", true)[0];
                    decimal montoRecibido = 0;
                    if (!decimal.TryParse(txtMontoRecibido.Text, out montoRecibido) || montoRecibido < _total)
                    {
                        MessageBox.Show("El monto recibido debe ser mayor o igual al total", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else if (rbTarjeta.Checked)
                {
                    metodoPago = "Tarjeta";
                }
                else if (rbTransferencia.Checked)
                {
                    metodoPago = "Transferencia";
                }

                // Crear objeto venta
                CapaEntidades.Ventas venta = new CapaEntidades.Ventas
                {
                    IdCliente = _clienteSeleccionado.IdCliente,
                    IdUsuario = UsuarioActual.IdUsuario,
                    FechaVenta = DateTime.Now,
                    Total = _total,
                    MetodoPago = metodoPago,
                    Estado = true
                };

                // Procesar venta
                int idVenta = _ventasBL.AgregarVenta(venta);

                if (idVenta > 0)
                {
                    MessageBox.Show("Venta procesada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Preguntar si desea imprimir factura
                    DialogResult result = MessageBox.Show("¿Desea imprimir la factura?", "Imprimir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Código para imprimir factura
                        MessageBox.Show("Funcionalidad de impresión no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Limpiar formulario
                    NuevaVenta();
                }
                else
                {
                    MessageBox.Show("Error al procesar venta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar venta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelarVenta()
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea cancelar la venta?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                NuevaVenta();
            }
        }

        private void NuevaVenta()
        {
            // Limpiar cliente
            TextBox txtCliente = (TextBox)Controls.Find("txtCliente", true)[0];
            TextBox txtDocumento = (TextBox)Controls.Find("txtDocumento", true)[0];
            TextBox txtTelefono = (TextBox)Controls.Find("txtTelefono", true)[0];

            txtCliente.Text = "";
            txtDocumento.Text = "";
            txtTelefono.Text = "";

            _clienteSeleccionado = null;

            // Limpiar producto
            TextBox txtProducto = (TextBox)Controls.Find("txtProducto", true)[0];
            NumericUpDown nudCantidad = (NumericUpDown)Controls.Find("nudCantidad", true)[0];

            txtProducto.Text = "";
            nudCantidad.Value = 1;

            _productoSeleccionado = null;

            // Limpiar carrito
            _detallesVenta.Clear();
            ActualizarCarrito();

            // Limpiar totales
            TextBox txtDescuento = (TextBox)Controls.Find("txtDescuento", true)[0];
            TextBox txtMontoRecibido = (TextBox)Controls.Find("txtMontoRecibido", true)[0];

            txtDescuento.Text = "0.00";
            txtMontoRecibido.Text = "";

            ActualizarTotal();

            // Seleccionar efectivo como método de pago
            RadioButton rbEfectivo = (RadioButton)Controls.Find("rbEfectivo", true)[0];
            rbEfectivo.Checked = true;
        }
    }
}