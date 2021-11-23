﻿using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
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

        public async Task<List<Usuario>> ObterTodosAsync()
        {
            var lista = await _connection.QueryAsync<Usuario>("select * from usuarios");
            return lista.ToList();
        }

        public async Task<Usuario> ObterPorIdAsync(int id)
        {
            return await _connection
                .QuerySingleOrDefaultAsync<Usuario>("select * from usuarios where id = @id", new
                {
                    id
                });
        }

        public async Task<int> Adicionar(Usuario usuario)
        {
            const string sql = @"INSERT INTO Usuarios
           (Nome
           ,Email
           ,Sexo
           ,RG
           ,CPF
           ,NomeMae
           ,SituacaoCadastro
           ,DataCadastro)
     VALUES
           (@Nome
           ,@Email
           ,@Sexo
           ,@RG
           ,@CPF
           ,@NomeMae
           ,@SituacaoCadastro
           ,@DataCadastro); select cast(SCOPE_IDENTITY() as int)";

            return  await _connection.QuerySingleAsync<int>(sql, usuario);
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}