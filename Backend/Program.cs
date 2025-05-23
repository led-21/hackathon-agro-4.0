using Core.Data;
using Core.Services;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Receituario AI API", Version = "v1" });
    c.SchemaFilter<TextAreaSchemaFilter>();
});

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

if(app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapOpenApi();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.MapPost("verificar/receituario/bula", async (AIService aIService, string receituario, string? bula) =>
{
    try
    {
        var result = await aIService.AvaliarReceituario(receituario, bula ?? AgronomicMockData.bula);

        return Results.Content(result);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapPost("verificar/receituario", async (AIService aIService, string receituario) =>
{
    try
    {
        var result = await aIService.AvaliarReceituario(receituario, await aIService.BuscarBula(receituario));

        return Results.Content(result);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapPost("verificar/receituario/rag", async (AIService aIService, string receituario) =>
{
    try
    {
        var result = await aIService.AvaliarReceituario(receituario, await aIService.BuscarRAG(receituario));

        return Results.Content(result);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.Run();


public class TextAreaSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(string))
        {
            schema.Format = "textarea";
        }
    }
}