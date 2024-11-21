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
// Registra los Repositorios
//PERSONAL
builder.Services.AddScoped<PersonalRepository>();
builder.Services.AddScoped<Perfil_PersonalRepository>();
builder.Services.AddScoped<Tipo_TrabajoRepository>();
builder.Services.AddScoped<LocalMTRepository>();
//PRODUCTO
builder.Services.AddScoped<ProductoRepository>();
builder.Services.AddScoped<CategoriaRepository>();
builder.Services.AddScoped<MarcaRepository>();
builder.Services.AddScoped<ColorRepository>();
builder.Services.AddScoped<ModeloRepository>();
builder.Services.AddScoped<TallaRepository>();

builder.Services.AddScoped<ClienteRepository>();
builder.Services.AddScoped<MembresiaRepository>();
//PEDIDO Y VENTASS
builder.Services.AddScoped<PedidoRepository>();
builder.Services.AddScoped<VentaRepository>();
builder.Services.AddScoped<DetallePedidoRepository>();
builder.Services.AddScoped<DetalleVentaRepository>();


var app = builder.Build();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); //Permite el acceso a la API desde cualquier origen
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