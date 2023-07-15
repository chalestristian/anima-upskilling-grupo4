using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class DadosIniciais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Pessoas",
                columns: new[] { "Id", "Nome", "CPF", "Celular", "Email" },
                values: new object[] { 1, "Administrador", "12345678909", "(99) 91234-5678", "admin@admin.com.br" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "PessoaId", "Login", "Senha", "DataUltimoAcesso", "DataCadastro" },
                values: new object[] { 1, 1, "admin", "admin", null, DateTime.UtcNow });

            migrationBuilder.InsertData(
                table: "Aplicacoes",
                columns: new[] { "Id", "Nome", "AppKey", "SecretKey" },
                values: new object[] { 1, "FrontInscricao", "TourPALaTEtrimbasmontylEYlenDI", "RigRAdmuEntErgastRADVIsIVErOgreQUawaVEnTaiRviNEleV" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Aplicacoes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pessoas",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
