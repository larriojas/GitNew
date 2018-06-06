using Alemana.Nucleo.Estadisticas.Contrato.Models;
using System.Collections.Generic;

namespace Alemana.Nucleo.Estadisticas.Contrato.ServiceInterfaces
{
    public interface IFiltrosServices
    {
        IEnumerable<Profesional> GetProfesionales(decimal idUsuario);

        IEnumerable<Area> GetAreas(decimal idUsuario);
    }
}