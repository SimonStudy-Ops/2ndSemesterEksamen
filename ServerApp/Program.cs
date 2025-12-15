using WebApplication1.Data;
using WebApplication1.Repositories;
using System.Text.Json;
using System.Text.Json.Serialization;
using Core.Models;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });

builder.Services.AddSingleton<VarerRepository>();
builder.Services.AddSingleton<VarerBeholdningRepository>();
builder.Services.AddSingleton<BrugerRepository>();
builder.Services.AddSingleton<KategoriRepository>();
builder.Services.AddSingleton<IFileRepository,AzureStorageFileRepository>();

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

var mongoConn = builder.Configuration["MongoDb:ConnectionString"] ?? "mongodb+srv://eaa24mofh_db_user:mohamed123@2ndsemestereksamen.ghi4mwz.mongodb.net/?appName=2ndsemestereksamen";
var mongoDbName = builder.Configuration["MongoDb:DatabaseName"] ?? "basement";

builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(mongoConn));
builder.Services.AddScoped(sp => sp.GetRequiredService<IMongoClient>().GetDatabase(mongoDbName));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();
    var brugereCol = db.GetCollection<Bruger>("Brugers");
    var count = brugereCol.EstimatedDocumentCount();
    if (count == 0)
    {
        DatabaseSeederSimple.SeedAsync(db).GetAwaiter().GetResult();
    }
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

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