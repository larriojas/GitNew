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
    public class SeguridadUnitTest
    {
        private ComponentContainer componentContainer;
        private ISeguridadService iSeguridadService;
        private IListaLinealService iListaLineal;

        public SeguridadUnitTest()
        {
            this.componentContainer = new ComponentContainer();

            this.componentContainer.Register<ISeguridadService, SeguridadService>();
            this.componentContainer.Register<IListaLinealService, ListaLinealService>();

            this.iSeguridadService = componentContainer.Resolve<ISeguridadService>();
            this.iListaLineal = componentContainer.Resolve<IListaLinealService>();
        }

        [TestMethod]
        public void GetProfesionales()
        {
            decimal topRegistros = MockDataHelper.Rdm.Next(1, 5);
            string descripcionProfesionales = "";

            var profesionales = this.iSeguridadService.GetProfesionales(descripcionProfesionales, topRegistros);

            Assert.IsNotNull(profesionales);

            foreach (var profesional in profesionales)
            {
                Assert.IsTrue(profesional.Id > 0);
                Assert.IsTrue(!string.IsNullOrWhiteSpace(profesional.Nombre));
            }
        }

        [TestMethod]
        public void GetProfesionalesAreasCategoria()
        {
            bool boolProfesional = false;

            var empresas = this.iListaLineal.GetEmpresas(0);

            foreach (var empresa in empresas)
            {
                var categorias = this.iListaLineal.GetCategorias(empresa.Codigo, Estado.Ambas);

                foreach (var categoria in categorias)
                {
                    var profesionales = this.iSeguridadService.GetProfesionalesAreasCategoria(categoria.Codigo);

                    if (profesionales.Profesionales != null)
                    {
                        foreach (var profesional in profesionales.Profesionales)
                        {
                            Assert.IsTrue(profesional.Id > 0);
                            Assert.IsTrue(!string.IsNullOrWhiteSpace(profesional.Nombre));

                            boolProfesional = true;
                        }
                    }

                    if (profesionales.Areas != null)
                    {
                        foreach (var area in profesionales.Areas)
                        {
                            Assert.IsTrue(area.Id > 0);
                            Assert.IsTrue(!string.IsNullOrWhiteSpace(area.Descripcion));

                            boolProfesional = true;
                        }
                    }
                }
            }
            Assert.IsTrue(boolProfesional);
        }

        [TestMethod]
        public void PostProfesionalCategoria()
        {
            var empresa = this.iListaLineal.GetEmpresas(0).FirstOrDefault();

            var categoria = this.iListaLineal.GetCategorias(empresa.Codigo, Estado.Ambas).FirstOrDefault();

            var profesional = this.iSeguridadService.GetProfesionales("", 1).FirstOrDefault();

            Assert.IsNotNull(profesional);

            Assert.IsNotNull(this.iSeguridadService.PostProfesionalCategoria(categoria.Codigo, profesional.Id, 11));

            Assert.IsNotNull(this.iSeguridadService.DeleteProfesionalCategoria(categoria.Codigo, profesional.Id, 11));
        }

        [TestMethod]
        public void DeleteProfesionalCategoria()
        {
            var empresa = this.iListaLineal.GetEmpresas(0).FirstOrDefault();

            var categoria = this.iListaLineal.GetCategorias(empresa.Codigo, Estado.Ambas).FirstOrDefault();

            var profesional = this.iSeguridadService.GetProfesionales("", 1).FirstOrDefault();

            Assert.IsNotNull(profesional);

            Assert.IsNotNull(this.iSeguridadService.PostProfesionalCategoria(categoria.Codigo, profesional.Id, 11));

            Assert.IsNotNull(this.iSeguridadService.DeleteProfesionalCategoria(categoria.Codigo, profesional.Id, 11));
        }

        [TestMethod]
        public void GetAreas()
        {
            decimal topRegistros = MockDataHelper.Rdm.Next(1, 3);
            string descripcionArea = "";

            var areas = this.iSeguridadService.GetAreas(descripcionArea, topRegistros);

            Assert.IsNotNull(areas);

            foreach (var area in areas)
            {
                Assert.IsTrue(area.Id > 0);
                Assert.IsTrue(!string.IsNullOrWhiteSpace(area.Descripcion));
            }
        }

        [TestMethod]
        public void GetAreasHijas()
        {
            decimal topRegistros = 0;
            string descripcionArea = "descripcion";

            var areas = this.iSeguridadService.GetAreas(descripcionArea, topRegistros);

            foreach (var area in areas)
            {
                var listaAreas = this.iSeguridadService.GetAreasHijas(area.Id);
                Assert.IsNotNull(listaAreas);

                foreach (var areaHija in listaAreas)
                {
                    Assert.IsTrue(areaHija.Id > 0);
                    Assert.IsTrue(!string.IsNullOrWhiteSpace(areaHija.Descripcion));
                }
            }
        }

        [TestMethod]
        public void PostAreaCategoria()
        {
            var empresa = this.iListaLineal.GetEmpresas(0).FirstOrDefault();

            var categoria = this.iListaLineal.GetCategorias(empresa.Codigo, Estado.Ambas).FirstOrDefault();

            var area = this.iSeguridadService.GetAreas("", 1).FirstOrDefault();

            decimal idArea2 = this.iSeguridadService.PostAreaCategoria(categoria.Codigo, area.Id, 1, 11);

            var area2 = this.iSeguridadService.GetAreas("", 0).FirstOrDefault(a => a.Id == idArea2);
        }

        [TestMethod]
        public void PutAreaCategoria()
        {
            var empresa = this.iListaLineal.GetEmpresas(0).FirstOrDefault();

            var categoria = this.iListaLineal.GetCategorias(empresa.Codigo, Estado.Ambas).FirstOrDefault();

            var area = this.iSeguridadService.GetAreas("", 1).FirstOrDefault();

            decimal idArea2 = this.iSeguridadService.PostAreaCategoria(categoria.Codigo, area.Id, 5, 11);

            var area2 = this.iSeguridadService.GetAreas("", 0).FirstOrDefault(a => a.Id == idArea2);

            idArea2 = this.iSeguridadService.PostAreaCategoria(categoria.Codigo, 0, 5, 11);
        }
    }
}