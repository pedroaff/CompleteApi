using System;
using System.Threading.Tasks;
using Pedro.Business.Intefaces;
using Pedro.Business.Models;
using Pedro.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Pedro.Data.Repository;

public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
{
    public EnderecoRepository(MeuDbContext context) : base(context) { }

    public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
    {
        return await Db.Enderecos.AsNoTracking()
            .FirstOrDefaultAsync(f => f.FornecedorId == fornecedorId);
    }
}