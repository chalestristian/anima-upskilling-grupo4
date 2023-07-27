using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace secretaria_academica.Models
{
    public class NotaModels
    {
        public int IdNota { get; set; }
        public AlunoModels Aluno { get; set; }
        public ModuloModels Modulo { get; set; }
        public CursoModels Curso { get; set; }
        public decimal Nota { get; set; }
        public DateTime DataLancamento { get; set; }

        public static decimal GetNotaDoAlunoPorModulo(int cursoId, int alunoId, int moduloId)
        {
            decimal nota = 0m;

            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = "SELECT \"Nota\" FROM \"Notas\" " +
                                 "WHERE \"CursoId\" = @CursoId AND \"AlunoId\" = @AlunoId AND \"ModuloId\" = @moduloId";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@CursoId", cursoId);
                        cmd.Parameters.AddWithValue("@AlunoId", alunoId);
                        cmd.Parameters.AddWithValue("@moduloId", moduloId);

                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            nota = Convert.ToDecimal(result);
                        }
                    }
                }
                catch (NpgsqlException ex)
                {
                }
                catch (Exception ex)
                {
                }
            }

            return nota;
        }

        public static void SalvarNotaAluno(int cursoId, int alunoId, int moduloId, decimal nota)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string selectSql = "SELECT COUNT(*) FROM Notas WHERE CursoId = @CursoId AND MatriculaId = @MatriculaId AND ModuloId = @ModuloNome";
                    using (NpgsqlCommand selectCmd = new NpgsqlCommand(selectSql, con))
                    {
                        selectCmd.Parameters.AddWithValue("@CursoId", cursoId);
                        selectCmd.Parameters.AddWithValue("@MatriculaId", alunoId);
                        selectCmd.Parameters.AddWithValue("@ModuloId", moduloId);

                        int count = Convert.ToInt32(selectCmd.ExecuteScalar());
                        if (count > 0)
                        {
                            string updateSql = "UPDATE Notas SET Nota = @Nota WHERE CursoId = @CursoId AND MatriculaId = @MatriculaId AND ModuloId = @ModuloNome";
                            using (NpgsqlCommand updateCmd = new NpgsqlCommand(updateSql, con))
                            {
                                updateCmd.Parameters.AddWithValue("@Nota", nota);
                                updateCmd.Parameters.AddWithValue("@CursoId", cursoId);
                                updateCmd.Parameters.AddWithValue("@MatriculaId", alunoId);
                                updateCmd.Parameters.AddWithValue("@ModuloId", moduloId);

                                updateCmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            string insertSql = "INSERT INTO Notas (CursoId, AlunoId, ModuloNome, Nota) VALUES (@CursoId, @MatriculaId, @ModuloId, @Nota)";
                            using (NpgsqlCommand insertCmd = new NpgsqlCommand(insertSql, con))
                            {
                                insertCmd.Parameters.AddWithValue("@CursoId", cursoId);
                                insertCmd.Parameters.AddWithValue("@MatriculaId", alunoId);
                                insertCmd.Parameters.AddWithValue("@ModuloId", moduloId);
                                insertCmd.Parameters.AddWithValue("@Nota", nota);

                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch (NpgsqlException ex)
                {
                }
            }
        }

        public static List<NotaModels> GetNotasDoAlunoPorCurso(int alunoId, int cursoId)
        {
            List<NotaModels> notasDoAluno = new List<NotaModels>();

            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = "SELECT n.* FROM Notas n " +
                                 "INNER JOIN Alunos a ON n.AlunoId = a.Id " +
                                 "INNER JOIN Matriculas m ON a.MatriculaId = m.Id " +
                                 "WHERE n.AlunoId = @MatriculaId AND n.CursoId = @CursoId";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@MatriculaId", alunoId);
                        cmd.Parameters.AddWithValue("@CursoId", cursoId);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                NotaModels nota = new NotaModels
                                {
                                    Curso = new CursoModels { IdCurso = Convert.ToInt32(reader["CursoId"]) },
                                    Aluno = new AlunoModels { Id = Convert.ToInt32(reader["AlunoId"]) },
                                    Modulo = new ModuloModels { IdModulo = Convert.ToInt32(reader["ModuloId"]) },
                                    Nota = Convert.ToDecimal(reader["Nota"])
                                };
                                notasDoAluno.Add(nota);
                            }
                        }
                    }
                }
                catch (NpgsqlException ex)
                {
                    // Trate a exceção conforme sua necessidade
                }
            }

            return notasDoAluno;
        }

    }
}