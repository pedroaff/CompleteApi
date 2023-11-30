using AutoMapper;
using Pedro.App.DTO;
using Pedro.Business.Models;

namespace Pedro.App.Configuration;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<Fornecedor, FornecedorDto>().ReverseMap();
        CreateMap<Endereco, EnderecoDto>().ReverseMap();
        CreateMap<Produto, ProdutoDto>().ReverseMap();
    }
}
