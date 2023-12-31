﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="CadastroCurso.aspx.cs" Inherits="secretaria_academica.Views.CadastroCurso" %>

<asp:Content ID="contentTitle" ContentPlaceHolderID="Title" runat="server">
    Cadastro de Cursos
</asp:Content>

<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Styles/CadastroCurso.css">
</asp:Content>

<asp:Content ID="contentMain" ContentPlaceHolderID="ContentMain" runat="server">
    <form runat="server">
        <div class="p-5">
            <h2 class="text-center">Gerenciamento de Cursos</h2>
            <asp:Button ID="btnVoltarParaTelaAnterior" runat="server" Text="Voltar para tela principal" OnClick="btnVoltarParaTelaAnterior_Click" CssClass="d-block mb-3 btn btn-secondary"  />

            <asp:Label ID="lblMensagemErro" runat="server" CssClass="mb-3 mt-3 text-danger"></asp:Label>
            <asp:Label ID="lblMensagemSucesso" runat="server" CssClass="mb-3 mt-3 text-success"></asp:Label>

            <!-- Formulário de Cadastro -->
            <div id="divCadastro" runat="server" class="form-inline row"  visible="true">
            <h2>Cadastro de Cursos</h2>
            <div class="form-group col-6">
                <label for="TextBox1">Nome do Curso:</label>
                <asp:TextBox ID="txtNomeCurso" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group col-auto">
                <label for="TextBox2">Carga Horária:</label>
                <asp:TextBox ID="txtCargaHoraria" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group col-auto">
                <label for="TextBox3">Valor (R$):</label>
                <asp:TextBox ID="txtValor" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
                <div class="form-group col-auto">
                    <asp:Button ID="Button1" runat="server" Text="Cadastrar Curso" OnClick="btnCadastrarCurso_Click" CssClass="btn btn-primary mt-4" />
                </div>
        </div>


            <!-- Formulário de Edição -->
            <div id="divEdicao" runat="server" class="form-inline row" visible="false">
                <h2>Editar Curso</h2>
                <div class="form-group col-6">
                    <label>Nome do Curso:</label>
                    <asp:TextBox ID="txtNomeCursoEdicao" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group col-auto">
                    <label>Carga Horária:</label>
                    <asp:TextBox ID="txtCargaHorariaEdicao" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group col-auto">
                    <label>Valor (R$):</label>
                    <asp:TextBox ID="txtValorEdicao" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="form-group col-auto">
                    <asp:HiddenField ID="hdnIdCurso" runat="server" />
                    <asp:Button ID="btnSalvarEdicao" runat="server" Text="Salvar" OnClick="btnSalvarEdicao_Click" CssClass="btn mt-4" />
                </div>
                <div class="form-group col-auto">
                    <asp:Button ID="btnCancelarEdicao" runat="server" Text="Cancelar" OnClick="btnCancelarEdicao_Click" CssClass="btn mt-4" />
                </div>
            </div>

            <!-- GRID de Listagem -->
            <asp:GridView ID="gridCursos" runat="server" AutoGenerateColumns="False" CssClass="table table-flex table-striped"  OnRowCommand="gridCursos_RowCommand">
                <Columns>
                    <asp:BoundField DataField="IdCurso" HeaderText="ID" />
                    <asp:BoundField DataField="NomeCurso" HeaderText="Nome do Curso" />
                    <asp:BoundField DataField="CargaHoraria" HeaderText="Carga Horária" ControlStyle-CssClass="text-center" />
                    <asp:BoundField DataField="ValorCurso" HeaderText="Valor (R$)" ControlStyle-CssClass="text-right" />

                    <asp:TemplateField HeaderText="Ações">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEditar" runat="server" Text="Editar" CommandName="Editar" CommandArgument='<%# Eval("IdCurso") %>' />
                            <asp:LinkButton ID="lnkGerenciarModulos" runat="server" Text="Ver Módulos" CommandName="GerenciarModulos" CommandArgument='<%# Eval("IdCurso") %>' />
                            <asp:LinkButton ID="lnkExcluir" runat="server" Text="Excluir" CommandName="Excluir" CommandArgument='<%# Eval("IdCurso") %>' CssClass="text-danger" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>


        </div>
    </form>
</asp:Content>