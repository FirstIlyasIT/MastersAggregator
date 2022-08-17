using MastersAggregatorService.Repositories;
using MastersAggregatorService.Models;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/*builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "PrintFramer API",
        Description = "Calculates the cost of a picture frame based on its dimensions.",
        TermsOfService = new Uri("https://go.microsoft.com/fwlink/?LinkID=206977"),
        Contact = new OpenApiContact
        {
            Name = "Your name",
            Email = string.Empty,
            Url = new Uri("https://www.microsoft.com/learn")
        }
    });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});*/

//Добавили в сервис наши Repository 
builder.Services.AddScoped<ImageRepository>()
                .AddScoped<UserRepository>()
                .AddScoped<OrderRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

