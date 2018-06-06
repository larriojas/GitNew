using Alemana.Nucleo.Common.ComponentModel;
using Alemana.Nucleo.Shared.Contrato.ServiceInterfaces;
using Alemana.Nucleo.Shared.Servicio.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Alemana.Nucleo.Shared.Test
{
    [TestClass]
    public class ListaLinealUnitTest
    {
        private ComponentContainer componentContainer;
        private IListaLinealService iListaLinealService;

        public ListaLinealUnitTest()
        {
            this.componentContainer = new ComponentContainer();
            this.componentContainer.Register<IListaLinealService, ListaLinealService>();

            this.iListaLinealService = componentContainer.Resolve<IListaLinealService>();
        }

        [TestMethod]
        public void GetEmpresasTest()
        {
            var empresas = this.iListaLinealService.GetEmpresas(0);

            Assert.IsTrue(empresas.Count() > 0);

            foreach (var empresa in empresas)
            {
                Assert.IsTrue(empresa.Codigo > 0);
                Assert.IsTrue(!string.IsNullOrWhiteSpace(empresa.Descripcion));
            }
        }

        [TestMethod]
        public void GetCategoriasTest()
        {
            var boolcategorias = false;

            var empresas = this.iListaLinealService.GetEmpresas(0);

            Assert.IsTrue(empresas.Count() > 0);

            foreach (var empresa in empresas)
            {
                var categorias = this.iListaLinealService.GetCategorias(empresa.Codigo, Contrato.Models.Estado.Ambas);

                foreach (var categoria in categorias)
                {
                    Assert.IsTrue(categoria.Codigo > 0);
                    Assert.IsTrue(!string.IsNullOrWhiteSpace(categoria.Descripcion));

                    boolcategorias = true;
                }
            }

            Assert.IsTrue(boolcategorias);
        }

        [TestMethod]
        public void GetPlantillasTest()
        {
            bool boolPlantilla = false;

            var empresas = this.iListaLinealService.GetEmpresas(0);

            Assert.IsTrue(empresas.Count() > 0);

            foreach (var empresa in empresas)
            {
                var categorias = this.iListaLinealService.GetCategorias(empresa.Codigo, Contrato.Models.Estado.Ambas);

                foreach (var categoria in categorias)
                {
                    var plantillas = this.iListaLinealService.GetPlantillas(categoria.Codigo, Contrato.Models.Estado.Ambas);

                    foreach (var plantilla in plantillas)
                    {
                        Assert.IsTrue(plantilla.Codigo > 0);
                        Assert.IsTrue(!string.IsNullOrWhiteSpace(plantilla.Descripcion));

                        boolPlantilla = true;
                    }
                }
            }
            Assert.IsTrue(boolPlantilla);
        }

        [TestMethod]
        public void GetModulosTest()
        {
            bool boolModulo = false;

            var empresas = this.iListaLinealService.GetEmpresas(0);

            Assert.IsTrue(empresas.Count() > 0);

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
                            Assert.IsTrue(modulo.Codigo > 0);
                            Assert.IsTrue(!string.IsNullOrWhiteSpace(modulo.Nombre));

                            boolModulo = true;
                        }
                    }
                }
            }
            Assert.IsTrue(boolModulo);
        }

        [TestMethod]
        public void GetAgrupadoresTest()
        {
            bool boolAgrupador = false;

            var empresas = this.iListaLinealService.GetEmpresas(0);

            Assert.IsTrue(empresas.Count() > 0);

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
                            var agrupadores = this.iListaLinealService.GetModulos(modulo.Codigo, Contrato.Models.Estado.Ambas);

                            foreach (var agrupador in agrupadores)
                            {
                                Assert.IsTrue(agrupador.Codigo > 0);
                                Assert.IsTrue(!string.IsNullOrWhiteSpace(agrupador.Nombre));

                                boolAgrupador = true;
                            }
                        }
                    }
                }
            }
            Assert.IsTrue(boolAgrupador);
        }

        [TestMethod]
        public void GetVariablesTest()
        {
            bool boolVariable = false;

            var empresas = this.iListaLinealService.GetEmpresas(0);

            Assert.IsTrue(empresas.Count() > 0);

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
                            var agrupadores = this.iListaLinealService.GetModulos(modulo.Codigo, Contrato.Models.Estado.Ambas);

                            foreach (var agrupador in agrupadores)
                            {
                                var variables = this.iListaLinealService.GetVariables(agrupador.Codigo, Contrato.Models.Estado.Ambas);

                                foreach (var variable in variables)
                                {
                                    Assert.IsTrue(variable.Codigo > 0);
                                    Assert.IsTrue(!string.IsNullOrWhiteSpace(variable.Nombre));

                                    boolVariable = true;
                                }
                            }
                        }
                    }
                }
            }
            Assert.IsTrue(boolVariable);
        }
    }
}