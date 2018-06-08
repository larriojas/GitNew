using Alemana.Nucleo.Vacunas.Contrato.Models;
using System.Collections.ObjectModel;
using System.Data;

namespace Alemana.Nucleo.Vacunas.Contrato.ServiceInterfaces
{
    public interface IVacunasService
    {
        DataTable GetListadoVacunasTodos(decimal ppn);
        ObservableCollection<ListaVacunas> GetListadoVacunasTodos2(decimal ppn, bool is_detailed = false);
        ObservableCollection<DosisModel> GetListadoDosis(decimal IdVacuna, decimal IdSucursal);
        ObservableCollection<EdadDosisModel> GetListadoEdad(decimal IdVacuna, decimal IdSucursal);
        ObservableCollection<LugarAdmModel> GetListadoLugar(decimal IdVacuna);
        ObservableCollection<SitioAdmModel> GetListadoSitioAdm();
        ObservableCollection<VacunasModel> GetListadoAllVacunas(decimal idLugar);
        decimal GetLugarSesion();
        void SaveOT(OrdenTrabajoModel Ot);
        ObservableCollection<ViaAdmModel> GetListadoViaAdm(decimal IdVacuna, decimal IdSucursal);
        ObservableCollection<SitioAdmModel> GetListadoSitioAdm(decimal IdVacuna, decimal IdSucursal);
    }
}
