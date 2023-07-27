<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="CadastroCurso.aspx.cs" Inherits="secretaria_academica.Views.CadastroCurso" %>

<asp:Content ID="contentTitle" ContentPlaceHolderID="Title" runat="server">
    Cadastro de Cursos
</asp:Content>

<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Styles/CadastroCurso.css">
</asp:Content>

<asp:Content ID="contentMain" ContentPlaceHolderID="ContentMain" runat="server">
    <form runat="server">
        <div class="page">
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
            
            <asp:Button ID="btnCadastrarCurso" runat="server" Text="Cadastrar Curso" OnClick="btnCadastrarCurso_Click" CssClass="btn" />
            <br />
            <br />
            <label>Selecione um curso para deletar:</label>
            <asp:DropDownList ID="ddlCursos" runat="server" DataTextField="NomeCurso" DataValueField="IdCurso"></asp:DropDownList>
            <br />
            <asp:Button ID="btnDeletar" runat="server" Text="Deletar Curso" OnClick="btnDeletar_Click" CssClass="btn" />
            <br />
            <br />
            <asp:Label ID="lblMensagem" runat="server" CssClass="mensagem"></asp:Label>

            <!-- Cadastro de Módulo -->
            <h2>Cadastro de Módulo</h2>            
            <br />
            <label>Curso:</label>
            <asp:DropDownList ID="ddlCursosModulo" runat="server" DataTextField="NomeCurso" DataValueField="IdCurso"></asp:DropDownList>
            <br />
            <label>Nome do Módulo:</label>
            <asp:TextBox ID="txtNomeModulo" runat="server"></asp:TextBox>
            <br />
            <label>Carga Horária do Módulo:</label>
            <asp:TextBox ID="txtCargaHorariaModulo" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnCadastrarModulo" runat="server" Text="Cadastrar Módulo" OnClick="btnCadastrarModulo_Click" CssClass="btn" />
            <br />
            <br />
            <asp:Label ID="lblMensagemModulo" runat="server" CssClass="mensagem"></asp:Label>
        </div>
    </form>
</asp:Content>