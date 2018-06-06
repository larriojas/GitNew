using Alemana.Nucleo.Estadisticas.Wpf.ViewModels;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Alemana.Nucleo.Estadisticas.Wpf.Views
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    [Export("Estadisticas.Inicio")]
    public partial class Main : UserControl
    {
        [ImportingConstructor]
        public Main(MainViewModel model)
        {
            this.DataContext = model;
            InitializeComponent();
        }
    }
}