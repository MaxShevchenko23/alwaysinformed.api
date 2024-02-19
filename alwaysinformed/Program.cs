using alwaysinformed.DbContexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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
builder.Services.AddScoped<ArticleStatisticService>();


builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<AidbContext>(dbContextOptions => dbContextOptions.UseSqlServer("Server=DESKTOP-KKLFTJP;Database=aidb;Trusted_Connection=True;TrustServerCertificate=True"),ServiceLifetime.Scoped);

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
    options.AddPolicy("forAuthor", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("userRole", "author");
    });
    options.AddPolicy("forUser(reader)", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("userRole", "reader");
    });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
