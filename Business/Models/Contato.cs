namespace Estudos.Dapper.Api.Business.Models
{
    public class Contato : Entidade
    {
        public int UsuarioId { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }

        public Usuario Usuario { get; set; }
    }
}