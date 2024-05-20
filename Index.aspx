<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Login_InfoToolsSV.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="Recursos/LocalStorageDespuesDeLogin.js"></script>
    <link href="Recursos/CSS/Estilos.css" rel="stylesheet" />

    <title>Inicio</title>
</head>
<body>
    <div class="wrapper">
        <form id="formulario_index" class="card" runat="server">
            <div class="user">
                <asp:Label ID="lblBienvenida" runat="server" Text="Bienvenido" CssClass="h2"></asp:Label>
                <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar sesión" CssClass="session-button" OnClick="BtnCerrar_Click"/>
            </div>
            <div class="user-info">
                <div class="mid-container">
                    <div class="min-container">
                        <asp:Label ID="direccion" runat="server" Text="Dirección: " CssClass="h1"></asp:Label>
                        <asp:Label ID="lblDireccion" runat="server" Text="Dirección" CssClass=""></asp:Label>
                    </div>
                    <div class="min-container">
                        <asp:Label ID="Cp" runat="server" Text="Código postal: " CssClass="h1"></asp:Label>
                        <asp:Label ID="lblCp" runat="server" Text="Cp" CssClass=""></asp:Label>
                    </div>
                </div>

                <hr />

                <div class="mid-container">
                    <div class="min-container">
                        <asp:Label ID="nacimiento" runat="server" Text="Fecha de nacimiento: " CssClass="h1"></asp:Label>
                        <asp:Label ID="lblNacimiento" runat="server" Text="Fecha de nacimiento" CssClass=""></asp:Label>
                    </div>
                    <div class="min-container">
                        <asp:Label ID="edad" runat="server" Text="Edad: " CssClass="h1"></asp:Label>
                        <asp:Label ID="lblEdad" runat="server" Text="Edad" CssClass=""></asp:Label>
                    </div>
                </div>

                <hr />

                <div class="mid-container">
                    <div class="min-container">
                        <asp:Label ID="promedio" runat="server" Text="Promedio: " CssClass="h1" max="3"></asp:Label>
                        <asp:Label ID="lblPromedio" runat="server" Text="Promedio" CssClass=""></asp:Label>
                    </div>
                    <div class="min-container">
                        <asp:Label ID="cita" runat="server" Text="Fecha de cita: " CssClass="h1"></asp:Label>
                        <asp:Label ID="lblCita" runat="server" CssClass="2"></asp:Label>
                    </div>
                </div>
    

                <asp:Button ID="GenerarCita" runat="server" Text="Generar cita" CssClass="session-button" OnClick="GenerarCita_Click"/>
                <asp:Button ID="GenerarPdf" runat="server" Text="Generar Comprobante" CssClass="session-button" OnClick="GenerarPdf_Click"/>
            </div>
        </form>
    </div>


</body>
</html>
