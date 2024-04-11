using Explorations.Pipelines.Api.ConfigOptions;
using Explorations.Pipelines.Api.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.ConfigureOptions<RouteConfigOptions>();

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("Database"));

builder.Services.AddDbContext<OrdersDbContext>((sp, b) =>
{
    var settings = sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
    
    b.UseSqlServer(
        $"Server={settings.Server};Initial Catalog={settings.Name};Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=Active Directory Default;",
        c => c.EnableRetryOnFailure().UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
