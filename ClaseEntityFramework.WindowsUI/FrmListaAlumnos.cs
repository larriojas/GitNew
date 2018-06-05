using System;
using System.Windows.Forms;
using ClaseEntityFramework.LogicaNegocio;

namespace ClaseEntityFramework.WindowsUI
{
    public partial class FrmListaAlumnos : Form
    {
        public FrmListaAlumnos()
        {
            InitializeComponent();

            Load += (s, e) =>
            {
                btnMostrar.PerformClick();
            };
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            try
            {
                alumnoReadOnlyListBindingSource.DataSource = AlumnoReadOnlyList.GetReadOnlyList(string.Empty);
                alumnoReadOnlyListBindingSource.ResetBindings(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                using (var frm = new FrmMntAlumno(AlumnoRoot.NewEditableRoot()))
                {
                    var result = frm.ShowDialog(this);
                    if (result == DialogResult.OK)
                        btnMostrar.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException?.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(alumnoReadOnlyListBindingSource.Current is AlumnoReadOnly seleccionado)) return;

                using (var frm = new FrmMntAlumno(AlumnoRoot.GetEditableRoot(seleccionado.Id)))
                {
                    if (frm.ShowDialog(this) == DialogResult.OK)
                        btnMostrar.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(alumnoReadOnlyListBindingSource.Current is AlumnoReadOnly seleccionado)) return;

                if (MessageBox.Show(@"Desea eliminar el registro seleccionado?", Text, MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No) return;

                AlumnoRoot.DeleteEditableRoot(seleccionado.Id);

                btnMostrar.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            if (!(alumnoReadOnlyListBindingSource.Current is AlumnoReadOnly seleccionado)) return;

            using (var frm = new FrmAsignarCurso(AlumnoRoot.GetEditableRoot(seleccionado.Id)))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    btnMostrar.PerformClick();
            }
        }
    }
}
