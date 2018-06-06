using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Alemana.Nucleo.Shared.Helpers;
using Alemana.Nucleo.Shared.Contrato.Models;
using System.Collections.Generic;
using Alemana.Nucleo.Common.Extensions;

namespace Alemana.Nucleo.Shared.Test
{
    [TestClass]
    public class WsGestionresultadosLabTest
    {
        [TestMethod]
        public void ConectividadTest()
        {
            var cliente = new wsResultadosLab.WsgestionresultadoslabWebClient();

            using (ServiceChannel.AsDisposable(cliente))
            {
                var result = cliente.rlbspRecuperaConceptos();

                var categorias = new List<Item>();
                foreach (var item in result.listadoconceptos.Enum())
                {
                    categorias.Add(
                        new Item
                        {
                            Id = item.idAnalito,
                            Valor = item.descripcionAnalito
                        });
                }

                Assert.IsNotNull(categorias);
            }
        }
    }
}
