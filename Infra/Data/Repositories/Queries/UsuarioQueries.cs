namespace Estudos.Dapper.Api.Infra.Data.Repositories.Queries
{
    public static class UsuarioQueries
    {
        #region Select

        public static string ObterTodos => "select * from usuarios";
        public static string ObterTodosCompletos => @"select * from Usuarios u
        left join Contatos c on c.UsuarioId = u.Id
        left join EnderecosEntrega ee on ee.UsuarioId = u.Id";
        public static string ObterPorId => @"SELECT *
                FROM Usuarios
                LEFT JOIN Contatos ON Contatos.UsuarioId = Usuarios.Id
                LEFT JOIN EnderecosEntrega ee on ee.UsuarioId = Usuarios.Id
                WHERE Usuarios.Id = @id";
        
        #endregion

        #region Insert

        public static string AdicionarUsuario => @"INSERT INTO Usuarios
        (Nome, Email, Sexo, RG, CPF, NomeMae, SituacaoCadastro, DataCadastro)
        VALUES (@Nome, @Email, @Sexo, @RG, @CPF, @NomeMae, @SituacaoCadastro, @DataCadastro);
        SELECT CAST(SCOPE_IDENTITY() AS INT);";
        public static string AdicionarContato => @"INSERT INTO Contatos
        (UsuarioId, Telefone, Celular) 
        VALUES (@UsuarioId, @Telefone, @Celular);
        SELECT CAST(SCOPE_IDENTITY() AS INT);";

        public static string AdicionarEnderecoEntrega => @"INSERT INTO EnderecosEntrega
        (UsuarioId, NomeEndereco, CEP, Estado, Cidade, Bairro, Endereco, Numero, Complemento) 
        VALUES (@UsuarioId, @NomeEndereco, @CEP, @Estado, @Cidade, @Bairro, @Endereco, @Numero, @Complemento);
        SELECT CAST(SCOPE_IDENTITY() AS INT);";

        #endregion

        #region Update

        public static string AtualizarUsuario => @"UPDATE Usuarios SET 
        Nome = @Nome, Email = @Email, Sexo = @Sexo, RG = @RG, CPF = @CPF, NomeMae = @NomeMae,
        SituacaoCadastro = @SituacaoCadastro, DataCadastro = @DataCadastro WHERE Id = @Id";

        public static string AtualizarContato => @"UPDATE Contatos SET 
            Telefone = @Telefone, Celular = @Celular WHERE Id = @Id";

        #endregion

        #region Delete

        public static string RemoverUsuario => "DELETE FROM Usuarios WHERE Id = @Id";

        #endregion
    }
}