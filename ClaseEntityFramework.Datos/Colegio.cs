using System.Collections.Generic;
using ClaseEntityFramework.Entidades;

namespace ClaseEntityFramework.Datos
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Colegio : DbContext
    {
        public Colegio()
            : base("name=Colegio")
        {
        }

        public virtual DbSet<Alumno> Alumno { get; set; }
        public virtual DbSet<AlumnoCurso> AlumnoCurso { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alumno>()
                .HasMany(e => e.AlumnoCurso)
                .WithRequired(e => e.Alumno)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Curso>()
                .HasMany(e => e.AlumnoCurso)
                .WithRequired(e => e.Curso)
                .WillCascadeOnDelete(false);
        }

        public ICollection<AlumnosPorCurso> ListarCursosPorAlumno()
        {
            return Database.SqlQuery<AlumnosPorCurso>("uspListarCursosPorAlumno").ToList();
        }
    }
}
