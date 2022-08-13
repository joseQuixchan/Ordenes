using BerakahOrdenes.BerakahMapper;
using BerakahOrdenes.Datos;
using BerakahOrdenes.Repository;
using BerakahOrdenes.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Description = "Autenticacion JWT",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer"
        });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Id = "Bearer",
                Type = ReferenceType.SecurityScheme
            }
        }, new List<string>()
    }
    });
});


//Conexion a base de datos
builder.Services.AddDbContext<DBOrdenes>(Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionDB")));

//Modelos
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddAutoMapper(typeof(BerakahMapper));

//Agregar dependencia del token
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
