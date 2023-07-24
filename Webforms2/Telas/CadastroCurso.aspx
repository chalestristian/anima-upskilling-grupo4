<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadastroCurso.aspx.cs" Inherits="WebForms2.Telas.CadastroCurso" %>

<!DOCTYPE html>
<html lang="pt-BR">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Cadastro de Curso</title>
    <link rel="stylesheet" href="Styles/CadastroCurso.css">
</head>
<body>
    <form runat="server" class="formCadastroCurso">
        <div class="page">
            <h2>Cadastro de Curso</h2>
            <label for="txtNomeCurso">Nome do Curso:</label>
            <asp:TextBox ID="txtNomeCurso" runat="server"></asp:TextBox>
            <br />
            <label for="txtCargaHoraria">Carga Horária:</label>
            <asp:TextBox ID="txtCargaHoraria" runat="server"></asp:TextBox>
            <br />
            <label for="txtValor">Valor:</label>
            <asp:TextBox ID="txtValor" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnCadastrar" runat="server" Text="Cadastrar" OnClick="btnCadastrar_Click" CssClass="btn" />
        </div>
    </form>
</body>
</html>
