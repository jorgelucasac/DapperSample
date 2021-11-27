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
        public IActionResult StoredGet()
        {
            var usuarios = _connection.Query<Usuario>("SelecionarUsuarios", commandType: CommandType.StoredProcedure);

            return Ok(usuarios);
        }

        [HttpGet("stored/usuarios/{id}")]
        public IActionResult StoredGet(int id)
        {
            var usuarios = _connection.Query<Usuario>("SelecionarUsuario", new { Id = id }, commandType: CommandType.StoredProcedure);

            return Ok(usuarios);
        }
    }
}