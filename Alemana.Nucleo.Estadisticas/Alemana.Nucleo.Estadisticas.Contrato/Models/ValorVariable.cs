using System;

namespace Alemana.Nucleo.Estadisticas.Contrato.Models
{
    public class ValorVariable
    {
        public decimal Id { get; set; }

        public decimal IdVariable { get; set; }

        public decimal IdAgrupador { get; set; }

        public decimal IdModuloIncidencia { get; set; }

        public decimal IdModuloPaciente { get; set; }

        #region Variables Numéricas

        public decimal IdConcepto { get; set; }

        public decimal IdTablaActualizarDatos { get; set; }

        #endregion Variables Numéricas

        #region Variables tipo Item Lista y Matriz

        public decimal DescriptionId { get; set; }

        public string DescriptionNombre { get; set; }

        public decimal IdCategoriaNosologia { get; set; }

        #endregion Variables tipo Item Lista y Matriz

        public string Valor { get; set; }

        public decimal IdCreador { get; set; }

        public DateTime FechaCreacion { get; set; }

        public decimal IdModificador { get; set; }

        public DateTime FechaModificacion { get; set; }

        public string JustificacionImposibilidad { get; set; }

        public decimal Fila { get; set; }

        public decimal Columna { get; set; }

        public bool PuedeEjecutarAccionNosologica { get; set; }
    }
}