using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoApi.Data;
using MarcasAutosApi.Models;

namespace AutoApi.Controllers
{
    //api/marcasautos
    [Route("api/[controller]")]
    [ApiController]
    public class MarcasAutosController : ControllerBase
    {
        private readonly AutoDbContext _context;

        public MarcasAutosController(AutoDbContext context)
        {
            _context = context;
        }

        // GET: api/marcasautos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarcaAuto>>> GetMarcasAutos()
        {
            // Obtiene todas las marcas de autos desde la base de datos
            var marcas = await _context.MarcasAutos.ToListAsync();
            return Ok(marcas);
        }
    }
}
