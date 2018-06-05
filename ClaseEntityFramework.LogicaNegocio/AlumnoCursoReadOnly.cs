using System;
using ClaseEntityFramework.Entidades;
using Csla;

namespace ClaseEntityFramework.LogicaNegocio
{
    [Serializable]
    public class AlumnoCursoReadOnly : ReadOnlyBase<AlumnoCursoReadOnly>
    {
        #region Business Methods

        public int AlumnoCursoId { get; set; }
        public int AlumnoId { get; set; }
        public string Alumno { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public string Curso { get; set; }
        public int NroCreditos { get; set; }

        #endregion

        #region Factory Methods

        internal static AlumnoCursoReadOnly GetReadOnlyChild(AlumnosPorCurso childData)
        {
            return DataPortal.FetchChild<AlumnoCursoReadOnly>(childData);
        }

        #endregion

        #region Data Access

        private void Child_Fetch(AlumnosPorCurso childData)
        {
            AlumnoCursoId = childData.AlumnoCursoId;
            AlumnoId = childData.AlumnoId;
            Alumno = childData.Alumno;
            FechaInscripcion = childData.FechaInscripcion;
            Curso = childData.Curso;
            NroCreditos = childData.NroCreditos;
        }

        #endregion
    }
}