using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Store.Products;
using Store.Products.Validations;
using Store.Shared.Dependency;
using Store.Shared.Entities;
using Store.Shared.Repositories.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var defaultPolicyName = "localhost";
var assemblies = new List<Assembly>
{
    typeof(Program).Assembly,
    typeof(StoreDbContext).Assembly,
    typeof(Entity).Assembly,
    typeof(ProductService).Assembly,
};

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(assemblies);
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductDtoValidator>();

EntityFrameworkCoreConfigurer.Configure(builder.Services, builder.Configuration);
assemblies.ForEach(assembly =>
    ConventionalRegistrar.RegisterAssemblyByConvention(builder.Services, assembly)
);

builder.Services.AddCors(options =>
    options.AddPolicy(
        defaultPolicyName,
        b =>
            b.WithOrigins(
                    builder
                        .Configuration["App:CorsOrigins"]
                        .Split(",", StringSplitOptions.RemoveEmptyEntries)
                        .ToArray()
                )
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
    )
);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
await ProductSeeder.SeedAsync(context);

// app.UseHttpsRedirection();
app.UseCors(defaultPolicyName);
app.UseAuthorization();
app.MapControllers();
app.Run();