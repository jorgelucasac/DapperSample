using Dapper;
using Estudos.Dapper.Api.Business.Interfaces.Repositories;
using Estudos.Dapper.Api.Business.Models;
using Estudos.Dapper.Api.Extension;
using Estudos.Dapper.Api.Infra.Data.Repositories.Queries;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Estudos.Dapper.Api.Infra.Data.Repositories
{
    public class DicaRepository : IDicaRepository
    {
        private readonly IDbConnection _connection;

        public DicaRepository(IOptions<SqlConnectionExtension> option)
        {
            _connection = new SqlConnection(option.Value.SqlConnectionString);
        }

        public async Task<Usuario> ObterPorIdAsync(int id)
        {
            string sql = DicasQueries.SelecionarUsuarioPorId +
                         DicasQueries.SelecionarContatosPorUsuarioId +
                         DicasQueries.SelecionarEnderecosEntregaPorUsuarioId +
                         DicasQueries.SelecionarDepartamentosPorUsuarioId;

            using var multipleResultSets = await _connection.QueryMultipleAsync(sql, new { Id = id });

            var usuario = await multipleResultSets.ReadFirstOrDefaultAsync<Usuario>();
            if (usuario is null) return null;

            var contato = await multipleResultSets.ReadFirstOrDefaultAsync<Contato>();
            var enderecos = await multipleResultSets.ReadAsync<EnderecoEntrega>();
            var departamentos = await multipleResultSets.ReadAsync<Departamento>();

            usuario.Contato = contato;
            usuario.EnderecosEntrega = enderecos.ToList();
            usuario.Departamentos = departamentos.ToList();

            return usuario;
        }

        public Task<IEnumerable<Usuario>> StoredProcedureObterTodos()
        {
            return _connection.QueryAsync<Usuario>
                ("SelecionarUsuarios", commandType: CommandType.StoredProcedure);
        }

        public async Task<Usuario> StoredProcedureObterPorIdAsync(int id)
        {
            return await _connection.QueryFirstOrDefaultAsync<Usuario>
                ("SelecionarUsuario", new { Id = id }, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<UsuarioCamposDiferentes>> MapperUsandoSqlAsync()
        {
            return await _connection.QueryAsync<UsuarioCamposDiferentes>(DicasQueries.SelectUsuarioComAlias);
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}