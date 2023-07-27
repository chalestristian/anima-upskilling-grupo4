<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="LancarNotas.aspx.cs" Inherits="secretaria_academica.Views.LancarNotas" %>

<asp:Content ID="contentTitle" ContentPlaceHolderID="Title" runat="server">
    Lançar Notas
</asp:Content>

<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Styles/LancarNotas.css">
</asp:Content>

<asp:Content ID="contentMain" ContentPlaceHolderID="ContentMain" runat="server">
    <form runat="server">
        <div class="p-5">
            <h2 class="text-center">Lançar Notas</h2>
            <asp:Button ID="btnVoltarParaTelaAnterior" runat="server" Text="Voltar para tela principal" OnClick="btnVoltarParaTelaAnterior_Click" CssClass="d-block mb-3 btn btn-secondary"  />

            <div class="form-group">
                <label for="ddlCurso">Selecione o Curso:</label>
                <asp:DropDownList ID="ddlCurso" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCurso_SelectedIndexChanged"></asp:DropDownList>
            </div>

            <div class="form-group">
                <label for="ddlAluno">Selecione o Aluno:</label>
                <asp:DropDownList ID="ddlAluno" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlAluno_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </div>

            <asp:GridView ID="gridNotas" runat="server" AutoGenerateColumns="False" CssClass="table table-flex table-striped" DataKeyNames="ModuloId">
                <Columns>
                    <asp:BoundField DataField="ModuloNome" HeaderText="Módulo" />
                    <asp:TemplateField HeaderText="Nota">
                        <ItemTemplate>
                            <asp:TextBox ID="txtNota" runat="server" Text='<%# Bind("Nota") %>' CssClass="form-control"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>


            <asp:Button ID="btnSalvar" runat="server" Text="Salvar Notas" OnClick="btnSalvar_Click" CssClass="btn btn-primary mt-3" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" CssClass="btn mt-4" />

            <div class="row">
                <asp:Label ID="lblMensagemErro" runat="server" CssClass="mb-3 mt-3 text-danger"></asp:Label>
                <asp:Label ID="lblMensagemSucesso" runat="server" CssClass="mb-3 mt-3 text-success"></asp:Label>
            </div>
        </div>
    </form>
</asp:Content>
