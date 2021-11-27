using Dapper;
using Estudos.Dapper.Api.Business.Interfaces.Repositories;
using Estudos.Dapper.Api.Business.Models;
using Estudos.Dapper.Api.Extension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Estudos.Dapper.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DicasController : ControllerBase
    {
        private readonly IDicaRepository _dicaRepository;

        public DicasController(IDicaRepository dicaRepository)
        {
            _dicaRepository = dicaRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetAsync(int id)
        {
            var usuario = await _dicaRepository.ObterPorIdAsync(id);
            if (usuario is null) return NotFound();

            return Ok(usuario);
        }

        [HttpGet("stored/usuarios")]
        public async Task<IActionResult> StoredGetAsync()
        {
            var usuarios = await _dicaRepository.StoredProcedureObterTodos();

            return Ok(usuarios);
        }

        [HttpGet("stored/usuarios/{id}")]
        public async Task<IActionResult> StoredGetAsync(int id)
        {
            var usuarios = await _dicaRepository.StoredProcedureObterPorIdAsync(id);

            return Ok(usuarios);
        }
    }
}