using Alemana.Nucleo.Estadisticas.Contrato.Models;
using Alemana.Nucleo.Shared.Entities;
using System;
using System.Collections.Generic;

namespace Alemana.Nucleo.Estadisticas.Contrato.ServiceInterfaces
{
    public interface IIndicadoresServices
    {
        IndicadorPaciente GetIndicadoresPacientes(IEnumerable<decimal> usuarios, IEnumerable<decimal> areas, DateTime fechaInicio, DateTime fechaFin);

        IndicadorCanalesVirtuales GetIndicadoresCanalesVirtuales(IEnumerable<decimal> usuarios, IEnumerable<decimal> areas, DateTime fechaInicio, DateTime fechaFin);

        IEnumerable<IndicadorDiagnostico> GetIndicadoresDiagnostico(IEnumerable<decimal> usuarios, IEnumerable<decimal> areas, DateTime fechaInicio, DateTime fechaFin);

        IEnumerable<Paciente> GetPacientesDiagnosticados(IEnumerable<decimal> usuarios, IEnumerable<decimal> areas, DateTime fechaInicio, DateTime fechaFin, decimal diagnosticoId);

        IndicadorIndicadores GetIndicadores(IEnumerable<decimal> usuarios, IEnumerable<decimal> areas, DateTime fechaInicio, DateTime fechaFin);

        decimal GetIndicadoresCRM(decimal usuario, DateTime fechaInicio, DateTime fechaFin);

        decimal GetIndicadoresPacientesAtendidos(IEnumerable<decimal> usuarios, IEnumerable<decimal> areas, DateTime fechaInicio, DateTime fechaFin);
    }
}