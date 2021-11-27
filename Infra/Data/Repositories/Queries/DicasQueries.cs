namespace Estudos.Dapper.Api.Infra.Data.Repositories.Queries
{
    public class DicasQueries
    {
        public static string SelecionarTodosUsuarios => "SELECT * FROM Usuarios;";

        public static string SelecionarUsuarioPorId => "SELECT * FROM Usuarios WHERE Id = @Id;";

        public static string SelecionarContatosPorUsuarioId => "SELECT * FROM Contatos WHERE UsuarioId = @Id;";

        public static string SelecionarEnderecosEntregaPorUsuarioId => "SELECT * FROM EnderecosEntrega WHERE UsuarioId = @Id;";

        public static string SelecionarDepartamentosPorUsuarioId => @"SELECT D.* FROM UsuariosDepartamentos UD
        INNER JOIN Departamentos D ON UD.DepartamentoId = D.Id WHERE UD.UsuarioId = @Id;";

        public static string SelectUsuarioComAlias => @"SELECT
        Id Cod, Nome NomeCompleto, Email, Sexo, RG, CPF, NomeMae NomeCompletoMae, SituacaoCadastro Situacao, DataCadastro
        FROM Usuarios;";
    }
}