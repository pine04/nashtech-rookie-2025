namespace aspnetfundamentals;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Adding Swagger for fun :).
        app.UseSwagger();
        app.UseSwaggerUI((c) =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger UI");
        });

        app.UseLogging();

        app.Use(async (context, next) =>
        {
            await context.Response.WriteAsync("Logged request.");
            await next(context);
        });

        app.Run();
    }
}