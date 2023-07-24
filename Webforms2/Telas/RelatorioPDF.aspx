<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatorioPDF.aspx.cs" Inherits="WebForms2.Telas.Relatorio" %>

<!DOCTYPE html>
<html lang="pt-BR">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Relatório de Cursos e Alunos Matriculados</title>
    <link rel="stylesheet" href="Styles/Relatorio.css">
    <link rel="stylesheet" href="../Styles/Main.css">
</head>
<body>
    <form runat="server" class="formRelatorio">
        <div class="page">
            <h2>Relatório de Cursos e Alunos Matriculados</h2>
            <asp:Button ID="btnGerarRelatorio" runat="server" Text="Gerar Relatório PDF" OnClick="btnGerarRelatorio_Click" CssClass="btn" />
        </div>
    </form>
</body>
</html>
