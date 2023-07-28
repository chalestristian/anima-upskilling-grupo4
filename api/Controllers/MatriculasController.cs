using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;
using api.Data;
using Microsoft.AspNetCore.Authorization;
using api.Models.DTO;

namespace api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MatriculasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Matriculas
        [HttpGet]
        public ActionResult<IEnumerable<Matricula>> GetMatriculas()
        {
            var matriculas = _context.Matriculas.Include(m => m.Aluno).ThenInclude(a => a.Pessoa).Include(m => m.Curso).ToList();
            return Ok(matriculas);
        }

        // GET: api/Matriculas/1
        [HttpGet("{id}")]
        public ActionResult<Matricula> GetMatricula(int id)
        {
            var matricula = _context.Matriculas.Include(m => m.Aluno).ThenInclude(a => a.Pessoa).Include(m => m.Curso).FirstOrDefault(m => m.Id == id);
            if (matricula == null)
            {
                return NotFound();
            }
            return Ok(matricula);
        }

        // POST: api/Matriculas
        [HttpPost]
        public ActionResult<Matricula> CreateMatricula(MatriculaCreateModel matriculaCreateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var alunoExistente = _context.Alunos.FirstOrDefault(a => a.Id == matriculaCreateModel.AlunoId);

            if (alunoExistente == null)
            {
                return BadRequest("O aluno com o ID especificado não foi encontrado.");
            }

            var cursoExistente = _context.Cursos.FirstOrDefault(c => c.Id == matriculaCreateModel.CursoId);

            if (cursoExistente == null)
            {
                return BadRequest("O curso com o ID especificado não foi encontrado.");
            }

            var matriculaExistente = _context.Matriculas.FirstOrDefault(m =>
                m.Aluno.Id == matriculaCreateModel.AlunoId && m.Curso.Id == matriculaCreateModel.CursoId);

            if (matriculaExistente != null)
            {
                return Conflict("O aluno já está matriculado no mesmo curso.");
            }

            var matricula = new Matricula
            {
                Aluno = alunoExistente,
                Curso = cursoExistente,
                ValorMatricula = matriculaCreateModel.ValorMatricula,
                DataMatricula = DateTime.UtcNow,
                MatriculaConfirmada = false 
            };

            _context.Matriculas.Add(matricula);
            _context.SaveChanges();

            matricula = _context.Matriculas
                .Include(m => m.Aluno)
                .ThenInclude(a => a.Pessoa)
                .Include(m => m.Curso)
                .FirstOrDefault(m => m.Id == matricula.Id);

            return Ok(matricula);
        }

        // PUT: api/Matriculas/1
        [HttpPut("{id}")]
        public IActionResult UpdateMatricula(int id, Matricula matricula)
        {
            if (id != matricula.Id)
            {
                return BadRequest();
            }

            _context.Entry(matricula).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/Matriculas/1
        [HttpDelete("{id}")]
        public IActionResult DeleteMatricula(int id)
        {
            var matricula = _context.Matriculas.FirstOrDefault(m => m.Id == id);
            if (matricula == null)
            {
                return NotFound();
            }

            _context.Matriculas.Remove(matricula);
            _context.SaveChanges();
            return NoContent();
        }

        // GET: api/Matriculas/ByAlunoCurso?alunoId=1&cursoId=2
        [HttpGet("ByAlunoCurso")]
        public ActionResult<Matricula> GetMatriculaByAlunoCurso(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var matricula = _context.Matriculas
                .Include(m => m.Aluno).ThenInclude(a => a.Pessoa)
                .Include(m => m.Curso).Where(m => m.Aluno.Id == id);

            if (matricula == null)
            {
                return NotFound("Matrícula não encontrada para o aluno e curso especificados.");
            }

            return Ok(matricula);
        }
    }
}
