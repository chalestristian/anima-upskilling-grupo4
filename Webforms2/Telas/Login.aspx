<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebForms2.Telas.Login" %>

<!DOCTYPE html>
<html>
<head>
    <title>Tela de Login</title>
</head>
<body>
    <form runat="server">
        <div>
            <h2>Tela de Login</h2>
            <label>Usuário:</label>
            <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
            <br />
            <label>Senha:</label>
            <asp:TextBox ID="txtSenha" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <asp:Button ID="btnEntrar" runat="server" Text="Entrar" OnClick="btnEntrar_Click" />
            <label>Mensagem:</label>
            <asp:Label ID="lblMensagem" runat="server" Text=""></asp:Label>
            <br />
        </div>
    </form>
</body>
</html>
