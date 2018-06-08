
namespace Alemana.Nucleo.Vacunas.Contrato.Models
{
    public class VacunasModel
    {
        public decimal VacunaId { get; set; }
        public string Vacuna { get; set; }
        public decimal vacu_codigo               { get; set; }
        public string vacu_nombre       { get; set; }           
        public string vacu_pni       { get; set; }
        public string vacu_vigente       { get; set; }
        public string vacu_contraindicaciones       { get; set; }
        public string vacu_lote_diluyente       { get; set; }
        public decimal vacu_orden_ministerial       { get; set; }
        public string vacu_nom_cert { get; set; }
        public decimal grupovacuna { get; set; }
    }
}
