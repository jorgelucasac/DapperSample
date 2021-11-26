using Dapper.Contrib.Extensions;

namespace Estudos.Dapper.Api.Business.Models
{
    public abstract class Entidade
    {
        [Key]
        public int Id { get; set; }
    }
}