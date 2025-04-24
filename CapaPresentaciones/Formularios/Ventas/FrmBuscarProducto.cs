// CapaPresentacion/Formularios/Inventario/FrmBuscarProducto.cs
using CapaNegocio;
using CapaPresentaciones.Formularios;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SIS_Heladeria.CapaPresentacion.Formularios.Inventario
{
    public partial class FrmBuscarProducto : Form
    {
        private readonly ProductosBL _productosBL;

        public FrmBuscarProducto()
        {
            InitializeComponent();
            _productosBL = new ProductosBL();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Buscar Producto - Sistema de Heladería";
            this.ResumeLayout(false);

            ConfigurarFormulario();
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
                Text = "BUSCAR PRODUCTO",
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

            // Panel de búsqueda
            Panel panelBusqueda = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                Padding = new Padding(20)
            };
            panelPrincipal.Controls.Add(panelBusqueda);

            // Label para texto de búsqueda
            Label lblBuscar = new Label
            {
                Text = "Buscar:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(20, 25),
                AutoSize = true
            };
            panelBusqueda.Controls.Add(lblBuscar);

            // TextBox para texto de búsqueda
            TextBox txtBuscar = new TextBox
            {
                Name = "txtBuscar",
                Font = new Font("Segoe UI", 10),
                Location = new Point(80, 20),
                Size = new Size(200, 30),
                BorderStyle = BorderStyle.FixedSingle
            };
            panelBusqueda.Controls.Add(txtBuscar);

            // Botón para buscar
            Button btnBuscar = new Button
            {
                Text = "Buscar",
                Font = new Font("Segoe UI", 10),
                Size = new Size(100, 30),
                Location = new Point(300, 20),
                BackColor = Color.FromArgb(41, 128, 185),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnBuscar.FlatAppearance.BorderSize = 0;
            btnBuscar.Click += (sender, e) => {
                BuscarProductos(txtBuscar.Text);
            };
            panelBusqueda.Controls.Add(btnBuscar);

            // DataGridView para mostrar productos
            DataGridView dgvProductos = new DataGridView
            {
                Name = "dgvProductos",
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
            panelPrincipal.Controls.Add(dgvProductos);

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
        }

        private void BuscarProductos(string textoBusqueda)
        {
            try
            {
                var productos = _productosBL.Buscar(textoBusqueda);

                DataGridView dgvProductos = (DataGridView)Controls.Find("dgvProductos", true)[0];
                dgvProductos.Rows.Clear();

                foreach (var producto in productos)
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
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}