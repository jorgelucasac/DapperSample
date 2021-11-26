using Estudos.Dapper.Api.Business.Models;
using System.Collections.Generic;
using System.Linq;

namespace Estudos.Dapper.Api.Extension
{
    public static class ListExtension
    {
        public static bool AdicionarSeNaoExiste<T>(this ICollection<T> list, T entidade) where T : Entidade
        {
            if (entidade is null || list.Any(a => a.Id == entidade.Id)) return false;

            list.Add(entidade);
            return true;
        }
    }
}