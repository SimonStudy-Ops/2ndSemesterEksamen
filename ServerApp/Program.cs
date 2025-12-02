using WebApplication1.Data;
using WebApplication1.Repositories;
using System.Text.Json;
using System.Text.Json. Serialization;

var builder = WebApplication. CreateBuilder(args);

// Add services to the container.
builder. Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options. JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions. Converters.Add(new DateOnlyJsonConverter());
    });

builder. Services.AddSingleton<VarerRepository>();
builder.Services.AddSingleton<VarerBeholdningRepository>();
builder.Services.AddSingleton<BrugerRepository>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddOpenApi();

var app = builder.Build();

DatabaseSeederSimple.Seed("mongodb://localhost:27017", "basement");

if (app.Environment.IsDevelopment())
{
    app. MapOpenApi();
}

app. UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Custom DateOnly converter
public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private const string Format = "yyyy-MM-dd";

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateOnly.ParseExact(reader.GetString()!, Format);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format));
    }
}