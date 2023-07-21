<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Relatorio.aspx.cs" Inherits="WebForms2.Telas.Relatorio" %>

<!DOCTYPE html>
<html>
<head>
    <title>Relatório de Cursos e Alunos Matriculados</title>
</head>
<body>
    <form runat="server">
        <div>
            <h2>Relatório de Cursos e Alunos Matriculados</h2>
            <asp:Button ID="btnGerarRelatorio" runat="server" Text="Gerar Relatório PDF" OnClick="btnGerarRelatorio_Click" />
        </div>
    </form>
</body>
</html>
