

var builder = WebApplication.CreateBuilder(args);
// Add logging providers
builder.Logging.ClearProviders();
builder.Logging.AddConsole();


// Add services to the container.
builder.Services
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddApiServices(builder.Configuration);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.InitialMigration();
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseApiServices();

app.Run();
