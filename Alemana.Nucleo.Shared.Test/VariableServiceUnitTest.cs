using Alemana.Nucleo.Common.ComponentModel;
using Alemana.Nucleo.Shared.Contrato.Models;
using Alemana.Nucleo.Shared.Contrato.ServiceInterfaces;
using Alemana.Nucleo.Shared.Servicio.Implementation;
using Alemana.Nucleo.Shared.Servicio.MocksImplementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Alemana.Nucleo.Shared.Test
{
    [TestClass]
    public class VariableServiceUnitTest
    {
        private ComponentContainer componentContainer;
        private IVariableService iVariableService;
        private IListaLinealService iListaLinealService;

        public VariableServiceUnitTest()
        {
            this.componentContainer = new ComponentContainer();

            this.componentContainer.Register<IVariableService, VariableService>();
            this.componentContainer.Register<IListaLinealService, ListaLinealService>();

            this.iVariableService = componentContainer.Resolve<IVariableService>();
            this.iListaLinealService = componentContainer.Resolve<IListaLinealService>();
        }

        [TestMethod]
        public void GetVariableTest()
        {
            var empresas = this.iListaLinealService.GetEmpresas(0);

            List<decimal> idVariables = new List<decimal>();

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

                                    idVariables.Add(variable.Codigo);
                                }
                            }
                        }
                    }
                }
            }

            foreach (var idVariable in idVariables)
            {
                var gotIdVariable = this.iVariableService.GetVariable(idVariable);

                Assert.IsNotNull(gotIdVariable);
            }
        }

        [TestMethod]
        public void CreateOrUpdateVariableTest()
        {
            var variables = MockDataHelper.Variables;
            List<decimal> idVariables = new List<decimal>();

            foreach (var variable in variables)
            {
                variable.Vigencia = Vigencia.NoVigente;
                variable.Codigo = 0;
                variable.IdAgrupador = 2;

                var idVariable = this.iVariableService.CreateOrUpdateVariable(11, variable);

                Assert.IsTrue(idVariable > 0);

                idVariables.Add(idVariable);
            }

            foreach (var idVariable in idVariables)
            {
                var getIdVariable = this.iVariableService.GetVariable(idVariable);

                Assert.IsNotNull(getIdVariable);
            }
        }

        [TestMethod]
        public void GetTipoVariable()
        {
            var listaTipoVariables = this.iVariableService.GetTipoVariable(Vigencia.Vigente);

            foreach (var tipoVariable in listaTipoVariables)
            {
                Assert.IsNotNull(tipoVariable);

                Assert.IsTrue(tipoVariable.Codigo > 0);
                Assert.IsTrue(tipoVariable.Nombre != "");
                Assert.IsTrue(tipoVariable.Descripcion != "");
                Assert.IsNotNull(tipoVariable.TipoValidacion);
            }
        }
    }
}