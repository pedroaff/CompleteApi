using Pedro.Business.Intefaces;
using Pedro.Data.Context;
using Pedro.Data.Repository;

namespace Pedro.App.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        services.AddScoped<MeuDbContext>();
        services.AddScoped<IFornecedorRepository, FornecedorRepository>();


        return services;
    }
}
