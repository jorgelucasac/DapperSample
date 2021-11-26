using Estudos.Dapper.Api.Business.Interfaces.Repositories;
using Estudos.Dapper.Api.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Estudos.Dapper.Api.Controllers
{
    [ApiController]
    [Route("api/v1/contrib-usuarios")]
    public class ContribUsuariosController : ControllerBase
    {
        private readonly IContribUsuarioRepository _usuarioRepository;

        public ContribUsuariosController(IContribUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> ObterTodosAsync()
        {
            return Ok(await _usuarioRepository.ObterTodosAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Usuario>> ObterPorIdAsync(int id)
        {
            return Ok(await _usuarioRepository.ObterPorIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarAsync(Usuario usuario)
        {
            var id = await _usuarioRepository.AdicionarAsync(usuario);
            usuario.Id = id;
            return CreatedAtAction("ObterPorId", new { id }, usuario);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> AtualizarAsync(int id, Usuario usuario)
        {
            if (id != usuario.Id) return BadRequest("Dados informados não conferem");

            await _usuarioRepository.AtualizarAsync(usuario);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoverAsync(int id)
        {
            await _usuarioRepository.RemoverAsync(id);
            return Ok();
        }
    }
}