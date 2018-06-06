using Alemana.Nucleo.Common.Tracing;
using Alemana.Nucleo.Estadisticas.Contrato.Models;
using Alemana.Nucleo.Estadisticas.Contrato.ServiceInterfaces;
using Alemana.Nucleo.Estadisticas.Servicio.WsFiltros;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alemana.Nucleo.Estadisticas.Servicio.Implementation
{
    public class FiltrosServices : IFiltrosServices
    {
        public IEnumerable<Area> GetAreas(decimal idUsuario)
        {
            using (Tracer t = new Tracer())
            {
                t.TraceVerbose("GetAreas idUsuario: [{0}]", idUsuario);

                IEnumerable<Area> areas = new List<Area>();

                try
                {
                    EstspAreaSelResult wsAreas = null;
                    using (WsfiltrosestadisticasWebClient wsClient = new WsfiltrosestadisticasWebClient())
                    {
                        wsAreas = wsClient.estspAreaSel(idUsuario);
                    }

                    areas = TransformWSAreasToAreas(wsAreas);
                }
                catch (Exception ex)
                {
                    String error = ex.Message;
                    Console.WriteLine("Error = " + error);
                    t.TraceError("Error = " + error);
                }

                return areas;
            }
        }

        public IEnumerable<Profesional> GetProfesionales(decimal idUsuario)
        {
            using (Tracer t = new Tracer())
            {
                t.TraceVerbose("GetProfesionales idUsuario: [{0}]", idUsuario);

                IEnumerable<Profesional> profesionales = new List<Profesional>();

                try
                {
                    EstspProfesionalSelResult wsProfesionales = null;
                    using (WsfiltrosestadisticasWebClient wsClient = new WsfiltrosestadisticasWebClient())
                    {
                        wsProfesionales = wsClient.estspProfesionalSel(idUsuario);
                    }

                    profesionales = TransformWSProfesionalesToProfesionales(wsProfesionales);
                }
                catch (Exception ex)
                {
                    String error = ex.Message;
                    Console.WriteLine("Error = " + error);
                    t.TraceError("Error = " + error);
                }

                return profesionales;
            }
        }

        private IEnumerable<Area> TransformWSAreasToAreas(EstspAreaSelResult areas)
        {
            List<Area> result = new List<Area>();

            if ((areas != null) && (areas.areas != null))
            {
                foreach (var area in areas.areas)
                {
                    result.Add(new Area()
                    {
                        Id = area.id,
                        Nombre = area.descripcion
                    });
                }
            }

            return result;
        }

        private IEnumerable<Profesional> TransformWSProfesionalesToProfesionales(EstspProfesionalSelResult profesionales)
        {
            List<Profesional> result = new List<Profesional>();

            if ((profesionales != null) && (profesionales.profesionales != null))
            {
                foreach (var profesional in profesionales.profesionales)
                {
                    result.Add(new Profesional()
                    {
                        Id = profesional.usuarioid,
                        Nombre = string.Format("{0} {1} {2}",
                                               profesional.apellidopaterno,
                                               profesional.apellidomaterno,
                                               profesional.nombres)
                    });
                }
            }

            return result.OrderBy(x => x.Nombre).ToList();
        }
    }
}