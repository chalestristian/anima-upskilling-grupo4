using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;

namespace secretaria_academica.Models
{
    public class AlunoModels
    {
        public int Id { get; set; }
        public PessoaModels Pessoa { get; set; }
        public DateTime? DataCadastro { get; set; }

        public static List<AlunoModels> GetAlunosMatriculadosPorCurso(int cursoId)
        {
            List<AlunoModels> alunosMatriculados = new List<AlunoModels>();

            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = "SELECT a.*, p.\"Nome\" as \"NomePessoa\" FROM \"Matriculas\" m " +
                                 "INNER JOIN \"Alunos\" a ON m.\"AlunoId\" = a.\"Id\" " +
                                 "INNER JOIN \"Pessoas\" p ON a.\"PessoaId\" = p.\"Id\" " +
                                 "INNER JOIN \"Cursos\" c ON m.\"CursoId\" = c.\"Id\" " +
                                 "WHERE \"CursoId\" = @CursoId";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@CursoId", cursoId);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AlunoModels aluno = new AlunoModels
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Pessoa = new PessoaModels { IdPessoa = Convert.ToInt32(reader["PessoaId"]), NomePessoa = reader["NomePessoa"].ToString() },
                                    DataCadastro = Convert.ToDateTime(reader["DataCadastro"])
                                };
                                alunosMatriculados.Add(aluno);
                            }
                        }
                    }
                }
                catch (NpgsqlException ex)
                {
                    // Trate o erro aqui
                }
                catch (Exception ex)
                {
                    // Trate o erro aqui
                }
            }

            return alunosMatriculados;
        }

        public static void AtualizarMediaEStatus(int alunoId, int cursoId, decimal media, string status)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = "UPDATE \"Matriculas\" " +
                                 "SET \"Media\" = @Media, \"Status\" = @Status " +
                                 "WHERE \"AlunoId\" = @AlunoId and \"CursoId\" = @CursoId";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Media", media);
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.Parameters.AddWithValue("@AlunoId", alunoId);
                        cmd.Parameters.AddWithValue("@CursoId", cursoId);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (NpgsqlException ex)
                {
                }
            }
        }

        public static void PreencherDataConclusao(int alunoId, int cursoId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = "UPDATE \"Matriculas\" " +
                                 "SET \"DataConclusao\" = NOW() " +
                                 "WHERE \"AlunoId\" = @AlunoId  and \"CursoId\" = @CursoId AND (\"DataConclusao\" IS NULL)";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@AlunoId", alunoId);
                        cmd.Parameters.AddWithValue("@CursoId", cursoId);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (NpgsqlException ex)
                {
                }
            }
        }

    }
}