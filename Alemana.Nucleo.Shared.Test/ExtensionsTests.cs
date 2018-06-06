using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Alemana.Nucleo.Shared.Extensions;
using System.Collections.Generic;

namespace Alemana.Nucleo.Shared.Test
{
    [TestClass]
    public class ExtensionsTests
    {
        [TestMethod]
        public void EnumsTests()
        {
            var a = Entities.ConfiguredAmbito.Ambulatorio;

            var b = a.GetEnumDescription();

            Assert.AreNotEqual(b, string.Empty);
        }

        [TestMethod]
        public void CollectionsTests()
        {
            var a = new List<string>();

            //verificar que la collection tenga elementos
            Assert.AreEqual(false, a.HasElements());

            //hacer un foreach en una collection nula
            foreach (var it in a.Enum())
            {
                //do something...
            }
        }
    }
}
