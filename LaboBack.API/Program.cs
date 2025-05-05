using LaboBack.BLL.Interfaces;
using LaboBack.BLL.Services;
using LaboBack.DAL;
using LaboBack.DAL.Interfaces;
using LaboBack.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LaboBack.API.Tools;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Définir les information générales de notre API dans swagger
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "BookManager",
        Version = "v1"
    });

    // Déclarer une schema de sécurité de type Bearer pour Swagger
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Token bearer utilise le scheama (Bearer {token})",
        Name = "Authorization", // Nom de l'en-tête HTTP
        In = Microsoft.OpenApi.Models.ParameterLocation.Header, // Indique que l'info est envoyé dans le header HTTP
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey, // Déclare une clé API de type Bearer
        Scheme = "Bearer" // Nom du schéma utilisé
    });

    // Ajoute une exigence de sécurité globale pour toutes les routes prottégés par [Authorize]
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2", // Nécéssaire pour swagger (interface)
                Name = "Bearer",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            },
            new List<string>() // Liste vide => Pas de scopes spécifique nécéssaires...
        }
    });
});

builder.Services.AddScoped<IUtilisateurService, UtilisateurService>();
builder.Services.AddScoped<IUtilisateurRepository, UtilisateurRepository>();

builder.Services.AddScoped<IProduitService, ProduitService>();
builder.Services.AddScoped<IProduitRepository, ProduitRepository >();

builder.Services.AddScoped<ICommandeService, CommandeService>();
builder.Services.AddScoped<ICommandeRepository, CommandeRepository>();

builder.Services.AddScoped<Connection>(sp =>
    new Connection(builder.Configuration.GetConnectionString("Default"))
);

builder.Services.AddSingleton<TokenManager>();

// Configuration de l'authentification JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["jwt:issuer"],
            ValidAudience = builder.Configuration["jwt:audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"]))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
