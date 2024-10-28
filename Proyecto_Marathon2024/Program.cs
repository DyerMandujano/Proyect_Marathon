using Proyecto_Marathon2024.Abstracion;
using Proyecto_Marathon2024.Repository;
using Proyecto_Marathon2024.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<DataAccses>();  //Acceso a la base de datos
// Registra los Repostiorios
builder.Services.AddScoped<Perfil_PersonalRepository>();  
builder.Services.AddScoped<PersonalRepository>();  


var app = builder.Build();

builder.Services.AddEndpointsApiExplorer();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();





// Registrar los servicios en el contenedor DI ANTES de construir la aplicación
//builder.Services.AddScoped<IPerfil_Personal, Perfil_PersonalRepository>();  // Registra el repositorio con la interfaz