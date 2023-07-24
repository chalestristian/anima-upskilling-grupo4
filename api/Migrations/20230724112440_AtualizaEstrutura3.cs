using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaEstrutura3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlunoId",
                table: "Usuarios",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PerfilAdministrativo",
                table: "Usuarios",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PerfilAluno",
                table: "Usuarios",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Boleto",
                table: "Matriculas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MatriculaConfirmada",
                table: "Matriculas",
                type: "boolean",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_AlunoId",
                table: "Usuarios",
                column: "AlunoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Alunos_AlunoId",
                table: "Usuarios",
                column: "AlunoId",
                principalTable: "Alunos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Alunos_AlunoId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_AlunoId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "AlunoId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "PerfilAdministrativo",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "PerfilAluno",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Boleto",
                table: "Matriculas");

            migrationBuilder.DropColumn(
                name: "MatriculaConfirmada",
                table: "Matriculas");
        }
    }
}
