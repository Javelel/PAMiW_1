using Microsoft.EntityFrameworkCore;
using P05Shop.API.Models;
using P06Shop.Shared.Services.MovieService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DataContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IMovieService, P05Shop.API.Services.MovieService.MovieService>();

// addScoped - obiekt jest tworzony za kazdym razem dla nowego zapytania http
// jedno zaptranie tworzy jeden obiekt 

// addTransinet obiekt jest tworzony za kazdym razem kiedy odwolujmey sie do konstuktora 
// nawet wielokrotnie w cyklu jedengo zaptrania 

 //addsingleton - nowa instancja klasy tworzona jest tylko 1 na caly cykl trwania naszej aplikacji 

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsePolicy", builder =>
    builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyCorsePolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
