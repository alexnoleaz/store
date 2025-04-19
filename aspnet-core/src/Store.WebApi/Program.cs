using System.Reflection;
using Store.Shared.Dependency;

var builder = WebApplication.CreateBuilder(args);
var assemblies = new List<Assembly> { typeof(Program).Assembly };

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(assemblies);

assemblies.ForEach(assembly =>
    ConventionalRegistrar.RegisterAssemblyByConvention(builder.Services, assembly)
);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
