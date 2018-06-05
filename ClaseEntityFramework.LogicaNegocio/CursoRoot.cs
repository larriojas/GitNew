using ClaseEntityFramework.Datos;
using ClaseEntityFramework.Entidades;
using Csla;
using Csla.Data.EF6;
using Csla.Rules.CommonRules;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace ClaseEntityFramework.LogicaNegocio
{
    [Serializable]
    public class CursoRoot : BusinessBase<CursoRoot>
    {
        // FieldManager
        /*
         * Es una coleccion de los valores de las propiedades de CSLA
         * para poder saber, si algo ha cambiado.
         * */

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            set { SetProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<string> NombreProperty = RegisterProperty<string>(c => c.Nombre);
        [Required]
        public string Nombre
        {
            get { return GetProperty(NombreProperty); }
            set { SetProperty(NombreProperty, value); }
        }

        public static readonly PropertyInfo<string> CodigoProperty = RegisterProperty<string>(c => c.Codigo);
        [Required]
        public string Codigo
        {
            get { return GetProperty(CodigoProperty); }
            set { SetProperty(CodigoProperty, value); }
        }

        public static readonly PropertyInfo<int> NroCreditosProperty = RegisterProperty<int>(c => c.NroCreditos);
        public int NroCreditos
        {
            get { return GetProperty(NroCreditosProperty); }
            set { SetProperty(NroCreditosProperty, value); }
        }

        // Reglas de Negocio.
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            BusinessRules.AddRule(new MinValue<int>(NroCreditosProperty, 1));
        }

        // Metodos Fabrica

        public static CursoRoot NewEditableRoot()
        {
            return DataPortal.Create<CursoRoot>();
        }

        public static CursoRoot GetEditableRoot(int id)
        {
            return DataPortal.Fetch<CursoRoot>(id);
        }

        public static void DeleteEditableRoot(int id)
        {
            DataPortal.Delete<CursoRoot>(id);
        }

        protected override void DataPortal_Create()
        {
            base.DataPortal_Create();
            NroCreditos = 1;
            BusinessRules.CheckRules();
        }

        protected void DataPortal_Fetch(int id)
        {
            using (BypassPropertyChecks)
            {
                using (var ctx = DbContextManager<Colegio>.GetManager())
                {
                    var curso = ctx.DbContext.Set<Curso>().Find(id);
                    if (curso == null) throw new InvalidOperationException("No se encuentra el registro");

                    Id = curso.CursoId;
                    Nombre = curso.Nombre;
                    Codigo = curso.Codigo;
                    NroCreditos = curso.NroCreditos;

                }
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (BypassPropertyChecks)
            {
                using (var ctx = DbContextManager<Colegio>.GetManager())
                {
                    var curso = new Curso
                    {
                        Nombre = Nombre,
                        Codigo = Codigo,
                        NroCreditos = NroCreditos,
                        EstadoRegistro = true
                    };

                    ctx.DbContext.Set<Curso>().Add(curso);
                    ctx.DbContext.SaveChanges();
                }
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (BypassPropertyChecks)
            {
                using (var ctx = DbContextManager<Colegio>.GetManager())
                {
                    var curso = ctx.DbContext.Set<Curso>().Find(Id);
                    if (curso == null) throw new InvalidOperationException("No se encuentra el registro");

                    curso.Nombre = Nombre;
                    curso.Codigo = Codigo;
                    curso.NroCreditos = NroCreditos;

                    ctx.DbContext.Set<Curso>().Attach(curso);
                    ctx.DbContext.Entry(curso).State = EntityState.Modified;
                    ctx.DbContext.SaveChanges();
                }
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(Id);
        }

        protected void DataPortal_Delete(int id)
        {
            using (BypassPropertyChecks)
            {
                using (var ctx = DbContextManager<Colegio>.GetManager())
                {
                    var curso = ctx.DbContext.Set<Curso>().Find(id);
                    if (curso == null) throw new InvalidOperationException("No se encuentra el registro");

                    curso.EstadoRegistro = false;

                    ctx.DbContext.Set<Curso>().Attach(curso);
                    ctx.DbContext.Entry(curso).State = EntityState.Modified;
                    ctx.DbContext.SaveChanges();
                }
            }
        }
    }
}
