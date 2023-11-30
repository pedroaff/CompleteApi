using System;
using System.Threading.Tasks;
using Pedro.Business.Models;

namespace Pedro.Business.Intefaces;

public interface IFornecedorService : IDisposable
{
    Task<bool> Adicionar(Fornecedor fornecedor);
    Task Atualizar(Fornecedor fornecedor);
    Task Remover(Guid id);

    Task AtualizarEndereco(Endereco endereco);
}