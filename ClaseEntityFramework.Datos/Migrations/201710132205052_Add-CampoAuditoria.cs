namespace ClaseEntityFramework.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCampoAuditoria : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alumno",
                c => new
                    {
                        AlumnoId = c.Int(nullable: false, identity: true),
                        Nombres = c.String(nullable: false, maxLength: 50),
                        Apellidos = c.String(nullable: false, maxLength: 50),
                        Correo = c.String(nullable: false, maxLength: 100),
                        FechaInscripcion = c.DateTime(nullable: false),
                        Telefono = c.String(),
                        EstadoRegistro = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AlumnoId);
            
            CreateTable(
                "dbo.AlumnoCurso",
                c => new
                    {
                        AlumnoCursoId = c.Int(nullable: false, identity: true),
                        AlumnoId = c.Int(nullable: false),
                        CursoId = c.Int(nullable: false),
                        EstadoRegistro = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AlumnoCursoId)
                .ForeignKey("dbo.Curso", t => t.CursoId)
                .ForeignKey("dbo.Alumno", t => t.AlumnoId)
                .Index(t => t.AlumnoId)
                .Index(t => t.CursoId);
            
            CreateTable(
                "dbo.Curso",
                c => new
                    {
                        CursoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Codigo = c.String(nullable: false, maxLength: 10),
                        NroCreditos = c.Int(nullable: false),
                        EstadoRegistro = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CursoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AlumnoCurso", "AlumnoId", "dbo.Alumno");
            DropForeignKey("dbo.AlumnoCurso", "CursoId", "dbo.Curso");
            DropIndex("dbo.AlumnoCurso", new[] { "CursoId" });
            DropIndex("dbo.AlumnoCurso", new[] { "AlumnoId" });
            DropTable("dbo.Curso");
            DropTable("dbo.AlumnoCurso");
            DropTable("dbo.Alumno");
        }
    }
}
