<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebForms2.Telas.Login" %>

<!DOCTYPE html>
<html>
<head>
    <title>Tela de Login</title>
    <link rel="stylesheet" href="Styles/Login.css" />
</head>
<body>
    <form runat="server">
        <div class="page">
            <div class="formLogin">
                <h1>Login</h1>
                <p>Digite os seus dados de acesso no campo abaixo.</p>
                <label for="email">E-mail</label>
                <asp:TextBox ID="txtUsuario" runat="server" placeholder="Digite seu e-mail" autofocus="true" />
                <label for="password">Senha</label>
                <asp:TextBox ID="txtSenha" runat="server" placeholder="Digite sua senha" TextMode="Password" />
                <a href="/">Esqueci minha senha</a>
                <asp:Label ID="lblMensagem" runat="server" ForeColor="Red"></asp:Label>
                <asp:Button ID="btnEntrar" runat="server" Text="Acessar" OnClick="btnEntrar_Click" CssClass="btn" />
            </div>
        </div>
    </form>
</body>
</html>
