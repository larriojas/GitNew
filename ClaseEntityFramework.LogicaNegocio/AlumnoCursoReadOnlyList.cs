using System;
using ClaseEntityFramework.Datos;
using Csla;
using Csla.Data.EF6;

namespace ClaseEntityFramework.LogicaNegocio
{
    [Serializable]
    public class AlumnoCursoReadOnlyList : ReadOnlyListBase<AlumnoCursoReadOnlyList, AlumnoCursoReadOnly>
    {
        #region Factory Methods

        public static AlumnoCursoReadOnlyList GetReadOnlyList()
        {
            return DataPortal.Fetch<AlumnoCursoReadOnlyList>();
        }

        #endregion

        #region Data Access

        private void DataPortal_Fetch()
        {
            RaiseListChangedEvents = false;
            IsReadOnly = false;
            using (var ctx = DbContextManager<Colegio>.GetManager())
            {
                var lista = ctx.DbContext.ListarCursosPorAlumno();

                foreach (var entidad in lista)
                {
                    Add(AlumnoCursoReadOnly.GetReadOnlyChild(entidad));
                }
            }
            IsReadOnly = true;
            RaiseListChangedEvents = true;
        }

        #endregion
    }
}