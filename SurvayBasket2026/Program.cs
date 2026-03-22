
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependancies(builder.Configuration);

builder.Host.UseSerilog((context, configration) =>
{
    configration.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();

app.MapOpenApi();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("SurvayBasket").WithTheme(ScalarTheme.Mars).
        WithDefaultHttpClient(ScalarTarget.CSharp , ScalarClient.HttpClient);
    });
}
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
