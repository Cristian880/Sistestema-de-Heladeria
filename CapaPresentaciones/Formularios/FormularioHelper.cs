using System;
using System.Drawing;
using System.Windows.Forms;
using SIS_Heladeria.CapaPresentacion.Formularios.Seguridad_Principales;

namespace CapaPresentaciones.Formularios
{
    public static class FormularioHelper
    {
        public static void AgregarBotonVolver(Form formulario, string formularioPadre = "Administradores")
        {
            Button btnVolver = new Button
            {
                Text = "← Volver",
                Size = new Size(100, 30),
                Location = new Point(10, 10),
                BackColor = Color.FromArgb(41, 128, 185),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Tag = formularioPadre
            };
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.Click += new EventHandler(BtnVolver_Click);
            formulario.Controls.Add(btnVolver);

            // Asegurar que el botón esté en el frente
            btnVolver.BringToFront();
        }

        private static void BtnVolver_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Form formularioActual = btn.FindForm();

            // Volver al formulario de administradores
            FrmAdministradores frmAdmin = new FrmAdministradores();
            formularioActual.Close();
            frmAdmin.FormClosed += (s, args) => formularioActual.Close();
            frmAdmin.Show();
        }
    }
}