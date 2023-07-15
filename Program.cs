using Aula2.Domain.Interfaces;
using Aula2.Infra.DelegateHandlers;
using Aula2.Infra.Repository;
using Aula2.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region injecao de dependencias

builder.Services.AddTransient<MercadoLivreAccessTokenHandler>();

builder.Services.AddTransient<OrdersRepository>();
builder.Services.AddTransient<OrderService>();
builder.Services.AddTransient<IMercadoLivreApiService, MercadoLivreApiService>();

builder.Services.AddHttpClient<IMercadoLivreApiService, MercadoLivreApiService>(httpClient =>
{
    httpClient.BaseAddress = new Uri("https://api.mercadolibre.com/");
}).AddHttpMessageHandler<MercadoLivreAccessTokenHandler>();


#endregion
//builder.Services.AddDbContext<IntegrationMercadoLivreContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("IntegrationMercadoLivre"));
//});


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
