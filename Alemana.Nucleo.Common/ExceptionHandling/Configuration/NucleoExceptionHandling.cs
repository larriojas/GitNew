using System;

namespace Alemana.Nucleo.Common.ExceptionHandling.Configuration
{
    public static class NucleoExceptionHandling
    {
        public static string HandleException(Exception ex)
        {
            //Episodio cerrado
            if (ex.Message.Contains("ORA-20014"))
                return "No se permite realizar nuevas acciones sobre el paciente ya que el episodio se encuentra cerrado.";

            if (ex.Message.Contains("ORA-20022"))
                return "No es posible realizar la desasignación de tratancia, pues el rol de usuario no corresponde";

            if (ex.Message.Contains("ORA-20023"))
                return "No es posible realizar la asignación de tratancia, pues el rol de usuario no corresponde";

            if (ex.Message.Contains("ORA-20051"))
                return "Desde Núcleo los pacientes hospitalizados son solo de consulta.";

            if (ex.Message.Contains("ORA-20080"))
                return "No se permite realizar esta acción sobre el paciente ya que el encuentro seleccionado se encuentra abierto.";

            if (ex.Message.Contains("ORA-20015"))
                return "Se detectó anomalía con Evoluciones; Favor informar a Soporte Nucleo(Anexo 3486)";

            if (ex.Message.Contains("ORA-20040"))
                return "Ud. no tiene privilegios para asignarse la tratancia. \n \n Si considera que es un error, favor enviar mail a nucleo@alemana.cl solicitando este privilegio.";

            return string.Empty;
        }
    }
}
