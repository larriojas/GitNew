using Alemana.Nucleo.Estadisticas.Contrato.Models;
using Alemana.Nucleo.Estadisticas.Contrato.ServiceInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace Alemana.Nucleo.Estadisticas.Servicio.MocksImplementation
{
    public class MockFiltrosServices : IFiltrosServices
    {
        public IEnumerable<Profesional> GetProfesionales(decimal idUsuario)
        {
            IEnumerable<Profesional> result = null;

            if ((idUsuario % 2) == 0)
            {
                result = DataHelper.Profesionales.Where(x => x.Id % 2 == 0);
            }
            else
            {
                result = DataHelper.Profesionales.Where(x => x.Id % 2 != 0);
            }

            return result.OrderBy(x => x.Nombre).ToList();
        }

        public IEnumerable<Area> GetAreas(decimal idUsuario)
        {
            if ((idUsuario % 2) == 0)
            {
                return DataHelper.Areas.Where(x => x.Id % 2 == 0);
            }
            else
            {
                return DataHelper.Areas.Where(x => x.Id % 2 != 0);
            }
        }
    }
}