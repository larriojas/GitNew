CREATE PROCEDURE [dbo].uspListarCursosPorAlumno
AS
BEGIN
	
	SELECT 
		AC.AlumnoCursoId,
		A.Nombres + ' ' + A.Apellidos Alumno,
		A.FechaInscripcion,
		C.Nombre Curso,
		C.NroCreditos
	FROM Alumno A
	INNER JOIN AlumnoCurso AC ON A.AlumnoId = AC.AlumnoId
	INNER JOIN Curso C ON C.CursoId = AC.CursoId

END