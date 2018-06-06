using Alemana.Nucleo.Common.ComponentModel;
using Alemana.Nucleo.Shared.Contrato.ServiceInterfaces;
using Alemana.Nucleo.Shared.Servicio.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Alemana.Nucleo.Shared.Test
{
    [TestClass]
    public class CambiarOrdenUnitTest
    {
        private ComponentContainer componentContainer;
        private IListaLinealOrdenService iListaLinealOrdenService;
        private IListaLinealService iListaLinealService;
        private ICategoriasService iCategoriaService;
        private IPlantillaService iPlantillaService;
        private IModuloService iModuloService;
        private IAgrupadorService iAgrupadorService;

        public CambiarOrdenUnitTest()
        {
            this.componentContainer = new ComponentContainer();
            this.componentContainer.Register<IListaLinealOrdenService, ListaLinealOrdenService>();
            this.componentContainer.Register<IListaLinealService, ListaLinealService>();
            this.componentContainer.Register<ICategoriasService, CategoriaService>();
            this.componentContainer.Register<IPlantillaService, PlantillaService>();
            this.componentContainer.Register<IModuloService, ModuloService>();
            this.componentContainer.Register<IAgrupadorService, AgrupadorService>();

            this.iListaLinealOrdenService = componentContainer.Resolve<IListaLinealOrdenService>();
            this.iListaLinealService = componentContainer.Resolve<IListaLinealService>();
            this.iCategoriaService = componentContainer.Resolve<ICategoriasService>();
            this.iPlantillaService = componentContainer.Resolve<IPlantillaService>();
            this.iModuloService = componentContainer.Resolve<IModuloService>();
            this.iAgrupadorService = componentContainer.Resolve<IAgrupadorService>();
        }

        [TestMethod]
        public void PutCambiarOrdenCategoria()
        {
            var empresa = this.iListaLinealService.GetEmpresas(0).FirstOrDefault();

            Assert.IsNotNull(empresa);

            var idCategoria = this.iListaLinealService.GetCategorias(empresa.Codigo, Contrato.Models.Estado.Ambas).FirstOrDefault().Codigo;

            Assert.IsNotNull(idCategoria);

            var oldCategoria = this.iCategoriaService.GetCategoria(idCategoria);

            this.iListaLinealOrdenService.PutCambiarOrdenCategoria(11, oldCategoria.Orden + 1, idCategoria, DateTime.Now);

            var newCategoria = this.iCategoriaService.GetCategoria(idCategoria);

            Assert.IsTrue(oldCategoria.Orden != newCategoria.Orden);

            this.iListaLinealOrdenService.PutCambiarOrdenCategoria(11, oldCategoria.Orden, idCategoria, DateTime.Now);
        }

        [TestMethod]
        public void PutCambiarOrdenPlantilla()
        {
            var empresas = this.iListaLinealService.GetEmpresas(0);

            foreach (var empresa in empresas)
            {
                var categorias = this.iListaLinealService.GetCategorias(empresa.Codigo, Contrato.Models.Estado.Ambas);

                foreach (var categoria in categorias)
                {
                    var plantillas = this.iListaLinealService.GetPlantillas(categoria.Codigo, Contrato.Models.Estado.Ambas);

                    foreach (var plantilla in plantillas)
                    {
                        Assert.IsNotNull(plantilla);

                        var oldPlantilla = this.iPlantillaService.GetPlantilla(plantilla.Codigo);

                        this.iListaLinealOrdenService.PutCambiarOrdenPlantilla(11, oldPlantilla.Orden + 1, oldPlantilla.Codigo, DateTime.Now);

                        var newPlantilla = this.iPlantillaService.GetPlantilla(plantilla.Codigo);

                        Assert.IsNotNull(newPlantilla);

                        Assert.IsTrue(oldPlantilla.Orden != newPlantilla.Orden);

                        this.iListaLinealOrdenService.PutCambiarOrdenPlantilla(11, oldPlantilla.Orden, oldPlantilla.Codigo, DateTime.Now);
                    }
                }
            }
        }

        [TestMethod]
        public void PutCambiarOrdenModulo()
        {
            var empresas = this.iListaLinealService.GetEmpresas(0);

            foreach (var empresa in empresas)
            {
                var categorias = this.iListaLinealService.GetCategorias(empresa.Codigo, Contrato.Models.Estado.Ambas);

                foreach (var categoria in categorias)
                {
                    var plantillas = this.iListaLinealService.GetPlantillas(categoria.Codigo, Contrato.Models.Estado.Ambas);

                    foreach (var plantilla in plantillas)
                    {
                        var modulos = this.iListaLinealService.GetModulos(plantilla.Codigo, Contrato.Models.Estado.Ambas);

                        foreach (var modulo in modulos)
                        {
                            Assert.IsNotNull(modulo);

                            var oldModulo = this.iModuloService.GetModulo(modulo.Codigo);

                            this.iListaLinealOrdenService.PutCambiarOrdenModulo(11, oldModulo.Orden + 1, oldModulo.Codigo, DateTime.Now);

                            var newModulo = this.iModuloService.GetModulo(oldModulo.Codigo);

                            Assert.IsNotNull(newModulo);

                            Assert.IsTrue(oldModulo.Orden != newModulo.Orden);

                            this.iListaLinealOrdenService.PutCambiarOrdenModulo(11, oldModulo.Orden, oldModulo.Codigo, DateTime.Now);
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void PutCambiarOrdenAgrupador()
        {
            var empresas = this.iListaLinealService.GetEmpresas(0);

            foreach (var empresa in empresas)
            {
                var categorias = this.iListaLinealService.GetCategorias(empresa.Codigo, Contrato.Models.Estado.Ambas);

                foreach (var categoria in categorias)
                {
                    var plantillas = this.iListaLinealService.GetPlantillas(categoria.Codigo, Contrato.Models.Estado.Ambas);

                    foreach (var plantilla in plantillas)
                    {
                        var modulos = this.iListaLinealService.GetModulos(plantilla.Codigo, Contrato.Models.Estado.Ambas);

                        foreach (var modulo in modulos)
                        {
                            var agrupadores = this.iListaLinealService.GetAgrupador(modulo.Codigo, Contrato.Models.Estado.Ambas);

                            foreach (var agrupador in agrupadores)
                            {
                                Assert.IsNotNull(agrupador);

                                var oldAgrupador = this.iAgrupadorService.GetAgrupador(agrupador.Codigo);

                                this.iListaLinealOrdenService.PutCambiarOrdenAgrupador(11, oldAgrupador.Orden + 1, oldAgrupador.Codigo, modulo.Codigo, DateTime.Now);

                                var newAgrupador = this.iAgrupadorService.GetAgrupador(oldAgrupador.Codigo);

                                Assert.IsNotNull(newAgrupador);

                                Assert.IsTrue(oldAgrupador.Orden != newAgrupador.Orden);

                                this.iListaLinealOrdenService.PutCambiarOrdenAgrupador(11, oldAgrupador.Orden, oldAgrupador.Codigo, modulo.Codigo, DateTime.Now);
                            }
                        }
                    }
                }
            }
        }

        /* [TestMethod]
         public void PutCambiarOrdenVariable()
         {
             var oldVariable = this.iListaLinealService.GetVariables(11, Contrato.Models.Estado.Ambas).FirstOrDefault();

             Assert.IsNotNull(oldVariable);

             this.iListaLinealOrdenService.PutCambiarOrdenVariable(11, oldVariable.Orden + 1, oldVariable.Codigo, oldVariable.IdAgrupador, DateTime.Now);

             var newVariable = this.iListaLinealService.GetVariables(11, Contrato.Models.Estado.Ambas).FirstOrDefault(v => v.Codigo == oldVariable.Codigo);

             Assert.Equals(oldVariable, newVariable);

             this.iListaLinealOrdenService.PutCambiarOrdenVariable(11, oldVariable.Orden, oldVariable.Codigo, oldVariable.IdAgrupador, DateTime.Now);
         }*/
    }
}