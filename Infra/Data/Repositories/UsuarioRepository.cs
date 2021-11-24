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

        public async Task<List<Usuario>> ObterTodosCompletoAsync()
        {
            var usuarios = new Dictionary<int, Usuario>();
            await _connection.QueryAsync<Usuario, Contato, EnderecoEntrega, Usuario>(
                UsuarioQueries.ObterTodosCompletos,
                //executa uma vez para cada linha retornada
                (usuario, contato, enderecoEntrega) =>
                    MapearUsuarios(usuario, contato, enderecoEntrega, ref usuarios));

            return usuarios.Values.ToList();
        }

        public async Task<Usuario> ObterPorIdAsync(int id)
        {
            var usuarios = new Dictionary<int, Usuario>();
            //o ultimo item é o tipo de retorno
            await _connection.QueryAsync<Usuario, Contato, EnderecoEntrega, Usuario>(
                UsuarioQueries.ObterPorId,
                //mapeia como os dados devem ser retornados
                (usuario, contato, enderecoEntrega) =>
                    MapearUsuarios(usuario, contato, enderecoEntrega, ref usuarios),
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

                if (usuario.EnderecosEntrega != null && usuario.EnderecosEntrega.Any())
                {
                    foreach (var enderecoEntrega in usuario.EnderecosEntrega)
                    {
                        enderecoEntrega.UsuarioId = usuario.Id;
                        enderecoEntrega.Id = await _connection.QuerySingleAsync<int>
                            (UsuarioQueries.AdicionarEnderecoEntrega, enderecoEntrega, transaction);
                    }
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
            _connection.Open();
            using var transaction = _connection.BeginTransaction();
            try
            {
                await _connection.ExecuteAsync(UsuarioQueries.AtualizarUsuario, usuario, transaction);
                if (usuario.Contato is not null)
                    await _connection.ExecuteAsync(UsuarioQueries.AtualizarContato, usuario.Contato, transaction);

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

        private Usuario MapearUsuarios(Usuario usuario, Contato contato, EnderecoEntrega enderecoEntrega, ref Dictionary<int, Usuario> usuarios)
        {
            var existe = usuarios.ContainsKey(usuario.Id);
            if (existe && enderecoEntrega is null) return null;

            if (existe)
            {
                usuarios[usuario.Id].EnderecosEntrega.Add(enderecoEntrega);
            }
            else
            {
                if (enderecoEntrega is not null)
                    usuario.EnderecosEntrega.Add(enderecoEntrega);
                usuario.Contato = contato;
                usuarios.Add(usuario.Id, usuario);
            }

            return usuario;
        }
    }
}