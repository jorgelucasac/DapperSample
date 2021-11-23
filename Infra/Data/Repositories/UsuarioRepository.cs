using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Estudos.Dapper.Api.Business.Interfaces.Repositories;
using Estudos.Dapper.Api.Business.Models;
using Estudos.Dapper.Api.Infra.Data.Repositories.Queries;
using Microsoft.Extensions.Configuration;

namespace Estudos.Dapper.Api.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbConnection _connection;
        public UsuarioRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("SqlConnection"));
        }

        public async Task<List<Usuario>> ObterTodosAsync()
        {
            var lista = await _connection.QueryAsync<Usuario>(UsuarioQueries.ObterTodos);
            return lista.ToList();
        }

        public async Task<Usuario> ObterPorIdAsync(int id)
        {
            //o ultimo item é o tipo de retorno
            var lista = await _connection.QueryAsync<Usuario, Contato, Usuario>(
                 UsuarioQueries.ObterPorId,
                 //mapeia como os dados devem ser retornados
                 (usuario, contato) =>
                 {
                     usuario.Contato = contato;
                     return usuario;
                 },
                 new { id },
                 splitOn: "id"
                 );
            return lista.FirstOrDefault();
        }

        public async Task<int> AdicionarAsync(Usuario usuario)
        {
            _connection.Open();
            using var transaction = _connection.BeginTransaction();

            try
            {
                usuario.Id = await _connection.QuerySingleAsync<int>
                    (UsuarioQueries.AdicionarUsuario, usuario, transaction);

                if (usuario.Contato is not null)
                {
                    usuario.Contato.UsuarioId = usuario.Id;
                    usuario.Contato.Id = await _connection.QuerySingleAsync<int>
                        (UsuarioQueries.AdicionarContato, usuario.Contato, transaction);
                }

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
            finally
            {
                _connection.Close();
            }

            return usuario.Id;
        }

        public async Task<bool> AtualizarAsync(Usuario usuario)
        {
            return (await _connection.ExecuteAsync(UsuarioQueries.AtualizarUsuario, usuario) > 0);
        }

        public async Task<bool> RemoverAsync(int id)
        {
            return await _connection.ExecuteAsync(UsuarioQueries.RemoverUsuario, new { Id = id }) > 0;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}