using Microsoft.EntityFrameworkCore;
using Serilog;
using alwaysinformed_dal.Data;
using alwaysinformed_bll.Services;
using alwaysinformed_dal.Interfaces;


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
               builder => builder.WithOrigins("https://localhost:7115")
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


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowLocalhost");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
