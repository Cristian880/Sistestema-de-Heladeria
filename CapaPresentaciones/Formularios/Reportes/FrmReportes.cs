// CapaPresentacion/Formularios/Reportes/FrmReportes.cs
using CapaNegocio;
using CapaPresentaciones.Formularios;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SIS_Heladeria.CapaPresentacion.Formularios.Reportes
{
    public partial class FrmReportes : Form
    {
        private readonly VentasBL _ventasBL;
        private readonly ProductosBL _productosBL;

        public FrmReportes()
        {
            InitializeComponent();
            _ventasBL = new VentasBL();
            _productosBL = new ProductosBL();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reportes - Sistema de Heladería";
            this.WindowState = FormWindowState.Maximized;
            this.ResumeLayout(false);

            ConfigurarFormulario();
            CargarDatos();
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
                Text = "REPORTES Y ESTADÍSTICAS",
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

            // Panel de filtros
            Panel panelFiltros = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                Padding = new Padding(20, 10, 20, 10),
                BackColor = Color.FromArgb(240, 240, 240)
            };
            panelPrincipal.Controls.Add(panelFiltros);

            // Label para fecha inicio
            Label lblFechaInicio = new Label
            {
                Text = "Fecha Inicio:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(20, 15),
                AutoSize = true
            };
            panelFiltros.Controls.Add(lblFechaInicio);

            // DateTimePicker para fecha inicio
            DateTimePicker dtpFechaInicio = new DateTimePicker
            {
                Name = "dtpFechaInicio",
                Font = new Font("Segoe UI", 10),
                Location = new Point(110, 10),
                Size = new Size(150, 30),
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Now.AddMonths(-1)
            };
            panelFiltros.Controls.Add(dtpFechaInicio);

            // Label para fecha fin
            Label lblFechaFin = new Label
            {
                Text = "Fecha Fin:",
                Font = new Font("Segoe UI", 10),
                Location = new Point(280, 15),
                AutoSize = true
            };
            panelFiltros.Controls.Add(lblFechaFin);

            // DateTimePicker para fecha fin
            DateTimePicker dtpFechaFin = new DateTimePicker
            {
                Name = "dtpFechaFin",
                Font = new Font("Segoe UI", 10),
                Location = new Point(350, 10),
                Size = new Size(150, 30),
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Now
            };
            panelFiltros.Controls.Add(dtpFechaFin);

            // Botón para filtrar
            Button btnFiltrar = new Button
            {
                Text = "Filtrar",
                Font = new Font("Segoe UI", 10),
                Size = new Size(100, 30),
                Location = new Point(520, 10),
                BackColor = Color.FromArgb(41, 128, 185),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnFiltrar.FlatAppearance.BorderSize = 0;
            btnFiltrar.Click += (sender, e) => {
                CargarDatos();
            };
            panelFiltros.Controls.Add(btnFiltrar);

            // Botón para exportar
            Button btnExportar = new Button
            {
                Text = "Exportar a Excel",
                Font = new Font("Segoe UI", 10),
                Size = new Size(150, 30),
                Location = new Point(640, 10),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnExportar.FlatAppearance.BorderSize = 0;
            btnExportar.Click += (sender, e) => {
                MessageBox.Show("Funcionalidad de exportación no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            panelFiltros.Controls.Add(btnExportar);

            // Botón para imprimir
            Button btnImprimir = new Button
            {
                Text = "Imprimir",
                Font = new Font("Segoe UI", 10),
                Size = new Size(100, 30),
                Location = new Point(810, 10),
                BackColor = Color.FromArgb(52, 73, 94),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnImprimir.FlatAppearance.BorderSize = 0;
            btnImprimir.Click += (sender, e) => {
                MessageBox.Show("Funcionalidad de impresión no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            panelFiltros.Controls.Add(btnImprimir);

            // Panel de contenido
            Panel panelContenido = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };
            panelPrincipal.Controls.Add(panelContenido);

            // TabControl para diferentes reportes
            TabControl tabReportes = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10)
            };
            panelContenido.Controls.Add(tabReportes);

            // Tab para ventas
            TabPage tabVentas = new TabPage
            {
                Text = "Ventas",
                Padding = new Padding(10)
            };
            tabReportes.Controls.Add(tabVentas);

            // DataGridView para mostrar ventas
            DataGridView dgvVentas = new DataGridView
            {
                Name = "dgvVentas",
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
            tabVentas.Controls.Add(dgvVentas);

            // Configurar columnas del DataGridView
            dgvVentas.Columns.Add("IdVenta", "ID");
            dgvVentas.Columns.Add("Cliente", "Cliente");
            dgvVentas.Columns.Add("Usuario", "Usuario");
            dgvVentas.Columns.Add("Fecha", "Fecha");
            dgvVentas.Columns.Add("Total", "Total");

            // Estilo de las columnas
            dgvVentas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            dgvVentas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvVentas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvVentas.ColumnHeadersHeight = 40;
            dgvVentas.RowTemplate.Height = 35;

            // Tab para productos más vendidos
            TabPage tabProductos = new TabPage
            {
                Text = "Productos Más Vendidos",
                Padding = new Padding(10)
            };
            tabReportes.Controls.Add(tabProductos);

            // Chart para productos más vendidos
            Chart chartProductos = new Chart
            {
                Name = "chartProductos",
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            tabProductos.Controls.Add(chartProductos);

            // Configurar chart
            chartProductos.Titles.Add("Productos Más Vendidos");
            chartProductos.Titles[0].Font = new Font("Segoe UI", 14, FontStyle.Bold);
            chartProductos.Titles[0].ForeColor = Color.FromArgb(41, 128, 185);

            ChartArea areaProductos = new ChartArea("AreaProductos");
            areaProductos.AxisX.Title = "Productos";
            areaProductos.AxisX.TitleFont = new Font("Segoe UI", 10, FontStyle.Bold);
            areaProductos.AxisY.Title = "Cantidad Vendida";
            areaProductos.AxisY.TitleFont = new Font("Segoe UI", 10, FontStyle.Bold);
            chartProductos.ChartAreas.Add(areaProductos);

            Series serieProductos = new Series("Productos");
            serieProductos.ChartType = SeriesChartType.Column;
            serieProductos.Color = Color.FromArgb(41, 128, 185);
            chartProductos.Series.Add(serieProductos);

            // Tab para ventas por categoría
            TabPage tabCategorias = new TabPage
            {
                Text = "Ventas por Categoría",
                Padding = new Padding(10)
            };
            tabReportes.Controls.Add(tabCategorias);

            // Chart para ventas por categoría
            Chart chartCategorias = new Chart
            {
                Name = "chartCategorias",
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            tabCategorias.Controls.Add(chartCategorias);

            // Configurar chart
            chartCategorias.Titles.Add("Ventas por Categoría");
            chartCategorias.Titles[0].Font = new Font("Segoe UI", 14, FontStyle.Bold);
            chartCategorias.Titles[0].ForeColor = Color.FromArgb(41, 128, 185);

            ChartArea areaCategorias = new ChartArea("AreaCategorias");
            chartCategorias.ChartAreas.Add(areaCategorias);

            Series serieCategorias = new Series("Categorias");
            serieCategorias.ChartType = SeriesChartType.Pie;
            serieCategorias.IsValueShownAsLabel = true;
            serieCategorias.LabelFormat = "{0}%";
            chartCategorias.Series.Add(serieCategorias);

            // Tab para productos con bajo stock
            TabPage tabBajoStock = new TabPage
            {
                Text = "Productos con Bajo Stock",
                Padding = new Padding(10)
            };
            tabReportes.Controls.Add(tabBajoStock);

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
            tabBajoStock.Controls.Add(dgvBajoStock);

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

        private void CargarDatos()
        {
            try
            {
                DateTimePicker dtpFechaInicio = (DateTimePicker)Controls.Find("dtpFechaInicio", true)[0];
                DateTimePicker dtpFechaFin = (DateTimePicker)Controls.Find("dtpFechaFin", true)[0];

                DateTime fechaInicio = dtpFechaInicio.Value.Date;
                DateTime fechaFin = dtpFechaFin.Value.Date.AddDays(1).AddSeconds(-1);

                // Cargar ventas
                CargarVentas(fechaInicio, fechaFin);

                // Cargar productos más vendidos
                CargarProductosMasVendidos();

                // Cargar ventas por categoría
                CargarVentasPorCategoria(fechaInicio, fechaFin);

                // Cargar productos con bajo stock
                CargarProductosBajoStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarVentas(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                var ventas = _ventasBL.ObtenerVentasPorPeriodo(fechaInicio, fechaFin);

                DataGridView dgvVentas = (DataGridView)Controls.Find("dgvVentas", true)[0];
                dgvVentas.Rows.Clear();

                foreach (var venta in ventas)
                {
                    dgvVentas.Rows.Add(
                        venta.IdVenta,
                        venta.NombreCliente,
                        venta.NombreUsuario,
                        venta.FechaVenta.ToString("dd/MM/yyyy H:mm"),
                        venta.Total.ToString("C")
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar ventas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarProductosMasVendidos()
        {
            try
            {
                // Obtener estadísticas generales
                DataSet ds = _ventasBL.ObtenerEstadisticasGenerales();

                // Verificar si el DataSet contiene datos
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    // Obtener la tabla de productos más vendidos
                    DataTable dtProductos = ds.Tables[0];

                    // Limpiar serie existente
                    Chart chartProductos = (Chart)Controls.Find("chartProductos", true)[0];
                    chartProductos.Series["Productos"].Points.Clear();

                    // Agregar datos al chart
                    foreach (DataRow row in dtProductos.Rows)
                    {
                        string producto = row["Producto"].ToString();
                        int cantidad = Convert.ToInt32(row["Cantidad"]);
                        chartProductos.Series["Productos"].Points.AddXY(producto, cantidad);
                    }
                }
                else
                {
                    MessageBox.Show("No hay datos de productos más vendidos disponibles", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos más vendidos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarVentasPorCategoria(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                // Obtener ventas por categoría
                DataTable dtCategorias = _ventasBL.ObtenerVentasPorCategoria(fechaInicio, fechaFin);

                // Limpiar serie existente
                Chart chartCategorias = (Chart)Controls.Find("chartCategorias", true)[0];
                chartCategorias.Series["Categorias"].Points.Clear();

                // Agregar datos al chart
                foreach (DataRow row in dtCategorias.Rows)
                {
                    string categoria = row["Categoria"].ToString();
                    decimal total = Convert.ToDecimal(row["Total"]);
                    chartCategorias.Series["Categorias"].Points.AddXY(categoria, total);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar ventas por categoría: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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