using Alemana.Nucleo.Estadisticas.Wpf.ViewModels;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace Alemana.Nucleo.Estadisticas.Wpf.Views
{
    /// <summary>
    /// Interaction logic for Plantilla.xaml
    /// </summary>
    public partial class Plantilla : UserControl
    {
        private bool? firstRow;
        private bool firstCell;

        public Plantilla(PlantillaViewModel model)
        {
            this.DataContext = model;
            InitializeComponent();
        }

        private void CheckBoxArea_Checked(object sender, RoutedEventArgs e)
        {
            var chk = sender as CheckBox;

            if (chk.Content.ToString() != "Todos" && (DataContext as PlantillaViewModel).Areas.Count <= 1)
            {
                return;
            }

            if (chk.Content.ToString() == "Todas")
                (DataContext as PlantillaViewModel).Areas.Where(x => x.ID > 0).ToList().ForEach(x => x.IsChecked = false);
            else
                (DataContext as PlantillaViewModel).Areas.Single(x => x.ID == -1).IsChecked = false;
        }

        private void CheckBoxArea_UnChecked(object sender, RoutedEventArgs e)
        {
            var chk = sender as CheckBox;

            if (chk.Content.ToString() != "Todos" && (DataContext as PlantillaViewModel).Areas.Count <= 1)
            {
                MessageBox.Show("Usted no puede realizar esta operación, requiere al menos un aréa seleccionado.",
                "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);

                (DataContext as PlantillaViewModel).Areas.FirstOrDefault().IsChecked = true;
            }
        }

        private void CheckBoxProfesionales_Checked(object sender, RoutedEventArgs e)
        {
            var chk = sender as CheckBox;

            if (chk.Content.ToString() != "Todos" && (DataContext as PlantillaViewModel).Profesionales.Count <= 1)
            {
                return;
            }

            if (chk.Content.ToString() == "Todos")
                (DataContext as PlantillaViewModel).Profesionales.Where(x => x.ID > 0).ToList().ForEach(x => x.IsChecked = false);
            else
                (DataContext as PlantillaViewModel).Profesionales.Single(x => x.ID == -1).IsChecked = false;
        }

        private void CheckBoxProfesionales_UnChecked(object sender, RoutedEventArgs e)
        {
            var chk = sender as CheckBox;

            if (chk.Content.ToString() != "Todos" && (DataContext as PlantillaViewModel).Profesionales.Count <= 1)
            {
                MessageBox.Show("Usted no puede realizar esta operación, requiere al menos un profesional seleccionado.",
                                "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);

                (DataContext as PlantillaViewModel).Profesionales.FirstOrDefault().IsChecked = true;
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            dropAreas.IsOpen = false;
            dropProfesionales.IsOpen = false;
        }

        //private void ItemsControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    if ((((PlantillaViewModel)DataContext).Areas == null) || (((PlantillaViewModel)DataContext).Areas.Count() <= 1))
        //    {
        //        dropAreas.IsEnabled = false;
        //    }
        //    else
        //    {
        //        dropAreas.IsEnabled = true;
        //    }

        //    if ((((PlantillaViewModel)DataContext).Profesionales == null) || (((PlantillaViewModel)DataContext).Profesionales.Count() <= 1))
        //    {
        //        dropProfesionales.IsEnabled = false;
        //    }
        //    else
        //    {
        //        dropProfesionales.IsEnabled = true;
        //    }
        //}

        private void ChkTodos_Checked(object sender, RoutedEventArgs e)
        {
            var chk = sender as CheckBox;
            PlantillaPlanaItem item = chk.DataContext as PlantillaPlanaItem;

            if (item != null)
            {
                if (item.Tipo != PlantillaPlanaItem.PlantillaPlanaItemTipo.Variable)
                {
                    CheckAllChildrens(item, chk.IsChecked.Value, true);
                }

                if (chk.IsChecked.Value)
                {
                    item.UltimoResultado = false;
                }
            }
        }

        private void ChkUltimoResultado_Checked(object sender, RoutedEventArgs e)
        {
            var chk = sender as CheckBox;
            PlantillaPlanaItem item = chk.DataContext as PlantillaPlanaItem;

            if (item != null)
            {
                if (item.Tipo != PlantillaPlanaItem.PlantillaPlanaItemTipo.Variable)
                {
                    CheckAllChildrens(item, chk.IsChecked.Value, false);
                }

                if (chk.IsChecked.Value)
                {
                    item.Todo = false;
                }
            }
        }

        private void CheckAllChildrens(PlantillaPlanaItem item, bool checkValue, bool checkTodos)
        {
            PlantillaPlanaItem.PlantillaPlanaItemTipo targetType = ((item.Tipo == PlantillaPlanaItem.PlantillaPlanaItemTipo.Plantilla) ?
                                                                       (PlantillaPlanaItem.PlantillaPlanaItemTipo.Modulo) :
                                                                       ((item.Tipo == PlantillaPlanaItem.PlantillaPlanaItemTipo.Modulo) ?
                                                                           (PlantillaPlanaItem.PlantillaPlanaItemTipo.Agrupador) :
                                                                           (PlantillaPlanaItem.PlantillaPlanaItemTipo.Variable)));

            PlantillaViewModel model = (PlantillaViewModel)this.DataContext;
            if (checkTodos)
            {
                model.PlantillaPlana.Items.Where(x => x.ParentId == item.Id && x.Tipo == targetType).ToList().ForEach(p => p.Todo = checkValue);
                if (checkValue)
                {
                    model.PlantillaPlana.Items.Where(x => x.ParentId == item.Id && x.Tipo == targetType).ToList().ForEach(p => p.UltimoResultado = false);
                }
            }
            else
            {
                model.PlantillaPlana.Items.Where(x => x.ParentId == item.Id && x.Tipo == targetType).ToList().ForEach(p => p.UltimoResultado = checkValue);
                if (checkValue)
                {
                    model.PlantillaPlana.Items.Where(x => x.ParentId == item.Id && x.Tipo == targetType).ToList().ForEach(p => p.Todo = false);
                }
            }
        }

        private void gvExport_DataLoaded(object sender, EventArgs e)
        {
            if (gvExport.Items.Count > 1)
            {
                string extension = "xls";
                Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog()
                {
                    DefaultExt = extension,
                    Filter = String.Format("{1} files (*.{0})|*.{0}|All files (*.*)|*.*", extension, "Excel"),
                    FilterIndex = 1
                };
                if (dialog.ShowDialog() == true)
                {
                    using (System.IO.Stream stream = dialog.OpenFile())
                    {
                        firstRow = null;
                        firstCell = true;

                        gvExport.Visibility = System.Windows.Visibility.Visible;

                        gvExport.InitializingExcelMLStyles -= InitializingExcelMLStyles;
                        gvExport.InitializingExcelMLStyles += InitializingExcelMLStyles;
                        gvExport.ElementExporting -= ElementExporting;
                        gvExport.ElementExporting += ElementExporting;

                        GridViewExportOptions options = new GridViewExportOptions()
                        {
                            Format = Telerik.Windows.Controls.ExportFormat.ExcelML,
                            Encoding = Encoding.UTF8,
                            ShowColumnHeaders = false,
                            ShowColumnFooters = true,
                            ShowGroupFooters = false
                        };
                        gvExport.Export(stream, options);

                        gvExport.Visibility = System.Windows.Visibility.Hidden;
                    }
                }
            }
            else
            {
                MessageBox.Show("No se encontraron pacientes para exportar con incidencias en los módulos seleccionados entre las fechas ingresadas para las áreas y usuarios seleccionados para exportar",
                                "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void InitializingExcelMLStyles(object sender, ExcelMLStylesEventArgs e)
        {
            ExcelMLStyle style = new ExcelMLStyle("Header");
            style.Font.Bold = true;
            e.Styles.Add(style);

            style = new ExcelMLStyle("Cell");
            style.Font.Bold = false;
            e.Styles.Add(style);
        }

        private void ElementExporting(object sender, GridViewElementExportingEventArgs e)
        {
            var visParameters = e.VisualParameters as GridViewExcelMLVisualExportParameters;
            if (e.Element == ExportElement.Row)
            {
                if (!firstRow.HasValue)
                {
                    firstRow = true;
                }
                else if (firstRow.Value)
                {
                    firstRow = false;
                }
                firstCell = true;
            }
            else if (e.Element == ExportElement.Cell)
            {
                if ((firstRow.HasValue && firstRow.Value) || (firstCell))
                {
                    visParameters.StyleId = "Header";
                    firstCell = false;
                }
                else
                {
                    visParameters.StyleId = "Cell";
                }
            }
        }
    }
}