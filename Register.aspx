<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Login_InfoToolsSV.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <link href="Recursos/CSS/Estilos.css" rel="stylesheet" />
    <title>Register</title>
</head>
<body class="bg-light">
    <div class="wrapper">
        <div>
            <form id="formulario_login" runat="server" class="register">
                <div class="form-control">
                    <asp:Button ID="BtnCerrarSesion" runat="server" Text="Cerrar Sesión" OnClick="BtnCerrarSesion_Click" CssClass="btn btn-danger" UseSubmitBehavior="false"/>   
                    <div class="row">
                        <asp:Label class="h2 bienvenida" ID="lblBienvenida" runat="server" Text="Registro de usuario"></asp:Label>
                    </div>

                    <div class="input-container">
                        <div class="label-input">
                            <asp:Label ID="lblUsuario" runat="server" Text="Usuario:"></asp:Label>
                            <asp:TextBox ID="tbUsuario" CssClass="form-inputs" runat="server" placeholder="usuario@usuario.com" required="" OnTextChanged="tbUsuario_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                        <asp:Label runat="server" CssClass="alert-danger" ID="lblErrorUsuario"></asp:Label>
                    </div>

                    <div class="input-container"->
                        <div class="label-input">
                            <asp:Label ID="lblNombre" runat="server" Text="Nombre completo:"></asp:Label>
                            <asp:TextBox ID="tbNombre" CssClass="form-inputs" runat="server" placeholder="Nombre Apellido Apellido" required="" OnTextChanged="tbNombre_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                        <asp:Label runat="server" CssClass="alert-danger" ID="lblErrorNombre"></asp:Label>
                    </div>

                    <div class="input-container">
                        <p class="estructura">La contraseña debe contener letras minúsculas, al menos una mayúscula y un número, y tener de 8 a 12 caracteres</p>
                        <div class="label-input">
                            <asp:Label ID="lblPassword" runat="server" Text="Contraseña:"></asp:Label>
                            <asp:TextBox ID="tbPassword" CssClass="form-inputs" TextMode="Password" runat="server" placeholder="Contras123" required="" OnTextChanged="tbPassword_TextChanged" ></asp:TextBox>
                        </div>
                        <asp:Label runat="server" CssClass="alert-danger" ID="lblErrorPassword"></asp:Label>
                    </div>

                    <div class="input-container">
                        <div class="label-input">
                            <asp:Label ID="lblConfirmarPassword" runat="server" Text="Confirmar contraseña:" MaxLength="12" MinLength="8"></asp:Label>
                            <asp:TextBox ID="tbConfirmarPassword" CssClass="form-inputs" TextMode="Password" runat="server" placeholder="Contras123" required="" OnTextChanged="tbConfirmarPassword_TextChanged" ></asp:TextBox>
                        </div>
                        <asp:Label runat="server" CssClass="alert-danger" ID="lblErrorPasswordConf" MaxLength="12" MinLength="8"></asp:Label>
                    </div>

                    <div class="input-container">
                        <div class="label-input">
                            <asp:Label ID="lblDireccion" runat="server" Text="Dirección:"></asp:Label>
                            <asp:TextBox ID="tbDireccion" CssClass="form-inputs" runat="server" placeholder="Municipio, Calle #1" required="" OnTextChanged="tbDireccion_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                        <asp:Label runat="server" CssClass="alert-danger" ID="lblErrorDireccion"></asp:Label>
                    </div>

                    <div class="input-container">
                        <div class="label-input">
                            <asp:Label ID="lblCp" runat="server" Text="Código postal:"></asp:Label>
                            <asp:TextBox ID="tbCp" CssClass="form-inputs" runat="server" placeholder="12345" required="" MaxLength="5" OnTextChanged="tbCp_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                        <asp:Label runat="server" CssClass="alert-danger" ID="lblErrorCp"></asp:Label>
                    </div>

                    <div class="input-container">
                        <div class="label-input">
                            <asp:Label ID="lblPromedio" runat="server" Text="Promedio:"></asp:Label>
                            <asp:TextBox ID="tbPromedio" CssClass="form-inputs" runat="server" placeholder="8" required="" OnTextChanged="tbPromedio_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                        <asp:Label runat="server" CssClass="alert-danger" ID="lblErrorPromedio"></asp:Label>
                    </div>

                    <div class="input-container">
                        <div class="label-input">
                            <asp:Label ID="lblNacimiento" runat="server" Text="Fecha de nacimiento:"></asp:Label>
                            <asp:TextBox ID="tbNacimiento" CssClass="form-inputs"  runat="server" placeholder="YYYY-MM-DD" required="" OnTextChanged="tbNacimiento_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                        <asp:Label runat="server" CssClass="alert-danger" ID="lblErrorNacimiento"></asp:Label>
                    </div>

                    <div class="row">
                        <asp:Label runat="server" CssClass="alert-danger" ID="lblError"></asp:Label>
                    </div>

                    <div class="row">
                        <asp:Button ID="BtnRegistrar" CssClass="session-button" runat="server" Text="Registrar" OnClick="BtnRegistrar_Click" />
                    </div>
                </div>
            </form>
        </div>
    </div>
</body>
</html>

