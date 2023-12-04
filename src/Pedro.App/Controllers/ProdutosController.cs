using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pedro.App.DTO;
using Pedro.Business.Intefaces;
using Pedro.Business.Models;
using System.Net;

namespace Pedro.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutosController : MainController
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IProdutoService _produtoService;
    private readonly IMapper _mapper;

    public ProdutosController(INotificador notificador,
                              IProdutoRepository produtoRepository,
                              IProdutoService produtoService,
                              IMapper mapper) : base(notificador)
    {
        _produtoRepository = produtoRepository;
        _produtoService = produtoService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<ProdutoDto>> GetAll()
    {
        return _mapper.Map<IEnumerable<ProdutoDto>>(await _produtoRepository.ObterProdutosFornecedores());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProdutoDto>> GetById(Guid id)
    {
        ProdutoDto produtoDto = await ObterProduto(id);

        if (produtoDto is null) return NotFound();

        return produtoDto; 
    }

    private async Task<ProdutoDto> ObterProduto(Guid id)
    {
        return _mapper.Map<ProdutoDto>(await _produtoRepository.ObterProdutoFornecedor(id));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProdutoDto>> Remove(Guid id)
    {
        ProdutoDto produtoDto = await ObterProduto(id);

        if (produtoDto is null) return NotFound();

        await _produtoService.Remover(id);

        return CustomResponse(produtoDto);
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoDto>> Create(ProdutoDto produtoDto)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        string imagemNome = Guid.NewGuid() + "_" + produtoDto.Imagem;

        if (!Upload(produtoDto.ImagemUpload, imagemNome)) return CustomResponse(produtoDto);

        await _produtoService.Adicionar(_mapper.Map<Produto>(produtoDto));

        return CustomResponse(produtoDto); 
    }

    private bool Upload(string arquivo, string imgNome)
    {
        var imageDataByteArray = Convert.FromBase64String(arquivo);

        if (string.IsNullOrEmpty(arquivo))
        {
            NotificarError("Forneça um arquivo de imagem para este produto");
            return false;
        }

        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgNome);

        if (System.IO.File.Exists(filePath))
        {
            NotificarError("Já existe um arquivo com esse nome");
            return false;
        } 

        System.IO.File.WriteAllBytes(filePath, imageDataByteArray);

        return true;
    }
}
