using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConexionCSharpconMySQL
{
    public partial class FormClientes : Form
    {
        public FormClientes()
        {
            InitializeComponent();
        }

      

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Cliente pCliente = new Cliente();
            pCliente.Nombre = txtNombre.Text.Trim();
            pCliente.Apellido = txtApellido.Text.Trim();
            pCliente.Fecha_Nac = dtpFechaNacimiento.Value.Year + "/" + dtpFechaNacimiento.Value.Month + "/" + dtpFechaNacimiento.Value.Day;
            pCliente.Direccion = txtDireccion.Text.Trim();

            int resultado = ClietesDAL.Agregar(pCliente);
            if (resultado > 0)
            {
                MessageBox.Show("Cliente Guardado Con Exito!!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se pudo guardar el cliente", "Fallo!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        
    }
}
