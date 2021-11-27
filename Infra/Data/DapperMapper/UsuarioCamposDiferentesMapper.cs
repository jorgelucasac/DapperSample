using Dapper.FluentMap.Mapping;
using Estudos.Dapper.Api.Business.Models;

namespace Estudos.Dapper.Api.Infra.Data.DapperMapper
{
    public class UsuarioCamposDiferentesMapper : EntityMap<UsuarioCamposDiferentes>
    {
        public UsuarioCamposDiferentesMapper()
        {
            Map(p => p.Cod).ToColumn("Id");
            Map(p => p.NomeCompleto).ToColumn("Nome");
            Map(p => p.NomeCompletoMae).ToColumn("NomeMae");
            Map(p => p.Situacao).ToColumn("SituacaoCadastro");
        }
    }
}