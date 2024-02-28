using Newtonsoft.Json.Serialization;
using WebApplication1;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc()
    .AddNewtonsoftJson(it =>
    {
        it.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        it.SerializerSettings.Converters.Add(new DoubleTwoDigitalConverter(2));
    })
    .AddMultiNewtonsoftJsonOptions("level4",
        it => { it.SerializerSettings.Converters.Add(new DoubleTwoDigitalConverter(4)); })
    .AddMultiNewtonsoftJsonOptions("level6",
        it => { it.SerializerSettings.Converters.Add(new DoubleTwoDigitalConverter(6)); });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();