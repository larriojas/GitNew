using System;
using System.Windows.Forms;
using ClaseEntityFramework.LogicaNegocio;

namespace ClaseEntityFramework.WindowsUI
{
    public partial class FrmListaCursos : Form
    {
        public FrmListaCursos()
        {
            InitializeComponent();

            Shown += (s, e) =>
            {
                btnMostrar.PerformClick();
            };
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            try
            {
                cursoBindingSource.DataSource = CursoReadOnlyList.GetReadOnlyList();
                cursoBindingSource.ResetBindings(false);

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
                using (var frm = new FrmMntCurso(CursoRoot.NewEditableRoot()))
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
                if (!(cursoBindingSource.Current is CursoReadOnly seleccionado)) return;

                using (var frm = new FrmMntCurso(CursoRoot.GetEditableRoot(seleccionado.Id)))
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
                if (!(cursoBindingSource.Current is CursoReadOnly seleccionado)) return;

                if (MessageBox.Show(@"Desea eliminar el registro seleccionado?", Text, MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No) return;

                CursoRoot.DeleteEditableRoot(seleccionado.Id);

                btnMostrar.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {

        }
    }
}
