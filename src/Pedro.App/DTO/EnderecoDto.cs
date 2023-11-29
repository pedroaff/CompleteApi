using Pedro.Business.Models;
using System.ComponentModel.DataAnnotations;

namespace Pedro.App.DTO;

public class EnderecoDto
{
    [Key]
    public Guid FornecedorId { get; set; }
    [Required(ErrorMessage = "O Campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e (1} caracteres", MinimumLength = 2)]
    public string Logradouro { get; set; }
    [Required(ErrorMessage = "O Campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e (1} caracteres", MinimumLength = 2)]
    public string Numero { get; set; }
    public string Complemento { get; set; }
    [Required(ErrorMessage = "O Campo {0} é obrigatório")]
    [StringLength(8, ErrorMessage = "O campo {0} precisa ter entre {2} e (1} caracteres", MinimumLength = 2)]
    public string Cep { get; set; }
    [Required(ErrorMessage = "O Campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e (1} caracteres", MinimumLength = 2)]
    public string Bairro { get; set; }
    [Required(ErrorMessage = "O Campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e (1} caracteres", MinimumLength = 2)]
    public string Cidade { get; set; }
    [Required(ErrorMessage = "O Campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e (1} caracteres", MinimumLength = 2)]
    public string Estado { get; set; }

    /* EF Relation */
    public Fornecedor Fornecedor { get; set; }
}