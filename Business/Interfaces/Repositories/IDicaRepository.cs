using Estudos.Dapper.Api.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Estudos.Dapper.Api.Business.Interfaces.Repositories
{
    public interface IDicaRepository : IDisposable
    {
        public Task<Usuario> ObterPorIdAsync(int id);

        public Task<IEnumerable<Usuario>> StoredProcedureObterTodos();

        public Task<Usuario> StoredProcedureObterPorIdAsync(int id);
    }
}