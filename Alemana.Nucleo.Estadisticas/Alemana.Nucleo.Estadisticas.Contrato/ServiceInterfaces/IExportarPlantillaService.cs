using Alemana.Nucleo.Estadisticas.Contrato.Models;
using Alemana.Nucleo.Shared.Entities;
using System;
using System.Collections.Generic;

namespace Alemana.Nucleo.Estadisticas.Contrato.ServiceInterfaces
{
    public interface IExportarPlantillaService
    {
        IEnumerable<PlantillaEstadistica> GetPlantillas(IEnumerable<decimal> usuarios, IEnumerable<decimal> areas, DateTime fechaInicio, DateTime fechaFin, TipoFecha tipo);

        PlantillaEstadistica GetPlantilla(decimal idPlantilla);

        IEnumerable<Paciente> GetPacientesPlantilla(decimal idPlantilla, IEnumerable<decimal> usuarios, IEnumerable<decimal> areas, DateTime fechaInicio, DateTime fechaFin, decimal tipoFecha);

        IEnumerable<IncidenciaModulo> GetIncidenciasModulos(decimal idPlantilla, decimal idPaciente, IEnumerable<decimal> usuarios, IEnumerable<decimal> areas, DateTime fechaInicio, DateTime fechaFin, decimal tipoFecha);

        IEnumerable<ValorVariable> GetValoresVariablesIncidenciaModulo(decimal idIncidenciaModulo);
    }
}