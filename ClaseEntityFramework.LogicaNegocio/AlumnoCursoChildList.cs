using System;
using System.Linq;
using ClaseEntityFramework.Datos;
using ClaseEntityFramework.Entidades;
using Csla;
using Csla.Data.EF6;

namespace ClaseEntityFramework.LogicaNegocio
{
    [Serializable]
    public class AlumnoCursoChildList: BusinessListBase<AlumnoCursoChildList, AlumnoCursoChild>
    {

        public static AlumnoCursoChildList NewEditableChildList()
        {
            return DataPortal.Create<AlumnoCursoChildList>();
        }

        public static AlumnoCursoChildList GetEditableChildList(int id)
        {
            return DataPortal.Fetch<AlumnoCursoChildList>(id);
        }

        public new void Add(AlumnoCursoChild child)
        {
            if (Contains(child))
                throw new InvalidOperationException("El Curso ya ha sido asignado");

            base.Add(child);
        }

        protected void DataPortal_Fetch(int id)
        {
            using (var ctx = DbContextManager<Colegio>.GetManager())
            {
                var lista = ctx.DbContext.Set<AlumnoCurso>()
                    .Where(p => p.EstadoRegistro
                                && p.AlumnoId == id);

                foreach (var alumnoCurso in lista)
                {
                    Add(AlumnoCursoChild.GetEditableChild(alumnoCurso));
                }
            }
        }

    }
}