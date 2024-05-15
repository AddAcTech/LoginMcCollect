using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Login_InfoToolsSV
{
    public partial class Login_InfoToolsSV : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        string patron = "Hash";
        protected void BtnIngresar_Click(object sender, EventArgs e)
        {
            var emailValidator = new EmailAddressAttribute();
            if (!emailValidator.IsValid(tbUsuario.Text))
            {
                lblError.Text = "Formato de correo inválido";
                return;
            }

            if (tbPassword.Text.Length < 8 || tbPassword.Text.Length > 12)
            {
                lblError.Text = "La contraseña debe tener entre 8 y 12 caracteres";
                return;
            }

            string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            SqlConnection sqlConectar = new SqlConnection(conectar);

            try
            {
                sqlConectar.Open();

                // Ejecutar el procedimiento almacenado UserLogin
                using (SqlCommand cmd = new SqlCommand("UserLogin", sqlConectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 50).Value = tbUsuario.Text;
                    cmd.Parameters.Add("@Contrasenia", SqlDbType.VarChar, 50).Value = tbPassword.Text;
                    cmd.Parameters.Add("@Patron", SqlDbType.VarChar, 50).Value = patron;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            // Guardar datos del usuario en la sesión
                            Session["usuariologueado"] = new { Usuario = tbUsuario.Text, TipoUsuario = dr.GetBoolean(dr.GetOrdinal("TipoUsuario")) };
                            bool isAdmin = dr.GetBoolean(dr.GetOrdinal("TipoUsuario"));
                            if (isAdmin)
                            {
                                Response.Redirect("Register.aspx");
                            }

                        }
                        else
                        {
                            lblError.Text = "Usuario o Contraseña invalidos";
                        }
                    }
                }

                // Ejecutar el procedimiento almacenado MostrarDatos
                using (SqlCommand data = new SqlCommand("MostrarDatos", sqlConectar))
                {
                    data.CommandType = CommandType.StoredProcedure;
                    data.Parameters.Add("@Usuario", SqlDbType.VarChar, 50).Value = tbUsuario.Text;

                    using (SqlDataReader mostrar = data.ExecuteReader())
                    {
                        if (mostrar.Read())
                        {
                            // Guardar datos de la nueva tabla en la sesión
                            Session["datosUsuario"] = new
                            {
                                UserId = mostrar.GetInt32(mostrar.GetOrdinal("user_id")),
                                Nombre = mostrar.GetString(mostrar.GetOrdinal("Nombre")),
                                Edad = mostrar.GetInt32(mostrar.GetOrdinal("Edad")),
                                Direccion = mostrar.GetString(mostrar.GetOrdinal("Direccion")),
                                Cp = mostrar.GetString(mostrar.GetOrdinal("Cp")),
                                Promedio = mostrar.GetDouble(mostrar.GetOrdinal("Promedio")),
                                Nacimiento = mostrar.GetDateTime(mostrar.GetOrdinal("Nacimiento")).ToString("yyyy-MM-dd"),
                                Cita = mostrar.GetDateTime(mostrar.GetOrdinal("FechaCita")).ToString("yyyy-MM-dd"),
                                Ruta = mostrar.GetString(mostrar.GetOrdinal("RutaCita")),
                            };
                            Response.Redirect("Index.aspx");
                        }
                    }
                }
            }
            catch (Exception ex)    
            {
                lblError.Text = ex.ToString();
            }
            finally
            {
                // Asegúrate de cerrar la conexión
                sqlConectar.Close();
            }
        }

        protected void BtnRegistrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
    }
}
