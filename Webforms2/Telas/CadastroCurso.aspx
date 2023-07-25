<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CadastroCurso.aspx.cs" Inherits="WebForms2.Telas.CadastroCurso" %>

<!DOCTYPE html>
<html>
<head>
    <title>Cadastro de Curso</title>
    <link rel="stylesheet" href="Styles/CadastroCurso.css" />
</head>
<body>
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

            <!-- Seção de Cadastro de Módulo -->
            <h2>Cadastro de Módulo</h2>
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
</body>
</html>
