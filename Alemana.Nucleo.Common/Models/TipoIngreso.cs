using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alemana.Nucleo.Common.Models
{
    public enum TipoIngreso
    {
        [Description("Normal")]
        Normal = 1,

        [Description("Contingencia Programada")]
        ContingenciaProgramada = 2,

        [Description("Contingencia Pasiva")]
        ContingenciaPasiva = 3,

        [Description("Contingencia de Red")]
        ContingenciaRed = 4
    }
}
