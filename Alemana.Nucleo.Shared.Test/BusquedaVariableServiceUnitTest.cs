using Alemana.Nucleo.Common.ComponentModel;
using Alemana.Nucleo.Shared.Contrato.Models;
using Alemana.Nucleo.Shared.Contrato.ServiceInterfaces;
using Alemana.Nucleo.Shared.Servicio.Implementation;
using Alemana.Nucleo.Shared.Servicio.MocksImplementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alemana.Nucleo.Shared.Test
{
    /// <summary>
    /// Summary description for BusquedaVariableServiceUnitTest
    /// </summary>
    [TestClass]
    public class BusquedaVariableServiceUnitTest
    {
        private ComponentContainer componentContainer;
        private IVariableService iVariableService;
        private IListaLinealService iListaLinealService;

        public BusquedaVariableServiceUnitTest()
        {
            this.componentContainer = new ComponentContainer();

            this.componentContainer.Register<IVariableService, VariableService>();
            this.componentContainer.Register<IListaLinealService, ListaLinealService>();

            this.iVariableService = componentContainer.Resolve<IVariableService>();
            this.iListaLinealService = componentContainer.Resolve<IListaLinealService>();
        }

        [TestMethod]
        public void GetVariablesTest()
        {
            decimal cantidadregistrosBusqueda = MockDataHelper.Rdm.Next(1, 5);

            for (int i = 1; i < 4; i++)
            {
                var variables = iVariableService.SearchVariable("", i, Estado.Ambas, ContextoVariable.Documento, cantidadregistrosBusqueda);

                foreach (var variable in variables)
                {
                    Assert.IsNotNull(variable);
                    Assert.IsTrue(variable.Codigo > 0);
                }
            }
        }
    }
}