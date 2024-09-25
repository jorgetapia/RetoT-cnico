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
    public class FormasFarmaceuticasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FormasFarmaceuticasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/FormasFarmaceuticas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Formasfarmaceutica>>> GetFormasFarmaceuticas()
        {
            return await _context.Formasfarmaceuticas.ToListAsync();
        }

        
    }
}
