<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebForms2.Telas.Login" %>

<!DOCTYPE html>
<html lang="pt-BR">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Tela de Login</title>
    <link rel="stylesheet" href="Login.css">
</head>
<body>
    <form runat="server" class="formLogin">
        <div class="page">
            <h1>Login</h1>
            <p>Digite os seus dados de acesso no campo abaixo.</p>
            <label for="txtUsuario">Usuário:</label>
            <asp:TextBox ID="txtUsuario" runat="server" placeholder="Digite seu e-mail" ClientIDMode="Static"></asp:TextBox>
            <label for="txtSenha">Senha:</label>
            <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" placeholder="Digite sua senha" ClientIDMode="Static"></asp:TextBox>
            <a href="/">Esqueci minha senha</a>
            <asp:Button ID="btnEntrar" runat="server" Text="Acessar" OnClick="btnEntrar_Click" CssClass="btn" />
            <label ID="lblMensagem" runat="server" CssClass="error-message"></label>
        </div>
    </form>
</body>
</html>
