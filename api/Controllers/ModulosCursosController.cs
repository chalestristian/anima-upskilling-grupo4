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
    public class ModulosCursosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ModulosCursosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ModulosCursos
        [HttpGet]
        public ActionResult<IEnumerable<ModuloCurso>> GetModulosCursos()
        {
            var modulosCursos = _context.ModulosCursos.Include(mc => mc.Curso).ToList();
            return Ok(modulosCursos);
        }

        // GET: api/ModulosCursos/1
        [HttpGet("{id}")]
        public ActionResult<ModuloCurso> GetModuloCurso(int id)
        {
            var moduloCurso = _context.ModulosCursos.Include(mc => mc.Curso).FirstOrDefault(mc => mc.Id == id);
            if (moduloCurso == null)
            {
                return NotFound();
            }
            return Ok(moduloCurso);
        }

        // POST: api/ModulosCursos
        [HttpPost]
        public ActionResult<ModuloCurso> CreateModuloCurso(ModuloCurso moduloCurso)
        {
            _context.ModulosCursos.Add(moduloCurso);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetModuloCurso), new { id = moduloCurso.Id }, moduloCurso);
        }

        // PUT: api/ModulosCursos/1
        [HttpPut("{id}")]
        public IActionResult UpdateModuloCurso(int id, ModuloCurso moduloCurso)
        {
            if (id != moduloCurso.Id)
            {
                return BadRequest();
            }

            _context.Entry(moduloCurso).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/ModulosCursos/1
        [HttpDelete("{id}")]
        public IActionResult DeleteModuloCurso(int id)
        {
            var moduloCurso = _context.ModulosCursos.FirstOrDefault(mc => mc.Id == id);
            if (moduloCurso == null)
            {
                return NotFound();
            }

            _context.ModulosCursos.Remove(moduloCurso);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
