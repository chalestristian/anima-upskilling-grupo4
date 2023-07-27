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
    public class CursosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CursosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Cursos
        [HttpGet]
        public ActionResult<IEnumerable<Curso>> GetCursos()
        {
            var cursos = _context.Cursos.ToList();
            /*foreach (var curso in cursos)
            {
                curso.ModulosCurso = _context.ModulosCursos.Where(m => m.Curso.Id == curso.Id).ToList();
            }*/
            return Ok(cursos);
        }

        // GET: api/Cursos/1
        [HttpGet("{id}")]
        public ActionResult<Curso> GetCurso(int id)
        {
            var curso = _context.Cursos.FirstOrDefault(c => c.Id == id);
            curso.ModulosCurso = _context.ModulosCursos.Where(m => m.Curso.Id == curso.Id).ToList();
            if (curso == null)
            {
                return NotFound();
            }
            return Ok(curso);
        }

        // POST: api/Cursos
        [HttpPost]
        public ActionResult<Curso> CreateCurso(Curso curso)
        {
            _context.Cursos.Add(curso);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCurso), new { id = curso.Id }, curso);
        }

        // PUT: api/Cursos/1
        [HttpPut("{id}")]
        public IActionResult UpdateCurso(int id, Curso curso)
        {
            if (id != curso.Id)
            {
                return BadRequest();
            }

            _context.Entry(curso).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/Cursos/1
        [HttpDelete("{id}")]
        public IActionResult DeleteCurso(int id)
        {
            var curso = _context.Cursos.FirstOrDefault(c => c.Id == id);
            if (curso == null)
            {
                return NotFound();
            }

            _context.Cursos.Remove(curso);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
