﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LancamentoNota.aspx.cs" Inherits="WebForms2.Telas.LancamentoNota" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lançamento de Notas</title>
    <link rel="stylesheet" href="Styles/LancamentoNota.css">
</head>
<body>
    <form id="form1" runat="server" class="formLancamentoNota">
        <div class="page">
            <h1>Lançamento de Notas</h1>
            <asp:Label ID="lblMensagem" runat="server" ForeColor="Red"></asp:Label>
            <br />
            <asp:TextBox ID="txtRAAluno" runat="server" placeholder="RA do Aluno"></asp:TextBox>
            <br />
            <asp:DropDownList ID="ddlCursos" runat="server" DataTextField="NomeCurso" DataValueField="IdCurso">
                <!-- lista curso preenchida código-behind -->
            </asp:DropDownList>
            <br />
            <asp:TextBox ID="txtNota" runat="server" placeholder="Nota"></asp:TextBox>
            <br />
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" CssClass="btn" />
        </div>
    </form>
</body>
</html>