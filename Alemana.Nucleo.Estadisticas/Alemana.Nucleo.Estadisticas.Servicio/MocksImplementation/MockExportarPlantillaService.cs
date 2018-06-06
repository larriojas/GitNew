using Alemana.Nucleo.Estadisticas.Contrato.Models;
using Alemana.Nucleo.Estadisticas.Contrato.ServiceInterfaces;
using Alemana.Nucleo.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alemana.Nucleo.Estadisticas.Servicio.MocksImplementation
{
    public class MockExportarPlantillaService : IExportarPlantillaService
    {
        public PlantillaEstadistica GetPlantilla(decimal idPlantilla)
        {
            int idusuario;
            if ((int.TryParse(Alemana.Nucleo.Common.Security.SecurityManager.CurrentUser.NucleoIdentity.UsuarioId, out idusuario)) && (idusuario % 2) == 0)
            {
                return DataHelper.Plantillas.Where(x => x.Codigo % 2 == 0).First();
            }
            else
            {
                return DataHelper.Plantillas.Where(x => x.Codigo % 2 != 0).First();
            }
        }

        public IEnumerable<PlantillaEstadistica> GetPlantillas(IEnumerable<decimal> usuarios, IEnumerable<decimal> areas, DateTime fechaInicio, DateTime fechaFin, Contrato.Models.TipoFecha tipo)
        {
            int idusuario;
            if ((int.TryParse(Alemana.Nucleo.Common.Security.SecurityManager.CurrentUser.NucleoIdentity.UsuarioId, out idusuario)) && (idusuario % 2) == 0)
            {
                return DataHelper.Plantillas.Where(x => x.Codigo % 2 == 0);
            }
            else
            {
                return DataHelper.Plantillas.Where(x => x.Codigo % 2 != 0);
            }
        }

        public IEnumerable<Paciente> GetPacientesPlantilla(decimal idPlantilla, IEnumerable<decimal> usuarios, IEnumerable<decimal> areas, DateTime fechaInicio, DateTime fechaFin, decimal tipoFecha)
        {
            int idusuario;
            if ((int.TryParse(Alemana.Nucleo.Common.Security.SecurityManager.CurrentUser.NucleoIdentity.UsuarioId, out idusuario)) && (idusuario % 2) == 0)
            {
                return DataHelper.PacientesPlantilla.Where(x => x.IdPaciente % 2 == 0);
            }
            else
            {
                return DataHelper.PacientesPlantilla.Where(x => x.IdPaciente % 2 != 0);
            }
        }

        public IEnumerable<IncidenciaModulo> GetIncidenciasModulos(decimal idPlantilla, decimal idPaciente, IEnumerable<decimal> usuarios, IEnumerable<decimal> areas, DateTime fechaInicio, DateTime fechaFin, decimal tipoFecha)
        {
            return DataHelper.IncidenciasModulos.Where(x => x.IdPlantilla == idPlantilla &&
                                                            x.IdPaciente == idPaciente &&
                                                            x.Fecha >= fechaInicio && x.Fecha <= fechaFin);
        }

        public IEnumerable<ValorVariable> GetValoresVariablesIncidenciaModulo(decimal idIncidenciaModulo)
        {
            return null;
        }
    }
}