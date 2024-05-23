var builder = WebApplication.CreateBuilder(args);

// configure service dependencies
var app = builder.Build();

// Configure HTTP Pipeline

app.Run();
