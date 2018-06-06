using Alemana.Nucleo.Common.Tracing;
using Alemana.Nucleo.Estadisticas.Contrato.Models;
using Alemana.Nucleo.Estadisticas.Contrato.ServiceInterfaces;
using Alemana.Nucleo.Estadisticas.Servicio.WsIndicadores;
using Alemana.Nucleo.Estadisticas.Servicio.WsAgendaMedica;
using Alemana.Nucleo.Shared.Entities;
using Alemana.Nucleo.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alemana.Nucleo.Estadisticas.Servicio.Implementation
{
    public class IndicadoresServices : IIndicadoresServices
    {
        public IndicadorIndicadores GetIndicadores(IEnumerable<decimal> usuarios, IEnumerable<decimal> areas, DateTime fechaInicio, DateTime fechaFin)
        {
            using (Tracer t = new Tracer())
            {
                t.TraceVerbose("GetIndicadores usuarios: [{0}], areas: [{1}], fechaInicio: [{2}], fechaFin: [{3}]",
                               string.Join(",", usuarios), string.Join(",", areas), fechaInicio, fechaFin);

                IndicadorIndicadores indicadores = new IndicadorIndicadores();

                try
                {
                    EstspIndIndicadoresSelResult wsIndicadores = null;
                    using (WsindicadoresestadisticasWebClient wsClient = new WsindicadoresestadisticasWebClient())
                    {
                        wsIndicadores = wsClient.estspIndIndicadoresSel(string.Join(",", usuarios),
                                                                        string.Join(",", areas),
                                                                        fechaInicio,
                                                                        fechaFin);
                    }

                    indicadores = TransformWSIndicadoresToIndicador(wsIndicadores);
                }
                catch (Exception ex)
                {
                    String error = ex.Message;
                    Console.WriteLine("Error = " + error);
                    t.TraceError("Error = " + error);
                }

                return indicadores;
            }
        }

        public IndicadorCanalesVirtuales GetIndicadoresCanalesVirtuales(IEnumerable<decimal> usuarios, IEnumerable<decimal> areas, DateTime fechaInicio, DateTime fechaFin)
        {
            using (Tracer t = new Tracer())
            {
                t.TraceVerbose("GetIndicadoresCanalesVirtuales usuarios: [{0}], areas: [{1}], fechaInicio: [{2}], fechaFin: [{3}]",
                               string.Join(",", usuarios), string.Join(",", areas), fechaInicio, fechaFin);

                IndicadorCanalesVirtuales indicadores = new IndicadorCanalesVirtuales();

                string usuariosList = GetList(usuarios);
                string areasList = GetList(areas);

                try
                {
                    EstspIndCanVirtualSelResult wsCanales = null;
                    using (WsindicadoresestadisticasWebClient wsClient = new WsindicadoresestadisticasWebClient())
                    {
                        wsCanales = wsClient.estspIndCanVirtualSel(usuariosList,
                                                                   areasList,
                                                                   fechaInicio,
                                                                   fechaFin);
                    }

                    if (wsCanales.indicadoresCanalesVirtuales != null)
                    {
                        indicadores = TransformWSCanalesToCanal(wsCanales);
                    }

                    EstspPpnidPacientesSelResult wsIndicador = null;
                    using (WsindicadoresestadisticasWebClient wsClient = new WsindicadoresestadisticasWebClient())
                    {
                        wsIndicador = wsClient.estspPpnidPacientesSel(usuariosList,
                                                                   areasList);
                    }

                    foreach (var paciente in wsIndicador.ppnidPacientes)
                    {
                        decimal result = 0;
                        result = GetIndicadoresCRM(paciente.usunuPersonaId,
                                                    fechaInicio,
                                                    fechaFin);

                        indicadores.CRM = indicadores.CRM + result;
                    }

                    indicadores.TotalMensajes = indicadores.CRM + indicadores.RCE + indicadores.PortalPacientes;
                }
                catch (Exception ex)
                {
                    String error = ex.Message;
                    Console.WriteLine("Error = " + error);
                    t.TraceError("Error = " + error);
                }

                return indicadores;
            }
        }

        public IEnumerable<IndicadorDiagnostico> GetIndicadoresDiagnostico(IEnumerable<decimal> usuarios, IEnumerable<decimal> areas, DateTime fechaInicio, DateTime fechaFin)
        {
            using (Tracer t = new Tracer())
            {
                t.TraceVerbose("GetIndicadoresDiagnostico usuarios: [{0}], areas: [{1}], fechaInicio: [{2}], fechaFin: [{3}]",
                               string.Join(",", usuarios), string.Join(",", areas), fechaInicio, fechaFin);

                IEnumerable<IndicadorDiagnostico> indicadores = new List<IndicadorDiagnostico>();

                try
                {
                    EstspIndDiagnosticoSelResult wsDiagnosticos = null;
                    using (WsindicadoresestadisticasWebClient wsClient = new WsindicadoresestadisticasWebClient())
                    {
                        wsDiagnosticos = wsClient.estspIndDiagnosticoSel(string.Join(",", usuarios),
                                                                         string.Join(",", areas),
                                                                         fechaInicio,
                                                                         fechaFin);
                    }

                    indicadores = TransformWSDiagnosticosToDiagnosticos(wsDiagnosticos);
                }
                catch (Exception ex)
                {
                    String error = ex.Message;
                    Console.WriteLine("Error = " + error);
                    t.TraceError("Error = " + error);
                }

                return indicadores;
            }
        }

        public IEnumerable<Paciente> GetPacientesDiagnosticados(IEnumerable<decimal> usuarios, IEnumerable<decimal> areas, DateTime fechaInicio, DateTime fechaFin, decimal diagnosticoId)
        {
            using (Tracer t = new Tracer())
            {
                t.TraceVerbose("GetPacientesDiagnosticados usuarios: [{0}], areas: [{1}], fechaInicio: [{2}], fechaFin: [{3}], diagnosticoId: [{4}]",
                               string.Join(",", usuarios), string.Join(",", areas), fechaInicio, fechaFin, diagnosticoId);

                IEnumerable<Paciente> pacientes = new List<Paciente>();

                try
                {
                    EstspPacientesDiagSelResult wsPacientes = null;
                    using (WsindicadoresestadisticasWebClient wsClient = new WsindicadoresestadisticasWebClient())
                    {
                        wsPacientes = wsClient.estspPacientesDiagSel(string.Join(",", usuarios),
                                                                     string.Join(",", areas),
                                                                     fechaInicio,
                                                                     fechaFin,
                                                                     diagnosticoId);
                    }

                    pacientes = TransformWSPacientesToPacientes(wsPacientes);
                }
                catch (Exception ex)
                {
                    String error = ex.Message;
                    Console.WriteLine("Error = " + error);
                    t.TraceError("Error = " + error);
                }

                return pacientes;
            }
        }

        public IndicadorPaciente GetIndicadoresPacientes(IEnumerable<decimal> usuarios, IEnumerable<decimal> areas, DateTime fechaInicio, DateTime fechaFin)
        {
            using (Tracer t = new Tracer())
            {
                t.TraceVerbose("GetIndicadoresDiagnostico usuarios: [{0}], areas: [{1}], fechaInicio: [{2}], fechaFin: [{3}]",
                               string.Join(",", usuarios), string.Join(",", areas), fechaInicio, fechaFin);

                IndicadorPaciente indicadores = new IndicadorPaciente();

                string usuariosList = GetList(usuarios);
                string areasList = GetList(areas);

                try
                {
                    EstspIndPacienteSelResult wsPacientes = null;
                    using (WsindicadoresestadisticasWebClient wsClient = new WsindicadoresestadisticasWebClient())
                    {
                        wsPacientes = wsClient.estspIndPacienteSel(usuariosList,
                                                                   areasList,
                                                                   fechaInicio,
                                                                   fechaFin);
                    }
                    
                             
                    decimal atendidos = GetIndicadoresPacientesAtendidos(usuarios, areas,fechaInicio, fechaFin);		
		
                    indicadores = TransformWSPacientesToPaciente(wsPacientes);		               
		
                    indicadores.Atendidos = atendidos;
                }
                catch (Exception ex)
                {
                    String error = ex.Message;
                    Console.WriteLine("Error = " + error);
                    t.TraceError("Error = " + error);
                }

                return indicadores;
            }
        }

        private IndicadorIndicadores TransformWSIndicadoresToIndicador(EstspIndIndicadoresSelResult indicador)
        {
            IndicadorIndicadores result = new IndicadorIndicadores()
            {
                Farmacologia = indicador.indicadoresIndicaciones.First().farmacologia,
                Imagenologia = indicador.indicadoresIndicaciones.First().imagenologia,
                Laboratorio = indicador.indicadoresIndicaciones.First().laboratorio
            };

            return result;
        }

        private IndicadorCanalesVirtuales TransformWSCanalesToCanal(EstspIndCanVirtualSelResult canal)
        {
            IndicadorCanalesVirtuales result = new IndicadorCanalesVirtuales()
            {
                CRM = canal.indicadoresCanalesVirtuales.Where(m => m.msjnuEstadoMensaje == (int)EstadoMensaje.ABIERTO && m.msjnuFuente == (int)Fuente.CRM).Count(),
                CRMRespuesta = canal.indicadoresCanalesVirtuales.Where(m => m.msjnuEstadoMensaje == (int)EstadoMensaje.CERRADO && m.msjnuFuente == (int)Fuente.CRM).Count(),
                PortalPacientes = canal.indicadoresCanalesVirtuales.Where(m => m.msjnuEstadoMensaje == (int)EstadoMensaje.ABIERTO && m.msjnuFuente == (int)Fuente.PortalSalud).Count(),
                PortalPacientesRespuesta = canal.indicadoresCanalesVirtuales.Where(m => m.msjnuEstadoMensaje == (int)EstadoMensaje.CERRADO && m.msjnuFuente == (int)Fuente.PortalSalud).Count(),
                RCE = canal.indicadoresCanalesVirtuales.Where(m => m.msjnuEstadoMensaje == (int)EstadoMensaje.ABIERTO && m.msjnuFuente == (int)Fuente.RCE).Count(),
                RCERespuesta = canal.indicadoresCanalesVirtuales.Where(m => m.msjnuEstadoMensaje == (int)EstadoMensaje.CERRADO && m.msjnuFuente == (int)Fuente.PortalSalud).Count(),
                TotalMensajes = canal.indicadoresCanalesVirtuales.Where(m => m.msjnuEstadoMensaje == (int)EstadoMensaje.ABIERTO).Count(),
                TotalMensajesRespuesta = canal.indicadoresCanalesVirtuales.Where(m => m.msjnuEstadoMensaje == (int)EstadoMensaje.CERRADO).Count()
            };
            return result;
        }

        private IEnumerable<IndicadorDiagnostico> TransformWSDiagnosticosToDiagnosticos(EstspIndDiagnosticoSelResult diagnosticos)
        {
            List<IndicadorDiagnostico> result = new List<IndicadorDiagnostico>();

            if ((diagnosticos != null) && (diagnosticos.indicadoresDiagnosticos != null))
            {
                foreach (var diagnostico in diagnosticos.indicadoresDiagnosticos)
                {
                    result.Add(new IndicadorDiagnostico()
                    {
                        Id = diagnostico.id,
                        Nombre = diagnostico.nombre,
                        Numero = diagnostico.numero
                    });
                }
            }

            return result;
        }

        private IEnumerable<Paciente> TransformWSPacientesToPacientes(EstspPacientesDiagSelResult pacientes)
        {
            List<Paciente> result = new List<Paciente>();

            if ((pacientes != null) && (pacientes.pacientesDiagnostico != null))
            {
                foreach (var paciente in pacientes.pacientesDiagnostico)
                {
                    result.Add(new Paciente()
                    {
                        Nombre = paciente.nombre
                    });
                }
            }

            return result;
        }

        private IndicadorPaciente TransformWSPacientesToPaciente(EstspIndPacienteSelResult paciente)
        {
            IndicadorPaciente result = new IndicadorPaciente()
            {
                Atendidos = paciente.indicadoresPacientes.First().atendidos,
                Etiquetados = paciente.indicadoresPacientes.First().etiquetados
            };

            return result;
        }

        private string GetList(IEnumerable<decimal> list)
        {
            string lista = "";

            if (list == null)
                return "";

            foreach (var item in list)
            {
                if (lista == "")
                    lista = string.Format("{0}", item);
                else
                    lista = string.Format("{0}, {1}", lista, item);
            }
            return lista;
        }

        public decimal GetIndicadoresCRM(decimal usuario, DateTime fechaInicio, DateTime fechaFin)
        {
            decimal ppnId = usuario;

            decimal crm = 0;

            using (Tracer t = new Tracer())
            {
                t.TraceVerbose("llamada a GetCabecerasMensaje ppnId: [{0}]", ppnId);

                try
                {
                    var cliente = new WsIntegracionCRM.AL_INTEGRACION_NUCLEO_PortTypeClient();

                    using (ServiceChannel.AsDisposable(cliente))
                    {
                        WsIntegracionCRM.AL_CONS_REQXMED_RESUMEN_CREQType ppnIdResumen = new WsIntegracionCRM.AL_CONS_REQXMED_RESUMEN_CREQType()
                        {
                            AL_CONS_REQXMED_RESUMEN_PREQ = new WsIntegracionCRM.AL_CONS_REQXMED_RESUMEN_PREQ_TypeShape()
                            {
                                AL_PPN_PROF_WKI = new WsIntegracionCRM.AL_PPN_PROF_WKIMsgDataRecord_TypeShape() { AL_PPN_PROF = ppnId.ToString() }
                            }
                        };

                        var result = cliente.AL_CONS_REQXMED_RESUMEN(ppnIdResumen);

                        if (result != null && result.Any(m => m != null && m.AL_REQCDRES_WKI != null))
                        {
                            crm = result.Where(m => m != null
                                                && m.AL_REQCDRES_WKI != null
                                                && Convert.ToDateTime(m.AL_REQCDRES_WKI.ROW_ADDED_DTTM).Date >= fechaInicio.Date
                                                && fechaFin.Date >= Convert.ToDateTime(m.AL_REQCDRES_WKI.ROW_ADDED_DTTM).Date)
                                                .Count();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error = " + ex.Message);
                    t.TraceError("Error = " + ex.Message);
                }
            }

            return crm;
        }

        public decimal GetIndicadoresPacientesAtendidos(IEnumerable<decimal> usuarios, IEnumerable<decimal> areas, DateTime fechaInicio, DateTime fechaFin)
        {
            using (Tracer t = new Tracer())
            {
                t.TraceVerbose("GetIndicadoresDiagnostico usuarios: [{0}], areas: [{1}], fechaInicio: [{2}], fechaFin: [{3}]",
                               string.Join(",", usuarios), string.Join(",", areas), fechaInicio, fechaFin);

                decimal indicadores = 0;

                decimal agenda = 4;
                string usuariosList = GetList(usuarios);
                string areasList = GetList(areas);

                try
                {
                    WsAgendaMedica.IntspCantidadEstadosResult wsAgenda = null;
                    using (WsagendamedicaWebClient wsClient = new WsagendamedicaWebClient())
                    {
                        wsAgenda = wsClient.intspCantidadEstados(  agenda,
                                                                   fechaInicio,
                                                                   fechaFin.AddDays(0).AddHours(23).AddMinutes(59),
                                                                   usuariosList,
                                                                   areasList
                                                                   );
                    }

                    indicadores = wsAgenda.poIntnuCantidad;
                }
                catch (Exception ex)
                {
                    String error = ex.Message;
                    Console.WriteLine("Error = " + error);
                    t.TraceError("Error = " + error);
                }

                return indicadores;
            }
        }
    }
}