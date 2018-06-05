using System;
using System.Windows.Forms;
using ClaseEntityFramework.LogicaNegocio;
using Csla;
using Csla.Rules;

namespace ClaseEntityFramework.WindowsUI
{
    public partial class FrmMntCurso : Form
    {
        private CursoRoot _curso;

        public FrmMntCurso(CursoRoot curso)
        {
            InitializeComponent();
            _curso = curso;
            cursoBindingSource.DataSource = _curso;
            cursoBindingSource.ResetBindings(false);
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                cursoBindingSource.EndEdit();
                _curso = _curso.Save();
                DialogResult = DialogResult.OK;

                // DataPortalException - Aqui se devuelve la Excepcion ocurrida dentro de DataPortal.
                // ValidationException - Cuando una regla de Validacion/Negocio no se cumpla.

            }
            catch (ValidationException)
            {
                MessageBox.Show(_curso.GetBrokenRules().ToString(), Text, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (DataPortalException ex)
            {
                MessageBox.Show(ex.BusinessException.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cursoBindingSource.CancelEdit();
            DialogResult = DialogResult.Cancel;
        }
    }
}
