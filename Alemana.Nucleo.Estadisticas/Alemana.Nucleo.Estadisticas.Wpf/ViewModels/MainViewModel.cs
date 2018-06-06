using Alemana.Nucleo.Common.ComponentModel;
using Alemana.Nucleo.Infrastructure.Helpers;
using Alemana.Nucleo.Infrastructure.Models;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition;
using System.Windows;

namespace Alemana.Nucleo.Estadisticas.Wpf.ViewModels
{
    [Export]
    public class MainViewModel : ViewModelBase
    {
        private FrameworkElement contenidoUsoHCE;
        private FrameworkElement contenidoPlantillas;

        private IRegionManager regionManager;
        private IComponentContainer componentContainer;
        private IEventAggregator eventAggregator;
        private ModalDialogHelper modalDialogHelper;

        [ImportingConstructor]
        public MainViewModel(IRegionManager regionManager, IComponentContainer componentContainer, IEventAggregator eventAggregator, ModalDialogHelper modalDialogHelper)
        {
            ContenidoUsoHCE = new Alemana.Nucleo.Estadisticas.Wpf.Views.UsoHCE(new UsoHCEViewModel(componentContainer, modalDialogHelper, eventAggregator));
            ContenidoPlantillas = new Alemana.Nucleo.Estadisticas.Wpf.Views.Plantilla(new PlantillaViewModel(componentContainer));

            this.regionManager = regionManager;
            this.componentContainer = componentContainer;
            this.eventAggregator = eventAggregator;
            this.modalDialogHelper = modalDialogHelper;
        }

        public override string Title
        {
            get { return "Estadísticas"; }
        }

        public FrameworkElement ContenidoUsoHCE
        {
            get { return contenidoUsoHCE; }
            set
            {
                contenidoUsoHCE = value;
                RaisePropertyChanged(() => ContenidoUsoHCE);
            }
        }

        public FrameworkElement ContenidoPlantillas
        {
            get { return contenidoPlantillas; }
            set
            {
                contenidoPlantillas = value;
                RaisePropertyChanged(() => ContenidoPlantillas);
            }
        }
    }
}