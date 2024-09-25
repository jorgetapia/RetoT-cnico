using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAccesoDatosReto.dbRetoTecnico;

namespace ApiAccesoDatosReto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MedicamentoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Medicamento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VMedicamento>>> GetMedicamentos()
        {

            return await _context.VMedicamentos.ToListAsync();
        }

        // GET: api/Medicamento/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VMedicamento>> GetMedicamento(int id)
        {
            var medicamento = await _context.VMedicamentos.Where(c=>c.Id==id).FirstAsync();

            if (medicamento == null)
            {
                return NotFound();
            }

            return medicamento;
        }

        // PUT: api/Medicamento/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicamento(int id, Medicamento medicamento)
        {
            if (ModelState.IsValid)
            {
                if (id != medicamento.Idmedicamento)
                {
                    return BadRequest();
                }

                _context.Entry(medicamento).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicamentoExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }

        // POST: api/Medicamento
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Medicamento>> PostMedicamento(Medicamento medicamento)
        {
            if (ModelState.IsValid)
            {
                _context.Medicamentos.Add(medicamento);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetMedicamento", new { id = medicamento.Idmedicamento }, medicamento);
            }
            else
            {
                return BadRequest(ModelState);//En caso de que no se cumplan las validaciones de entrada las regresamos al front para que se muestren al usuario.
            }
        }

        // DELETE: api/Medicamento/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicamento(int id)
        {
            var medicamento = await _context.Medicamentos.FindAsync(id);
            if (medicamento == null)
            {
                return NotFound();
            }

            _context.Medicamentos.Remove(medicamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedicamentoExists(int id)
        {
            return _context.Medicamentos.Any(e => e.Idmedicamento == id);
        }
    }
}
