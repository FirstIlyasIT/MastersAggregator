using MastersAggregatorService.Repositories;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
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
            Email = "In process",
            Url = new Uri("https://www.microsoft.com/learn")
        }
    });
builder.Services.AddCors(); // возможно следует удалить в будущем

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

//Добавили в сервис наши Repository 
builder.Services.AddScoped<ImageRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<OrderRepository>()
                .AddScoped<MasterRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyMethod().AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

app.Run();
