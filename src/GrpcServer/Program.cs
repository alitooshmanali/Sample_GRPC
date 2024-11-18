using Application;
using Application.Behaviors;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Grpc.SDK;
using GrpcServer.Services;
using Infrastructure;
using Infrastructure.Contexts;
using Infrastructure.Services;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc(options =>
{
    {
        options.EnableDetailedErrors = true;
        options.Interceptors.Add<ExceptionHandler>();
    }
});

builder.Services.AddGrpcSDK();
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
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextPool<WriteDbContext>(i => { i.UseNpgsql(connectionString); });

var app = builder.Build();

var serviceProvider = builder.Services.BuildServiceProvider();
var createScope = serviceProvider.CreateScope();
var serviceProviderFactory = createScope.ServiceProvider.GetService<IServiceScopeFactory>();

var dbContext = createScope.ServiceProvider.GetService<WriteDbContext>();

dbContext.Database.Migrate();
var dbConnection = dbContext.Database.GetDbConnection();
dbConnection.Open();
((NpgsqlConnection)dbConnection).ReloadTypes();
dbConnection.Close();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<UserService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
