using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Estudos.Dapper.Api.Business.Interfaces.Repositories;
using Estudos.Dapper.Api.Business.Models;
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

        public List<Usuario> ObterTodos()
        {
            throw new System.NotImplementedException();
        }

        public Usuario ObterPorId(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Adicionar(Usuario usuario)
        {
            throw new System.NotImplementedException();
        }

        public void Atualizar(Usuario usuario)
        {
            throw new System.NotImplementedException();
        }

        public void Remover(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}