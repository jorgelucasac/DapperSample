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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbConnection _connection;

        public UsuarioRepository(IOptions<SqlConnectionExtension> options)
        {
            _connection = new SqlConnection(options.Value.SqlConnectionString);
        }

        public async Task<List<Usuario>> ObterTodosAsync()
        {
            var lista = await _connection.QueryAsync<Usuario>(UsuarioQueries.ObterTodos);
            return lista.ToList();
        }

        public async Task<List<Usuario>> ObterTodosCompletoAsync()
        {
            var usuarios = new Dictionary<int, Usuario>();
            await _connection.QueryAsync<Usuario, Contato, EnderecoEntrega, Departamento, Usuario>(
                UsuarioQueries.ObterTodosCompletos,
                //executa uma vez para cada linha retornada
                (usuario, contato, enderecoEntrega, departamento) =>
                    MapearUsuarios(usuario, contato, enderecoEntrega, departamento, ref usuarios));

            return usuarios.Values.ToList();
        }

        public async Task<Usuario> ObterPorIdAsync(int id)
        {
            var usuarios = new Dictionary<int, Usuario>();
            //o ultimo item é o tipo de retorno
            await _connection.QueryAsync<Usuario, Contato, EnderecoEntrega, Departamento, Usuario>(
                UsuarioQueries.ObterPorId,
                //mapeia como os dados devem ser retornados
                (usuario, contato, enderecoEntrega, departamento) =>
                    MapearUsuarios(usuario, contato, enderecoEntrega, departamento, ref usuarios),
                new { id },
                splitOn: "id"
            );
            return usuarios.Values.FirstOrDefault();
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

                await AdicionarEnderecoEntregaAsync(usuario.EnderecosEntrega, usuario.Id, transaction);
                await AdicionarDepartamentosAsync(usuario.Departamentos, usuario.Id, transaction);

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
            _connection.Open();
            using var transaction = _connection.BeginTransaction();
            try
            {
                await _connection.ExecuteAsync(UsuarioQueries.AtualizarUsuario, usuario, transaction);
                if (usuario.Contato is not null)
                    await _connection.ExecuteAsync(UsuarioQueries.AtualizarContato, usuario.Contato, transaction);

                await _connection.ExecuteAsync(UsuarioQueries.RemoverEnderecosEntrega, usuario, transaction);
                AdicionarEnderecoEntregaAsync(usuario.EnderecosEntrega, usuario.Id, transaction);

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                return false;
            }
            finally
            {
                _connection.Close();
            }

            return true;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            return await _connection.ExecuteAsync(UsuarioQueries.RemoverUsuario, new { Id = id }) > 0;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        private Usuario MapearUsuarios(Usuario usuario, Contato contato, EnderecoEntrega enderecoEntrega, Departamento departamento, ref Dictionary<int, Usuario> usuarios)
        {
            if (usuarios.ContainsKey(usuario.Id))
            {
                usuario = usuarios[usuario.Id];
            }
            else
            {
                usuario.Contato = contato;
                usuarios.Add(usuario.Id, usuario);
            }

            usuario.EnderecosEntrega.AdicionarSeNaoExiste(enderecoEntrega);
            usuario.Departamentos.AdicionarSeNaoExiste(departamento);

            return usuario;
        }

        private async Task AdicionarEnderecoEntregaAsync(ICollection<EnderecoEntrega> enderecosEntrega, int usuarioId, IDbTransaction transaction)
        {
            if (enderecosEntrega is null || enderecosEntrega.Count == 0) return;

            foreach (var enderecoEntrega in enderecosEntrega)
            {
                enderecoEntrega.UsuarioId = usuarioId;
                enderecoEntrega.Id = await _connection.QuerySingleAsync<int>
                    (UsuarioQueries.AdicionarEnderecoEntrega, enderecoEntrega, transaction);
            }
        }

        private async Task AdicionarDepartamentosAsync(ICollection<Departamento> departamentos, int usuarioId, IDbTransaction transaction)
        {
            if (departamentos is null && departamentos.Count == 0) return;

            foreach (var departamento in departamentos)
            {
                await _connection.ExecuteAsync(UsuarioQueries.AdicionarDepartamentos, new { UsuarioId = usuarioId, DepartamentoId = departamento.Id }, transaction);
            }
        }
    }
}