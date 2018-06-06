using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ConexionCSharpconMySQL
{
    class ClietesDAL
    {
        public static int Agregar(Cliente pCliente)
        {

            int retorno = 0;

            MySqlCommand comando = new MySqlCommand(string.Format("Insert into clientes (Nombre, Apellido, Fecha_Nacimiento, Direccion) values ('{0}','{1}','{2}', '{3}')",
                pCliente.Nombre, pCliente.Apellido, pCliente.Fecha_Nac, pCliente.Direccion), BdComun.ObtenerConexion());

            retorno = comando.ExecuteNonQuery();

            return retorno;
        }


    }
}
