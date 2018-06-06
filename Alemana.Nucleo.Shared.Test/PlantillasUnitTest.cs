using Alemana.Nucleo.Common.ComponentModel;
using Alemana.Nucleo.Shared.Contrato.Models;
using Alemana.Nucleo.Shared.Contrato.ServiceInterfaces;
using Alemana.Nucleo.Shared.Servicio.Implementation;
using Alemana.Nucleo.Shared.Servicio.MocksImplementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

﻿

namespace Alemana.Nucleo.Shared.Test
{
    [TestClass]
    public class PlantillasUnitTest
    {
        private ComponentContainer componentContainer;
        private IPlantillaService iPlantillaService;
        private ICategoriasService iCategoriasService;

        public PlantillasUnitTest()
        {
            this.componentContainer = new ComponentContainer();
            this.componentContainer.Register<IPlantillaService, PlantillaService>();
            this.componentContainer.Register<ICategoriasService, CategoriaService>();

            this.iPlantillaService = componentContainer.Resolve<IPlantillaService>();
            this.iCategoriasService = componentContainer.Resolve<ICategoriasService>();
        }

        [TestMethod]
        public void GetPlantilla()
        {
            decimal idCategoria = 0;
            decimal idPlantilla = 0;
            decimal idEmpresa = 11;//asegurar que el id de empresa existe

            var categoria = this.iCategoriasService.GetCategoria(1);

            if (categoria == null)//si empresa no tiene categoria se crea una
            {
                categoria = MockDataHelper.Categorias.FirstOrDefault();
                categoria.Codigo = 0;
                categoria.IdEmpresa = idEmpresa;
                categoria.TipoCategoria = TipoCategoria.Plantilla;
                categoria.Vigencia = Vigencia.NoVigente;
                idCategoria = this.iCategoriasService.CreateOrUpdateCategoria(11, categoria);
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

            plantilla = this.iPlantillaService.GetPlantilla(idPlantilla);

            Assert.IsNotNull(plantilla);
            Assert.IsNotNull(plantilla.Codigo);
            Assert.IsTrue(plantilla.IdCategoria == idCategoria);
        }

        [TestMethod]
        public void CreateOrUpdatePlantilla()
        {
            decimal idCategoria = 0;
            decimal idPlantilla = 0;
            decimal idEmpresa = 11;//asegurar que el id de empresa existe

            var categoria = this.iCategoriasService.GetCategoria(1);

            if (categoria == null)//si empresa no tiene categoria con id = 1, se crea una
            {
                categoria = MockDataHelper.Categorias.FirstOrDefault();
                categoria.Codigo = 0;
                categoria.IdEmpresa = idEmpresa;
                categoria.TipoCategoria = TipoCategoria.Plantilla;
                categoria.Vigencia = Vigencia.NoVigente;
                idCategoria = this.iCategoriasService.CreateOrUpdateCategoria(11, categoria);
            }
            else
                idCategoria = categoria.Codigo;

            Assert.IsTrue(idCategoria > 0);

            var oldPlantilla = this.iPlantillaService.GetPlantilla(1);

            if (oldPlantilla == null)//si plantilla es null, se crea una
            {
                oldPlantilla = MockDataHelper.Plantillas.FirstOrDefault();
                oldPlantilla.Codigo = 0;
                oldPlantilla.IdCategoria = idCategoria;
                oldPlantilla.Vigencia = Vigencia.NoVigente;
                idPlantilla = this.iPlantillaService.CreateOrUpdatePlantilla(11, oldPlantilla);
                oldPlantilla.Codigo = idPlantilla;
            }

            Assert.IsNotNull(idPlantilla);

            var newPlantilla = this.iPlantillaService.GetPlantilla(idPlantilla);

            //Assert.IsTrue(newPlantilla.Ambitos.GetHashCode() == oldPlantilla.Ambitos.GetHashCode());
            Assert.IsTrue(newPlantilla.Codigo == oldPlantilla.Codigo);
            Assert.IsTrue(newPlantilla.Descripcion == oldPlantilla.Descripcion);
            Assert.IsTrue(newPlantilla.EvolucionAutomatica == oldPlantilla.EvolucionAutomatica);
            Assert.IsTrue(newPlantilla.IdCategoria == oldPlantilla.IdCategoria);
            Assert.IsTrue(newPlantilla.Orden == oldPlantilla.Orden);
            Assert.IsTrue(newPlantilla.PermiteEvolucion == oldPlantilla.PermiteEvolucion);
            Assert.IsTrue(newPlantilla.Tipo == oldPlantilla.Tipo);

            newPlantilla.Ambitos = MockDataHelper.GetListAmbito();
            newPlantilla.Descripcion = oldPlantilla.Descripcion + " modificado";
            newPlantilla.EvolucionAutomatica = !oldPlantilla.EvolucionAutomatica;
            newPlantilla.Orden = oldPlantilla.Orden + 1;
            newPlantilla.PermiteEvolucion = !oldPlantilla.PermiteEvolucion;
            newPlantilla.Tipo = MockDataHelper.GetTipo();
            newPlantilla.Vigencia = Vigencia.NoVigente;

            idPlantilla = this.iPlantillaService.CreateOrUpdatePlantilla(11, newPlantilla);

            var newPlantillaModificada = this.iPlantillaService.GetPlantilla(idPlantilla);

            //Assert.IsTrue(newPlantillaModificada.Ambitos == newPlantilla.Ambitos);
            Assert.IsTrue(newPlantillaModificada.Codigo == newPlantilla.Codigo);
            Assert.IsTrue(newPlantillaModificada.Descripcion == newPlantilla.Descripcion);
            Assert.IsTrue(newPlantillaModificada.EvolucionAutomatica == newPlantilla.EvolucionAutomatica);
            Assert.IsTrue(newPlantillaModificada.IdCategoria == newPlantilla.IdCategoria);
            Assert.IsTrue(newPlantillaModificada.Orden == newPlantilla.Orden);
            Assert.IsTrue(newPlantillaModificada.PermiteEvolucion == newPlantilla.PermiteEvolucion);
            Assert.IsTrue(newPlantillaModificada.Tipo == newPlantilla.Tipo);
            //la vigencia de plantillas y categoria creadas, se inicializaron como no vigente
        }
    }
}