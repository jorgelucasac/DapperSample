using System.Collections.Generic;

namespace Estudos.Dapper.Api.Business.Models
{
    public class Departamento : Entidade
    {
        public string Nome { get; set; }

        public ICollection<Usuario> Usuarios { get; set; }
    }
}