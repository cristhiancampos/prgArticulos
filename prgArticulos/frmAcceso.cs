using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;// Permite hacer las conexiones con la base de datos
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


// llamado  de las referencias propias del proyecto 
using System.Data.SqlClient;
using Modelo;
using Controlador;

// espacio donde se encuentra la ventana
namespace prgArticulos
{
    public partial class frmAcceso : Form
    {

        #region Atributos
        clsConexionSQL conexion;
        clsEntidadUsuario pEntidadUsuario;
        clsUsuario usuario;
        SqlDataReader dtrUsuario;// para el retorno de las tuplas
        int contador=0;//contador de veces se intenta entrar con un usuario  
        #endregion

        public frmAcceso()
        {
            // aqui se deben ingresar los componentes, objetos para que la ventana funcione
            conexion = new clsConexionSQL();
            pEntidadUsuario = new clsEntidadUsuario();
            usuario = new clsUsuario();

            InitializeComponent();
        }


        private void frmAcceso_Load(object sender, EventArgs e)
        {

        }

       

        private void btnSalir_Click(object sender, EventArgs e)
        {

            
            Application.Exit();
        }

        private void txUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
                //el evento Focus permite trasladar el cursor del mouse al objeto indicado
                this.txClave.Focus();
        }

        private void txClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if(mValidarDatos()==true)
                {
                    this.btnIngresar.Enabled = true;
                }
              
            }
        }//fin del key press de clave

        #region Metodos

        //este meodo permite verificar la existencia  del usuario segun el codigo y la clave  digitada
            private Boolean mValidarDatos()
        {
            if (contador <= 2)
            {
                //llenado de los atributos de la BD

                //este es para conectarme con el servidor
                conexion.setCodigo("admEstudiante");
                conexion.setClave("123");


                //llenado de los atributos de la clase EntidadUsuario, para coonectarme con la aplicacon
                pEntidadUsuario.setCodigo(this.txUsuario.Text.Trim());
                pEntidadUsuario.setClave(this.txClave.Text.Trim());

                //consultamos si el usuario existe

                dtrUsuario = usuario.mConsultarUsu(conexion, pEntidadUsuario);


                //evalua si retorna tuplas o  datos

                if (dtrUsuario != null)
                {
                    if (dtrUsuario.Read())
                    {
                        pEntidadUsuario.setPerfil(dtrUsuario.GetString(2));
                        pEntidadUsuario.setEstado(dtrUsuario.GetInt32(3));

                        if (pEntidadUsuario.getEstado()==1)
                        {
                            this.btnIngresar.Enabled = true;
                            MessageBox.Show("entro");
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("El usuario esta bloqueado", "ERROR", MessageBoxButtons.OK);
                            return false;
                        }//fin del pEntidadUsuario
                    }
                    else
                    {
                        MessageBox.Show("El usuario no existe ", "ERROR", MessageBoxButtons.OK);

                        return false;
                    }//fin del if del Read

                }
                else
                {
                    MessageBox.Show("El usuario no existe", "ERROR", MessageBoxButtons.OK);
                    return false;

                }//fin de if del null

            }
            else {

                MessageBox.Show("Usted digito 3 veces su usuario de forma erronea", "USUARIO BLOQUEADO", MessageBoxButtons.OK,MessageBoxIcon.Information);
                return false;
            }//fin del if del contador

        }
        #endregion

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            this.SetVisibleCore(false);
            mdiMenu menu = new mdiMenu(conexion);
            menu.Show();
        }
    }
}
