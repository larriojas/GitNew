using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Alemana.Nucleo.Estadisticas.Wpf.ViewModels
{
    /// <summary>
    /// Interaction logic for PacientesDiagnosticados.xaml
    /// </summary>
    [Export("Estadisticas.PacientesDiagnosticados")]
    public partial class PacientesDiagnosticados : UserControl
    {
        [ImportingConstructor]
        public PacientesDiagnosticados(PacientesDiagnosticadosViewModel model)
        {
            this.DataContext = model;
            InitializeComponent();
        }
    }
}