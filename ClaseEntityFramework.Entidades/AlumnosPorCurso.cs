using System;

namespace ClaseEntityFramework.Entidades
{
    public class AlumnosPorCurso
    {
        public int AlumnoCursoId { get; set; }
        public int AlumnoId { get; set; }
        public string Alumno { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public string Curso { get; set; }
        public int NroCreditos { get; set; }
    }
}