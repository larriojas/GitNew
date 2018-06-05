using System;
using System.Linq;
using ClaseEntityFramework.Datos;
using Csla;
using ClaseEntityFramework.Entidades;
using Csla.Data.EF6;

namespace ClaseEntityFramework.LogicaNegocio
{
    [Serializable]
    public class AlumnoReadOnlyList : ReadOnlyListBase<AlumnoReadOnlyList, AlumnoReadOnly>
    {
        #region Factory Methods

        public static AlumnoReadOnlyList GetReadOnlyList(string filter)
        {
            return DataPortal.Fetch<AlumnoReadOnlyList>(filter);
        }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(string criteria)
        {
            RaiseListChangedEvents = false;
            IsReadOnly = false;
            using (var ctx = DbContextManager<Colegio>.GetManager())
            {
                var lista = ctx.DbContext.Set<Alumno>()
                    .Where(p => p.EstadoRegistro);

                foreach (var alumno in lista)
                {
                    Add(AlumnoReadOnly.GetReadOnlyChild(alumno));
                }
            }
            IsReadOnly = true;
            RaiseListChangedEvents = true;
        }

        #endregion
    }
}