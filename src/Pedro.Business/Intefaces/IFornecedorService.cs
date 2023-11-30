using System;
using System.Threading.Tasks;
using Pedro.Business.Models;

namespace Pedro.Business.Intefaces;

public interface IFornecedorService : IDisposable
{
    Task<bool> Adicionar(Fornecedor fornecedor);
    Task<bool> Atualizar(Fornecedor fornecedor);
    Task<bool> Remover(Guid id);

    Task AtualizarEndereco(Endereco endereco);
}