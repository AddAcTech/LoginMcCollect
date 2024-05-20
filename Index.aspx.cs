using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.ComponentModel.DataAnnotations;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Web.SessionState;


namespace Login_InfoToolsSV
{
    public partial class Index : System.Web.UI.Page
    {
        private UserInformationManager userManager;
        private SessionManager sessionManager;
        private AppointmentManager appointmentManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            userManager = new UserInformationManager();
            sessionManager = new SessionManager();
            appointmentManager = new AppointmentManager();

            if (!sessionManager.IsUserLoggedIn(Session))
            {
                sessionManager.RedirectToLoginPage();
            }
            else
            {
                dynamic userInfo = sessionManager.GetLoggedInUserInfo(Session);

                userManager.DisplayUserInfo(userInfo, lblBienvenida, lblCp, lblEdad, lblDireccion, lblPromedio, lblNacimiento, lblCita, GenerarCita, GenerarPdf);

                if (lblCita.Text != string.Empty)
                {
                    GenerarPdf.Visible = true;
                }
                else
                {
                    GenerarPdf.Visible = false;
                }
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e)
        {
            sessionManager.ClearSession(Session);
            sessionManager.RedirectToLoginPage();
        }

        protected void GenerarCita_Click(object sender, EventArgs e)
        {
            dynamic userInfo = sessionManager.GetLoggedInUserInfo(Session);
            dynamic userLog = sessionManager.GetLoggedInUser(Session);
            appointmentManager.GenerateAppointment(userInfo, lblCita, GenerarCita, GenerarPdf);
            ActualizarSesionConRutaComprobante(userLog.Usuario);
        }

        private void ActualizarSesionConRutaComprobante(string usuario)
        {
            string conectar = DB.Conectando();
            //string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                sqlConectar.Open();

                SqlCommand cmd = new SqlCommand("MostrarDatos", sqlConectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 50).Value = usuario;

                using (SqlDataReader mostrar = cmd.ExecuteReader())
                {
                    if (mostrar.Read())
                    {
                        // Guardar datos de la nueva tabla en la sesión, incluyendo la ruta del comprobante
                        Session["datosUsuario"] = new
                        {
                            UserId = mostrar.GetInt32(mostrar.GetOrdinal("user_id")),
                            Nombre = mostrar.GetString(mostrar.GetOrdinal("Nombre")),
                            Edad = mostrar.GetInt32(mostrar.GetOrdinal("Edad")),
                            Direccion = mostrar.GetString(mostrar.GetOrdinal("Direccion")),
                            Cp = mostrar.GetString(mostrar.GetOrdinal("Cp")),
                            Promedio = mostrar.GetDouble(mostrar.GetOrdinal("Promedio")),
                            Nacimiento = mostrar.GetDateTime(mostrar.GetOrdinal("Nacimiento")).ToShortDateString(),
                            Cita = mostrar.GetDateTime(mostrar.GetOrdinal("FechaCita")).ToShortDateString(),
                            Ruta = mostrar.GetString(mostrar.GetOrdinal("RutaCita")),
                        };
                    }
                }
            }
        }


        protected void GenerarPdf_Click(object sender, EventArgs e)
{
    dynamic userInfo = sessionManager.GetLoggedInUserInfo(Session);

    if (string.IsNullOrEmpty(userInfo.Ruta) || !File.Exists(userInfo.Ruta))
    {
        string nombreUsuario = userInfo.Nombre;
        int userId = userInfo.UserId;

        // Ruta base para la carpeta Pdfs
        string rutaBase = @"C:\Alexis_Login\Pdfs\";

        try
        {
            // Comprueba si la carpeta existe, si no, la crea
            if (!Directory.Exists(rutaBase))
            {
                Directory.CreateDirectory(rutaBase);
            }

            string nombreArchivoPDF = nombreUsuario + "_ComprobanteCita.pdf";
            string ruta = Path.Combine(rutaBase, nombreArchivoPDF);

            Document comprobante = new Document();
            PdfWriter.GetInstance(comprobante, new FileStream(ruta, FileMode.Create));
            comprobante.Open();

                // Crear una tabla con 2 columnas
                PdfPTable table = new PdfPTable(2);
                table.WidthPercentage = 100; // Ancho de la tabla en porcentaje

                foreach (var property in userInfo.GetType().GetProperties())
                {
                    string propertyName = property.Name;
                    object propertyValue = property.GetValue(userInfo, null);

                    if (propertyName != "Ruta" && propertyName != "UserId")
                    {

                        if (propertyName == "Direccion")
                            {
                                PdfPCell cellName = new PdfPCell(new Phrase("Dirección"));
                                PdfPCell cellValue = new PdfPCell(new Phrase(propertyValue.ToString()));
                                table.AddCell(cellName);
                                table.AddCell(cellValue);
                            }
                        else if (propertyName == "Cp")
                            {
                                PdfPCell cellName = new PdfPCell(new Phrase("Código postal"));
                                PdfPCell cellValue = new PdfPCell(new Phrase(propertyValue.ToString()));
                                table.AddCell(cellName);
                                table.AddCell(cellValue);
                            }
                        else {                            
                                PdfPCell cellName = new PdfPCell(new Phrase(propertyName));
                                PdfPCell cellValue = new PdfPCell(new Phrase(propertyValue.ToString()));
                                table.AddCell(cellName);
                                table.AddCell(cellValue);
                            }
                    }

                }

                comprobante.Add(table);

                comprobante.Close();

            ConsultarProcedimientoAlmacenado(userId, ruta);

            Response.ContentType = "Application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + nombreArchivoPDF);
            Response.TransmitFile(ruta);
            Response.End();
        }
        catch (Exception ex)
        {
            // Manejo de errores en la creación de la carpeta o archivo
            Response.Write("Error al generar el PDF: " + ex.Message);
        }
    }
    else
    {
        Response.ContentType = "Application/pdf";
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + userInfo.Nombre + "_ComprobanteCita.pdf");
        Response.TransmitFile(userInfo.Ruta);
        Response.End();
    }
}


        private void ConsultarProcedimientoAlmacenado(int userId, string rutaArchivoPDF)
        {
            string conectar = DB.Conectando();
            //string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection sqlConectar = new SqlConnection(conectar))
            {
                sqlConectar.Open();

                SqlCommand cmd = new SqlCommand("ActualizarRutaDeCita", sqlConectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                cmd.Parameters.Add("@Ruta", SqlDbType.VarChar, 255).Value = rutaArchivoPDF;

                cmd.ExecuteNonQuery();
            }
        }
    }

    public class UserInformationManager
    {
        public void DisplayUserInfo(dynamic userInfo, Label lblBienvenida, Label lblCp, Label lblEdad, Label lblDireccion, Label lblPromedio, Label lblNacimiento, Label lblCita, Button GenerarCita, Button GenerarPdf)
        {
            lblBienvenida.Text = "Bienvenido " + userInfo.Nombre ;
            lblCp.Text =  userInfo.Cp.ToString();
            lblEdad.Text = userInfo.Edad.ToString();
            lblDireccion.Text = userInfo.Direccion.ToString();
            lblPromedio.Text = userInfo.Promedio.ToString("0.00");
            lblNacimiento.Text = userInfo.Nacimiento;

            if (userInfo.Cita.ToString() != "1900-01-01")
            {
                lblCita.Text = userInfo.Cita.ToString();
                GenerarCita.Visible = false;
            }
            if (userInfo.Ruta.Length > 0)
            {
                GenerarPdf.Text = "Descargar comprobante";
            }
        }
    }

    public class SessionManager
    {
        public bool IsUserLoggedIn(System.Web.SessionState.HttpSessionState session)
        {
            return session["usuariologueado"] != null && session["datosUsuario"] != null;
        }

        public dynamic GetLoggedInUserInfo(System.Web.SessionState.HttpSessionState session)
        {
            return session["datosUsuario"];
        }
        public dynamic GetLoggedInUser(System.Web.SessionState.HttpSessionState session)
        {
            return session["usuariologueado"];
        }

        public void ClearSession(System.Web.SessionState.HttpSessionState session)
        {
            session.Remove("usuariologueado");
            session.Remove("datosUsuario");
        }

        public void RedirectToLoginPage()
        {
            System.Web.HttpContext.Current.Response.Redirect("Login_InfoToolsSV.aspx");
        }
    }

    public class AppointmentManager
    {
        public void GenerateAppointment(dynamic userInfo, Label lblCita, Button GenerarCita, Button GenerarPdf)
        {
            string conectar = DB.Conectando();
            //string conectar = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            SqlConnection sqlConectar = new SqlConnection(conectar);

            sqlConectar.Open();

            SqlCommand cmd = new SqlCommand("GenerarCita", sqlConectar);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserId", SqlDbType.Int, 50).Value = userInfo.UserId;
            cmd.Parameters.Add("@Promedio", SqlDbType.Float, 50).Value = userInfo.Promedio;

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string citaGenerada = dr.GetDateTime(dr.GetOrdinal("Dia")).ToString("yyyy-mm-dd");
                lblCita.Text = "Su cita se generó correctamente para el día " + citaGenerada;
                GenerarCita.Visible = false;
                GenerarPdf.Visible = true;
            }
            else
            {
                lblCita.Text = "Hubo un error al generar la cita";
            }
        }
    }
}
