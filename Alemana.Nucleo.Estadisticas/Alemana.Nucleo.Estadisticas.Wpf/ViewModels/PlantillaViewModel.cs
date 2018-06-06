using Alemana.Nucleo.Common.ComponentModel;
using Alemana.Nucleo.Common.Extensions;
using Alemana.Nucleo.Common.Security;
using Alemana.Nucleo.Estadisticas.Contrato.Models;
using Alemana.Nucleo.Estadisticas.Contrato.ServiceInterfaces;
using Alemana.Nucleo.Estadisticas.Wpf.Resources;
using Alemana.Nucleo.Infrastructure.Models;
using Alemana.Nucleo.Shared.Entities;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Alemana.Nucleo.Estadisticas.Wpf.ViewModels
{
    public class PlantillaViewModel : ViewModelBase, IPartImportsSatisfiedNotification
    {
        private readonly IFiltrosServices filtroService;
        private readonly IExportarPlantillaService plantillasService;

        public PlantillaViewModel(IComponentContainer componentContainer)
        {
            this.filtroService = componentContainer.Resolve<IFiltrosServices>();
            this.plantillasService = componentContainer.Resolve<IExportarPlantillaService>();

            this.AplicarFiltroCommand = new DelegateCommand(AplicarFiltro);
            this.RemoverFiltroCommand = new DelegateCommand(RemoverFiltro);
            this.ObtenerPlantillaCommand = new DelegateCommand(ObtenerPlantillaSelected);
            this.ExportarExcelCommand = new DelegateCommand(ExportarExcel);

            this.FiltroRangoFechas = new KeyValuePair<RangoFechasUtility.RangoFecha, string>(RangoFechasUtility.RangoFecha.UltimoMes,
                                                                                             RangoFechasUtility.RangoFechas[RangoFechasUtility.RangoFecha.UltimoMes]);
            this.TipoFechaCreacion = true;

            CallAsynchronousLoadFilters();
        }

        public override string Title
        {
            get { return "Estadísticas Plantilla"; }
        }

        public void OnImportsSatisfied()
        {
            CallAsynchronousLoadFilters();
        }

        public DelegateCommand AplicarFiltroCommand { get; private set; }

        public DelegateCommand RemoverFiltroCommand { get; private set; }

        public DelegateCommand ObtenerPlantillaCommand { get; private set; }

        public DelegateCommand ExportarExcelCommand { get; private set; }

        #region Busy

        private bool isBusy { get; set; }

        public bool IsBusy
        {
            get { return IsBusyLoadingFilters || isBusyLoadingPlantillas; }
        }

        private bool isBusyLoadingFilters;

        public bool IsBusyLoadingFilters
        {
            get { return isBusyLoadingFilters; }
            set
            {
                isBusyLoadingFilters = value;
                RaisePropertyChanged(() => this.IsBusyLoadingFilters);
                RaisePropertyChanged(() => this.IsBusy);
            }
        }

        private bool isBusyLoadingPlantillas;

        public bool IsBusyLoadingPlantillas
        {
            get { return isBusyLoadingPlantillas; }
            set
            {
                isBusyLoadingPlantillas = value;
                RaisePropertyChanged(() => this.IsBusyLoadingPlantillas);
                RaisePropertyChanged(() => this.IsBusy);
            }
        }

        private bool isBusyLoadingPlantilla;

        public bool IsBusyLoadingPlantilla
        {
            get { return isBusyLoadingPlantilla; }
            set
            {
                isBusyLoadingPlantilla = value;
                RaisePropertyChanged(() => this.IsBusyLoadingPlantilla);
            }
        }

        #endregion Busy

        #region Filtro Areas

        private ObservableCollection<CheckableFilterModel> areas = new ObservableCollection<CheckableFilterModel>();

        public ObservableCollection<CheckableFilterModel> Areas
        {
            get { return areas; }
            set
            {
                areas = value;
                RaisePropertyChanged(() => this.Areas);
            }
        }

        private int areaSelectedIndex;

        public int AreaSelectedIndex
        {
            get { return areaSelectedIndex; }
            set
            {
                areaSelectedIndex = value;
                RaisePropertyChanged(() => this.AreaSelectedIndex);
            }
        }

        private decimal areaSelectedKey;

        public decimal AreaSelectedKey
        {
            get { return areaSelectedKey; }
            set
            {
                areaSelectedKey = value;
                RaisePropertyChanged(() => this.AreaSelectedKey);
            }
        }

        private string descripcionFiltroAreas;

        public string DescripcionFiltroAreas
        {
            get { return descripcionFiltroAreas; }
            set
            {
                descripcionFiltroAreas = value;
                RaisePropertyChanged(() => this.DescripcionFiltroAreas);
            }
        }

        public bool IsFilteringAreas
        {
            get
            {
                return !Areas.Any(x => x.ID == -1 && x.IsChecked);
            }
        }

        private bool profesionalEneable;

        public bool ProfesionalEneable
        {
            get { return profesionalEneable; }
            set
            {
                profesionalEneable = value;
                RaisePropertyChanged(() => this.ProfesionalEneable);
            }
        }

        private bool areaEneable;

        public bool AreaEneable
        {
            get { return areaEneable; }
            set
            {
                areaEneable = value;
                RaisePropertyChanged(() => this.AreaEneable);
            }
        }

        public List<CheckableFilterModel> SelectedAreas
        {
            get
            {
                return Areas.Any(x => x.ID == -1 && x.IsChecked) ? Areas.Where(x => x.ID != -1).ToList() : Areas.Where(x => x.ID != -1 && x.IsChecked).ToList();
            }
        }

        #endregion Filtro Areas

        #region Filtro Profesionales

        private ObservableCollection<CheckableFilterModel> profesionales = new ObservableCollection<CheckableFilterModel>();

        public ObservableCollection<CheckableFilterModel> Profesionales
        {
            get { return profesionales; }
            set
            {
                profesionales = value;
                RaisePropertyChanged(() => this.Profesionales);
            }
        }

        private int profesionalSelectedIndex;

        public int ProfesionalSelectedIndex
        {
            get { return profesionalSelectedIndex; }
            set
            {
                profesionalSelectedIndex = value;
                RaisePropertyChanged(() => this.ProfesionalSelectedIndex);
            }
        }

        private decimal profesionalSelectedKey;

        public decimal ProfesionalSelectedKey
        {
            get { return profesionalSelectedKey; }
            set
            {
                profesionalSelectedKey = value;
                RaisePropertyChanged(() => this.ProfesionalSelectedKey);
            }
        }

        private string descripcionFiltroProfesionales;

        public string DescripcionFiltroProfesionales
        {
            get { return descripcionFiltroProfesionales; }
            set
            {
                descripcionFiltroProfesionales = value;
                RaisePropertyChanged(() => this.DescripcionFiltroProfesionales);
            }
        }

        public bool IsFilteringProfesionales
        {
            get
            {
                return !Profesionales.Any(x => x.ID == -1 && x.IsChecked);
            }
        }

        public List<CheckableFilterModel> SelectedProfesionales
        {
            get
            {
                return Profesionales.Any(x => x.ID == -1 && x.IsChecked) ? Profesionales.Where(x => x.ID != -1).ToList() : Profesionales.Where(x => x.ID != -1 && x.IsChecked).ToList();
            }
        }

        #endregion Filtro Profesionales

        #region Filtro Fechas

        private DateTime fechaInicio;

        public DateTime FechaInicio
        {
            get { return fechaInicio; }
            set
            {
                fechaInicio = value;
                RaisePropertyChanged(() => FechaInicio);
            }
        }

        private DateTime fechaFin;

        public DateTime FechaFin
        {
            get { return fechaFin; }
            set
            {
                fechaFin = value;
                RaisePropertyChanged(() => FechaFin);
            }
        }

        private KeyValuePair<RangoFechasUtility.RangoFecha, string> filtroRangoFechas;

        public KeyValuePair<RangoFechasUtility.RangoFecha, string> FiltroRangoFechas
        {
            get { return filtroRangoFechas; }
            set
            {
                filtroRangoFechas = value;

                FechaFin = DateTime.Today;
                switch (FiltroRangoFechas.Key)
                {
                    case RangoFechasUtility.RangoFecha.Todo:
                        FechaInicio = DateTime.MinValue;
                        break;

                    case RangoFechasUtility.RangoFecha.UltimoAnio:
                        FechaInicio = DateTime.Today.AddYears(-1);
                        break;

                    case RangoFechasUtility.RangoFecha.UltimoMes:
                        FechaInicio = DateTime.Today.AddMonths(-1);
                        break;

                    case RangoFechasUtility.RangoFecha.UltimaSemana:
                        FechaInicio = DateTime.Today.AddDays(-7);
                        break;

                    case RangoFechasUtility.RangoFecha.Hoy:
                        FechaInicio = DateTime.Today;
                        break;

                    default:
                        break;
                }

                RaisePropertyChanged(() => FiltroRangoFechas);
            }
        }

        private bool tipoFechaCreacion;

        public bool TipoFechaCreacion
        {
            get { return tipoFechaCreacion; }
            set
            {
                tipoFechaCreacion = value;
                RaisePropertyChanged(() => TipoFechaCreacion);
            }
        }

        private bool tipoFechaModificacion;

        public bool TipoFechaModificacion
        {
            get { return tipoFechaModificacion; }
            set
            {
                tipoFechaModificacion = value;
                RaisePropertyChanged(() => TipoFechaModificacion);
            }
        }

        #endregion Filtro Fechas

        #region Plantillas

        private ObservableCollection<PlantillaEstadistica> plantillas = new ObservableCollection<PlantillaEstadistica>();

        public ObservableCollection<PlantillaEstadistica> Plantillas
        {
            get { return plantillas; }
            set
            {
                plantillas = value;
                RaisePropertyChanged(() => this.Plantillas);
            }
        }

        private decimal plantillaSelected;

        public decimal PlantillaSelected
        {
            get { return plantillaSelected; }
            set
            {
                plantillaSelected = value;
                RaisePropertyChanged(() => this.PlantillaSelected);
            }
        }

        private PlantillaEstadistica plantilla;

        public PlantillaEstadistica Plantilla
        {
            get { return plantilla; }
            set
            {
                plantilla = value;
                RaisePropertyChanged(() => this.Plantilla);
            }
        }

        private PlantillaPlana plantillaPlana;

        public PlantillaPlana PlantillaPlana
        {
            get { return plantillaPlana; }
            set
            {
                plantillaPlana = value;
                RaisePropertyChanged(() => this.PlantillaPlana);
            }
        }

        #endregion Plantillas

        #region Excel Export

        private DataTable dataToExport;

        public DataTable DataToExport
        {
            get { return dataToExport; }
            set
            {
                dataToExport = value;
                RaisePropertyChanged(() => this.DataToExport);
            }
        }

        #endregion Excel Export

        #region Private Methods

        private async void CallAsynchronousLoadFilters()
        {
            try
            {
                IsBusyLoadingFilters = true;

                var taskAreas = Task.Run(() => filtroService.GetAreas(ObtenerUsuarioIdComoDecimal()));
                var taskProfesionales = Task.Run(() => filtroService.GetProfesionales(ObtenerUsuarioIdComoDecimal()));

                var areas = await taskAreas;

                if (areas.HasElements())
                {
                    Areas = new ObservableCollection<CheckableFilterModel>(areas.Select(a => new CheckableFilterModel { ID = a.Id, Nombre = a.Nombre, IsChecked = false }));

                    if (Areas != null && Areas.Count > 1)
                        Areas.Insert(0, new CheckableFilterModel() { ID = -1M, IsChecked = true, Nombre = "Todas" });
                }

                var profesionales = await taskProfesionales;

                if (profesionales.HasElements())
                {
                    Profesionales = new ObservableCollection<CheckableFilterModel>(profesionales.Select(p => new CheckableFilterModel { ID = p.Id, Nombre = p.Nombre, IsChecked = false }));

                    var selfProfesional = Profesionales.FirstOrDefault(p => p.ID == ObtenerUsuarioIdComoDecimal());

                    if (selfProfesional != null)
                        selfProfesional.IsChecked = true;

                    if (Profesionales != null && Profesionales.Count > 1)
                        Profesionales.Insert(0, new CheckableFilterModel() { ID = -1M, IsChecked = (selfProfesional == null), Nombre = "Todos" });
                }

                ActualizarDescripcionFiltros();

                RaisePropertyChanged(() => this.IsFilteringAreas);
                RaisePropertyChanged(() => this.IsFilteringProfesionales);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error, " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsBusyLoadingFilters = false;
            }

            ObtenerPlantillas();
        }

        private void AplicarFiltro()
        {
            RaisePropertyChanged(() => this.IsFilteringAreas);
            RaisePropertyChanged(() => this.IsFilteringProfesionales);

            ActualizarDescripcionFiltros();

            ObtenerPlantillas();
        }

        private void RemoverFiltro()
        {
            foreach (var area in Areas)
                if (area.ID <= 0)
                    area.IsChecked = true;
                else
                    area.IsChecked = false;

            foreach (var pro in Profesionales)
                if (pro.ID <= 0)
                    pro.IsChecked = true;
                else
                    pro.IsChecked = false;

            CheckableFilterModel selfProfesional = this.Profesionales.Where(x => x.ID == ObtenerUsuarioIdComoDecimal()).FirstOrDefault();
            if (selfProfesional != null)
                selfProfesional.IsChecked = true;

            RaisePropertyChanged(() => this.IsFilteringAreas);
            RaisePropertyChanged(() => this.IsFilteringProfesionales);

            ActualizarDescripcionFiltros();

            this.FiltroRangoFechas = new KeyValuePair<RangoFechasUtility.RangoFecha, string>(RangoFechasUtility.RangoFecha.UltimoMes,
                                                                                 RangoFechasUtility.RangoFechas[RangoFechasUtility.RangoFecha.UltimoMes]);

            ObtenerPlantillas();
        }

        public void ActualizarDescripcionFiltros()
        {
            var descripcionFiltros = (Areas.Any(f => f.ID >= 0 && f.IsChecked) ?
                string.Join(", ", Areas.Where(f => f.ID >= 0 && f.IsChecked).Select(f => f.Nombre)) : "Todas");

            if (this.Areas.Count == 1)
            {
                descripcionFiltros = this.Areas[0].Nombre;
                this.Areas.FirstOrDefault().IsChecked = true;
                AreaEneable = false;
            }
            else
                AreaEneable = true;

            DescripcionFiltroAreas = (descripcionFiltros.Length > 68 ? descripcionFiltros.Substring(0, 66) + "..." : descripcionFiltros);

            descripcionFiltros = (Profesionales.Any(f => f.ID >= 0 && f.IsChecked) ?
                string.Join(", ", Profesionales.Where(f => f.ID >= 0 && f.IsChecked).Select(f => f.Nombre)) : "Todos");
            if (this.Profesionales.Count == 1)
            {
                descripcionFiltros = this.Profesionales[0].Nombre;
                this.Profesionales.FirstOrDefault().IsChecked = true;
                ProfesionalEneable = false;
            }
            else
                ProfesionalEneable = true;

            DescripcionFiltroProfesionales = (descripcionFiltros.Length > 68 ? descripcionFiltros.Substring(0, 66) + "..." : descripcionFiltros);
        }

        private async void ObtenerPlantillas()
        {
            try
            {
                IsBusyLoadingPlantillas = true;

                IEnumerable<decimal> selectedAreasIds = SelectedAreas.Select(x => x.ID);
                IEnumerable<decimal> selectedProfesionalsIds = SelectedProfesionales.Select(x => x.ID);

                var plantillasFromService = await Task.Run(() => plantillasService.GetPlantillas(selectedProfesionalsIds,
                                                                            selectedAreasIds,
                                                                            FechaInicio,
                                                                            FechaFin,
                                                                            (TipoFechaCreacion) ? (TipoFecha.Creacion) : (TipoFecha.UltimaModificacion)));

                if (!plantillasFromService.HasElements())
                {
                    Plantillas = null;
                    PlantillaSelected = 0;
                    Plantilla = null;
                    PlantillaPlana = null;
                    return;
                }

                Plantillas = new ObservableCollection<PlantillaEstadistica>(plantillasFromService.OrderBy(p => p.Descripcion));

                if (Plantillas.HasElements())
                {
                    PlantillaSelected = Plantillas.FirstOrDefault().Codigo;
                    Plantilla = await Task.Run(() => plantillasService.GetPlantilla(PlantillaSelected));
                    PlantillaPlana = PlantillaPlana.AplanarPlantilla(Plantilla);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsBusyLoadingPlantillas = false;
            }
        }

        private async void ObtenerPlantillaSelected()
        {
            if (PlantillaSelected < 1)
                return;

            try
            {
                IsBusyLoadingPlantilla = true;

                Plantilla = await Task.Run(() => plantillasService.GetPlantilla(PlantillaSelected));
                PlantillaPlana = PlantillaPlana.AplanarPlantilla(Plantilla);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsBusyLoadingPlantilla = false;
            }
        }

        private async void ExportarExcel()
        {
            IsBusyLoadingPlantillas = true;
            DataTable dtData = new DataTable();

            try
            {
                dtData.Columns.Add(new DataColumn("Paciente", typeof(string)));

                //Cada item seleccionado como "Todo" o "Último Resultado" se agrega como columna de la DataTable a exportar
                foreach (var item in PlantillaPlana.Items.Where(i => i.Tipo == PlantillaPlanaItem.PlantillaPlanaItemTipo.Variable))
                {
                    if (item.Todo || item.UltimoResultado)
                    {
                        AddModuleAsColumn(dtData, item);

                        if (!dtData.Columns.Contains("V" + item.Id.ToString()))
                            dtData.Columns.Add(new DataColumn("V" + item.Id.ToString(), typeof(string)));
                    }
                }

                if (dtData.Columns.Count <=  1)
                {
                    MessageBox.Show("Debe selecionar al menos un item (Todo o Último Resultado).");
                    return;
                }

                var taskPacientesPlantilla = GetPacientesPlantilla();
                var taskPlantilla = Task.Run(() => plantillasService.GetPlantilla(PlantillaSelected));

                //Se obtiene la lista de pacientes que tienen alguna incidencia registrada para algún módulo de la planilla seleccionada con los filtros seleccionados
                IEnumerable<Paciente> pacientes = await taskPacientesPlantilla;

                IEnumerable<IncidenciaModulo> incidenciasModulos;
                IEnumerable<ValorVariable>[] valoresArray;
                IEnumerable<ValorVariable> todosValores;
                IEnumerable<ValorVariable> valoresIncidencia;
                PlantillaPlanaItem agrupadorValores;
                decimal idModulo;
                DataRow dtRow;

                if (pacientes.HasElements())
                {
                    var incidenciasArray = await Task.WhenAll(pacientes.Select(p => GetIncidenciasModulos(p.IdPaciente)));

                    if (incidenciasArray.Any(inc => inc.HasElements()))
                    {
                        var todasIncidencias = incidenciasArray.Where(inc => inc.HasElements()).SelectMany(inc => inc);

                        foreach (var paciente in pacientes)
                        {
                            //Se obtiene la lista de incidencias de módulos para el paciente con la planilla seleccionada y con los filtros seleccionados
                            incidenciasModulos = todasIncidencias.Where(i => i.IdPaciente == paciente.IdPaciente);

                            if (incidenciasModulos.HasElements())
                            {
                                valoresArray = await Task.WhenAll(incidenciasModulos.Select(i => GetValoresVariablesIncidenciaModuloAsync(i.IdPlantilla)));

                                if (valoresArray.Any(val => val.HasElements()))
                                {
                                    todosValores = valoresArray.Where(val => val.HasElements()).SelectMany(val => val);

                                    foreach (var incidenciaModulo in incidenciasModulos)
                                    {
                                        valoresIncidencia = todosValores.Where(v => v.IdModuloIncidencia == incidenciaModulo.IdPlantilla);

                                        if (valoresIncidencia == null)
                                            continue;

                                        valoresIncidencia = valoresIncidencia.OrderBy(v => v.IdAgrupador);

                                        agrupadorValores = PlantillaPlana.Items.FirstOrDefault(i => i.Tipo == PlantillaPlanaItem.PlantillaPlanaItemTipo.Agrupador && i.Id == valoresIncidencia.First().IdAgrupador);

                                        if (agrupadorValores == null)
                                            continue;

                                        idModulo = agrupadorValores.ParentId;

                                        //Agregar la fecha de la incidencia del módulo si fue seleccionado y luego los valores de las variables seleccionadas del módulo
                                        if (dtData.Columns.Contains("M" + idModulo.ToString()))
                                        {
                                            dtRow = dtData.NewRow();
                                            dtRow["Paciente"] = paciente.Nombre;
                                            dtRow["M" + idModulo.ToString()] = incidenciaModulo.Fecha;

                                            foreach (var valor in valoresIncidencia)
                                            {
                                                if (dtData.Columns.Contains("V" + valor.IdVariable.ToString()))
                                                    dtRow["V" + valor.IdVariable.ToString()] = valor.Valor;
                                            }

                                            dtData.Rows.Add(dtRow);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //Traducir los ids de módulos y variables a sus nombres y nombres a exportar
                //Como puede haber nombres de columnas repetidos (distintos módulos y variables con mismo nombre) La traducción se agrega como una fila al inicio
                PlantillaEstadistica plantilla = await taskPlantilla;

                decimal currentModuleId = 0;
                Modulo currentModule;
                DataRow drTranslation = dtData.NewRow();
                Variable variable;

                for (int i = 1; i < dtData.Columns.Count; i++)
                {
                    if (dtData.Columns[i].ColumnName.StartsWith("M"))
                    {
                        currentModuleId = Convert.ToDecimal(dtData.Columns[i].ColumnName.Substring(1));
                        drTranslation[i] = plantilla.Modulos.First(x => x.Codigo == currentModuleId).Nombre;
                    }
                    else if (dtData.Columns[i].ColumnName.StartsWith("V"))
                    {
                        currentModule = plantilla.Modulos.First(x => x.Codigo == currentModuleId);
                        foreach (var agrupador in currentModule.Agrupadores)
                        {
                            variable = agrupador.Variables.FirstOrDefault(x => x.Codigo == Convert.ToDecimal(dtData.Columns[i].ColumnName.Substring(1)));

                            if (variable != null)
                            {
                                drTranslation[i] = agrupador.Variables.First(x => x.Codigo == Convert.ToDecimal(dtData.Columns[i].ColumnName.Substring(1))).NombreExportar;
                                break;
                            }
                        }
                    }
                }
   
                dtData.Rows.InsertAt(drTranslation, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error, " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsBusyLoadingPlantillas = false;
            }

            DataToExport = dtData;
        }

        private Task<IEnumerable<Paciente>> GetPacientesPlantilla()
        {
            IEnumerable<decimal> selectedAreasIds = SelectedAreas.Select(x => x.ID);
            IEnumerable<decimal> selectedProfesionalsIds = SelectedProfesionales.Select(x => x.ID);

            return Task.Run(() => plantillasService.GetPacientesPlantilla(PlantillaSelected, selectedProfesionalsIds, selectedAreasIds, FechaInicio, FechaFin, TipoFechaCreacion ? 1 : 0));
        }

        private Task<IEnumerable<IncidenciaModulo>> GetIncidenciasModulos(decimal idPaciente)
        {
            IEnumerable<decimal> selectedAreasIds = SelectedAreas.Select(x => x.ID);
            IEnumerable<decimal> selectedProfesionalsIds = SelectedProfesionales.Select(x => x.ID);

            return Task.Run(() => plantillasService.GetIncidenciasModulos(PlantillaSelected, idPaciente, selectedProfesionalsIds, selectedAreasIds, FechaInicio, FechaFin, TipoFechaCreacion ? 1 : 0));
        }

        private Task<IEnumerable<ValorVariable>> GetValoresVariablesIncidenciaModuloAsync(decimal idIncidencia)
        {
            return Task.Run(() => plantillasService.GetValoresVariablesIncidenciaModulo(idIncidencia));
        }

        private void AddModuleAsColumn(DataTable dtData, PlantillaPlanaItem variable)
        {
            if (variable == null || variable.ParentId < 1)
                return;

            //Obtener el módulo (padre del padre) y si no está su nombre en la lista de columnas -> agregarlo
            var agrupador = PlantillaPlana.Items.FirstOrDefault(i => i.Tipo == PlantillaPlanaItem.PlantillaPlanaItemTipo.Agrupador && i.Id == variable.ParentId);

            if (agrupador == null || agrupador.ParentId < 1)
                return;

            if (!(dtData.Columns.Contains("M" + agrupador.ParentId.ToString())))
                dtData.Columns.Add(new DataColumn("M" + agrupador.ParentId.ToString(), typeof(string)));
        }

        private decimal ObtenerUsuarioIdComoDecimal()
        {
            decimal userId;
            decimal.TryParse(SecurityManager.CurrentUser.NucleoIdentity.UsuarioId, out userId);

            return userId;
        }

        #endregion Private Methods
    }

    public class PlantillaPlana
    {
        public IEnumerable<PlantillaPlanaItem> Items { get; set; }

        public PlantillaPlana()
        {
            Items = new List<PlantillaPlanaItem>();
        }

        public static PlantillaPlana AplanarPlantilla(PlantillaEstadistica plantilla)
        {
            List<PlantillaPlanaItem> listItems;

            listItems = new List<PlantillaPlanaItem>();
            listItems.Add(new PlantillaPlanaItem(plantilla.Codigo,
                                                 plantilla.Descripcion,
                                                 "Todo",
                                                 "Último Resultado",
                                                 false,
                                                 false,
                                                 0,
                                                 PlantillaPlanaItem.PlantillaPlanaItemTipo.Plantilla));

            foreach (Modulo modulo in plantilla.Modulos)
            {
                listItems.Add(new PlantillaPlanaItem(modulo.Codigo,
                                                     modulo.Nombre,
                                                     "Todo",
                                                     "Último Resultado",
                                                     false,
                                                     false,
                                                     plantilla.Codigo,
                                                     PlantillaPlanaItem.PlantillaPlanaItemTipo.Modulo));

                foreach (Agrupador agrupador in modulo.Agrupadores)
                {
                    listItems.Add(new PlantillaPlanaItem(agrupador.Codigo,
                                                         agrupador.Nombre,
                                                         "Todo",
                                                         "Último Resultado",
                                                         false,
                                                         false,
                                                         modulo.Codigo,
                                                         PlantillaPlanaItem.PlantillaPlanaItemTipo.Agrupador));

                    foreach (Variable variable in agrupador.Variables)
                    {
                        listItems.Add(new PlantillaPlanaItem(variable.Codigo,
                                                             variable.Nombre,
                                                             string.Empty,
                                                             string.Empty,
                                                             false,
                                                             false,
                                                             agrupador.Codigo,
                                                             PlantillaPlanaItem.PlantillaPlanaItemTipo.Variable));
                    }
                }
            }

            PlantillaPlana plantillaAplanada = new PlantillaPlana();
            plantillaAplanada.Items = listItems;
            return plantillaAplanada;
        }
    }

    public class PlantillaPlanaItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public enum PlantillaPlanaItemTipo
        {
            [Description("Plantilla")]
            Plantilla = 1,

            [Description("Modulo")]
            Modulo = 2,

            [Description("Agrupador")]
            Agrupador = 3,

            [Description("Variable")]
            Variable = 4
        }

        public decimal Id { get; set; }

        public string Nombre { get; set; }

        public string TextoTodo { get; set; }

        public string TextoUltimoResultado { get; set; }

        private bool todo;

        public bool Todo
        {
            get { return todo; }
            set
            {
                todo = value;
                OnPropertyChanged("Todo");
            }
        }

        private bool ultimoResultado;

        public bool UltimoResultado
        {
            get { return ultimoResultado; }
            set
            {
                ultimoResultado = value;
                OnPropertyChanged("UltimoResultado");
            }
        }

        public PlantillaPlanaItemTipo Tipo { get; set; }

        public decimal ParentId { get; set; }

        public PlantillaPlanaItem()
        {
        }

        public PlantillaPlanaItem(decimal id, string nombre, string textoTodo, string textoUltimoResultado, bool todo, bool ultimoResultado, decimal parentId, PlantillaPlanaItemTipo tipo)
        {
            Id = id;
            Nombre = nombre;
            TextoTodo = textoTodo;
            TextoUltimoResultado = textoUltimoResultado;
            Todo = todo;
            UltimoResultado = ultimoResultado;
            ParentId = parentId;
            Tipo = tipo;
        }
    }
}