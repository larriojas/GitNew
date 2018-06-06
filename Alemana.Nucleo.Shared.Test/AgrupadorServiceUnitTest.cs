using Alemana.Nucleo.Common.ComponentModel;
using Alemana.Nucleo.Shared.Contrato.Models;
using Alemana.Nucleo.Shared.Contrato.ServiceInterfaces;
using Alemana.Nucleo.Shared.Servicio.Implementation;
using Alemana.Nucleo.Shared.Servicio.MocksImplementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Alemana.Nucleo.Shared.Test
{
    [TestClass]
    public class AgrupadorServiceUnitTest
    {
        private ComponentContainer componentContainer;
        private IModuloService iModuloService;
        private IAgrupadorService iAgrupadorService;
        private IPlantillaService iPlantillaService;
        private ICategoriasService iCategoriaService;

        public AgrupadorServiceUnitTest()
        {
            this.componentContainer = new ComponentContainer();
            this.componentContainer.Register<IAgrupadorService, AgrupadorService>();

            this.componentContainer.Register<IModuloService, ModuloService>();
            this.componentContainer.Register<IPlantillaService, PlantillaService>();
            this.componentContainer.Register<ICategoriasService, CategoriaService>();

            this.iAgrupadorService = componentContainer.Resolve<IAgrupadorService>();
            this.iModuloService = componentContainer.Resolve<IModuloService>();
            this.iPlantillaService = componentContainer.Resolve<IPlantillaService>();
            this.iCategoriaService = componentContainer.Resolve<ICategoriasService>();
        }

        [TestMethod]
        public void GetAgrupadorUnitTest()
        {
            var agrupador1 = MockDataHelper.Agrupadores.FirstOrDefault();

            var modulo = MockDataHelper.Modulos.FirstOrDefault();
            var plantilla = MockDataHelper.Plantillas.FirstOrDefault();
            var categoria = MockDataHelper.Categorias.FirstOrDefault();

            categoria.Codigo = 0;
            categoria.IdEmpresa = 11;
            categoria.Vigencia = Vigencia.NoVigente;

            plantilla.Codigo = 0;
            plantilla.Vigencia = Vigencia.NoVigente;

            modulo.Codigo = 0;
            modulo.Vigencia = Vigencia.NoVigente;

            var idcategoria = this.iCategoriaService.CreateOrUpdateCategoria(11, categoria);

            plantilla.IdCategoria = idcategoria;
            plantilla.Ambitos = MockDataHelper.GetListAmbito();

            var idPlantilla = this.iPlantillaService.CreateOrUpdatePlantilla(11, plantilla);

            modulo.IdPlantilla = idPlantilla;

            var idModulo = this.iModuloService.CreateOrUpdateModulo(11, modulo);

            agrupador1.Codigo = 0;
            agrupador1.IdModulo = idModulo;

            var id = this.iAgrupadorService.CreateOrUpdateAgrupador(11, agrupador1);

            Assert.IsTrue(id > 0);

            var agrupador2 = this.iAgrupadorService.GetAgrupador(id);

            Assert.IsTrue(agrupador1.AlineacionAyuda == agrupador2.AlineacionAyuda);
            Assert.IsTrue(agrupador1.IdModulo == agrupador2.IdModulo);
            Assert.IsTrue(agrupador1.Nombre == agrupador2.Nombre);
            Assert.IsTrue(agrupador1.IsNombreVisible == agrupador2.IsNombreVisible);
            Assert.IsTrue(agrupador1.Orden == agrupador2.Orden);
            Assert.IsTrue(agrupador1.TextoAyuda == agrupador2.TextoAyuda);
            Assert.IsTrue(agrupador1.Vigencia == agrupador2.Vigencia);
        }

        [TestMethod]
        public void CreateOrUpdateAgrupadorTest()
        {
            var agrupador1 = MockDataHelper.Agrupadores.FirstOrDefault();

            var modulo = MockDataHelper.Modulos.FirstOrDefault();
            var plantilla = MockDataHelper.Plantillas.FirstOrDefault();
            var categoria = MockDataHelper.Categorias.FirstOrDefault();

            categoria.Codigo = 0;
            categoria.IdEmpresa = 11;
            categoria.Vigencia = Vigencia.NoVigente;

            plantilla.Codigo = 0;
            plantilla.Vigencia = Vigencia.NoVigente;

            modulo.Codigo = 0;
            modulo.Vigencia = Vigencia.NoVigente;

            var idcategoria = this.iCategoriaService.CreateOrUpdateCategoria(11, categoria);

            plantilla.IdCategoria = idcategoria;
            plantilla.Ambitos = MockDataHelper.GetListAmbito();

            var idPlantilla = this.iPlantillaService.CreateOrUpdatePlantilla(11, plantilla);

            modulo.IdPlantilla = idPlantilla;

            var idModulo = this.iModuloService.CreateOrUpdateModulo(11, modulo);

            agrupador1.Codigo = 0;
            agrupador1.IdModulo = idModulo;

            var id = this.iAgrupadorService.CreateOrUpdateAgrupador(11, agrupador1);

            Assert.IsTrue(id > 0);

            var agrupador2 = this.iAgrupadorService.GetAgrupador(id);

            Assert.IsNotNull(agrupador2);
            Assert.IsTrue(agrupador1.AlineacionAyuda == agrupador2.AlineacionAyuda);
            Assert.IsTrue(agrupador1.IdModulo == agrupador2.IdModulo);
            Assert.IsTrue(agrupador1.Nombre == agrupador2.Nombre);
            Assert.IsTrue(agrupador1.IsNombreVisible == agrupador2.IsNombreVisible);
            Assert.IsTrue(agrupador1.Orden == agrupador2.Orden);
            Assert.IsTrue(agrupador1.TextoAyuda == agrupador2.TextoAyuda);
            Assert.IsTrue(agrupador1.Vigencia == agrupador2.Vigencia);

            agrupador2.AlineacionAyuda = agrupador2.AlineacionAyuda + 1;

            agrupador2.Nombre = agrupador2.Nombre + "modificado";
            agrupador2.IsNombreVisible = !agrupador2.IsNombreVisible;
            agrupador2.Orden = agrupador2.Orden + 1;
            agrupador2.TextoAyuda = agrupador2.TextoAyuda + "modificado";
            agrupador2.Vigencia = Vigencia.Vigente;

            id = this.iAgrupadorService.CreateOrUpdateAgrupador(11, agrupador2);

            Assert.IsTrue(id > 0);

            var agrupador3 = this.iAgrupadorService.GetAgrupador(id);

            Assert.IsNotNull(agrupador3);
            Assert.IsTrue(agrupador3.AlineacionAyuda == agrupador2.AlineacionAyuda);
            Assert.IsTrue(agrupador3.IdModulo == agrupador2.IdModulo);
            Assert.IsTrue(agrupador3.Nombre == agrupador2.Nombre);
            Assert.IsTrue(agrupador3.IsNombreVisible == agrupador2.IsNombreVisible);
            Assert.IsTrue(agrupador3.Orden == agrupador2.Orden);
            Assert.IsTrue(agrupador3.TextoAyuda == agrupador2.TextoAyuda);
            Assert.IsTrue(agrupador3.Vigencia == agrupador2.Vigencia);

            agrupador1.Vigencia = Vigencia.NoVigente;
            agrupador2.Vigencia = Vigencia.NoVigente;

            this.iAgrupadorService.CreateOrUpdateAgrupador(11, agrupador1);
            this.iAgrupadorService.CreateOrUpdateAgrupador(11, agrupador2);
        }
    }
}