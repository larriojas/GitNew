using Alemana.Nucleo.Common.ComponentModel;
using Alemana.Nucleo.Estadisticas.Contrato.ServiceInterfaces;
using Alemana.Nucleo.Estadisticas.Servicio.Implementation;
using Alemana.Nucleo.Shared.Contrato.ServiceInterfaces;
using Alemana.Nucleo.Shared.Servicio.Implementation;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition;

namespace Alemana.Nucleo.Estadisticas.Wpf
{
    [ModuleExport(typeof(EstadisticasModule))]
    public class EstadisticasModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IComponentContainer componentContainer;

        [ImportingConstructor]
        public EstadisticasModule(IRegionManager regionManager, IComponentContainer componentContainer)
        {
            this.regionManager = regionManager;
            this.componentContainer = componentContainer;
        }

        public void Initialize()
        {
            this.componentContainer.Register<IFiltrosServices, FiltrosServices>();
            this.componentContainer.Register<IIndicadoresServices, IndicadoresServices>();
            this.componentContainer.Register<IListaLinealService, ListaLinealService>();
            this.componentContainer.Register<IExportarPlantillaService, ExportarPlantillaService>();
        }
    }
}