using Alemana.Nucleo.Common.ComponentModel;
using Alemana.Nucleo.Common.Extensions;
using Alemana.Nucleo.Common.Security;
using Alemana.Nucleo.Estadisticas.Contrato.ServiceInterfaces;
using Alemana.Nucleo.Estadisticas.Wpf.Resources;
using Alemana.Nucleo.Infrastructure.Helpers;
using Alemana.Nucleo.Infrastructure.Models;
using Alemana.Nucleo.Shared.Entities;
using Alemana.Nucleo.Shared.Events;
using Alemana.Nucleo.Shared.Events.EstadisticaEvents;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Alemana.Nucleo.Estadisticas.Wpf.ViewModels
{
    public class UsoHCEViewModel : ViewModelBase
    {
        private readonly IFiltrosServices filtroService;
        private readonly IIndicadoresServices indicadoresService;
        private readonly IEventAggregator eventAggregator;

        private ModalDialogHelper modalDialogHelper;

        public UsoHCEViewModel(IComponentContainer componentContainer, ModalDialogHelper modalDialogHelper, IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            this.filtroService = componentContainer.Resolve<IFiltrosServices>();
            this.indicadoresService = componentContainer.Resolve<IIndicadoresServices>();

            this.modalDialogHelper = modalDialogHelper;

            this.AplicarFiltroCommand = new DelegateCommand(AplicarFiltro);
            this.RemoverFiltroCommand = new DelegateCommand(RemoverFiltro);
            this.MostrarListadoPacientesDiagnosticadosCommand = new DelegateCommand<decimal?>(MostrarListadoPacientesDiagnosticados);

            this.FiltroRangoFechas = new KeyValuePair<RangoFechasUtility.RangoFecha, string>(RangoFechasUtility.RangoFecha.Hoy,
                                                                                             RangoFechasUtility.RangoFechas[RangoFechasUtility.RangoFecha.Hoy]);

            CallAsynchronousLoadFilters();

            EstadisticaPacientesChangedEvent recargaPacientes = this.eventAggregator.GetEvent<EstadisticaPacientesChangedEvent>();
            EtiquetasChangedEvent recargaEtiquetas = this.eventAggregator.GetEvent<EtiquetasChangedEvent>();

            recargaPacientes.Subscribe(OnIndicadorPacientes);
            recargaEtiquetas.Subscribe(OnIndicadorPacientes);
        }

        public override string Title
        {
            get { return "Estadísticas Uso HCE"; }
        }

        public DelegateCommand AplicarFiltroCommand { get; private set; }

        public DelegateCommand RemoverFiltroCommand { get; private set; }

        public DelegateCommand<decimal?> MostrarListadoPacientesDiagnosticadosCommand { get; private set; }

        #region Busy

        public bool IsBusy
        {
            get { return IsBusyLoadingFilters || IsBusyLoadingIndicadores; }
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

        private bool isBusyLoadingIndicadores;

        public bool IsBusyLoadingIndicadores
        {
            get { return isBusyLoadingIndicadores; }
            set
            {
                isBusyLoadingIndicadores = value;
                RaisePropertyChanged(() => this.IsBusyLoadingIndicadores);
                RaisePropertyChanged(() => this.IsBusy);
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

        public List<CheckableFilterModel> SelectedAreas
        {
            get
            {
                List<CheckableFilterModel> areas = Areas.Any(x => x.ID == -1 && x.IsChecked) ? Areas.Where(x => x.ID != -1).ToList() : Areas.Where(x => x.ID != -1 && x.IsChecked).ToList();
                return areas;
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
                List<CheckableFilterModel> profs = Profesionales.Any(x => x.ID == -1 && x.IsChecked) ? Profesionales.Where(x => x.ID != -1).ToList() : Profesionales.Where(x => x.ID != -1 && x.IsChecked).ToList();
                return profs;
            }
        }

        #endregion Filtro Profesionales

        #region Filtro Fechas

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

        #endregion Filtro Fechas

        #region Grillas

        private ObservableCollection<IndicadorItem> misPacientes = new ObservableCollection<IndicadorItem>();

        public ObservableCollection<IndicadorItem> MisPacientes
        {
            get { return misPacientes; }
            set
            {
                misPacientes = value;
                RaisePropertyChanged(() => this.MisPacientes);
            }
        }

        private ObservableCollection<IndicadorItem> canalesVirtuales = new ObservableCollection<IndicadorItem>();

        public ObservableCollection<IndicadorItem> CanalesVirtuales
        {
            get { return canalesVirtuales; }
            set
            {
                canalesVirtuales = value;
                RaisePropertyChanged(() => this.CanalesVirtuales);
            }
        }

        private ObservableCollection<IndicadorItem> indicaciones = new ObservableCollection<IndicadorItem>();

        public ObservableCollection<IndicadorItem> Indicaciones
        {
            get { return indicaciones; }
            set
            {
                indicaciones = value;
                RaisePropertyChanged(() => this.Indicaciones);
            }
        }

        private ObservableCollection<IndicadorItem> diagnosticos = new ObservableCollection<IndicadorItem>();

        public ObservableCollection<IndicadorItem> Diagnosticos
        {
            get { return diagnosticos; }
            set
            {
                diagnosticos = value;
                RaisePropertyChanged(() => this.Diagnosticos);
            }
        }

        #endregion Grillas

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
                MessageBox.Show("Ha ocurrido un error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsBusyLoadingFilters = false;
            }

            BuscarIndicadores();
        }

        private void AplicarFiltro()
        {
            RaisePropertyChanged(() => this.IsFilteringAreas);
            RaisePropertyChanged(() => this.IsFilteringProfesionales);

            ActualizarDescripcionFiltros();

            BuscarIndicadores();
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

            CheckableFilterModel selfProfesional = this.Profesionales.FirstOrDefault(x => x.ID == ObtenerUsuarioIdComoDecimal());

            if (selfProfesional != null)
                selfProfesional.IsChecked = true;

            RaisePropertyChanged(() => this.IsFilteringAreas);
            RaisePropertyChanged(() => this.IsFilteringProfesionales);

            ActualizarDescripcionFiltros();

            this.FiltroRangoFechas = new KeyValuePair<RangoFechasUtility.RangoFecha, string>(RangoFechasUtility.RangoFecha.UltimoMes,
                                                                                 RangoFechasUtility.RangoFechas[RangoFechasUtility.RangoFecha.UltimoMes]);

            BuscarIndicadores();
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

        private async void BuscarIndicadores()
        {
            try
            {
                IsBusyLoadingIndicadores = true;

                IEnumerable<decimal> selectedAreasIds = SelectedAreas.Select(x => x.ID);
                IEnumerable<decimal> selectedProfesionalsIds = SelectedProfesionales.Select(x => x.ID);

                var taskIndicadoresMisPacientes = Task.Run(() => indicadoresService.GetIndicadoresPacientes(selectedProfesionalsIds, selectedAreasIds, FechaInicio, FechaFin));
                var taskIndicadoresCanalesVirtuales = Task.Run(() => indicadoresService.GetIndicadoresCanalesVirtuales(selectedProfesionalsIds, selectedAreasIds, FechaInicio, FechaFin));
                var taskIndicadoresIndicaciones = Task.Run(() => indicadoresService.GetIndicadores(selectedProfesionalsIds, selectedAreasIds, FechaInicio, FechaFin));
                var taskIndicadoresDiagnosticos = Task.Run(() => indicadoresService.GetIndicadoresDiagnostico(selectedProfesionalsIds, selectedAreasIds, FechaInicio, FechaFin));

                var indicadoresMisPacientes = await taskIndicadoresMisPacientes;
                MisPacientes = new ObservableCollection<IndicadorItem>(
                    new IndicadorItem[]{
                    new IndicadorItem() { Indicador = "Atendidos", Valor = (int)indicadoresMisPacientes.Atendidos },
                    new IndicadorItem() { Indicador = "Etiquetados", Valor = (int)indicadoresMisPacientes.Etiquetados }
                });

                var indicadoresCanalesVirtuales = await taskIndicadoresCanalesVirtuales;
                CanalesVirtuales = new ObservableCollection<IndicadorItem>(
                    new IndicadorItem[]{
                    new IndicadorItem() { Indicador = "Total Mensajes", Valor = (int)indicadoresCanalesVirtuales.TotalMensajes, ValorAuxiliar = indicadoresCanalesVirtuales.TotalMensajesRespuesta },
                    new IndicadorItem() { Indicador = "CRM", Valor = (int)indicadoresCanalesVirtuales.CRM, ValorAuxiliar = indicadoresCanalesVirtuales.CRMRespuesta },
                    new IndicadorItem() { Indicador = "RCE", Valor = (int)indicadoresCanalesVirtuales.RCE, ValorAuxiliar = indicadoresCanalesVirtuales.RCERespuesta },
                    new IndicadorItem() { Indicador = "Portal Pacientes", Valor = (int)indicadoresCanalesVirtuales.PortalPacientes, ValorAuxiliar = indicadoresCanalesVirtuales.PortalPacientesRespuesta }
                });

                var indicadoresIndicaciones = await taskIndicadoresIndicaciones;
                Indicaciones = new ObservableCollection<IndicadorItem>(
                    new IndicadorItem[]{
                    new IndicadorItem() { Indicador = "Farmacológicas", Valor = (int)indicadoresIndicaciones.Farmacologia },
                    new IndicadorItem() { Indicador = "Imagenología", Valor = (int)indicadoresIndicaciones.Imagenologia },
                    new IndicadorItem() { Indicador = "Laboratorio", Valor = (int)indicadoresIndicaciones.Laboratorio }
                });

                var indicadoresDiagnosticos = await taskIndicadoresDiagnosticos;
                Diagnosticos = new ObservableCollection<IndicadorItem>(indicadoresDiagnosticos.Enum().Select(
                    i => new IndicadorItem
                    {
                        Indicador = i.Nombre,
                        Valor = (int)i.Numero,
                        ValorAuxiliar = i.Id
                    }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsBusyLoadingIndicadores = false;
            }
        }

        private async void OnIndicadorPacientes(object data)
        {
            try
            {
                IsBusyLoadingIndicadores = true;

                IEnumerable<decimal> selectedAreasIds = SelectedAreas.Select(x => x.ID);
                IEnumerable<decimal> selectedProfesionalsIds = SelectedProfesionales.Select(x => x.ID);

                var indicadoresMisPacientes = await Task.Run(() => indicadoresService.GetIndicadoresPacientes(selectedProfesionalsIds, selectedAreasIds, FechaInicio, FechaFin));

               // var indicadores = await Task.Run(() => indicadoresService.GetIndicadoresPacientesAtendidos(selectedProfesionalsIds, selectedAreasIds, FechaInicio, FechaFin));


                if (indicadoresMisPacientes != null)
                    MisPacientes = new ObservableCollection<IndicadorItem>(new IndicadorItem[]{
                        new IndicadorItem() { Indicador = "Atendidos", Valor = (int)indicadoresMisPacientes.Atendidos },
                        new IndicadorItem() { Indicador = "Etiquetados", Valor = (int)indicadoresMisPacientes.Etiquetados }
                    });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsBusyLoadingIndicadores = false;
            }
        }

        private async void MostrarListadoPacientesDiagnosticados(decimal? indicador)
        {
            try
            {
                IsBusyLoadingIndicadores = true;

                IEnumerable<decimal> selectedAreasIds = SelectedAreas.Select(x => x.ID);
                IEnumerable<decimal> selectedProfesionalsIds = SelectedProfesionales.Select(x => x.ID);

                var pacientes = await Task.Run(() => indicadoresService.GetPacientesDiagnosticados(selectedProfesionalsIds, selectedAreasIds, FechaInicio, FechaFin, indicador.Value));

                var resp = modalDialogHelper.ShowModalDialog("Estadisticas.PacientesDiagnosticados",
                                                             0,
                                                             0,
                                                             System.Windows.ResizeMode.NoResize,
                                                             System.Windows.SizeToContent.WidthAndHeight,
                                                             pacientes);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsBusyLoadingIndicadores = false;
            }
        }

        private decimal ObtenerUsuarioIdComoDecimal()
        {
            decimal userId;
            decimal.TryParse(SecurityManager.CurrentUser.NucleoIdentity.UsuarioId, out userId);

            return userId;
        }

        #endregion Private Methods
    }

    public class IndicadorItem
    {
        public string Indicador { get; set; }

        public int Valor { get; set; }

        public decimal ValorAuxiliar { get; set; }
    }
}