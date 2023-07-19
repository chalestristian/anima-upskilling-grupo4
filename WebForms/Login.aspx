<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MeuProjetoWebForms.Login" %>

<!DOCTYPE html>

<html>
<head>
    <title>Tela de Login</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Tela de Login</h1>
            <div>
                <label for="txtUsuario">Usuário:</label>
                <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
            </div>
            <div>
                <label for="txtSenha">Senha:</label>
                <asp:TextBox ID="txtSenha" runat="server" TextMode="Password"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="btnLogin" runat="server" Text="Entrar" OnClick="btnLogin_Click" />
            </div>
            <div>
                <asp:Label ID="lblMensagemErro" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
