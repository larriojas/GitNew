using Alemana.Nucleo.Estadisticas.Wpf.ViewModels;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Alemana.Nucleo.Estadisticas.Wpf.Views
{
    /// <summary>
    /// Interaction logic for UsoHCE.xaml
    /// </summary>
    public partial class UsoHCE : UserControl
    {
        public UsoHCE(UsoHCEViewModel model)
        {
            this.DataContext = model;
            InitializeComponent();
        }

        private void CheckBoxArea_Checked(object sender, RoutedEventArgs e)
        {
            var chk = sender as CheckBox;

            if (chk.Content.ToString() != "Todos" && (DataContext as UsoHCEViewModel).Areas.Count <= 1)
            {
                return;
            }

            if (chk.Content.ToString() == "Todas")
                (DataContext as UsoHCEViewModel).Areas.Where(x => x.ID > 0).ToList().ForEach(x => x.IsChecked = false);
            else
                (DataContext as UsoHCEViewModel).Areas.Single(x => x.ID == -1).IsChecked = false;
        }

        private void CheckBoxArea_UnChecked(object sender, RoutedEventArgs e)
        {
            var chk = sender as CheckBox;

            if (chk.Content.ToString() != "Todos" && (DataContext as UsoHCEViewModel).Areas.Count <= 1)
            {
                MessageBox.Show("Usted no puede realizar esta operación, requiere al menos un aréa seleccionado.",
                "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);

                (DataContext as UsoHCEViewModel).Areas.FirstOrDefault().IsChecked = true;
            }
        }

        private void CheckBoxProfesionales_Checked(object sender, RoutedEventArgs e)
        {
            var chk = sender as CheckBox;

            if (chk.Content.ToString() != "Todos" && (DataContext as UsoHCEViewModel).Profesionales.Count <= 1)
            {
                return;
            }

            if (chk.Content.ToString() == "Todos")
                (DataContext as UsoHCEViewModel).Profesionales.Where(x => x.ID > 0).ToList().ForEach(x => x.IsChecked = false);
            else
                (DataContext as UsoHCEViewModel).Profesionales.Single(x => x.ID == -1).IsChecked = false;
        }

        private void CheckBoxProfesionales_UnChecked(object sender, RoutedEventArgs e)
        {
            var chk = sender as CheckBox;

            if (chk.Content.ToString() != "Todos" && (DataContext as UsoHCEViewModel).Profesionales.Count <= 1)
            {
                MessageBox.Show("Usted no puede realizar esta operación, requiere al menos un profesional seleccionado.",
                                "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);

                (DataContext as UsoHCEViewModel).Profesionales.FirstOrDefault().IsChecked = true;
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            dropAreas.IsOpen = false;
            dropProfesionales.IsOpen = false;
        }

        //private void ItemsControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    if ((((UsoHCEViewModel)DataContext).Areas == null) || (((UsoHCEViewModel)DataContext).Areas.Count() <= 1))
        //    {
        //        dropAreas.IsEnabled = false;
        //    }
        //    else
        //    {
        //        dropAreas.IsEnabled = true;
        //    }

        //    if ((((UsoHCEViewModel)DataContext).Profesionales == null) || (((UsoHCEViewModel)DataContext).Profesionales.Count() <= 1))
        //    {
        //        dropProfesionales.IsEnabled = false;
        //    }
        //    else
        //    {
        //        dropProfesionales.IsEnabled = true;
        //    }
        //}
    }
}