using System;
using System.Windows.Forms;

namespace ClaseEntityFramework.WindowsUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void alumnosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmListaAlumnos())
            {
                frm.ShowDialog();
            }
        }

        private void cursosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmListaCursos())
            {
                frm.ShowDialog();
            }
        }

        private void asignarCursosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmAsignacionCursos())
            {
                frm.ShowDialog();
            }
        }
    }
}
