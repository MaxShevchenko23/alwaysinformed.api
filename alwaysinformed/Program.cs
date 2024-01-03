using Microsoft.EntityFrameworkCore;
using Serilog;
using alwaysinformed_dal.Data;
using alwaysinformed_bll.Services;
using alwaysinformed_dal.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Cors.Infrastructure;


// Add services to the container.
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/alwaysinformedlogs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
               builder => builder.WithOrigins("http://127.0.0.1:5500","test")
                                 .AllowAnyHeader()
                                 .AllowAnyMethod()
                                 .WithExposedHeaders("X-Pagination"));
});


builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ArticleService>();
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ArticleSandboxService>();
builder.Services.AddScoped<CommentService>();
builder.Services.AddScoped<FavoriteService>();
builder.Services.AddScoped<ArticleSandboxStatusService>();
builder.Services.AddScoped<UserRoleService>();


builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<AidbContext>(dbContextOptions => dbContextOptions.UseSqlServer("Server=DESKTOP-KKLFTJP;Database=aidb;Trusted_Connection=True;TrustServerCertificate=True"),ServiceLifetime.Transient);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
#if DEBUG
            ValidAudience = "alwaysinformed",
#else
            ValidAudience = builder.Configuration["Authentication:Audience"],
#endif
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:Secret"]))
        };

    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("forAdmin", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("userRole", "admin");
    });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
