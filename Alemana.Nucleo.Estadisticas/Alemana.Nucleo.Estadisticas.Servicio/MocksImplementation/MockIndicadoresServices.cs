using Alemana.Nucleo.Estadisticas.Contrato.Models;
using Alemana.Nucleo.Estadisticas.Contrato.ServiceInterfaces;
using Alemana.Nucleo.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alemana.Nucleo.Estadisticas.Servicio.MocksImplementation
{
    public class MockIndicadoresServices : IIndicadoresServices
    {
        public IndicadorIndicadores GetIndicadores(IEnumerable<decimal> usuarios, IEnumerable<decimal> areas, DateTime fechaInicio, DateTime fechaFin)
        {
            return DataHelper.IndicadorIndicadores;
        }

        public IndicadorCanalesVirtuales GetIndicadoresCanalesVirtuales(IEnumerable<decimal> usuarios, IEnumerable<decimal> areas, DateTime fechaInicio, DateTime fechaFin)
        {
            return DataHelper.IndicadorCanalesVirtuales;
        }

        public IEnumerable<IndicadorDiagnostico> GetIndicadoresDiagnostico(IEnumerable<decimal> usuarios, IEnumerable<decimal> areas, DateTime fechaInicio, DateTime fechaFin)
        {
            IEnumerable<IndicadorDiagnostico> result = DataHelper.IndicadoresDiagnosticos.Take((fechaFin - fechaInicio).Days % 6);

            Random rand = new Random();
            foreach (var diagnostico in result)
            {
                diagnostico.Numero = rand.Next(10) + (fechaFin - fechaInicio).Days;
            }

            return result;
        }

        public IEnumerable<Paciente> GetPacientesDiagnosticados(IEnumerable<decimal> usuarios, IEnumerable<decimal> areas, DateTime fechaInicio, DateTime fechaFin, decimal diagnosticoId)
        {
            return new List<Paciente>() { new Paciente() { Nombre = "Paciente 1" + diagnosticoId.ToString() }, new Paciente() { Nombre = "Paciente 2" } };
        }

        public IndicadorPaciente GetIndicadoresPacientes(IEnumerable<decimal> usuarios, IEnumerable<decimal> areas, DateTime fechaInicio, DateTime fechaFin)
        {
            return DataHelper.IndicadorPaciente;
        }

        public decimal GetIndicadoresCRM(decimal usuario, DateTime fechaInicio, DateTime fechaFin)
        {
            return 0;
        }

        public decimal GetIndicadoresPacientesAtendidos(IEnumerable<decimal> usuarios, IEnumerable<decimal> areas, DateTime fechaInicio, DateTime fechaFin)
        {
            return 0;
        }
    }
}