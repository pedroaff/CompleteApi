﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Pedro.Business.Intefaces;
using Pedro.Business.Models;
using Pedro.Business.Models.Validations;

namespace Pedro.Business.Services;

public class FornecedorService : BaseService, IFornecedorService
{
    private readonly IFornecedorRepository _fornecedorRepository;
    private readonly IEnderecoRepository _enderecoRepository;

    public FornecedorService(IFornecedorRepository fornecedorRepository, 
                             IEnderecoRepository enderecoRepository,
                             INotificador notificador) : base(notificador)
    {
        _fornecedorRepository = fornecedorRepository;
        _enderecoRepository = enderecoRepository;
    }

    public async Task<bool> Adicionar(Fornecedor fornecedor)
    {
        if (!ExecutarValidacao(new FornecedorValidation(), fornecedor) 
            || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)) return false;

        if (_fornecedorRepository.Buscar(f => f.Documento == fornecedor.Documento).Result.Any())
        {
            Notificar("Já existe um fornecedor com este documento infomado.");
            return false;
        }

        await _fornecedorRepository.Adicionar(fornecedor);
        return true;
    }

    public async Task Atualizar(Fornecedor fornecedor)
    {
        if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)) return;

        if (_fornecedorRepository.Buscar(f => f.Documento == fornecedor.Documento && f.Id != fornecedor.Id).Result.Any())
        {
            Notificar("Já existe um fornecedor com este documento infomado.");
            return;
        }

        await _fornecedorRepository.Atualizar(fornecedor);
    }

    public async Task AtualizarEndereco(Endereco endereco)
    {
        if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

        await _enderecoRepository.Atualizar(endereco);
    }

    public async Task Remover(Guid id)
    {
        if (_fornecedorRepository.ObterFornecedorProdutosEndereco(id).Result.Produtos.Any())
        {
            Notificar("O fornecedor possui produtos cadastrados!");
            return;
        }

        var endereco = await _enderecoRepository.ObterEnderecoPorFornecedor(id);

        if (endereco != null)
        {
            await _enderecoRepository.Remover(endereco.Id);
        }

        await _fornecedorRepository.Remover(id);
    }

    public void Dispose()
    {
        _fornecedorRepository?.Dispose();
        _enderecoRepository?.Dispose();
    }
}