using System;
using System.Data.Entity;
using ClaseEntityFramework.Datos;
using ClaseEntityFramework.Entidades;
using Csla;
using Csla.Data.EF6;

namespace ClaseEntityFramework.LogicaNegocio
{
    [Serializable]
    public class AlumnoCursoChild : BusinessBase<AlumnoCursoChild>, IEquatable<AlumnoCursoChild>
    {

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);

        public int Id
        {
            get { return GetProperty(IdProperty); }
            set { SetProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<int> IdAlumnoProperty = RegisterProperty<int>(c => c.IdAlumno);

        public int IdAlumno
        {
            get { return GetProperty(IdAlumnoProperty); }
            set { SetProperty(IdAlumnoProperty, value); }
        }

        public static readonly PropertyInfo<int> IdCursoProperty = RegisterProperty<int>(c => c.IdCurso);

        public int IdCurso
        {
            get { return GetProperty(IdCursoProperty); }
            set { SetProperty(IdCursoProperty, value); }
        }

        public static readonly PropertyInfo<string> NombreCursoProperty = RegisterProperty<string>(c => c.NombreCurso);

        public string NombreCurso
        {
            get { return GetProperty(NombreCursoProperty); }
            set { SetProperty(NombreCursoProperty, value); }
        }


        public static AlumnoCursoChild GetEditableChild(AlumnoCurso child)
        {
            return DataPortal.FetchChild<AlumnoCursoChild>(child);
        }

        public static AlumnoCursoChild NewEditableChild()
        {
            return DataPortal.CreateChild<AlumnoCursoChild>();
        }

        protected override void Child_Create()
        {
            base.Child_Create();
        }

        protected void Child_Fetch(AlumnoCurso child)
        {
            Id = child.AlumnoCursoId;
            IdAlumno = child.AlumnoId;
            IdCurso = child.CursoId;
            NombreCurso = child.Curso.Nombre;
        }

        protected void Child_Insert()
        {
            using (BypassPropertyChecks)
            {
                var padre = ApplicationContext.LocalContext["padre"] as Alumno;

                using (var ctx = DbContextManager<Colegio>.GetManager())
                {
                    var alumnoCurso = new AlumnoCurso
                    {
                        Alumno = padre,
                        CursoId = IdCurso
                    };
                    ctx.DbContext.Set<AlumnoCurso>().Add(alumnoCurso);
                    ctx.DbContext.SaveChanges();
                }
            }
        }

        protected void Child_Update()
        {
            using (BypassPropertyChecks)
            {
                var padre = ApplicationContext.LocalContext["padre"] as Alumno;
                using (var ctx = DbContextManager<Colegio>.GetManager())
                {
                    var alumnoCurso = ctx.DbContext.Set<AlumnoCurso>().Find(Id);
                    if (alumnoCurso == null) return;

                    alumnoCurso.Alumno = padre;
                    alumnoCurso.CursoId = IdCurso;
                    ctx.DbContext.Set<AlumnoCurso>().Attach(alumnoCurso);
                    ctx.DbContext.Entry(alumnoCurso).State = EntityState.Modified;
                    ctx.DbContext.SaveChanges();
                }
            }
        }

        protected void Child_DeleteSelf()
        {
            using (var ctx = DbContextManager<Colegio>.GetManager())
            {
                var alumnoCurso = ctx.DbContext.Set<AlumnoCurso>().Find(Id);
                if (alumnoCurso == null) return;

                alumnoCurso.EstadoRegistro = false;

                ctx.DbContext.Set<AlumnoCurso>().Attach(alumnoCurso);
                ctx.DbContext.Entry(alumnoCurso).State = EntityState.Modified;
                ctx.DbContext.SaveChanges();
            }
        }

        public bool Equals(AlumnoCursoChild other)
        {
            return other != null && other.IdCurso.Equals(IdCurso);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AlumnoCursoChild) obj);
        }

        public override int GetHashCode()
        {
            return IdCurso.GetHashCode();
        }

        public static bool operator ==(AlumnoCursoChild left, AlumnoCursoChild right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AlumnoCursoChild left, AlumnoCursoChild right)
        {
            return !Equals(left, right);
        }
    }
}