using Estudos.Dapper.Api.Business.Models;
using System;
using System.Threading.Tasks;

namespace Estudos.Dapper.Api.Business.Interfaces.Repositories
{
    public interface IDicaRepository : IDisposable
    {
        public Task<Usuario> ObterPorIdAsync(int id);
    }
}