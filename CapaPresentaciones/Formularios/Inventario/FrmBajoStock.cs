using CapaNegocio;
using CapaPresentaciones.Formularios;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SIS_Heladeria.CapaPresentacion.Formularios.Inventario
{
    public partial class FrmBajoStock : Form
    {
        private readonly ProductosBL _productosBL;

        public FrmBajoStock()
        {
            InitializeComponent();
            _productosBL = new ProductosBL();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Productos con Bajo Stock - Sistema de Heladería";
            this.ResumeLayout(false);

            ConfigurarFormulario();
            CargarProductosBajoStock();
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
                Text = "PRODUCTOS CON BAJO STOCK",
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

            // DataGridView para mostrar productos con bajo stock
            DataGridView dgvBajoStock = new DataGridView
            {
                Name = "dgvBajoStock",
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
            panelPrincipal.Controls.Add(dgvBajoStock);

            // Configurar columnas del DataGridView
            dgvBajoStock.Columns.Add("IdProducto", "ID");
            dgvBajoStock.Columns.Add("Nombre", "Nombre");
            dgvBajoStock.Columns.Add("Precio", "Precio");
            dgvBajoStock.Columns.Add("Stock", "Stock");
            dgvBajoStock.Columns.Add("Categoria", "Categoría");

            // Estilo de las columnas
            dgvBajoStock.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            dgvBajoStock.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvBajoStock.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvBajoStock.ColumnHeadersHeight = 40;
            dgvBajoStock.RowTemplate.Height = 35;
        }

        private void CargarProductosBajoStock()
        {
            try
            {
                var productos = _productosBL.ObtenerProductosBajoStock();

                DataGridView dgvBajoStock = (DataGridView)Controls.Find("dgvBajoStock", true)[0];
                dgvBajoStock.Rows.Clear();

                foreach (var producto in productos)
                {
                    dgvBajoStock.Rows.Add(
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
                MessageBox.Show("Error al cargar productos con bajo stock: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}