using FutureTech.Application;
using FutureTech.Infrastructure;
using FutureTech.Infrastructure.Data;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info = new()
        {
            Title = "FutureTech Ticket Management API",
            Version = "v1",
            Description = "A Clean Architecture ASP.NET Core Web API for Ticket/Case Management"
        };
        return Task.CompletedTask;
    });
});

// Add Application and Infrastructure layers
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Seed database in Development environment
if (app.Environment.IsDevelopment())
{
    await ApplicationDbContextSeed.SeedAsync(app.Services);
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
