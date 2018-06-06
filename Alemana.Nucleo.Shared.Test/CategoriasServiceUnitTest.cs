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
    public class CategoriasServiceUnitTest
    {
        private ComponentContainer componentContainer;
        private ICategoriasService iCategoriaService;

        public CategoriasServiceUnitTest()
        {
            this.componentContainer = new ComponentContainer();
            this.componentContainer.Register<ICategoriasService, CategoriaService>();

            this.iCategoriaService = componentContainer.Resolve<ICategoriasService>();
        }

        [TestMethod]
        public void GetCategoriaUnitTest()
        {
            decimal id = 0;

            var oldCategoria = this.iCategoriaService.GetCategoria(1);

            if (oldCategoria == null)
            {
                oldCategoria = MockDataHelper.Categorias.FirstOrDefault();

                oldCategoria.Codigo = 0;
                oldCategoria.IdEmpresa = 11;
                oldCategoria.TipoCategoria = TipoCategoria.Plantilla;

                id = this.iCategoriaService.CreateOrUpdateCategoria(11, oldCategoria);
            }

            var newCategoria = this.iCategoriaService.GetCategoria(id);

            Assert.IsTrue(oldCategoria.Orden == newCategoria.Orden);
            Assert.IsTrue(oldCategoria.Descripcion == newCategoria.Descripcion);
            Assert.IsTrue(oldCategoria.IdEmpresa == newCategoria.IdEmpresa);
            Assert.IsTrue(oldCategoria.Vigencia == newCategoria.Vigencia);

            newCategoria.Vigencia = Vigencia.NoVigente;

            this.iCategoriaService.CreateOrUpdateCategoria(11, newCategoria);
        }

        [TestMethod]
        public void CreateOrUpdateCategoria()
        {
            var categoria = MockDataHelper.Categorias.FirstOrDefault();

            categoria.Codigo = 0;
            categoria.IdEmpresa = 11;
            categoria.TipoCategoria = TipoCategoria.Plantilla;

            var id = this.iCategoriaService.CreateOrUpdateCategoria(11, categoria);

            var newCategoria = this.iCategoriaService.GetCategoria(id);

            Assert.IsTrue(categoria.Orden == newCategoria.Orden);
            Assert.IsTrue(categoria.Descripcion == newCategoria.Descripcion);
            Assert.IsTrue(categoria.IdEmpresa == newCategoria.IdEmpresa);
            Assert.IsTrue(categoria.Vigencia == newCategoria.Vigencia);

            newCategoria.PlantillasNoVigentes = newCategoria.PlantillasNoVigentes + 1;
            newCategoria.IdEmpresa = newCategoria.IdEmpresa;
            newCategoria.TipoCategoria = TipoCategoria.Plantilla;
            newCategoria.Orden = newCategoria.Orden + 1;
            newCategoria.Descripcion = newCategoria.Descripcion + "modificado";
            newCategoria.Vigencia = Vigencia.Vigente;

            id = this.iCategoriaService.CreateOrUpdateCategoria(11, newCategoria);

            var newCategoriaModificada = this.iCategoriaService.GetCategoria(id);
            newCategoriaModificada.TipoCategoria = TipoCategoria.Plantilla;

            Assert.IsTrue(newCategoriaModificada.Orden == newCategoria.Orden);
            Assert.IsTrue(newCategoriaModificada.Descripcion == newCategoria.Descripcion);
            Assert.IsTrue(newCategoriaModificada.IdEmpresa == newCategoria.IdEmpresa);
            Assert.IsTrue(newCategoriaModificada.Vigencia == newCategoria.Vigencia);

            categoria.Vigencia = Vigencia.NoVigente;
            newCategoria.Vigencia = Vigencia.NoVigente;

            this.iCategoriaService.CreateOrUpdateCategoria(11, categoria);
            this.iCategoriaService.CreateOrUpdateCategoria(11, newCategoria);
        }
    }
}