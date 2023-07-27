<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="RelatorioPDF.aspx.cs" Inherits="secretaria_academica.Views.RelatorioPDF" %>

<asp:Content ID="contentTitle" ContentPlaceHolderID="Title" runat="server">
    Relatório
</asp:Content>

<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Styles/RelatorioPDF.css">
</asp:Content>

<asp:Content ID="contentMain" ContentPlaceHolderID="ContentMain" runat="server">
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
</asp:Content>