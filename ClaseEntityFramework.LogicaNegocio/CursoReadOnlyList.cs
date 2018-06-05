using System;
using System.Linq;
using ClaseEntityFramework.Datos;
using ClaseEntityFramework.Entidades;
using Csla;
using Csla.Data.EF6;

namespace ClaseEntityFramework.LogicaNegocio
{
    [Serializable]
    public class CursoReadOnlyList : ReadOnlyListBase<CursoReadOnlyList, CursoReadOnly>
    {

        public static CursoReadOnlyList GetReadOnlyList()
        {
            return DataPortal.Fetch<CursoReadOnlyList>();
        }


        protected void DataPortal_Fetch()
        {
            IsReadOnly = false;
            using (var ctx = DbContextManager<Colegio>.GetManager())
            {
                var lista = ctx.DbContext.Set<Curso>()
                    .Where(p => p.EstadoRegistro)
                    .ToList();

                foreach (var curso in lista)
                {
                    Add(CursoReadOnly.GetReadOnlyChild(curso));
                }
            }
            IsReadOnly = true;
        }
    }
}