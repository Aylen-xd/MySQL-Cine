using System.Data;
using Cine.Core.Persistencia;
using Cine.Persistencia.Dapper;
using Cine.Persistencia.Dapper.Repos;
using MySqlConnector;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//  Obtener la cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("MySQL");

//  Registrando IDbConnection para que se inyecte como dependencia
//  Cada vez que se inyecte, se creará una nueva instancia con la cadena de conexión
builder.Services.AddScoped<IDbConnection>(sp => new MySqlConnection(connectionString));

//Cada vez que necesite la interfaz, se va a instanciar automaticamente AdoDapper y se va a pasar al metodo de la API
//estos dos no tiene dependencia de nadie
builder.Services.AddScoped<IRepoGenero, RepoGenero>();
builder.Services.AddScoped<IRepoActor, RepoActor>();

//los que tiene dependencia 
builder.Services.AddScoped<IRepoTrailer, RepoTrailer>();
builder.Services.AddScoped<IRepoSaga, RepoSaga>();
builder.Services.AddScoped<IRepoEstudio, RepoEstudio>();
builder.Services.AddScoped<IRepoPelicula, RepoPelicula>();
builder.Services.AddScoped<IRepoProduccion, RepoProduccion>();



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "/openapi/{documentName}.json";
    });
    app.MapScalarApiReference();
}

//Para un GET en la ruta "/todoitems", 

//no tiene dependencia------------------------------------
app.MapGet("/generos", (IRepoGenero repo) =>
    repo.TraerElementos());

app.MapGet("/actores", (IRepoActor repo) =>
    repo.TraerElementos());

//si tiene dependencia--------------------------------------
app.MapGet("/trailers", (IRepoTrailer repo) =>
    repo.TraerElementos());

app.MapGet("/saga", (IRepoSaga repo) =>
    repo.TraerElementos());

app.MapGet("/estudio", (IRepoEstudio repo) =>
    repo.TraerElementos());

app.MapGet("/pelicula", (IRepoPelicula repo) =>
    repo.TraerElementos());

app.MapGet("/produccion", (IRepoProduccion repo) =>
    repo.TraerElementos());

//-----------------------------------------------------------

app.MapGet("/todoitems/{id}", async (int id, IADO repo) =>
    await repo.ObtenerTodoPorIdAsync(id)
        is Todo todo
            ? Results.Ok(todo)
            : Results.NotFound());

app.MapPost("/todoitems", async (Todo todo, IADO repo) =>
{
    await repo.AgregarTodoAsync(todo);

    return Results.Created($"/todoitems/{todo.Id}", todo);
});

app.MapPut("/todoitems/{id}", async (int id, Todo inputTodo, IADO repo) =>
{
    var todo = await repo.ObtenerTodoPorIdAsync(id);

    if (todo is null) return Results.NotFound();

    todo.Name = inputTodo.Name;
    todo.IsComplete = inputTodo.IsComplete;

    await repo.ActualizarTodoAsync(todo);

    return Results.NoContent();
});

app.MapDelete("/todoitems/{id}", async (int id, IADO repo) =>
{
    if (await repo.ObtenerTodoPorIdAsync(id) is Todo todo)
    {
        await repo.EliminarTodoAsync(todo);
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.Run();