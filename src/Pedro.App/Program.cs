using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedro.App.Configuration;
using Pedro.Data.Context;

var builder = WebApplication.CreateBuilder(args);
{
    var connString = builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services.AddDbContext<MeuDbContext>(opts => opts.UseMySql(connString, ServerVersion.AutoDetect(connString)));
    builder.Services.ResolveDependencies();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddIdentityConfiguration(builder.Configuration, connString);
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.UseAuthentication();
    app.MapControllers();

    app.Run();
}