using System.Reflection;
using MediatR;
using ShortLinksService.Entities;
using ShortLinksService.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Services.AddServices();
builder.Services.Configure<MongoEntitySettings>(
    builder.Configuration.GetSection("MongoEntity"));
builder.Services.AddStackExchangeRedisCache(configuration);
//builder.Services.AddMediatR(typeof(Program));
builder.Services.AddMediatR();
builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
