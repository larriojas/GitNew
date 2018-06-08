using System;

namespace Alemana.Nucleo.Vacunas.Contrato.Models
{
    public class OrdenTrabajoModel
    {
        public decimal piSucuIdHolding{ get; set; } 	
        public decimal piSucuIdEmpresa{ get; set; }
        public decimal piSucuIdSucursal{ get; set; }
        public decimal piSucuId{ get; set; }
        public decimal piOrtrId{ get; set; }
        public string piOrtrTipo{ get; set; }
        public string piOrtrNumeroOp{ get; set; }
        public decimal piOrtrPpnCliente{ get; set; }
        public decimal piOrtrEdadCliente{ get; set; }
        public string piOrtrComunaCliente{ get; set; }
        public string piOrtrEstadoOp{ get; set; }
        public string piOrtrEstado{ get; set; }
        public decimal piOrtrIdConcepto{ get; set; }
        public decimal piOrtrIdSubconcepto{ get; set; }
        public string piOrtrCitacion{ get; set; }
        public decimal piOrtrCodigoUnidad{ get; set; }
        public string piOrtrUnidad{ get; set; }
        public decimal piOrtrBoxAtencion{ get; set; }
        public string piOrtrUsuarioCreacion{ get; set; }
        public decimal piOrtrCodigoComuna{ get; set; }
        public string piOrtrObservaciones{ get; set; }
        public decimal piDeotId{ get; set; }
        public decimal piVacuCodigo{ get; set; }
        public DateTime piDeotFechaAdministracion{ get; set; }//yyyy-MM-ddTHH:mm:ss
        public decimal piDeotIdSitioAdm{ get; set; }
        public decimal piDeotIdViaAdm{ get; set; }
        public string piDeotLote{ get; set; }
        public string piDeotLoteDiluyente{ get; set; }
        public string piDeotRutProfesional{ get; set; }
        public string piDeotEstado{ get; set; }
        public string piDeotUsuario{ get; set; }
        public decimal piDovaNumeroDosis{ get; set; }
        public string piVacuNombre{ get; set; }
        public decimal piDeotEdadAdminisCliente{ get; set; }
        public DateTime piDeotProxCitacion{ get; set; }
        public string piTipoOt { get; set; }

        public static string piTipoOtNormal = "NORMAL";
        public static string piTipoOtExterna = "EXTERNA";


    }
}
