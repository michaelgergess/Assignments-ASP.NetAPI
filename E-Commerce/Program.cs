     // Program.cs
using E_Commerce.Application.Contract;
using E_Commerce.Application.Service;
using E_Commerce.Context;
using E_Commerce.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
     
     builder.Services.AddControllers();
builder.Services.AddAuthentication(op =>
{
    op.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(op =>
{
    op.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["jwt:Issuer"],
        ValidAudience = builder.Configuration["jwt:Audiences"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:Key"]))

    };
});
builder.Services.AddCors(op =>
{
    op.AddPolicy("Default", policy =>
    {
        // policy.WithHeaders("auth").WithOrigins("http://localhost:4200").WithMethods("Post")
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
     builder.Services.AddSwaggerGen(option =>
     {
         option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
         {
             Name = "Authorization",
             Type = SecuritySchemeType.ApiKey,
             Scheme = "Bearer",
             BearerFormat = "JWT",
             In = ParameterLocation.Header,
             Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
         });
     
         option.AddSecurityRequirement(new OpenApiSecurityRequirement
                  {
                      {
                            new OpenApiSecurityScheme
                              {
                                  Reference = new OpenApiReference
                                  {
                                      Type = ReferenceType.SecurityScheme,
                                      Id = "Bearer"
                                  }
                              },
                              new string[] {}
                      }
                 });
     });
     builder.Services.AddDbContext<DB_Context>(options => {
         options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
             b => b.MigrationsAssembly("E-Commerce"));
     });
     
     builder.Services.AddScoped<IProductService, ProductService>();
     builder.Services.AddScoped<IProductRepository, ProductRepository>();
     builder.Services.AddScoped<IUserService, UserService>();
     builder.Services.AddScoped<IUserRepository, UserRepository>();
     // Use the interface here, not the concrete class
     builder.Services.AddScoped<IRoleService, RoleService>();
     builder.Services.AddScoped<IRoleRepository, RoleRepository>();
     builder.Services.AddScoped<IUserRoleService, UserRoleService>();
     builder.Services.AddScoped<IUserRoleRepository, RoleUserRepository>();
     
     builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
     
     var app = builder.Build();
     
     // Configure the HTTP request pipeline.
     if (app.Environment.IsDevelopment())
     {
         app.UseSwagger();
         app.UseSwaggerUI();
     }

app.UseCors("Default");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
     
     app.Run();
     