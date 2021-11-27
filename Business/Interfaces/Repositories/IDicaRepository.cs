using Estudos.Dapper.Api.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Estudos.Dapper.Api.Business.Interfaces.Repositories
{
    public interface IDicaRepository : IDisposable
    {
        public Task<Usuario> ObterPorIdAsync(int id);

        public Task<IEnumerable<Usuario>> StoredProcedureObterTodos();

        public Task<Usuario> StoredProcedureObterPorIdAsync(int id);

        /// <summary>
        /// Problema: Mapear colunas com nomes diferentes das propriedades do objeto.
        /// Solução 01: SQL(ALIAS) Renomear a coluna.
        /// </summary>
        /// <returns><see cref="IList{UsuarioCamposDiferentes}"/></returns>
        public Task<IEnumerable<UsuarioCamposDiferentes>> MapperUsandoSqlAsync();

        /// <summary>
        /// Problema: Mapear colunas com nomes diferentes das propriedades do objeto.
        /// Solução 01: SQL(ALIAS) Renomear a coluna.
        /// </summary>
        /// <returns><see cref="IList{UsuarioCamposDiferentes}"/></returns>
        public Task<IEnumerable<UsuarioCamposDiferentes>> MapperUsandoConfigAsync();
    }
}