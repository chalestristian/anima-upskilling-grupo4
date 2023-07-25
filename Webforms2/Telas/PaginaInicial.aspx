<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaginaInicial.aspx.cs" Inherits="WebForms2.Telas.PaginaPosAcesso" %>

<!DOCTYPE html>
<html>
<head>
    <title>Página Pós Acesso</title>
</head>
<body>
    <form runat="server">
        <div>
            <h2>Bem-vindo à Página Inicial</h2>
            <p>O que você deseja fazer?</p>
            <asp:Button ID="btnCadastroCurso" runat="server" Text="Cadastro de Curso" OnClick="btnCadastroCurso_Click" />
            <asp:Button ID="btnLancamentoNotas" runat="server" Text="Lançamento de Notas" OnClick="btnLancamentoNotas_Click" />
            <asp:Button ID="btnRelatorioCursos" runat="server" Text="Relatório de Cursos e Alunos Matriculados" OnClick="btnRelatorioCursos_Click" />
        </div>
    </form>
</body>
</html>
