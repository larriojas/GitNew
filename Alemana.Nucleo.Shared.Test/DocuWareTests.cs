using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Alemana.Nucleo.Shared.Servicio.Implementation;

namespace Alemana.Nucleo.Shared.Test
{
    [TestClass]
    public class DocuWareTests
    {
        private decimal idficha = 66075;
        private string tipoDocumento = "1005,1006,1004,1008";

        [TestMethod]
        public void TestDocuWareService()
        {
            var svc = new ArchivoService();
        }
    }
}
