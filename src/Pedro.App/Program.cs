using Microsoft.EntityFrameworkCore;
using Pedro.Data.Context;

var builder = WebApplication.CreateBuilder(args);
{
    var connString = builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services.AddDbContext<MeuDbContext>(opts => opts.UseMySql(connString, ServerVersion.AutoDetect(connString)));

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
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
    app.MapControllers();

    app.Run();
}