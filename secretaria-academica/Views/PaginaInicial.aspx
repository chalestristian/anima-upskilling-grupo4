<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="PaginaInicial.aspx.cs" Inherits="secretaria_academica.Views.PaginaInicial" %>

<asp:Content ID="contentTitle" ContentPlaceHolderID="Title" runat="server">
    Área Administrativa</asp:Content>

<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Styles/PaginaInicial.css">
</asp:Content>

<asp:Content ID="contentMain" ContentPlaceHolderID="ContentMain" runat="server">
    <form runat="server">
        <div class="page">
            <h1>Área Administrativa</h1>
            <p>Bem-vindo à área administrativa!
                Selecione a opção desejada.
            </p>
            <a href="CadastroCurso.aspx" class="btn1">Cadastro de Curso</a>
            <a href="GestaoMatriculas.aspx" class="btn1">Gerenciar Matrículas</a>
            <a href="LancarNotas.aspx" class="btn1">Lançamento de Notas</a>
            <!-- <a href="RelatorioPDF.aspx" class="btn1">Relatórios</a> -->
            <asp:Button ID="btnSair" runat="server" Text="Sair" CssClass="btn btn-danger" OnClick="btnSair_Click" />

        </div>
        
    </form>


</asp:Content>
