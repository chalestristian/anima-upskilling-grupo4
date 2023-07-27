<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="GestaoMatriculas.aspx.cs" Inherits="secretaria_academica.Views.GestaoMatriculas" %>

<asp:Content ID="contentTitle" ContentPlaceHolderID="Title" runat="server">
    Gerenciamento de Matrículas
</asp:Content>

<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Styles/CadastroCurso.css">
</asp:Content>

<asp:Content ID="contentMain" ContentPlaceHolderID="ContentMain" runat="server">
    <form runat="server">
        <div class="p-5">
            <h2 class="text-center">Gerenciamento de Matrículas</h2>
            <asp:Button ID="btnVoltarParaTelaAnterior" runat="server" Text="Voltar para tela principal" OnClick="btnVoltarParaTelaAnterior_Click" CssClass="d-block mb-3 btn btn-secondary"  />

            <asp:Label ID="lblMensagemErro" runat="server" CssClass="mb-3 mt-3 text-danger"></asp:Label>
            <asp:Label ID="lblMensagemSucesso" runat="server" CssClass="mb-3 mt-3 text-success"></asp:Label>

            <asp:GridView ID="gridAlunos" runat="server" AutoGenerateColumns="False" CssClass="table table-flex table-striped" OnRowCommand="gridAlunos_RowCommand" OnRowDataBound="gridAlunos_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Matricula" />
                    <asp:BoundField DataField="Aluno.Pessoa.NomePessoa" HeaderText="Nome" />
                    <asp:BoundField DataField="Aluno.Pessoa.CPF" HeaderText="CPF" />
                    <asp:BoundField DataField="Curso.NomeCurso" HeaderText="Curso" />
                    <asp:BoundField DataField="Aluno.Pessoa.Email" HeaderText="E-mail" />
                    <asp:BoundField DataField="DataMatricula" HeaderText="Data Matrícula" DataFormatString="{0:dd/MM/yyyy}" ControlStyle-CssClass="text-center" />
                    <asp:BoundField DataField="ValorMatricula" HeaderText="Valor (R$)" DataFormatString="{0:C2}" ControlStyle-CssClass="text-right" />
                    <asp:BoundField DataField="Media" HeaderText="Média" DataFormatString="{0:N2}" ControlStyle-CssClass="text-right" />
                    <asp:BoundField DataField="Status" HeaderText="Status" ControlStyle-CssClass="text-center" />
                    <asp:BoundField DataField="DataConclusao" HeaderText="Data Conclusão" DataFormatString="{0:dd/MM/yyyy}" ControlStyle-CssClass="text-center" />

                    <asp:TemplateField HeaderText="Matrícula Confirmada">
                        <ItemTemplate>
                            <asp:Button ID="btnConfirmarMatricula" runat="server" Text="Confirmar" CssClass="btn btn-success" CommandName="ConfirmarMatricula" CommandArgument='<%# Eval("Id") %>' Visible='<%# !Convert.ToBoolean(Eval("MatriculaConfirmada")) %>' />
                            <asp:Button ID="btnCancelarMatricula" runat="server" Text="Cancelar" CssClass="btn btn-danger btn-sair" CommandName="CancelarMatricula" CommandArgument='<%# Eval("Id") %>' Visible='<%# Convert.ToBoolean(Eval("MatriculaConfirmada")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>


        </div>
    </form>
</asp:Content>