



var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;
builder.Services.AddCarter();

builder.Services.AddMediatR(con =>
{
    con.RegisterServicesFromAssemblies(assembly);
    con.AddOpenBehavior(typeof(ValidationBehavior<,>));
    con.AddOpenBehavior(typeof(LoggingBehavior<,>));
});


var app = builder.Build();
app.MapCarter();

app.MapGet("/", () => "Hello World!");

app.Run();
