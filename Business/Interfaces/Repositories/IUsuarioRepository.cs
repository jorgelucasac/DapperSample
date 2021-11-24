using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Estudos.Dapper.Api.Business.Models;

namespace Estudos.Dapper.Api.Business.Interfaces.Repositories
{
    public interface IUsuarioRepository : IDisposable
    {
        public Task<List<Usuario>> ObterTodosAsync();
        public Task<List<Usuario>> ObterTodosCompletoAsync();
        public Task<Usuario> ObterPorIdAsync(int id);
        public Task<int> AdicionarAsync(Usuario usuario);
        public Task<bool> AtualizarAsync(Usuario usuario);
        public Task<bool> RemoverAsync(int id);
    }
}