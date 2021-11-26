using Dapper.Contrib.Extensions;
using Estudos.Dapper.Api.Business.Interfaces.Repositories;
using Estudos.Dapper.Api.Business.Models;
using Estudos.Dapper.Api.Extension;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Estudos.Dapper.Api.Infra.Data.Repositories
{
    public class ContribUsuarioRepository : IContribUsuarioRepository
    {
        private readonly IDbConnection _connection;

        public ContribUsuarioRepository(IOptions<SqlConnectionExtension> options)
        {
            _connection = new SqlConnection(options.Value.SqlConnectionString);
        }

        public async Task<List<Usuario>> ObterTodosAsync()
        {
            var lista = await _connection.GetAllAsync<Usuario>();
            return lista.ToList();
        }

        public async Task<Usuario> ObterPorIdAsync(int id)
        {
            return await _connection.GetAsync<Usuario>(id);
        }

        public async Task<int> AdicionarAsync(Usuario usuario)
        {
            usuario.Id = await _connection.InsertAsync(usuario);
            return usuario.Id;
        }

        public async Task<bool> AtualizarAsync(Usuario usuario)
        {
            return await _connection.UpdateAsync(usuario);
        }

        public async Task<bool> RemoverAsync(int id)
        {
            return await _connection.DeleteAsync(new Usuario { Id = id });
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}