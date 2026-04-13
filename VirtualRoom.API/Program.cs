using Modules.Identity.Application.ApplicationDI;
using Modules.Identity.Infrastructure.DI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddIdentityModule(builder.Configuration);
builder.Services.AddIdentityApplication();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
