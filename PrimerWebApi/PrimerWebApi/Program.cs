internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();



        var Clients = new List<Client>();



        app.MapGet("/Clients", () =>
        {
            return Clients;
        });

        app.MapGet("/Clients/{id}", (int id) =>
        {

            var Client = Clients.FirstOrDefault(c => c.Id == id);

            return Clients;


        });

        app.MapPost("/Clients", (Client client) =>
        {
            Clients.Add(client);
            return Results.Ok();
        });



        app.MapPut("/Clients/{id}", (int id, Client client) =>
        {

            var existingClient = Clients.FirstOrDefault(c => c.Id == id);

            if (existingClient != null)
            {

                existingClient.Name = client.Name;

                existingClient.LastName = client.LastName;
                return Results.Ok();
            }

            else
            {
                return Results.NotFound();
            }

        });

        app.MapDelete("/Clients/{id}", (int id) =>
        {
            var existingClient = Clients.FirstOrDefault(c => c.Id == id);
            if (existingClient != null)
            {
                Clients.Remove(existingClient);
                return Results.Ok();
            }

            else
            {
                return Results.NotFound();
            }

        });
        app.Run();
    }
}

internal class Client
{

public int Id { get; set; }
    public string Name { get; set; }

    public string LastName { get; set; }

}

