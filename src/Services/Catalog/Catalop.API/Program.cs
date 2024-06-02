var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
});
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database"));
}).UseLightweightSessions();

// configure service dependencies
var app = builder.Build();

// Configure HTTP Pipeline
app.MapCarter();
app.Run();
