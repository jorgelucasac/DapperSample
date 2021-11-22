using System.Collections.Generic;
using System.Threading.Tasks;
using Estudos.Dapper.Api.Business.Models;

namespace Estudos.Dapper.Api.Business.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        public Task<List<Usuario>> ObterTodosAsync();
        public Task<Usuario> ObterPorIdAsync(int id);
      
    }
}