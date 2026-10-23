using Microsoft.EntityFrameworkCore;
using PorcelainStore.Contexts;
using PorcelainStore.Repositories;
using PorcelainStore.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(op => op.UseSqlite(builder.Configuration.GetConnectionString("def")));

builder.Services.AddTransient<ProductRepository>();
builder.Services.AddTransient<CustomerRepository>();

builder.Services.AddTransient<ProductService>();
builder.Services.AddTransient<CustomerService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(o => o.SwaggerEndpoint("/openapi/v1.json", "PorcelainStore"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
