using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleApi.Data;
using SimpleApi.Models;

namespace SimpleApi.Controllers
{   
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly EmpresaContext _context;

        public EmpresaController(EmpresaContext context)
        {
            _context = context;
        }

        // GET: api/Empresa
        [HttpGet]
        [DisableCors]
        public async Task<ActionResult<IEnumerable<Empresa>>> GetEmpresa()
        {
            if (_context.Empresa == null)
            {
                return NotFound();
            }
            return await _context.Empresa.ToListAsync();
        }

        // GET: api/Empresa/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Empresa>> GetEmpresa(int id)
        {
            if (_context.Empresa == null)
            {
                return NotFound();
            }
            var empresa = await _context.Empresa.FindAsync(id);

            if (empresa == null)
            {
                return NotFound();
            }

            return empresa;
        }

        // PUT: api/Empresa/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpresa(int id, Empresa empresa)
        {
            if (id != empresa.Id)
            {
                return BadRequest();
            }

            _context.Entry(empresa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpresaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Empresa
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [EnableCors]
        public async Task<ActionResult<Empresa>> PostEmpresa(Empresa empresa)
        {
            if (_context.Empresa == null)
            {
                return Problem("Entity set 'EmpresaContext.Empresa'  is null.");
            }
            _context.Empresa.Add(empresa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmpresa", new { id = empresa.Id }, empresa);
        }

        // DELETE: api/Empresa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            if (_context.Empresa == null)
            {
                return NotFound();
            }
            var empresa = await _context.Empresa.FindAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }

            _context.Empresa.Remove(empresa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmpresaExists(int id)
        {
            return (_context.Empresa?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
