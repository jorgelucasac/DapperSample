using System.Collections.Generic;
using Estudos.Dapper.Api.Business.Models;

namespace Estudos.Dapper.Api.Business.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        public List<Usuario> ObterTodos();
        public Usuario ObterPorId(int id);
        public void Adicionar(Usuario usuario);
        public void Atualizar(Usuario usuario);
        public void Remover(int id);
    }
}