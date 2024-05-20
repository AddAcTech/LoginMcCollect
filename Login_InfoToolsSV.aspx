<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login_InfoToolsSV.aspx.cs" Inherits="Login_InfoToolsSV.Login_InfoToolsSV" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/js/bootstrap.bundle.min.js"></script>
    <script src="Recursos/LocalStorage.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <link href="Recursos/CSS/Estilos.css" rel="stylesheet" />
    <title>Login</title>
</head>
<body class="bg-light">
    <div class="wrapper">
        <div class="formcontent">
            <form id="formulario_login" runat="server">
                <div class="form-control">
                    <div class="bienvenida">
                        <asp:Label class="h2" ID="lblBienvenida" runat="server" Text="Iniciar sesión"></asp:Label>
                        <p>Será redirigido a la página de inicio.</p>
                    </div>
                    <div class="Input">
<%--                        <asp:Label ID="lblUsuario" runat="server" Text="Usuario:"></asp:Label>--%>
                        <asp:TextBox ID="tbUsuario" CssClass="form-inputs" runat="server" placeholder="Ingrese correo electronico" ></asp:TextBox>
<%--                        <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>--%>
                        <asp:TextBox ID="tbPassword" CssClass="form-inputs" TextMode="Password" runat="server" placeholder="Ingrese contraseña"></asp:TextBox>
                    </div>
                    <div class="row">
                        <asp:Label runat="server" CssClass="alert-danger mt-2" ID="lblError"></asp:Label>
                    </div>
                    <div class="gap-4 p-2">
                        <asp:Button ID="BtnIngresar" CssClass="session-button" runat="server" Text="Ingresar" OnClick="BtnIngresar_Click" />
                    </div>
                </div>
            </form>
        </div>
    </div>
</body>
</html>
