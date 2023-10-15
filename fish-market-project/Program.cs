using fish_market_project.Filter;
using fish_market_project.Middlewares;
using FishMarketProjectData.Database;
using FishMarketProjectData.Database.Repositories;
using FishMarketProjectData.Database.Repositories.IRepositories;
using FishMarketProjectDomain.IService;
using FishMarketProjectService.Services;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddAutoMapper(typeof(Program));
// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ValidationFilter));
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.IgnoreNullValues = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

//repositories
builder.Services.AddTransient<IUserRepository, UserRepository>();
//services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IEmailSenderService, EmailSenderService>();

builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
   // c.IncludeXmlComments(xmlPath);
    c.SwaggerDoc("v1",
       new OpenApiInfo
       {
           Title = "Challenge API",
           Version = "v1",
           Description = "Challenge API",

       });

});
builder.Services.AddDbContext<FishMarketContextDb>(options =>
{
    options.UseSqlServer(configuration["ConnectionString"],
                                        sqlServerOptionsAction: sqlOptions =>
                                        {
                                            sqlOptions.MigrationsAssembly(typeof(FishMarketContextDb).GetTypeInfo().Assembly.GetName().Name);
                                            //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                        });
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

var app = builder.Build();

app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware(typeof(GlobalExceptionHandlingMiddleware));
app.UseSwagger(c =>
{
    c.RouteTemplate = "/swagger/swagger/{documentname}/swagger.json";
}); 
app.UseSwaggerUI(c =>
{

    c.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
