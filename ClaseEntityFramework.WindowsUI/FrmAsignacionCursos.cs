using System;
using System.Windows.Forms;
using ClaseEntityFramework.LogicaNegocio;

namespace ClaseEntityFramework.WindowsUI
{
    public partial class FrmAsignacionCursos : Form
    {
        public FrmAsignacionCursos()
        {
            InitializeComponent();
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                alumnosPorCursoBindingSource.DataSource = AlumnoCursoReadOnlyList.GetReadOnlyList();
                alumnosPorCursoBindingSource.ResetBindings(false);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(alumnosPorCursoBindingSource.Current is AlumnoCursoReadOnly seleccionado)) return;

                using (var frm = new FrmAsignarCurso(AlumnoRoot.GetEditableRoot(seleccionado.AlumnoId)))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                        btnMostrar.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
