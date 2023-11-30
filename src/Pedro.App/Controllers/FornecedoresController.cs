using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pedro.App.DTO;
using Pedro.Business.Intefaces;
using Pedro.Business.Models;

namespace Pedro.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FornecedoresController : MainController
{
    private readonly IFornecedorRepository _fornecedorRepository;
    private readonly IMapper _mapper;
    private readonly IFornecedorService _fornecedorService;

    public FornecedoresController(IFornecedorRepository fornecedorRepository, IMapper mapper, IFornecedorService fornecedorService)
    {
        _fornecedorRepository = fornecedorRepository;
        _mapper = mapper;
        _fornecedorService = fornecedorService;
    }

    public async Task<IEnumerable<FornecedorDto>> GetAll()
    {
        IEnumerable<FornecedorDto> fornecedores = _mapper.Map<IEnumerable<FornecedorDto>>(await _fornecedorRepository.ObterTodos());

        return fornecedores;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<FornecedorDto>> GetById(Guid id)
    {
        FornecedorDto? fornecedor = await GetFornecedorProductsAddress(id);

        if (fornecedor is null) return NotFound();

        return fornecedor;
    }

    [HttpPost]
    public async Task<ActionResult<FornecedorDto>> Create(FornecedorDto fornecedorDto)
    {
        if (!ModelState.IsValid) return BadRequest();

        Fornecedor fornecedor = _mapper.Map<Fornecedor>(fornecedorDto);
        bool result = await _fornecedorService.Adicionar(fornecedor);

        if (!result) return BadRequest();

        return Ok(fornecedor);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<FornecedorDto>> Update(Guid id, FornecedorDto fornecedorDto)
    {
        if (id != fornecedorDto.Id) return BadRequest();

        if (!ModelState.IsValid) return BadRequest();

        Fornecedor fornecedor = _mapper.Map<Fornecedor>(fornecedorDto);

        bool result = await _fornecedorService.Atualizar(fornecedor);

        if (!result) return BadRequest();

        return Ok(fornecedor);
    }

    public async Task<FornecedorDto> GetFornecedorProductsAddress(Guid id)
    {
        return _mapper.Map<FornecedorDto>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
    }
}
