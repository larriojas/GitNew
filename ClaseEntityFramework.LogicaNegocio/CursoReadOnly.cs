using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaseEntityFramework.Entidades;
using Csla;

namespace ClaseEntityFramework.LogicaNegocio
{
    [Serializable]
    public class CursoReadOnly : ReadOnlyBase<CursoReadOnly>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int NroCreditos { get; set; }

        public static CursoReadOnly GetReadOnlyChild(Curso entidad)
        {
            return DataPortal.FetchChild<CursoReadOnly>(entidad);
        }

        protected void Child_Fetch(Curso entidad)
        {
            Id = entidad.CursoId;
            Nombre = entidad.Nombre;
            Codigo = entidad.Codigo;
            NroCreditos = entidad.NroCreditos;
        }
    }
}
