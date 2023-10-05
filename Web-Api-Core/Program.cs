using Microsoft.EntityFrameworkCore;
using Web_Api_Core.Models;
using Web_Api_Core.Services;


var builder = WebApplication.CreateBuilder(args);

// Configura la cadena de conexión para MySQL
string mySqlConnectionStr = builder.Configuration.GetConnectionString("DefaultConnection");

// Agrega servicios a la colección de servicios.
builder.Services.AddControllers();

// Configura el DbContext para MySQL
builder.Services.AddDbContext<ev2Context>(options =>
{
    options.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr));
});

// Agregar servicios de documentación Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Mi API", Version = "v1" });
});

// Registra tus servicios personalizados
builder.Services.AddDbContext<ev2Context>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<PedidoService>();
builder.Services.AddScoped<DetallesPedidoServices>();
builder.Services.AddScoped<LoginService>();


var app = builder.Build();

// Configure el HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Habilita Swagger en modo de desarrollo
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API v1");
    });
}
else
{
    // Configura el manejo de errores personalizado u otros middlewares de producción
    // app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();

// Agregar la llamada app.UseRouting() antes de app.UseAuthorization() y app.MapControllers()
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
