using books_store_api.Settings;
using books_store_BLL.Dtos.Services;
using books_store_BLL.Settings;
using books_store_DAL;
using books_store_DAL.Entities.identity;
using books_store_DAL.Initializer;
using books_store_DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Add Repositories
builder.Services.AddScoped<AuthorRepository>();
builder.Services.AddScoped<GenreRepository>();
builder.Services.AddScoped<BookRepository>();

// Add services to the container.
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<GenreService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<JwtService>();

// Settings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));


// add automapper
builder.Services.AddAutoMapper(cfg => {
    cfg.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxODA1ODQ2NDAwIiwiaWF0IjoiMTc3NDM0NjQ0NyIsImFjY291bnRfaWQiOiIwMTlkMWY0OGUxODk3MmYzOGVjOWY1NDg5MzEwODYzMCIsImN1c3RvbWVyX2lkIjoiY3RtXzAxa21mbW1hdGdwZnlqaHB4ZGh2dHQzOHBqIiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.g7mA1v9jfZM85wj17IS36g5nf9lzCPbTrVNaouflfUUwgEjxPK0rGdFk8RQ-pOcmSGqmnwT0-iZo6-PeeSM7HhxSTLKA_Gtl4YummZcr88dIZHLfFDnd5KvuDAj9F9ryTz7IUDObMuhfdWHmPUtLe1gDrkqmxINUL0uv6UjtI10reZMAIirKlMbxW_W25zes5xgjC87Xim7_8nAu41qiGPPxApl-Uqx7IHxPucgXIrKckcjQYQN0MY-pAfNQSKWqm5l_zI_9BeLX_8-Izi-8QQuk3uo_j1WS1DjGVmRSFuPwxNSUMSsHzxTKAuq-UzVgsD-N_9sNI7WdGY2VTX7zow";
}, AppDomain.CurrentDomain.GetAssemblies());


// adding dbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("LocalDb")!;  // ? or !
    options.UseNpgsql(connectionString);
});
//add identity
builder.Services.AddIdentity<AppUserEntity, AppRoleEntity>(options =>
{
    options.User.RequireUniqueEmail = true;

    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireDigit = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();

// CORS - allowed to make queries from React.
string corsPolicy = "allowAll";   //name can be different
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(corsPolicy, cfg =>
    {
        cfg.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();

//add authentification
string? secretKey = builder.Configuration["JwtSettings:SecretKey"];
if (string.IsNullOrEmpty(secretKey))
{
    throw new ArgumentException("Jwt secret key is null");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        RequireExpirationTime = true,
        ClockSkew = TimeSpan.Zero,
        ValidateLifetime = true,
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsPolicy);

app.UseHttpsRedirection();


// static files
string root = app.Environment.ContentRootPath;
string storagePath = Path.Combine(root, StaticFilesSetting.StorageDir);
string booksPath = Path.Combine(storagePath, StaticFilesSetting.BooksDir);
if (!Directory.Exists(booksPath))
{
    Directory.CreateDirectory(booksPath); 
}
string authorsPath = Path.Combine(storagePath, StaticFilesSetting.AuthorsDir);
if (!Directory.Exists(authorsPath))
{
    Directory.CreateDirectory(authorsPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(booksPath),
    RequestPath = StaticFilesSetting.BookUrl
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(authorsPath),
    RequestPath = StaticFilesSetting.AuthorUrl
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.SeedAsync().Wait();    

app.Run();
