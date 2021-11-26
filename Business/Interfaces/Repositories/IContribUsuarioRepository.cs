using Estudos.Dapper.Api.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Estudos.Dapper.Api.Business.Interfaces.Repositories
{
    public interface IContribUsuarioRepository : IDisposable
    {
        public Task<List<Usuario>> ObterTodosAsync();

        public Task<Usuario> ObterPorIdAsync(int id);

        public Task<int> AdicionarAsync(Usuario usuario);

        public Task<bool> AtualizarAsync(Usuario usuario);

        public Task<bool> RemoverAsync(int id);
    }
}