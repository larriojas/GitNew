using System;
using System.Windows.Forms;
using ClaseEntityFramework.LogicaNegocio;

namespace ClaseEntityFramework.WindowsUI
{
    public partial class FrmAsignarCurso : Form
    {
        private AlumnoRoot _alumnoRoot;

        public FrmAsignarCurso(AlumnoRoot alumnoRoot)
        {
            InitializeComponent();
            _alumnoRoot = alumnoRoot;

            Shown += (s, e) =>
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;

                    alumnoRootBindingSource.DataSource = _alumnoRoot;
                    alumnoRootBindingSource.ResetBindings(false);

                    // Llenamos el Combo.
                    cursoNameValueListBindingSource.DataSource = CursoNameValueList.GetCursoNameValueList();
                    cursoNameValueListBindingSource.ResetBindings(false);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            };
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (!(cursoNameValueListBindingSource.Current is CursoNameValueList.NameValuePair cursoSeleccionado)) return;

                var alumnoCursoChild = AlumnoCursoChild.NewEditableChild();

                alumnoCursoChild.IdCurso = cursoSeleccionado.Key;
                alumnoCursoChild.NombreCurso = cursoSeleccionado.Value;
                
                _alumnoRoot.Cursos.Add(alumnoCursoChild);

                alumnoRootBindingSource.ResetBindings(false);

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

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                alumnoRootBindingSource.EndEdit();

                _alumnoRoot = _alumnoRoot.Save();

                DialogResult = DialogResult.OK;
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(cursosBindingSource.Current is AlumnoCursoChild seleccionado)) return;

                _alumnoRoot.Cursos.Remove(seleccionado);

                alumnoRootBindingSource.ResetBindings(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            alumnoRootBindingSource.CancelEdit();
            DialogResult = DialogResult.Cancel;
        }
    }
}
