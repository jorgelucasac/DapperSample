using Estudos.Dapper.Api.Business.Interfaces.Repositories;
using Estudos.Dapper.Api.Business.Models;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("mapper-usando-sql-alias")]
        public async Task<IActionResult> MapperUsandoSqlAsync()
        {
            var usuarios = await _dicaRepository.MapperUsandoSqlAsync();
            return Ok(usuarios);
        }

        [HttpGet("mapper-usando-sql-fluent")]
        public async Task<IActionResult> MapperUsandoFluentAsync()
        {
            var usuarios = await _dicaRepository.MapperUsandoConfigAsync();
            return Ok(usuarios);
        }
    }
}