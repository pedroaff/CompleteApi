using Pedro.Business.Models;
using System.ComponentModel.DataAnnotations;

namespace Pedro.App.DTO;

public class EnderecoDto
{
    [Key]
    public Guid Id { get; set; }
    [Required(ErrorMessage = "O Campo {0} é obrigatório")]
    public string Logradouro { get; set; }
    [Required(ErrorMessage = "O Campo {0} é obrigatório")]
    public string Numero { get; set; }
    public string Complemento { get; set; }
    [Required(ErrorMessage = "O Campo {0} é obrigatório")]
    public string Cep { get; set; }
    [Required(ErrorMessage = "O Campo {0} é obrigatório")]
    public string Bairro { get; set; }
    [Required(ErrorMessage = "O Campo {0} é obrigatório")]
    public string Cidade { get; set; }
    [Required(ErrorMessage = "O Campo {0} é obrigatório")]
    public string Estado { get; set; }
    //public Fornecedor Fornecedor { get; set; }
}