using System.Collections.Generic;
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

        public async Task<int> AdicionarAsync(Usuario usuario)
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

            return await _connection.QuerySingleAsync<int>(sql, usuario);
        }

        public async Task<bool> AtualizarAsync(Usuario usuario)
        {
            const string sql = "UPDATE Usuarios SET Nome = @Nome, Email = @Email, Sexo = @Sexo, RG = @RG, CPF = @CPF, NomeMae = @NomeMae, SituacaoCadastro = @SituacaoCadastro, DataCadastro = @DataCadastro WHERE Id = @Id";
            return (await _connection.ExecuteAsync(sql, usuario) > 0);
        }

        public async Task<bool> RemoverAsync(int id)
        {
            const string sql = "DELETE FROM Usuarios WHERE Id = @Id";
            return await _connection.ExecuteAsync(sql, new { Id = id }) > 0;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}