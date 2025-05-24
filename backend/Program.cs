using Microsoft.AspNetCore.Mvc;
using NHibernate;
using NHibernate.Cfg;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddSingleton<ISessionFactory>(provider =>
{
    var configuration = new Configuration();
    configuration.Configure(); // Reads hibernate.cfg.xml
    configuration.AddDirectory(new System.IO.DirectoryInfo("NHibernateMappings")); // Load mapping files
    configuration.SetProperty("nhibernate.logger.factory", "NHibernate.Extensions.Logging.LoggerFactory");

    return configuration.BuildSessionFactory();
});

builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<ITicketService, TicketService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


/////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////
// TODO: Scour committed files for revealing comments
/////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////

app.MapControllers();
app.Run();