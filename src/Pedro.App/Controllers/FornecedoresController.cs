using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pedro.App.DTO;
using Pedro.Business.Intefaces;

namespace Pedro.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FornecedoresController : MainController
{
    private readonly IFornecedorRepository _fornecedorRepository;
    private readonly IMapper _mapper;
    public FornecedoresController(IFornecedorRepository fornecedorRepository, IMapper mapper)
    {
        _fornecedorRepository = fornecedorRepository;
        _mapper = mapper;
    }

    public async Task<ActionResult<IEnumerable<FornecedorDto>>> GetAll()
    {
        IEnumerable<FornecedorDto> fornecedores = _mapper.Map<IEnumerable<FornecedorDto>>(await _fornecedorRepository.ObterTodos());

        return Ok(fornecedores);
    }
}
