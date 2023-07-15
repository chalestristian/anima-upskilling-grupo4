using api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<ModuloCurso> ModulosCursos { get; set; }
        public DbSet<Aplicacao> Aplicacoes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<NotaModulo> Notas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Defina aqui as configurações adicionais de modelagem, como chaves primárias, relacionamentos, etc.
        }
    }
}
