using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estudos.Dapper.Api.Business.Interfaces.Repositories;
using Estudos.Dapper.Api.Business.Models;

namespace Estudos.Dapper.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuariosController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodosAsync()
        {
            return Ok(await _usuarioRepository.ObterTodosAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ObterPorIdAsync(int id)
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
            if (id != usuario.Id)
            {
                return BadRequest("Dados informados não conferem");
            }

            await _usuarioRepository.AtualizarAsync(usuario);
            return NoContent();
        }
    }
}
