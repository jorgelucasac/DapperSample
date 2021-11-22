using System.Collections.Generic;

namespace Estudos.Dapper.Api.Business.Models
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public ICollection<Usuario> Usuarios { get; set; }
    }
}