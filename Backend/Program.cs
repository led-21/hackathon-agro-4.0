using Core.Services;
using Core.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Registra AIService como singleton
builder.Services.AddSingleton<AIService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.MapPost("verificar/receituario", async (AIService aIService, string receituario, string? bula) =>
{
    try
    {
        var result = await aIService.AvaliarReceituario(receituario, bula ?? AgronomicMockData.bula);

        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.Run();
