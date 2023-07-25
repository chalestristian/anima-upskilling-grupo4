using Microsoft.EntityFrameworkCore.Migrations;
using System.Security.Cryptography;
using System.Text;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarSenhaUsuarioAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sha256 = new SHA256Managed();
            var senhaBytes = Encoding.UTF8.GetBytes("admin");
            var senhaCriptografada = sha256.ComputeHash(senhaBytes);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "Senha",
                value: Convert.ToBase64String(senhaCriptografada));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
