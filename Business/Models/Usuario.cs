using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace Estudos.Dapper.Api.Business.Models
{
    [Table("Usuarios")]
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

        [Write(false)]
        public Contato Contato { get; set; }

        [Write(false)]
        public ICollection<EnderecoEntrega> EnderecosEntrega { get; set; }

        [Write(false)]
        public ICollection<Departamento> Departamentos { get; set; }
    }

    [Table("Usuarios")]
    public class UsuarioCamposDiferentes
    {
        [Key]
        public int Cod { get; set; }

        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Sexo { get; set; }
        public string RG { get; set; }
        public string CPF { get; set; }
        public string NomeCompletoMae { get; set; }
        public string Situacao { get; set; }
        public DateTimeOffset DataCadastro { get; set; }

        [Write(false)]
        public Contato Contato { get; set; }

        [Write(false)]
        public ICollection<EnderecoEntrega> EnderecosEntrega { get; set; }

        [Write(false)]
        public ICollection<Departamento> Departamentos { get; set; }
    }
}