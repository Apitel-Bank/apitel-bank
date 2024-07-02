using BankPartnerService.Repositories;
using BankPartnerService.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load();

// Add repository services to the container.
builder.Services.AddSingleton<Db>();
builder.Services.AddSingleton<CustomersRepository>();
builder.Services.AddSingleton<AccountsRepository>();
builder.Services.AddSingleton<BanksRepository>();
builder.Services.AddSingleton<DebitOrdersRespository>();
builder.Services.AddSingleton<ExternalAccountsRepository>();
builder.Services.AddSingleton<TransactionsRepository>();
builder.Services.AddSingleton<AccountTransactionStatusesRepository>();

// Add services to the container
builder.Services.AddSingleton<CustomersService>();
builder.Services.AddSingleton<BanksService>();
builder.Services.AddSingleton<DebitOrdersService>();
builder.Services.AddSingleton<TransactionsService>();
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

// Add controllers to the container
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Apitel Bank",
        Version = "v1",
        Description = "Partner API for Apitel Bank. Other banks, businesses, and personas."
    });

    // Enable XML comments if available
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();
