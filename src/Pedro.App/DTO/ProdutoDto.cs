﻿using Pedro.Business.Models;
using System.ComponentModel.DataAnnotations;

namespace Pedro.App.DTO;

public class ProdutoDto
{
    [Key]
    public Guid Id { get; set; }
    public Guid FornecedorId { get; set; }
    [Required(ErrorMessage = "O Campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e (1} caracteres", MinimumLength = 2)]
    public string Nome { get; set; }
    [Required(ErrorMessage = "O Campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e (1} caracteres", MinimumLength = 2)]
    public string Descricao { get; set; }
    public string Imagem { get; set; }
    public string ImagemUpload { get; set; }
    [Required(ErrorMessage = "O Campo {0} é obrigatório")]
    public decimal Valor { get; set; }
    [ScaffoldColumn(false)]
    public DateTime DataCadastro { get; set; }
    public bool Ativo { get; set; }
    [ScaffoldColumn(false)]
    public string NomeFornecedor { get; set; }
}