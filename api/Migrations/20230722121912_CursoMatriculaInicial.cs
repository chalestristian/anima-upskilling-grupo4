using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class CursoMatriculaInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Pessoas",
                columns: new[] { "Nome", "CPF", "Celular", "Email" },
                values: new object[] { "William Cleisson de Carvalho", "08021658690", "(35) 99847-4911", "williamcc89@gmail.com" });
            
            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "PessoaId", "Matricula", "DataCadastro" },
                values: new object[] { 2, "123456", DateTime.UtcNow });

            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "Id", "Nome", "CH", "Valor" },
                values: new object[] { 1, "Programação em C#", 120, 199.00 });

            migrationBuilder.InsertData(
                table: "ModulosCursos",
                columns: new[] { "CursoId", "Nome", "CH" },
                values: new object[] { 1, "Introdução ao DotNet Console", 30 });

            migrationBuilder.InsertData(
                table: "ModulosCursos",
                columns: new[] { "CursoId", "Nome", "CH" },
                values: new object[] { 1, "Introdução ao WindowsForms", 30 });

            migrationBuilder.InsertData(
                table: "ModulosCursos",
                columns: new[] { "CursoId", "Nome", "CH" },
                values: new object[] { 1, "Introdução ao WebForms", 30 });

            migrationBuilder.InsertData(
                table: "ModulosCursos",
                columns: new[] { "CursoId", "Nome", "CH" },
                values: new object[] { 1, "Introdução ao DotNet Core", 30 });

            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "Id", "Nome", "CH", "Valor" },
                values: new object[] { 2, "Programação com VueJs", 60, 99.00 });

            migrationBuilder.InsertData(
                table: "ModulosCursos",
                columns: new[] { "CursoId", "Nome", "CH" },
                values: new object[] { 2, "Introdução ao VueJS", 20 });

            migrationBuilder.InsertData(
                table: "ModulosCursos",
                columns: new[] { "CursoId", "Nome", "CH" },
                values: new object[] { 2, "Criando uma aplicação do zero", 40 });

            migrationBuilder.InsertData(
                table: "Matriculas",
                columns: new[] { "AlunoId", "CursoId", "DataMatricula", "ValorMatricula" },
                values: new object[] { 1, 1, DateTime.UtcNow, 199.00 });


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM \"Matriculas\"");
            migrationBuilder.Sql("DELETE FROM \"ModulosCursos\"");
            migrationBuilder.Sql("DELETE FROM \"Cursos\"");
            migrationBuilder.Sql("DELETE FROM \"Pessoas\"");
        }
    }
}
