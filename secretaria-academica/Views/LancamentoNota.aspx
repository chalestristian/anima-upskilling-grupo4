<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="LancamentoNota.aspx.cs" Inherits="secretaria_academica.Views.LancamentoNota" %>

<asp:Content ID="contentTitle" ContentPlaceHolderID="Title" runat="server">
    Lançamento de notas</asp:Content>

<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Styles/LancamentoNota.css">
</asp:Content>

<asp:Content ID="contentMain" ContentPlaceHolderID="ContentMain" runat="server">
    <form id="form1" runat="server" class="formLancamentoNota">
        <div class="page">
            <h1>Lançamento de Notas</h1>
            <asp:Label ID="lblMensagem" runat="server" ForeColor="Red"></asp:Label>
            <br />
            <asp:Label ID="lblRAAluno" runat="server" Text="RA do Aluno:"></asp:Label>
            <asp:TextBox ID="txtRAAluno" runat="server" placeholder="Digite o RA do Aluno"></asp:TextBox>
            <br />
            <asp:Label ID="lblCurso" runat="server" Text="Curso:"></asp:Label>
            <asp:DropDownList ID="ddlCursos" runat="server" DataTextField="NomeCurso" DataValueField="IdCurso" OnSelectedIndexChanged="ddlCursos_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <br />
            <asp:Label ID="lblModulo" runat="server" Text="Módulo:"></asp:Label>
            <asp:DropDownList ID="ddlModulos" runat="server" DataTextField="NomeModulo" DataValueField="IdModulo">
            </asp:DropDownList>
            <br />
            <asp:Label ID="lblNota" runat="server" Text="Nota:"></asp:Label>
            <asp:TextBox ID="txtNota" runat="server" placeholder="Digite a Nota"></asp:TextBox>
            <br />
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" CssClass="btn" />
            <asp:Button ID="btnRetornarPaginaInicial" runat="server" Text="Retornar à Página Inicial" OnClick="btnRetornarPaginaInicial_Click" CssClass="btn" />
        </div>
        <div class="buscarNotas">
            <h2>Buscar Notas Lançadas</h2>
            <asp:Label ID="lblMensagemBusca" runat="server" ForeColor="Red"></asp:Label>
            <br />
            <asp:Label ID="lblBuscarRAAluno" runat="server" Text="Buscar por RA do Aluno:"></asp:Label>
            <asp:TextBox ID="txtBuscarRAAluno" runat="server" placeholder="Digite o RA do Aluno"></asp:TextBox>
            <br />
            <asp:Button ID="btnBuscarNotas" runat="server" Text="Buscar Notas" OnClick="btnBuscarNotas_Click" CssClass="btn" />
            <br />
            <br />


            <asp:GridView ID="gridNotasLancadas" runat="server" AutoGenerateColumns="false" OnRowCommand="gridNotasLancadas_RowCommand">
                <Columns>
                    <asp:BoundField DataField="RAAluno" HeaderText="RA do Aluno" />
                    <asp:BoundField DataField="NomeCurso" HeaderText="Curso" />
                    <asp:BoundField DataField="NomeModulo" HeaderText="Módulo" />
                    <asp:BoundField DataField="Nota" HeaderText="Nota" />
                    <asp:ButtonField Text="Editar" ButtonType="Button" CommandName="EditarNota" />
                    <asp:ButtonField Text="Deletar" ButtonType="Button" CommandName="DeletarNota" />
                </Columns>
            </asp:GridView>
            <br />
                        
        </div>
    </form>
</asp:Content>