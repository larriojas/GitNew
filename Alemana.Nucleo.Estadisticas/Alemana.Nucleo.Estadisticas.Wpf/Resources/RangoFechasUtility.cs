using System.Collections.Generic;

namespace Alemana.Nucleo.Estadisticas.Wpf.Resources
{
    public class RangoFechasUtility
    {
        public enum RangoFecha
        {
            Todo,
            UltimoAnio,
            UltimoMes,
            UltimaSemana,
            Hoy
        }

        static RangoFechasUtility()
        {
            _rangoFechas = new Dictionary<RangoFecha, string>()
                           {
                               {RangoFecha.Todo,"Todo"},
                               {RangoFecha.UltimoAnio,"Último Año"},
                               {RangoFecha.UltimoMes,"Último Mes"},
                               {RangoFecha.UltimaSemana,"Última Semana"},
                               {RangoFecha.Hoy,"Hoy"}
                           };
        }

        private static Dictionary<RangoFecha, string> _rangoFechas;

        public static Dictionary<RangoFecha, string> RangoFechas
        {
            get { return _rangoFechas; }
        }
    }
}