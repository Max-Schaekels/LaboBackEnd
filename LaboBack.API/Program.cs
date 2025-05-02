using LaboBack.BLL.Interfaces;
using LaboBack.BLL.Services;
using LaboBack.DAL;
using LaboBack.DAL.Interfaces;
using LaboBack.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUtilisateurService, UtilisateurService>();
builder.Services.AddScoped<IUtilisateurRepository, UtilisateurRepository>();

builder.Services.AddScoped<IProduitService, ProduitService>();
builder.Services.AddScoped<IProduitRepository, ProduitRepository >();

builder.Services.AddScoped<ICommandeService, CommandeService>();
builder.Services.AddScoped<ICommandeRepository, CommandeRepository>();

builder.Services.AddScoped<Connection>(sp =>
    new Connection(builder.Configuration.GetConnectionString("Default"))
);

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
