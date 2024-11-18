using Application;
using Application.Behaviors;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Infrastructure;
using Infrastructure.Contexts;
using Infrastructure.Services;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using RestAPI;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddLogging(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddApiVersioning(options => options.ReportApiVersions = true);
builder.Services.AddHttpCacheHeaders();
builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true)
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var validationProblemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Type = "",
                Title = "",
                Status = StatusCodes.Status422UnprocessableEntity,
                Detail = "",
                Instance = context.HttpContext.Request.Path
            };

            validationProblemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);

            return new UnprocessableEntityObjectResult(validationProblemDetails);
        };
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(container =>
{
    var infrastructureAssembly = typeof(WriteDbContext).Assembly;
    var applicationAssembly = typeof(IUnitOfWork).Assembly;

    var configuration = MediatRConfigurationBuilder
            .Create(applicationAssembly)
            .WithAllOpenGenericHandlerTypesRegistered()
            .Build();

    container.RegisterMediatR(configuration);

    container.RegisterAssemblyTypes(infrastructureAssembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();


    container.RegisterAssemblyTypes(typeof(IUnitOfWork).Assembly)
            .AssignableTo(typeof(IUnitOfWork))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

    container.RegisterGeneric(typeof(TransactionBehavior<,>)).As(typeof(IPipelineBehavior<,>));
    //container.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));

    container.RegisterType<SystemDateTime>().As<ISystemDateTime>()
    .InstancePerLifetimeScope();
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "Rira Rest API", Version = "v1" });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextPool<WriteDbContext>(i => { i.UseNpgsql(connectionString); });

var app = builder.Build();
var serviceProvider = builder.Services.BuildServiceProvider();
var createScope = serviceProvider.CreateScope();
var serviceProviderFactory = createScope.ServiceProvider.GetService<IServiceScopeFactory>();

var dbContext = createScope.ServiceProvider.GetService<WriteDbContext>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rira Rest API v1"));
}

dbContext.Database.Migrate();
var dbConnection = dbContext.Database.GetDbConnection();
dbConnection.Open();
((NpgsqlConnection)dbConnection).ReloadTypes();
dbConnection.Close();

//DatabaseSeeder.Seed(serviceProviderFactory).GetAwaiter().GetResult();

app.UseHttpCacheHeaders();
app.UseRouting();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
//app.UseAuthorization();

app.Run();
