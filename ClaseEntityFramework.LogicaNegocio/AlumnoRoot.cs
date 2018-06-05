using System;
using Csla;
using Csla.Rules.CommonRules;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using ClaseEntityFramework.Datos;
using ClaseEntityFramework.Entidades;
using Csla.Data.EF6;

namespace ClaseEntityFramework.LogicaNegocio
{
    [Serializable]
    public class AlumnoRoot : BusinessBase<AlumnoRoot>
    {
        #region Propiedades de Negocio

        public static readonly PropertyInfo<int> IdAlumnoProperty = RegisterProperty<int>(c => c.IdAlumno);

        public int IdAlumno
        {
            get => GetProperty(IdAlumnoProperty);
            set => SetProperty(IdAlumnoProperty, value);
        }

        public static readonly PropertyInfo<string> NombresProperty = RegisterProperty<string>(c => c.Nombres);

        [Required]
        [MaxLength(50)]
        public string Nombres
        {
            get => GetProperty(NombresProperty);
            set => SetProperty(NombresProperty, value);
        }

        public static readonly PropertyInfo<string> ApellidosProperty = RegisterProperty<string>(c => c.Apellidos);

        [Required]
        [MaxLength(50)]
        public string Apellidos
        {
            get => GetProperty(ApellidosProperty);
            set => SetProperty(ApellidosProperty, value);
        }

        public static readonly PropertyInfo<string> CorreoProperty = RegisterProperty<string>(c => c.Correo, "Correo Electrónico");

        [MaxLength(100)]
        public string Correo
        {
            get => GetProperty(CorreoProperty);
            set => SetProperty(CorreoProperty, value);
        }

        public static readonly PropertyInfo<DateTime> FechaInscripcionProperty =
            RegisterProperty<DateTime>(c => c.FechaInscripcion);

        public DateTime FechaInscripcion
        {
            get => GetProperty(FechaInscripcionProperty);
            set => SetProperty(FechaInscripcionProperty, value);
        }

        public static readonly PropertyInfo<string> TelefonoProperty = RegisterProperty<string>(c => c.Telefono);

        public string Telefono
        {
            get => GetProperty(TelefonoProperty);
            set => SetProperty(TelefonoProperty, value);
        }

        public static readonly PropertyInfo<AlumnoCursoChildList> CursosProperty = 
            RegisterProperty<AlumnoCursoChildList>(c => c.Cursos, RelationshipTypes.Child);
        public AlumnoCursoChildList Cursos
        {
            get { return GetProperty(CursosProperty); }
            private set { SetProperty(CursosProperty, value); }
        }

        #endregion

        #region Reglas de Negocio y de Validacion

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            BusinessRules.AddRule(new RegExMatch(CorreoProperty,
                Properties.Resources.mascaraEmail, Properties.Resources.MensajeErrorEmail));

            BusinessRules.AddRule(new RegExMatch(TelefonoProperty,
                Properties.Resources.mascaraTelefono, Properties.Resources.MensajeErrorTelefono));
        }

        #endregion

        #region Metodos Factory

        public static AlumnoRoot NewEditableRoot()
        {
            return DataPortal.Create<AlumnoRoot>();
        }

        public static AlumnoRoot GetEditableRoot(int id)
        {
            return DataPortal.Fetch<AlumnoRoot>(id);
        }

        public static void DeleteEditableRoot(int id)
        {
            DataPortal.Delete<AlumnoRoot>(id);
        }

        #endregion

        #region Data Portal

        // CREATE
        //[RunLocal] // Este metodo DataPortal debe correr en el lado del cliente.
        protected override void DataPortal_Create()
        {
            // Inicializamos los valores por Default.
            FechaInscripcion = DateTime.Today;
            Cursos = AlumnoCursoChildList.NewEditableChildList();
            BusinessRules.CheckRules();
        }

        // FETCH (OBTENER)
        protected void DataPortal_Fetch(int criteria)
        {
            using (BypassPropertyChecks)
            {
                using (var ctx = DbContextManager<Colegio>.GetManager())
                {
                    var alumno = ctx.DbContext.Alumno.Find(criteria);

                    IdAlumno = alumno?.AlumnoId ?? throw new InvalidOperationException("No se encuentra el registro");
                    Nombres = alumno.Nombres;
                    Apellidos = alumno.Apellidos;
                    Correo = alumno.Correo;
                    Telefono = alumno.Telefono;
                    FechaInscripcion = alumno.FechaInscripcion;

                    Cursos = AlumnoCursoChildList.GetEditableChildList(IdAlumno);
                }
            }
        }

        // INSERT
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (BypassPropertyChecks)
            {
                using (var ctx = DbContextManager<Colegio>.GetManager())
                {
                    var alumno = new Alumno
                    {
                        Nombres =  Nombres,
                        Apellidos = Apellidos,
                        Correo = Correo,
                        Telefono = Telefono,
                        FechaInscripcion = FechaInscripcion,
                        EstadoRegistro = true
                    };

                    ctx.DbContext.Set<Alumno>().Add(alumno);
                    ApplicationContext.LocalContext["padre"] = alumno;
                    FieldManager.UpdateChildren(this); // Inserta, Actualiza o Elimina todos los Child.
                    ctx.DbContext.SaveChanges();
                }
            }
        }

        // UPDATE
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (BypassPropertyChecks)
            {
                using (var ctx = DbContextManager<Colegio>.GetManager())
                {
                    var alumno = ctx.DbContext.Alumno.Find(IdAlumno);
                    if (alumno == null) throw new InvalidOperationException("El registro no existe");

                    alumno.Nombres = Nombres;
                    alumno.Apellidos = Apellidos;
                    alumno.Correo = Correo;
                    alumno.Telefono = Telefono;
                    alumno.FechaInscripcion = FechaInscripcion;

                    ctx.DbContext.Set<Alumno>().Attach(alumno);
                    ctx.DbContext.Entry(alumno).State = EntityState.Modified;
                    ApplicationContext.LocalContext["padre"] = alumno;
                    FieldManager.UpdateChildren(this); // Inserta, Actualiza o Elimina todos los Child.
                    ctx.DbContext.SaveChanges();
                }
            }
        }


        // DELETE
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(IdAlumno);
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(int criteria)
        {
            using (var ctx = DbContextManager<Colegio>.GetManager())
            {
                var entidad = ctx.DbContext.Set<Alumno>().Find(criteria);
                if (entidad == null)
                    throw new InvalidOperationException("No se encuentra el registro");

                entidad.EstadoRegistro = false;

                ctx.DbContext.Set<Alumno>().Attach(entidad);
                ctx.DbContext.Entry(entidad).State = EntityState.Modified;

                ctx.DbContext.SaveChanges();
            }
        }

        #endregion
    }
}
