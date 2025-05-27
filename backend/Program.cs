using NHibernate;
using NHibernate.Cfg;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build())
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddSerilog(dispose: true);
});

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Configure CORS
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        // Restrict headers and methods
        policy.WithOrigins(allowedOrigins)
              .WithHeaders("Content-Type", "Authorization")
              .WithMethods("GET", "POST", "PUT", "DELETE");
    });
});

// Configure NHibernate
builder.Services.AddSingleton<ISessionFactory>(provider =>
{
    var configuration = new Configuration();
    configuration.Configure();
    var mappingsDirectory = builder.Configuration["NHibernate:MappingsDirectory"];
    if (string.IsNullOrEmpty(mappingsDirectory))
    {
        throw new InvalidOperationException("NHibernate:MappingsDirectory configuration is missing or empty.");
    }
    configuration.AddDirectory(new DirectoryInfo(mappingsDirectory));
    configuration.SetProperty("nhibernate.logger.factory", builder.Configuration["NHibernate:LoggerFactory"]);

    return configuration.BuildSessionFactory();
});

// Add scoped services
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<ITicketService, TicketService>();

var app = builder.Build();

// Add swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();
app.UseCors();
app.MapControllers();

Log.Information("Application starting...");

try
{
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.Information("Application shutting down...");
    Log.CloseAndFlush();
}