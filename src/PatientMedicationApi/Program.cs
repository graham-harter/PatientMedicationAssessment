using System;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PatientMedication.Infrastructure;
using PatientMedication.Infrastructure.Configuration;
using PatientMedication.Infrastructure.Configuration.Interfaces;
using PatientMedication.Infrastructure.ConnectionFactories;
using PatientMedication.Infrastructure.ConnectionFactories.Interfaces;
using PatientMedication.Infrastructure.DbContexts;

namespace PatientMedicationApi;

public class Program
{
    #region Public methods

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Get the application configuration.
        var configurationBuilder =
            new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCommandLine(args);

        var environmentalConfiguration = configurationBuilder.Build();

        var environment = GetEnvironment();

        var configuration =
            configurationBuilder
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json")
                .Build();

        // Add services to the container.
        var services = builder.Services;

        // Create the connection string object for the PatientMedication database.
        var patientMedicationConnectionString = CreatePatientMedicationConnectionString(configuration);

        // Register connection strings.
        RegisterConnectionStrings(services, patientMedicationConnectionString);

        // Register database connection factories.
        RegisterConnectionFactories(services, patientMedicationConnectionString);

        services.AddDbContext<PatientMedicationContext>(options =>
            options.UseSqlServer(
                patientMedicationConnectionString.Get(),
                builder => builder.MigrationsAssembly("PatientMedication.Infrastructure")));

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
            .AddNegotiate();

        builder.Services.AddAuthorization(options =>
        {
            // By default, all incoming requests will be authorized according to the default policy.
            options.FallbackPolicy = options.DefaultPolicy;
        });

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

        // Test code.
        // Todo: Remove this.
        //var provider = services.BuildServiceProvider();
        //PatientMedicationContext context = (PatientMedicationContext)provider.GetRequiredService(typeof(PatientMedicationContext));

        app.Run();
    }

    #endregion // #region Public methods

    #region Private methods

    private static string GetEnvironment()
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
        return environment;
    }

    private static IPatientMedicationConnectionString CreatePatientMedicationConnectionString(
        IConfiguration configuration)
    {
        // Get connection string.
        var patientMedicationConnectionString = configuration.GetConnectionString("PatientMedication")!;

        // Create strongly-typed connection string object.
        var typedPatientMedicationConnectionString = new PatientMedicationConnectionString(patientMedicationConnectionString);

        return typedPatientMedicationConnectionString;
    }

    private static void RegisterConnectionStrings(
        IServiceCollection services,
        IPatientMedicationConnectionString patientMedicationConnectionString)
    {
        services.AddSingleton<IPatientMedicationConnectionString>(patientMedicationConnectionString);
    }

    private static void RegisterConnectionFactories(
        IServiceCollection services,
        IPatientMedicationConnectionString patientMedicationConnectionString)
    {
        var patientMedicationConnectionFactory = new PatientMedicationConnectionFactory(patientMedicationConnectionString);
        services.AddSingleton<IPatientMedicationConnectionFactory>(patientMedicationConnectionFactory);
    }

    #endregion // #region Private methods
}
