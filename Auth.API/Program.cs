using Auth.Business.Services.Abstract;
using Auth.Business.Services.Concrete;
using Auth.Domain.Configurations;
using Auth.Infra.DbContexts;
using Auth.Infra.Repositories.Abstract;
using Auth.Infra.Repositories.Concrete;
using Auth.Infra.UnitOfWork.Abstract;
using Auth.Infra.UnitOfWork.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<authdbContext>(options =>
                options.UseSqlServer(builder.Configuration["ConnectionStrings:authdb"]));

builder.Services.AddScoped<IEncryptService, EncryptService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ISendEmailService, SendEmailService>();
builder.Services.AddScoped<IPasswordRecoveryService, PasswordRecoveryService>();
builder.Services.AddScoped<IKeyCodeService, KeyCodeService>();

builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IEmailSentRepository, EmailSentRepository>();
builder.Services.AddTransient<IEmailTemplateRepository, EmailTemplateRepository>();
builder.Services.AddTransient<IEmailTypeRepository, EmailTypeRepository>();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

var key = Encoding.ASCII.GetBytes(builder.Configuration["SecretKey"]);
builder.Services.AddAuthentication(x => 
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x => {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        { 
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };    
    });

builder.Services.AddSingleton<BaseConfigurations>(op => 
{
    var obj = new BaseConfigurations
    {
        ConnectionString_authdb = builder.Configuration["ConnectionStrings:authdb"],
        SecretKey = builder.Configuration["SecretKey"]
    };

    return obj;
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