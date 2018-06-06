using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionCSharpconMySQL
{
    class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Fecha_Nac { get; set; }
        public string Direccion { get; set; }

        public Cliente() { }

        public Cliente(int pId, string pNombre, string pApellido, string pFecha_Nac, string pDireccion)
        {
            this.Id = pId;
            this.Nombre = pNombre;
            this.Apellido = pApellido;
            this.Fecha_Nac = pFecha_Nac;
            this.Direccion = pDireccion;
        }

    }
}
