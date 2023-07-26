<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatorioPDF.aspx.cs" Inherits="WebForms2.Telas.RelatorioPDF" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Relatórios em PDF</title>
    <link rel="stylesheet" href="RelatorioPDF.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Relatórios em PDF</h1>
            <div class="buttons-container">
                <asp:Button ID="btnAlunosCadastrados" runat="server" Text="Alunos Cadastrados" OnClick="btnAlunosCadastrados_Click" CssClass="btn" />
                <asp:Button ID="btnCursosCadastrados" runat="server" Text="Cursos Cadastrados" OnClick="btnCursosCadastrados_Click" CssClass="btn" />
                <asp:Button ID="btnNotasLancadas" runat="server" Text="Notas Lançadas" OnClick="btnNotasLancadas_Click" CssClass="btn" />
            </div>
            <br />
            <asp:Button ID="btnRetornarPaginaInicial" runat="server" Text="Retornar à Página Inicial" OnClick="btnRetornarPaginaInicial_Click" CssClass="btn btn-sair" />
        </div>
    </form>
</body>
</html>
