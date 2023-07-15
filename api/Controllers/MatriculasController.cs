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
            var matriculas = _context.Matriculas.Include(m => m.Aluno).Include(m => m.Curso).ToList();
            return Ok(matriculas);
        }

        // GET: api/Matriculas/1
        [HttpGet("{id}")]
        public ActionResult<Matricula> GetMatricula(int id)
        {
            var matricula = _context.Matriculas.Include(m => m.Aluno).Include(m => m.Curso).FirstOrDefault(m => m.Id == id);
            if (matricula == null)
            {
                return NotFound();
            }
            return Ok(matricula);
        }

        // POST: api/Matriculas
        [HttpPost]
        public ActionResult<Matricula> CreateMatricula(Matricula matricula)
        {
            _context.Matriculas.Add(matricula);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetMatricula), new { id = matricula.Id }, matricula);
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
    }
}
