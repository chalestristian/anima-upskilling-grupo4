<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadastroCurso.aspx.cs" Inherits="WebForms2.Telas.CadastroCurso" %>

<!DOCTYPE html>
<html>
<head>
    <title>Cadastro de Curso</title>
</head>
<body>
    <form runat="server">
        <div>
            <h2>Cadastro de Curso</h2>
            <label>Nome do Curso:</label>
            <asp:TextBox ID="txtNomeCurso" runat="server"></asp:TextBox>
            <br />
            <label>Carga Horária:</label>
            <asp:TextBox ID="txtCargaHoraria" runat="server"></asp:TextBox>
            <br />
            <label>Valor:</label>
            <asp:TextBox ID="txtValor" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnCadastrar" runat="server" Text="Cadastrar" OnClick="btnCadastrar_Click" />
        </div>
    </form>
</body>
</html>
