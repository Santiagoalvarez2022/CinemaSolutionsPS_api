using CinemaSolutionApi.Data;
using CinemaSolutionApi.Dtos;
using CinemaSolutionApi.Entities;
using CinemaSolutionApi.Mapping;

namespace CinemaSolutionApi.Endpoints;

public static class ScreeningEndpoints
{
    const string GetScreeningEndpointName = "GetScreening";
    public static RouteGroupBuilder MapScreeningEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/screening");

        //POST /screening ()
        group.MapPost("/", (CreateScreeningDto newScreening, CinemaSolutionContext dbContext) =>
        {
            //toEntity() es un metodo definido en /screeningMapping, es un metodo de extenxion que toma la
            //instancia creada de CreateScreeningDto y le añade este
            //metodo nuevo.
            Screening screening = newScreening.ToEntity();

            //toEntity no tiene acceso al dbContext, por lo que debo añadirle el "Movie" por duera del metodo,
            //en este caso buscando la movie por Id con los metodos del contexto de la BD. 
            screening.Movie = dbContext.Movies.Find(newScreening.MovieId);

            dbContext.Screenings.Add(screening);
            dbContext.SaveChanges();//save the new record.

            return Results.Ok(screening.ToDto());
        }).RequireAuthorization();

        //GET/id Este metodo se usado tambien en la funcion post, para enviar la url del screening recien creado 
        group.MapGet("/{id}", (int id, CinemaSolutionContext dbContext) =>
        {
            /*
                crea un MovieDetailsDto para usar en Moviemapping para usar ese metodo en endpoin
            */
            var screening = dbContext.Screenings.Find(id);

            // return screening is null ? Results.NotFound() : Res
        });

        return group;
    }

}