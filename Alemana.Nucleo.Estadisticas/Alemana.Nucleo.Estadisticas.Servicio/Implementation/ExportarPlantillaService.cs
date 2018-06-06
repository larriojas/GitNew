using Alemana.Nucleo.Common.ComponentModel;
using Alemana.Nucleo.Common.Extensions;
using Alemana.Nucleo.Common.Tracing;
using Alemana.Nucleo.Estadisticas.Contrato.Models;
using Alemana.Nucleo.Estadisticas.Contrato.ServiceInterfaces;
using Alemana.Nucleo.Estadisticas.Servicio.WsIncidenciaModulos;
using Alemana.Nucleo.Estadisticas.Servicio.WsPlantilla;
using Alemana.Nucleo.Shared.Contrato.ServiceInterfaces;
using Alemana.Nucleo.Shared.Entities;
using Alemana.Nucleo.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alemana.Nucleo.Estadisticas.Servicio.Implementation
{
    public class ExportarPlantillaService : IExportarPlantillaService
    {
        public PlantillaEstadistica GetPlantilla(decimal idPlantilla)
        {
            using (Tracer t = new Tracer())
            {
                t.TraceVerbose("GetPlantilla idPlantilla: [{0}]", idPlantilla);

                PlantillaEstadistica plantilla = new PlantillaEstadistica()
                {
                    Codigo = idPlantilla,
                    Descripcion = string.Empty
                };

                try
                {
                    IListaLinealService listaService = ComponentContainer.Instance.Resolve<IListaLinealService>();
                    plantilla.Modulos = ConvertirADTOLocal(listaService.GetModulos(idPlantilla, Shared.Contrato.Models.Estado.Ambas));
                    foreach (var modulo in plantilla.Modulos)
                    {
                        modulo.Agrupadores = ConvertirADTOLocal(listaService.GetAgrupador(modulo.Codigo, Shared.Contrato.Models.Estado.Ambas));

                        foreach (var agrupador in modulo.Agrupadores)
                        {
                            agrupador.Variables = ConvertirADTOLocal(listaService.GetVariables(agrupador.Codigo, Shared.Contrato.Models.Estado.Ambas));
                        }
                    }
                }
                catch (Exception ex)
                {
                    String error = ex.Message;
                    Console.WriteLine("Error = " + error);
                    t.TraceError("Error = " + error);
                }

                return plantilla;
            }
        }

        public IEnumerable<PlantillaEstadistica> GetPlantillas(IEnumerable<decimal> usuarios, IEnumerable<decimal> areas, DateTime fechaInicio, DateTime fechaFin, Contrato.Models.TipoFecha tipo)
        {
            using (Tracer t = new Tracer())
            {
                t.TraceVerbose("GetPlantillas usuarios: [{0}], areas: [{1}], fechaInicio: [{2}], fechaFin: [{3}], tipo: [{4}]",
                               string.Join(",", usuarios), string.Join(",", areas), fechaInicio, fechaFin, tipo.ToString());

                IEnumerable<PlantillaEstadistica> plantillas = new List<PlantillaEstadistica>();

                string usuariosList = GetList(usuarios);
                string areasList = GetList(areas);

                try
                {
                    EstspPlantillasSelResult wsPlantillas = null;
                    using (WsplantillaestadisticaWebClient wsClient = new WsplantillaestadisticaWebClient())
                    {
                        wsPlantillas = wsClient.estspPlantillasSel(
                                                                    usuariosList,
                                                                    areasList,
                                                                   fechaInicio,
                                                                   fechaFin,
                                                                   (int)tipo);
                    }

                    plantillas = ConvertirADTOLocal(wsPlantillas);
                }
                catch (Exception ex)
                {
                    String error = ex.Message;
                    Console.WriteLine("Error = " + error);
                    t.TraceError("Error = " + error);
                }

                return plantillas;
            }
        }

        public IEnumerable<Paciente> GetPacientesPlantilla(decimal idPlantilla, IEnumerable<decimal> usuarios, IEnumerable<decimal> areas, DateTime fechaInicio, DateTime fechaFin, decimal tipoFecha)
        {
            using (Tracer t = new Tracer())
            {
                t.TraceVerbose("GetPacientesPlantilla idPlantilla: [{0}], usuarios: [{1}], areas: [{2}], fechaFin: [{3}], fechaInicio: [{4}]",
                               string.Join(",", idPlantilla), string.Join(",", usuarios), areas, fechaInicio, fechaFin);

                List<Paciente> pacientes = new List<Paciente>();

                string usuariosList = GetList(usuarios);
                string areasList = GetList(areas);

                try
                {
                    EstspPacientesPlantillaSelResult wsPacientes = null;

                    using (WsplantillaestadisticaWebClient wsClient = new WsplantillaestadisticaWebClient())
                    {
                        wsPacientes = wsClient.estspPacientesPlantillaSel
                                                (
                                                    idPlantilla,
                                                    usuariosList,
                                                    areasList,
                                                    fechaInicio,
                                                    fechaFin,
                                                    tipoFecha
                                                );

                        if (wsPacientes == null || wsPacientes.pacientes == null)
                            return null;

                        

                        foreach (var item in wsPacientes.pacientes)
                        {
                            Paciente paciente = new Paciente();

                            paciente.Nombre = item.nombre;
                            paciente.IdPaciente = item.pacienteid;

                            pacientes.Add(paciente);
                        }
                    }
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

        public IEnumerable<IncidenciaModulo> GetIncidenciasModulos(decimal idPlantilla, decimal idPaciente, IEnumerable<decimal> usuarios, IEnumerable<decimal> areas, DateTime fechaInicio, DateTime fechaFin, decimal tipoFecha)
        {
            //Codigo
            //Fecha
            //Variables
            //    Codigo
            //    Valor
            using (Tracer t = new Tracer())
            {
                t.TraceVerbose("GetIncidenciasModulos idPlantilla: [{0}], usuarios: [{1}], areas: [{2}], fechaFin: [{3}], fechaInicio: [{4}]",
                               string.Join(",", idPlantilla), string.Join(",", usuarios), areas, fechaInicio, fechaFin);

                string usuariosList = GetList(usuarios);
                string areasList = GetList(areas);

                try
                {
                    EstspIncsmoduloPacienteSelResult wsPacientes = null;

                    using (WsplantillaestadisticaWebClient wsClient = new WsplantillaestadisticaWebClient())
                    {
                        wsPacientes = wsClient.estspIncsmoduloPacienteSel
                                                (
                                                    idPlantilla,
                                                    idPaciente,
                                                    usuariosList,
                                                    areasList,
                                                    fechaInicio,
                                                    fechaFin,
                                                   tipoFecha
                                                );

                        if (wsPacientes == null || !wsPacientes.incidencias.HasElements())
                            return null;

                        return wsPacientes.incidencias.Select(i => new IncidenciaModulo { Fecha = i.fecha, IdPlantilla = i.codigo, IdPaciente = idPaciente });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error = " + ex.Message);
                    t.TraceError("Error = " + ex.Message);

                    return Enumerable.Empty<IncidenciaModulo>();
                }
            }
        }

        private PlantillaEstadistica ConvertirADTOLocal(Alemana.Nucleo.Shared.Contrato.Models.Plantilla item)
        {
            PlantillaEstadistica plantilla = new PlantillaEstadistica()
            {
                Codigo = item.Codigo,
                Descripcion = item.Descripcion
            };

            return plantilla;
        }

        private IEnumerable<Modulo> ConvertirADTOLocal(IEnumerable<Alemana.Nucleo.Shared.Contrato.Models.Modulo> items)
        {
            List<Modulo> modulos = new List<Modulo>();

            foreach (var item in items)
            {
                modulos.Add(new Modulo()
                    {
                        Codigo = item.Codigo,
                        Nombre = item.Nombre,
                        Orden = item.Orden,
                        IdPlantilla = item.IdPlantilla
                    });
            }

            return modulos;
        }

        private IEnumerable<Agrupador> ConvertirADTOLocal(IEnumerable<Alemana.Nucleo.Shared.Contrato.Models.Agrupador> items)
        {
            List<Agrupador> agrupadores = new List<Agrupador>();

            foreach (var item in items)
            {
                agrupadores.Add(new Agrupador()
                {
                    Codigo = item.Codigo,
                    Nombre = item.Nombre,
                    Orden = item.Orden,
                    IdModulo = item.IdModulo
                });
            }

            return agrupadores;
        }

        private IEnumerable<Variable> ConvertirADTOLocal(IEnumerable<Alemana.Nucleo.Shared.Contrato.Models.Variables.Variable> items)
        {
            List<Variable> variables = new List<Variable>();

            foreach (var item in items)
            {
                variables.Add(new Variable()
                {
                    Codigo = item.Codigo,
                    Nombre = item.Nombre,
                    NombreExportar = item.NombreExportar,
                    Orden = item.Orden,
                    IdAgrupador = item.IdAgrupador
                });
            }

            return variables;
        }

        private IEnumerable<PlantillaEstadistica> ConvertirADTOLocal(EstspPlantillasSelResult items)
        {
            List<PlantillaEstadistica> plantillas = new List<PlantillaEstadistica>();

            if (items.plantillas != null)
            {
                foreach (var item in items.plantillas)
                {
                    plantillas.Add(new PlantillaEstadistica()
                    {
                        Codigo = item.id,
                        Descripcion = item.descripcion
                    });
                }
            }

            return plantillas;
        }

        public IEnumerable<ValorVariable> GetValoresVariablesIncidenciaModulo(decimal idIncidenciaModulo)
        {
            using (Tracer t = new Tracer())
            {
                t.TraceVerbose("llamada a GetModuloHistorico idPlantilla: [{0}] ", idIncidenciaModulo);

                try
                {
                    var cliente = new WsincidenciamoduloWebClient();

                    using (ServiceChannel.AsDisposable(cliente))
                    {
                        var result = cliente.evpspVarIncModSel(idIncidenciaModulo);

                        if (result.poFcenuCodret != 0)
                            throw new ArgumentException(String.Format("Error en BD. Codigo de error: {0}. Descripcion de error: {1}", result.poFcenuCodret, result.poFcevaDesret));

                        if (result.variableIncidenciaModulo != null && result.variableIncidenciaModulo.Any())
                            return result.variableIncidenciaModulo.Select(v => new ValorVariable()
                            {
                                Id = v.id,
                                IdVariable = v.idVariable,
                                IdAgrupador = v.idAgrupador,
                                IdModuloIncidencia = idIncidenciaModulo,
                                Valor = v.valor,
                                Fila = v.fila,
                                Columna = v.columna,
                                JustificacionImposibilidad = v.justificacion
                            });
                    }
                }
                catch (Exception ex)
                {
                    String error = ex.Message;
                    Console.WriteLine("Error = " + error);
                    t.TraceError("Error = " + error);
                }
            }

            return Enumerable.Empty<ValorVariable>();
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
    }
}