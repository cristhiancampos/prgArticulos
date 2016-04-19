using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo;
using System.Data.SqlClient;
namespace Controlador
{
    public class clsUsuario
    {
        #region Atributos
        //perimite las sentencia del sql transact
        private string strSentencia;

        //permite enviar  la ejecucion de la sentencia al modelo  en la clase conexion
        clsConexionSQL conexion = new clsConexionSQL();
        #endregion

        #region Metodos
            //metodos para accesar al sistema
            public SqlDataReader mConsultarUsu(clsConexionSQL cone,Modelo.clsEntidadUsuario pEntidadUsuario )
             {
                strSentencia = "select * from tbUsuarios where  codigo = '"+ pEntidadUsuario.getCodigo()+"' and clave='"+ pEntidadUsuario.getClave()+"'  ";

            return conexion.mSeleccionar(strSentencia,cone);
             }
        #endregion

    }
}
