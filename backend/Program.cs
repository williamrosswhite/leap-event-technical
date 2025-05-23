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
builder.Services.AddSwaggerGen(); // Ensure Swagger services are added

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
    app.UseSwagger(); // Ensure Swagger middleware is added
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

/////////////////////////////////////////////////////////////////////////
// TODO: Add Move this to a the event controller
/////////////////////////////////////////////////////////////////////////

app.MapGet("/api/events", ([FromServices] IEventService eventService, [FromQuery] int days) =>
{
    if (days != 30 && days != 60 && days != 180)
    {
        return Results.BadRequest("Invalid value for 'days'. Allowed values are 30, 60, or 180.");
    }

    var events = eventService.GetEventsWithinDays(days);
    return Results.Ok(events);
})
.WithName("GetEvents");

app.Run();