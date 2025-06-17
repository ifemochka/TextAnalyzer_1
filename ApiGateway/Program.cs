using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("FileStoring", client =>
{
    client.BaseAddress = new Uri("http://filestoring");
});
builder.Services.AddHttpClient("FileAnalysis", client =>
{
    client.BaseAddress = new Uri("http://analysis");
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


app.Run();
