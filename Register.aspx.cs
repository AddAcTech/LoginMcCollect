using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.CodeDom;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;
using System.Web.SessionState;

namespace Login_InfoToolsSV
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NoAdmin noAdmin = new NoAdmin();
            dynamic usuario = Session["usuarioLogueado"];
            if (usuario == null || !usuario.TipoUsuario)
            {
                noAdmin.RedirectToLoginPage();
            }
        }

        string patron = "Hash";


        protected void BtnRegistrar_Click(object sender, EventArgs e)
        {
            void LimpiarErrores(){
            lblErrorUsuario.Text = "";
            lblErrorPassword.Text = "";
            lblErrorPasswordConf.Text = "";
            lblErrorNombre.Text = "";
            lblErrorDireccion.Text = "";
            lblErrorNacimiento.Text = "";
            lblErrorCp.Text = "";
            lblErrorPromedio.Text = "";
            }
            LimpiarErrores();

            void LimpiarCampos()
            {
                tbUsuario.Text = "";
                tbPassword.Text = "";
                tbConfirmarPassword.Text = "";
                tbNombre.Text = "";
                tbDireccion.Text = "";
                tbCp.Text = "";
                tbPromedio.Text = "";
                tbNacimiento.Text = "";
            }

            bool hayErrores = false;

            // Validación del correo electrónico
            var emailValidator = new EmailAddressAttribute();
            if (!emailValidator.IsValid(tbUsuario.Text))
            {
                lblErrorUsuario.Text = "Formato de correo inválido";
                hayErrores = true;
            }

            // Validación de la contraseña
            if (tbPassword.Text.Length < 8 || tbPassword.Text.Length > 12 || !tbPassword.Text.Any(char.IsUpper) || !tbPassword.Text.Any(char.IsDigit))
            {
                lblErrorPassword.Text = "La contraseña debe tener entre 8 y 12 caracteres, al menos una mayúscula y un número";
                hayErrores = true;
            }

            // Validación de confirmar contraseña
            if (tbPassword.Text != tbConfirmarPassword.Text)
            {
                lblErrorPasswordConf.Text = "Las contraseñas no coinciden";
                hayErrores = true;
            }

            // Validación del nombre
            if (!Regex.IsMatch(tbNombre.Text, @"^[a-zA-Z\s]{1,70}$"))
            {
                lblErrorNombre.Text = "El nombre debe contener solo letras y no exceder los 70 caracteres";
                hayErrores = true;
            }

            //Validar direccion
            if (tbDireccion.Text.Length == 0)
            {
                lblErrorDireccion.Text = "Formato de direccion incorrecto";
                hayErrores = true;
            }

            // Validación de la fecha de nacimiento
            DateTime nacimiento;
            if (!DateTime.TryParse(tbNacimiento.Text, out nacimiento) || nacimiento > DateTime.Now)
            {
                lblErrorNacimiento.Text = "La fecha de nacimiento no puede ser mayor que la fecha actual";
                hayErrores = true;
            }

            // Validación del código postal
            int cp;
            if (!int.TryParse(tbCp.Text, out cp) || tbCp.Text.Length > 5)
            {
                lblErrorCp.Text = "El código postal debe contener solo números enteros y tener una longitud de 5 caracteres";
                hayErrores = true;
            }

            // Validación del promedio
            float promedio;
            if (!float.TryParse(tbPromedio.Text, out promedio) || promedio < 0 || promedio > 10)
            {
                lblErrorPromedio.Text = "El promedio debe ser un número decimal entre 0 y 10";
                hayErrores = true;
            }

            if (!hayErrores)
            {
                // Hay errores, no registrar al usuario
                // Realizar el registro
                if (RegistrarUsuario(tbUsuario.Text, tbPassword.Text, patron, tbNombre.Text, tbDireccion.Text, tbCp.Text, tbPromedio.Text, tbNacimiento.Text))
                {
                    //Response.Redirect("Login_InfoToolsSV.aspx");
                    LimpiarCampos();
                    lblAlta.Text = "Usuario registrado correctamente";
                }
                else
                {
                    lblError.Text = "Correo electrónico existente";
                }
            }
            
            return;

        }


        // Método para conectar y registrar usuario
        private bool RegistrarUsuario(string usuario, string contrasenia, string patron, string nombre, string direccion, string cp, string promedio, string nacimiento)
        {
            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                SqlCommand cmd = new SqlCommand("UserRegister", sqlConectar)
                {
                    CommandType = CommandType.StoredProcedure
                };

                try
                {
                    cmd.Connection.Open();
                    cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 50).Value = usuario;
                    cmd.Parameters.Add("@Contrasenia", SqlDbType.VarChar, 50).Value = contrasenia;
                    cmd.Parameters.Add("@Patron", SqlDbType.VarChar, 50).Value = patron;
                    cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 50).Value = nombre;
                    cmd.Parameters.Add("@Edad", SqlDbType.Int).Value = CalcularEdad(nacimiento); ;
                    cmd.Parameters.Add("@Direccion", SqlDbType.VarChar, 50).Value = direccion;
                    cmd.Parameters.Add("@Cp", SqlDbType.VarChar, 50).Value = cp;
                    cmd.Parameters.Add("@Promedio", SqlDbType.Float).Value = float.Parse(promedio);
                    cmd.Parameters.Add("@Nacimiento", SqlDbType.VarChar, 50).Value = nacimiento;
                    SqlDataReader dr = cmd.ExecuteReader();
                    return !dr.Read();
                }
                catch
                {
                    lblError.Text = "Hubo un error al conectar a la base de datos";
                    return false;
                }
            }
        }

        // Método para calcular la edad
        private int CalcularEdad(string fechaNacimiento)
        {
            DateTime fechaNac = DateTime.Parse(fechaNacimiento);
            int edad = DateTime.Today.Year - fechaNac.Year;
            if (DateTime.Today < fechaNac.AddYears(edad))
            {
                edad--;
            }
            return edad;
        }

        protected void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            // Eliminar la sesión del usuario
            Session.Remove("usuarioLogueado");

            // Redirigir a la página de inicio de sesión
            Response.Redirect("Login_InfoToolsSV.aspx");
        }

        protected void tbUsuario_TextChanged(object sender, EventArgs e)
        {
            var emailValidator = new EmailAddressAttribute();
            if (!emailValidator.IsValid(tbUsuario.Text))
            {
                lblErrorUsuario.Text = "Formato de correo inválido";
            }
        }

        protected void tbNombre_TextChanged(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(tbNombre.Text, @"^[a-zA-Z\s]{1,70}$"))
            {
                lblErrorNombre.Text = "El nombre debe contener solo letras y no exceder los 70 caracteres";
            }
        }

        protected void tbPassword_TextChanged(object sender, EventArgs e)
        {
            if (tbPassword.Text.Length < 8 || tbPassword.Text.Length > 12 || !tbPassword.Text.Any(char.IsUpper) || !tbPassword.Text.Any(char.IsDigit))
            {
                lblErrorPassword.Text = "La contraseña debe tener entre 8 y 12 caracteres, al menos una mayúscula y un número";
            }
        }

        protected void tbConfirmarPassword_TextChanged(object sender, EventArgs e)
        {
            if (tbPassword.Text != tbConfirmarPassword.Text)
            {
                lblErrorPasswordConf.Text = "Las contraseñas no coinciden";
            }
        }

        protected void tbDireccion_TextChanged(object sender, EventArgs e)
        {
            lblErrorDireccion.Text = "";
            if (tbDireccion.Text.Length == 0)
            {
                lblErrorDireccion.Text = "Formato de direccion incorrecto";
            }
        }

        protected void tbCp_TextChanged(object sender, EventArgs e)
        {
            lblErrorCp.Text = "";
            int cp;
            if (!int.TryParse(tbCp.Text, out cp) || tbCp.Text.Length > 5)
            {
                lblErrorCp.Text = "El código postal debe contener solo números enteros y tener una longitud de 5 caracteres";
            }
        }

        protected void tbPromedio_TextChanged(object sender, EventArgs e)
        {
            lblErrorPromedio.Text = "";
            float promedio;
            if (!float.TryParse(tbPromedio.Text, out promedio) || promedio < 0 || promedio > 10)
            {
                lblErrorPromedio.Text = "El promedio debe ser un número decimal entre 0 y 10";
            }

        }

        protected void tbNacimiento_TextChanged(object sender, EventArgs e)
        {
            lblErrorNacimiento.Text = "";
            DateTime nacimiento;
            if (!DateTime.TryParse(tbNacimiento.Text, out nacimiento) || nacimiento > DateTime.Now)
            {
                lblErrorNacimiento.Text = "La fecha de nacimiento no puede ser mayor que la fecha actual";
            }
        }
    }
    public class NoAdmin
    {
        public bool IsUserLoggedIn(System.Web.SessionState.HttpSessionState session)
        {
            return session["usuariologueado"] != null;
        }
        public void RedirectToRegisterPage()
        {
            System.Web.HttpContext.Current.Response.Redirect("Register.aspx");
        }
        public void RedirectToLoginPage()
        {
            System.Web.HttpContext.Current.Response.Redirect("Login_InfoToolsSV.aspx");
        }
    }
}
