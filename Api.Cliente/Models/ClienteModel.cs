using System.ComponentModel.DataAnnotations;

namespace Api.Cliente.Models
{
    public class ClienteModel
    {

        [Required(ErrorMessage = "O campo CPF é obrigatório")]
        public string? Cpf { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo Sobrnome é obrigatório")]
        public string? Sobrenome { get; set; }
        public DateTime DataDeNascimento { get; set; }
    }
}