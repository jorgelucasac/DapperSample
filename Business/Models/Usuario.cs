using System;
using System.Collections.Generic;

namespace Estudos.Dapper.Api.Business.Models
{
    public class Usuario : Entidade
    {
        public Usuario()
        {
            DataCadastro = DateTimeOffset.Now;
            EnderecosEntrega = new List<EnderecoEntrega>();
            Departamentos = new List<Departamento>();
        }

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Sexo { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string NomeMae { get; set; }
        public string SituacaoCadastro { get; set; }

        // usado offSet para trabalhar com fuso horário
        public DateTimeOffset DataCadastro { get; set; }

        public Contato Contato { get; set; }
        public ICollection<EnderecoEntrega> EnderecosEntrega { get; set; }
        public ICollection<Departamento> Departamentos { get; set; }
    }
}