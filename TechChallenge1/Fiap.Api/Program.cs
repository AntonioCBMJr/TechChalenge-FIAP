using Fiap.Api.Interfaces;
using Fiap.Domain.Repositories;
using Fiap.Infra.Context;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
//ConfigureMvc(builder);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigureServices(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();



//void ConfigureMvc(WebApplicationBuilder builder)
//{
//    builder.Services.AddMemoryCache();
//    builder.Services.AddResponseCompression(options =>
//    {
//        // options.Providers.Add<BrotliCompressionProvider>();
//        options.Providers.Add<GzipCompressionProvider>();
//        // options.Providers.Add<CustomCompressionProvider>();
//    });
//    builder.Services.Configure<GzipCompressionProviderOptions>(options =>
//    {
//        options.Level = CompressionLevel.Optimal;
//    });
//    builder
//        .Services
//        .AddControllers()
//        .ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; })
//        .AddJsonOptions(x =>
//        {
//            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
//            x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
//        });
//}

void ConfigureServices(WebApplicationBuilder builder)
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<FiapDataContext>(options => options.UseNpgsql(connectionString));

    builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
}
