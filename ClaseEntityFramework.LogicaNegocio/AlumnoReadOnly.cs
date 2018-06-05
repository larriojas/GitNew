using Csla;
using System;
using ClaseEntityFramework.Entidades;

namespace ClaseEntityFramework.LogicaNegocio
{
    [Serializable]
    public class AlumnoReadOnly : ReadOnlyBase<AlumnoReadOnly>
    {
        #region Business Methods

        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaInscripcion { get; set; }


        #endregion

        #region Factory Methods

        internal static AlumnoReadOnly GetReadOnlyChild(Alumno childData)
        {
            return DataPortal.FetchChild<AlumnoReadOnly>(childData);
        }

        #endregion

        #region Data Access

        private void Child_Fetch(Alumno childData)
        {
            Id = childData.AlumnoId;
            Nombres = childData.Nombres;
            Apellidos = childData.Apellidos;
            Correo = childData.Correo;
            Telefono = childData.Telefono;
            FechaInscripcion = childData.FechaInscripcion;
        }

        #endregion
    }
}