using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;
using api.Data;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlunosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Alunos
        [HttpGet]
        public ActionResult<IEnumerable<Aluno>> GetAlunos()
        {
            var alunos = _context.Alunos.Include(a => a.Pessoa).ToList();
            return Ok(alunos);
        }

        // GET: api/Alunos/1
        [HttpGet("{id}")]
        public ActionResult<Aluno> GetAluno(int id)
        {
            var aluno = _context.Alunos.Include(a => a.Pessoa).FirstOrDefault(a => a.Id == id);
            if (aluno == null)
            {
                return NotFound();
            }
            return Ok(aluno);
        }

        // POST: api/Alunos
        [HttpPost]
        public ActionResult<Aluno> CreateAluno(Aluno aluno)
        {
            _context.Alunos.Add(aluno);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAluno), new { id = aluno.Id }, aluno);
        }

        // PUT: api/Alunos/1
        [HttpPut("{id}")]
        public IActionResult UpdateAluno(int id, Aluno aluno)
        {
            if (id != aluno.Id)
            {
                return BadRequest();
            }

            _context.Entry(aluno).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/Alunos/1
        [HttpDelete("{id}")]
        public IActionResult DeleteAluno(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null)
            {
                return NotFound();
            }

            _context.Alunos.Remove(aluno);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
