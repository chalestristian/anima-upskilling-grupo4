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
                                 "WHERE \"MatriculaId\" = (select \"Id\" from \"Matriculas\" where \"CursoId\" = @CursoId AND \"AlunoId\" = @AlunoId limit 1) AND \"ModuloId\" = @moduloId";
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
                    string selectSql = "SELECT COUNT(*) FROM \"Notas\" where \"MatriculaId\" = (select \"Id\" from \"Matriculas\" where \"CursoId\" = @CursoId AND \"AlunoId\" = @MatriculaId limit 1) AND \"ModuloId\" = @ModuloId";
                    using (NpgsqlCommand selectCmd = new NpgsqlCommand(selectSql, con))
                    {
                        selectCmd.Parameters.AddWithValue("@CursoId", cursoId);
                        selectCmd.Parameters.AddWithValue("@MatriculaId", alunoId);
                        selectCmd.Parameters.AddWithValue("@ModuloId", moduloId);

                        int count = Convert.ToInt32(selectCmd.ExecuteScalar());
                        if (count > 0)
                        {
                            string updateSql = "UPDATE \"Notas\" SET \"Nota\" = @Nota WHERE \"MatriculaId\" = (select \"Id\" from \"Matriculas\" where \"CursoId\" = @CursoId AND \"AlunoId\" = @MatriculaId limit 1) AND \"ModuloId\" = @ModuloId";
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
                            string insertSql = "INSERT INTO \"Notas\" (\"MatriculaId\", \"ModuloId\", \"Nota\") VALUES ((select \"Id\" from \"Matriculas\" where \"CursoId\" = @CursoId AND \"AlunoId\" = @MatriculaId limit 1), @ModuloId, @Nota)";
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
                    string sql = "SELECT n.* FROM \"Notas\" n " +
                                 "where \"MatriculaId\" = (select \"Id\" from \"Matriculas\" where \"CursoId\" = @CursoId AND \"AlunoId\" = @MatriculaId limit 1) ";

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