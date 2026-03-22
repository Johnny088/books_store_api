using books_store_api.Settings;
using books_store_BLL.Dtos.Services;
using books_store_DAL;
using books_store_DAL.Initializer;
using books_store_DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

//Add Repositories
builder.Services.AddScoped<AuthorRepository>();
builder.Services.AddScoped<GenreRepository>();
builder.Services.AddScoped<BookRepository>();

// Add services to the container.
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<ImageService>();


// adding dbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("LocalDb")!;  // ? or !
    options.UseNpgsql(connectionString);
});

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
string authorsPath = Path.Combine(storagePath, StaticFilesSetting.AuthorsDir);

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


app.UseAuthorization();

app.MapControllers();

app.SeedAsync().Wait();    

app.Run();
