// CapaPresentacion/Formularios/Ventas/FrmFacturas.cs
using CapaPresentaciones.Formularios;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SIS_Heladeria.CapaPresentacion.Formularios.Ventas
{
    public partial class FrmFacturas : Form
    {
        public FrmFacturas()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de Facturas - Sistema de Heladería";
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
                Text = "GESTIÓN DE FACTURAS",
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

            // TODO: Implementar la interfaz de gestión de facturas
            Label lblEnConstruccion = new Label
            {
                Text = "Módulo en Construcción",
                Font = new Font("Segoe UI", 14, FontStyle.Italic),
                ForeColor = Color.Gray,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            panelPrincipal.Controls.Add(lblEnConstruccion);
        }
    }
}