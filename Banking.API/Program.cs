using Banking.API.Filter;
using Banking.Application;
using Banking.Infrastructure;
using Banking.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastrucutre(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
