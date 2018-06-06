using Alemana.Nucleo.Estadisticas.Contrato.Models;
using Alemana.Nucleo.Shared.CurrentData;
using Alemana.Nucleo.Shared.Entities;
using System;
using System.Collections.Generic;

namespace Alemana.Nucleo.Estadisticas.Servicio.MocksImplementation
{
    public class DataHelper
    {
        public static IEnumerable<Profesional> Profesionales { get; set; }

        public static IEnumerable<Area> Areas { get; set; }

        public static IndicadorIndicadores IndicadorIndicadores { get; set; }

        public static IndicadorCanalesVirtuales IndicadorCanalesVirtuales { get; set; }

        public static IEnumerable<IndicadorDiagnostico> IndicadoresDiagnosticos { get; set; }

        public static IndicadorPaciente IndicadorPaciente { get; set; }

        public static IEnumerable<PlantillaEstadistica> Plantillas { get; set; }

        public static IEnumerable<Paciente> PacientesPlantilla { get; set; }

        public static IEnumerable<IncidenciaModulo> IncidenciasModulos { get; set; }

        static DataHelper()
        {
            Profesionales = ObtenerProfesionales();
            Areas = ObtenerAreas();
            IndicadorIndicadores = ObtenerIndicadorIndicadores();
            IndicadorCanalesVirtuales = ObtenerIndicadorCanalesVirtuales();
            IndicadoresDiagnosticos = ObtenerIndicadoresDiagnosticos();
            IndicadorPaciente = ObtenerIndicadorPaciente();
            Plantillas = ObtenerPlantillas();
            PacientesPlantilla = ObtenerPacientesPlantilla();
            IncidenciasModulos = ObtenerIncidenciasModulo();
        }

        public static decimal GetPpnId()
        {
            return CurrentProfesionalData.PersonaID;
        }

        private static IEnumerable<Profesional> ObtenerProfesionales()
        {
            return new[]
            {
                new Profesional()
                {
                    Id = 1,
                    Nombre = "PEREZ ULLOA Ignacio"
                },
                new Profesional()
                {
                    Id = 2,
                    Nombre = "ZARAGOZA Ernesto"
                },
                new Profesional()
                {
                    Id = 3,
                    Nombre = "AGÜERO Roberto"
                },
                new Profesional()
                {
                    Id = 4,
                    Nombre = "BENÍTEZ Carlos"
                },
                new Profesional()
                {
                    Id = 5,
                    Nombre = "MARTÍNEZ Luis"
                },
                new Profesional()
                {
                    Id = 6,
                    Nombre = "RAMÍREZ Miguel"
                },
                new Profesional()
                {
                    Id = 7,
                    Nombre = "SUÁREZ Alicia"
                },
                new Profesional()
                {
                    Id = 8,
                    Nombre = "RODRÍGUEZ María"
                },
                new Profesional()
                {
                    Id = 9,
                    Nombre = "PEREIRA José"
                },
                new Profesional()
                {
                    Id = 10,
                    Nombre = "PÉREZ Mario"
                }
            };
        }

        private static IEnumerable<Area> ObtenerAreas()
        {
            return new[]
            {
                new Area()
                {
                    Id = 1,
                    Nombre = "Nefrología"
                },
                new Area()
                {
                    Id = 2,
                    Nombre = "Traumatología"
                },
                new Area()
                {
                    Id = 3,
                    Nombre = "Otorrinolaringología"
                },
                new Area()
                {
                    Id = 4,
                    Nombre = "Kinesiología"
                },
                new Area()
                {
                    Id = 5,
                    Nombre = "Pediatría"
                },
                new Area()
                {
                    Id = 6,
                    Nombre = "Podología"
                },
                new Area()
                {
                    Id = 7,
                    Nombre = "Psiquiatría"
                },
                new Area()
                {
                    Id = 8,
                    Nombre = "Radiología"
                },
                new Area()
                {
                    Id = 9,
                    Nombre = "Oftalmología"
                },
                new Area()
                {
                    Id = 10,
                    Nombre = "Neurocirugía"
                }
            };
        }

        private static IndicadorIndicadores ObtenerIndicadorIndicadores()
        {
            Random rand = new Random();

            return new IndicadorIndicadores()
            {
                Farmacologia = rand.Next(0, 10),
                Imagenologia = rand.Next(0, 10),
                Laboratorio = rand.Next(0, 10)
            };
        }

        private static IndicadorCanalesVirtuales ObtenerIndicadorCanalesVirtuales()
        {
            Random rand = new Random();

            var indicador = new IndicadorCanalesVirtuales();
            indicador.CRM = rand.Next(0, 100);
            indicador.CRMRespuesta = rand.Next(0, (int)indicador.CRM);
            indicador.PortalPacientes = rand.Next(0, 100);
            indicador.PortalPacientesRespuesta = rand.Next(0, (int)indicador.PortalPacientes);
            indicador.RCE = rand.Next(0, 100);
            indicador.RCERespuesta = rand.Next(0, (int)indicador.RCE);
            indicador.TotalMensajes = indicador.CRM + indicador.PortalPacientes + indicador.RCE;
            indicador.TotalMensajesRespuesta = indicador.CRMRespuesta + indicador.PortalPacientesRespuesta + indicador.RCERespuesta;

            return indicador;
        }

        private static IEnumerable<IndicadorDiagnostico> ObtenerIndicadoresDiagnosticos()
        {
            Random rand = new Random();

            return new[]
            {
                new IndicadorDiagnostico() { Numero = rand.Next(0, 10), Nombre = "HIPERTENSIÓN ARTERIAL ESENCIAL" },
                new IndicadorDiagnostico() { Numero = rand.Next(0, 10), Nombre = "DIABETES MELLITUS TIPO 2" },
                new IndicadorDiagnostico() { Numero = rand.Next(0, 10), Nombre = "FORMULARIO ANTECEDENTES LABORALES" },
                new IndicadorDiagnostico() { Numero = rand.Next(0, 10), Nombre = "HIPERTENSIÓN ARTERIAL ESENCIAL B" },
                new IndicadorDiagnostico() { Numero = rand.Next(0, 10), Nombre = "DIABETES MELLITUS TIPO 2 B" },
                new IndicadorDiagnostico() { Numero = rand.Next(0, 10), Nombre = "FORMULARIO ANTECEDENTES LABORALES B" },
            };
        }

        private static IndicadorPaciente ObtenerIndicadorPaciente()
        {
            Random rand = new Random();

            return new IndicadorPaciente()
            {
                Atendidos = rand.Next(0, 100),
                Etiquetados = rand.Next(0, 100)
            };
        }

        private static IEnumerable<PlantillaEstadistica> ObtenerPlantillas()
        {
            return new[]
            {
                new PlantillaEstadistica()
                {
                    Codigo = 1,
                    Descripcion = "Plantilla 1",
                    Modulos = new []
                    {
                        new Modulo()
                        {
                            Codigo = 11,
                            IdPlantilla = 1,
                            Nombre = "Modulo 11_1",
                            Orden = 11,
                            Agrupadores = GetAgrupadores(1, 11, "")
                        },
                        new Modulo()
                        {
                            Codigo = 12,
                            IdPlantilla = 1,
                            Nombre = "Modulo 12_1",
                            Orden = 12,
                            Agrupadores = GetAgrupadores(2, 12, "")
                        },
                        new Modulo()
                        {
                            Codigo = 13,
                            IdPlantilla = 1,
                            Nombre = "Modulo 13_1",
                            Orden = 13,
                            Agrupadores = GetAgrupadores(3, 13, "")
                        }
                    }
                },
                new PlantillaEstadistica()
                {
                    Codigo = 2,
                    Descripcion = "Plantilla 2",
                    Modulos = new []
                    {
                        new Modulo()
                        {
                            Codigo = 21,
                            IdPlantilla = 2,
                            Nombre = "Modulo 21_1",
                            Orden = 21,
                            Agrupadores = GetAgrupadores(1, 21, "")
                        },
                        new Modulo()
                        {
                            Codigo = 22,
                            IdPlantilla = 2,
                            Nombre = "Modulo 22_1",
                            Orden = 22,
                            Agrupadores = GetAgrupadores(5, 22, "")
                        }
                    }
                },
                new PlantillaEstadistica()
                {
                    Codigo = 3,
                    Descripcion = "Plantilla 3",
                    Modulos = new []
                    {
                        new Modulo()
                        {
                            Codigo = 31,
                            IdPlantilla = 3,
                            Nombre = "Modulo 31_1",
                            Orden = 31,
                            Agrupadores = GetAgrupadores(2, 31, "")
                        },
                        new Modulo()
                        {
                            Codigo = 32,
                            IdPlantilla = 3,
                            Nombre = "Modulo 32_1",
                            Orden = 32,
                            Agrupadores = GetAgrupadores(1, 32, "")
                        }
                    }
                },
                new PlantillaEstadistica()
                {
                    Codigo = 4,
                    Descripcion = "Plantilla 4",
                    Modulos = new []
                    {
                        new Modulo()
                        {
                            Codigo = 41,
                            IdPlantilla = 24,
                            Nombre = "Modulo 41_1",
                            Orden = 41,
                            Agrupadores = GetAgrupadores(1, 41, "")
                        }
                    }
                }
            };
        }

        private static IEnumerable<Paciente> ObtenerPacientesPlantilla()
        {
            List<Paciente> pacientes = new List<Paciente>();

            for (int i = 1; i <= 10; i++)
            {
                pacientes.Add(new Paciente()
                    {
                        IdPaciente = i,
                        Nombre = "Paciente Nro. " + i
                    });
            }

            return pacientes;
        }

        private static IEnumerable<IncidenciaModulo> ObtenerIncidenciasModulo()
        {
            List<IncidenciaModulo> incidenciasModulos = new List<IncidenciaModulo>();

            Random rand = new Random();

            foreach (var plantilla in Plantillas)
            {
                foreach (var modulo in plantilla.Modulos)
                {
                    foreach (var paciente in PacientesPlantilla)
                    {
                        if (rand.Next(2) % 2 == 0)
                        {
                            List<IncidenciaModulo> incidenciasModulosTemp = new List<IncidenciaModulo>();

                            int qtyModuleInstanceForThisPatient = rand.Next(1, 3);
                            int startDay = rand.Next(5);
                            for (int i = 0; i <= qtyModuleInstanceForThisPatient; i++)
                            {
                                incidenciasModulosTemp.Add(new IncidenciaModulo()
                                {
                                    Codigo = modulo.Codigo,
                                    IdPlantilla = modulo.IdPlantilla,
                                    IdPaciente = paciente.IdPaciente,
                                    Nombre = modulo.Nombre,
                                    Orden = modulo.Orden,
                                    Fecha = DateTime.Now.AddDays(startDay + i)
                                });
                            }

                            foreach (var incidenciaModulo in incidenciasModulosTemp)
                            {
                                List<IncidenciaVariable> incidenciasVariablesTemp = new List<IncidenciaVariable>();
                                foreach (var agrupador in modulo.Agrupadores)
                                {
                                    foreach (var variable in agrupador.Variables)
                                    {
                                        incidenciasVariablesTemp.Add(new IncidenciaVariable()
                                            {
                                                Codigo = variable.Codigo,
                                                IdIncidenciaModulo = incidenciaModulo.Codigo,
                                                Nombre = variable.Nombre,
                                                NombreExportar = variable.NombreExportar,
                                                Orden = variable.Orden,
                                                Valor = "Valor " + rand.Next(1000)
                                            });
                                    }
                                }

                                incidenciaModulo.Variables = incidenciasVariablesTemp;

                                incidenciasModulos.Add(incidenciaModulo);
                            }
                        }
                    }
                }
            }

            return incidenciasModulos;
        }

        private static IEnumerable<Agrupador> GetAgrupadores(int cantidad, decimal idModulo, string nombreModulo)
        {
            List<Agrupador> agrupadores = new List<Agrupador>();
            for (int i = 1; i <= cantidad; i++)
            {
                agrupadores.Add(new Agrupador()
                {
                    Codigo = (idModulo * 10) + i,
                    IdModulo = idModulo,
                    Nombre = string.Format("Agrupador {0}{1}_{0}", nombreModulo, i.ToString()),
                    Orden = (idModulo * 10) + i,
                    Variables = GetVariables(5, (idModulo * 10) + i, string.Format("{0}{1}_{0}", nombreModulo, i.ToString()))
                });
            }
            return agrupadores;
        }

        private static IEnumerable<Variable> GetVariables(int cantidad, decimal idAgrupador, string nombreAgrupador)
        {
            List<Variable> vars = new List<Variable>();
            for (int i = 1; i <= cantidad; i++)
            {
                vars.Add(new Variable()
                        {
                            Codigo = (idAgrupador * 10) + i,
                            IdAgrupador = idAgrupador,
                            Nombre = string.Format("Variable {0}{1}_{0}", nombreAgrupador, i.ToString()),
                            NombreExportar = string.Format("Variable {0}{1}", nombreAgrupador, i.ToString()),
                            Orden = (idAgrupador * 10) + i
                        });
            }
            return vars;
        }
    }
}