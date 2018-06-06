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
    public class ModuloServiceUnitTest
    {
        private ComponentContainer componentContainer;
        private IModuloService iModuloService;
        private IPlantillaService iPlantillaService;
        private ICategoriasService iCategoriaService;
        private ISeguridadService iSeguridadService;

        public ModuloServiceUnitTest()
        {
            this.componentContainer = new ComponentContainer();

            this.componentContainer.Register<IModuloService, ModuloService>();
            this.componentContainer.Register<IPlantillaService, PlantillaService>();
            this.componentContainer.Register<ICategoriasService, CategoriaService>();
            this.componentContainer.Register<ISeguridadService, SeguridadService>();

            this.iModuloService = componentContainer.Resolve<IModuloService>();
            this.iPlantillaService = componentContainer.Resolve<IPlantillaService>();
            this.iCategoriaService = componentContainer.Resolve<ICategoriasService>();
            this.iSeguridadService = componentContainer.Resolve<ISeguridadService>();
        }

        [TestMethod]
        public void CreateModuloTest()
        {
            var modulo = MockDataHelper.Modulos.FirstOrDefault();
            var plantilla = MockDataHelper.Plantillas.FirstOrDefault();
            var categoria = MockDataHelper.Categorias.FirstOrDefault();

            var empresas = iSeguridadService.GetProfesionales("nico", 10);

            modulo.Codigo = 0;
            plantilla.Codigo = 0;
            categoria.Codigo = 0;
            categoria.IdEmpresa = 9;

            var idcategoria = this.iCategoriaService.CreateOrUpdateCategoria(1884, categoria);

            plantilla.IdCategoria = idcategoria;
            plantilla.Ambitos = MockDataHelper.GetListAmbito();

            var idPlantilla = this.iPlantillaService.CreateOrUpdatePlantilla(1884, plantilla);

            modulo.IdPlantilla = idPlantilla;

            var idModulo = this.iModuloService.CreateOrUpdateModulo(1884, modulo);

            Assert.IsTrue(idModulo > 0);
        }

        [TestMethod]
        public void UpdateModuloTest()
        {
            decimal idCategoria = 0;
            decimal idPlantilla = 0;
            decimal idEmpresa = 11;//asegurar que el id de empresa existe

            var categoria = this.iCategoriaService.GetCategoria(1);

            if (categoria == null)//si empresa no tiene categoria se crea una
            {
                categoria = MockDataHelper.Categorias.FirstOrDefault();
                categoria.Codigo = 0;
                categoria.IdEmpresa = idEmpresa;
                categoria.Vigencia = Vigencia.NoVigente;
                idCategoria = this.iCategoriaService.CreateOrUpdateCategoria(11, categoria);
            }

            Assert.IsNotNull(idCategoria);

            var plantilla = this.iPlantillaService.GetPlantilla(1);

            if (plantilla == null)//si plantilla es null, se crea una
            {
                plantilla = MockDataHelper.Plantillas.FirstOrDefault();
                plantilla.Codigo = 0;
                plantilla.IdCategoria = idCategoria;
                plantilla.Vigencia = Vigencia.NoVigente;
                idPlantilla = this.iPlantillaService.CreateOrUpdatePlantilla(11, plantilla);
            }

            Assert.IsNotNull(idPlantilla);

            var modulo1 = MockDataHelper.Modulos.FirstOrDefault();
            modulo1.Codigo = 0;
            modulo1.IdPlantilla = idPlantilla;

            var idModulo = this.iModuloService.CreateOrUpdateModulo(11, modulo1);

            Assert.IsNotNull(idModulo);

            modulo1 = this.iModuloService.GetModulo(idModulo);

            modulo1.Etiqueta = "Etiqueta modificada";
            modulo1.Nombre = "Nombre modificado";
            modulo1.Orden = MockDataHelper.Rdm.Next(1, 5);
            modulo1.PermiteActualizar = !modulo1.PermiteActualizar;
            modulo1.PermiteReincidencia = !modulo1.PermiteActualizar;
            modulo1.Vigencia = Vigencia.NoVigente;

            var idModulo1 = this.iModuloService.CreateOrUpdateModulo(11, modulo1);

            Assert.IsNotNull(idModulo1);

            var modulo2 = this.iModuloService.GetModulo(idModulo1);

            Assert.IsNotNull(modulo2);

            Assert.IsTrue(modulo1.Etiqueta == modulo2.Etiqueta);
            Assert.IsTrue(modulo1.IdPlantilla == modulo2.IdPlantilla);
            Assert.IsTrue(modulo1.Nombre == modulo2.Nombre);
            Assert.IsTrue(modulo1.Orden == modulo2.Orden);
            Assert.IsTrue(modulo1.PermiteActualizar == modulo2.PermiteActualizar);
            Assert.IsTrue(modulo1.PermiteReincidencia == modulo2.PermiteReincidencia);
            Assert.IsTrue(modulo1.Vigencia == modulo2.Vigencia);

            modulo1.Vigencia = Vigencia.NoVigente;

            this.iModuloService.CreateOrUpdateModulo(11, modulo1);
        }

        [TestMethod]
        public void GetModuloTest()
        {
            decimal idCategoria = 0;
            decimal idPlantilla = 0;
            decimal idEmpresa = 11;//asegurar que el id de empresa existe

            var categoria = this.iCategoriaService.GetCategoria(1);

            if (categoria == null)//si empresa no tiene categoria se crea una
            {
                categoria = MockDataHelper.Categorias.FirstOrDefault();
                categoria.Codigo = 0;
                categoria.IdEmpresa = idEmpresa;
                categoria.Vigencia = Vigencia.NoVigente;
                idCategoria = this.iCategoriaService.CreateOrUpdateCategoria(11, categoria);
            }

            Assert.IsNotNull(idCategoria);

            var plantilla = this.iPlantillaService.GetPlantilla(1);

            if (plantilla == null)//si plantilla es null, se crea una
            {
                plantilla = MockDataHelper.Plantillas.FirstOrDefault();
                plantilla.Codigo = 0;
                plantilla.IdCategoria = idCategoria;
                plantilla.Vigencia = Vigencia.NoVigente;
                idPlantilla = this.iPlantillaService.CreateOrUpdatePlantilla(11, plantilla);
            }

            Assert.IsNotNull(idPlantilla);

            var modulo1 = this.iModuloService.GetModulo(1);

            if (modulo1 == null)
            {
                modulo1 = MockDataHelper.Modulos.FirstOrDefault();
                modulo1.Codigo = 0;
                modulo1.IdPlantilla = idPlantilla;
            }

            var idModulo = this.iModuloService.CreateOrUpdateModulo(11, modulo1);

            Assert.IsNotNull(idModulo);

            var modulo2 = this.iModuloService.GetModulo(idModulo);

            Assert.IsNotNull(modulo2);

            Assert.IsTrue(modulo1.Etiqueta == modulo2.Etiqueta);
            Assert.IsTrue(modulo1.IdPlantilla == modulo2.IdPlantilla);
            Assert.IsTrue(modulo1.Nombre == modulo2.Nombre);
            Assert.IsTrue(modulo1.Orden == modulo2.Orden);
            Assert.IsTrue(modulo1.PermiteActualizar == modulo2.PermiteActualizar);
            Assert.IsTrue(modulo1.PermiteReincidencia == modulo2.PermiteReincidencia);
            Assert.IsTrue(modulo1.Vigencia == modulo2.Vigencia);

            modulo1.Vigencia = Vigencia.NoVigente;

            this.iModuloService.CreateOrUpdateModulo(11, modulo1);
        }
    }
}