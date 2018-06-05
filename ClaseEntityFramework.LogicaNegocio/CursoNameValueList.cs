using System;
using System.Linq;
using ClaseEntityFramework.Datos;
using ClaseEntityFramework.Entidades;
using Csla;
using Csla.Data.EF6;

namespace ClaseEntityFramework.LogicaNegocio
{
    [Serializable]
    public class CursoNameValueList : NameValueListBase<int, string>
    {

        public static CursoNameValueList GetCursoNameValueList()
        {
            return DataPortal.Fetch<CursoNameValueList>();
        }

        protected void DataPortal_Fetch()
        {
            IsReadOnly = false;
            using (var ctx = DbContextManager<Colegio>.GetManager())
            {
                var lista = ctx.DbContext.Set<Curso>()
                    .Where(p => p.EstadoRegistro)
                    .Select(p => new
                    {
                        Id = p.CursoId,
                        p.Codigo,
                        p.Nombre
                    });

                foreach (var item in lista)
                {
                    Add(new NameValuePair(item.Id, $"{item.Codigo}-{item.Nombre}"));
                }
            }
            IsReadOnly = true;
        }

    }
}