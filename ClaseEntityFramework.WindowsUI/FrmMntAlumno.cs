using System;
using System.Windows.Forms;
using ClaseEntityFramework.LogicaNegocio;
using Csla;
using Csla.Rules;

namespace ClaseEntityFramework.WindowsUI
{
    public partial class FrmMntAlumno : Form
    {
        private AlumnoRoot _alumno;

        public FrmMntAlumno(AlumnoRoot alumno)
        {
            InitializeComponent();
            _alumno = alumno;
            alumnoBindingSource.DataSource = _alumno;
            alumnoBindingSource.ResetBindings(false);
        }

        private void FrmMntAlumno_Load(object sender, EventArgs e)
        {
            nombresTextBox.Focus();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                alumnoBindingSource.EndEdit();
                _alumno = _alumno.Save();
                DialogResult = DialogResult.OK;

            }
            catch (ValidationException)
            {
                MessageBox.Show(_alumno.GetBrokenRules().ToString(), Text, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            catch (DataPortalException ex)
            {
                MessageBox.Show(ex.BusinessException.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            alumnoBindingSource.CancelEdit();
            DialogResult = DialogResult.Cancel;
        }
    }
}
