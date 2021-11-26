namespace Estudos.Dapper.Api.Infra.Data.Repositories.Queries
{
    public static class UsuarioQueries
    {
        #region Select

        public static string ObterTodos => "select * from usuarios";

        public static string ObterTodosCompletos => @"SELECT U.*, C.*, EE.*, D.* FROM Usuarios as U
        LEFT JOIN Contatos C ON C.UsuarioId = U.Id
        LEFT JOIN EnderecosEntrega EE ON EE.UsuarioId = U.Id
        LEFT JOIN UsuariosDepartamentos UD ON UD.UsuarioId = U.Id
        LEFT JOIN Departamentos D ON UD.DepartamentoId = D.Id";

        public static string ObterPorId => @"SELECT U.*, C.*, EE.*, D.* FROM Usuarios as U
        LEFT JOIN Contatos C ON C.UsuarioId = U.Id
        LEFT JOIN EnderecosEntrega EE ON EE.UsuarioId = U.Id
        LEFT JOIN UsuariosDepartamentos UD ON UD.UsuarioId = U.Id
        LEFT JOIN Departamentos D ON UD.DepartamentoId = D.Id
        WHERE U.Id = @Id";

        #endregion Select

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

        #endregion Insert

        #region Update

        public static string AtualizarUsuario => @"UPDATE Usuarios SET
        Nome = @Nome, Email = @Email, Sexo = @Sexo, RG = @RG, CPF = @CPF, NomeMae = @NomeMae,
        SituacaoCadastro = @SituacaoCadastro, DataCadastro = @DataCadastro WHERE Id = @Id";

        public static string AtualizarContato => @"UPDATE Contatos SET
            Telefone = @Telefone, Celular = @Celular WHERE Id = @Id";

        #endregion Update

        #region Delete

        public static string RemoverUsuario => "DELETE FROM Usuarios WHERE Id = @Id";
        public static string RemoverEnderecosEntrega = "DELETE FROM EnderecosEntrega WHERE UsuarioId = @Id";

        #endregion Delete
    }
}