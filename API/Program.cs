using API.Middleware;
using Application.Activities.Queries;
using Application.Activities.Validators;
using Application.Core;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Order for services does not matter
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors();
builder.Services.AddMediatR(x =>
{
    x.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxODE4MDI4ODAwIiwiaWF0IjoiMTc4NjU2ODMyNSIsImFjY291bnRfaWQiOiIwMTlmZjdjMmFjYTA3Y2U1ODJiNTA4YTMwMTA1Zjc2NSIsImN1c3RvbWVyX2lkIjoiMDE5ZmY3YzJhY2EwN2NlNTgyYjUwOGEzMDEwNWY3NjUiLCJzdWJfaWQiOiItIiwiZWRpdGlvbiI6IjAiLCJ0eXBlIjoiMiJ9.X0sUzxRo0VAFQIsKwjvBLaz1x89TJ_nfplgVExSHnmXqMQGQUrKP0q2i-GW_3_IRBSr_9IsaXuhoGBEDcG3Lba0CuiWjUnA2jcAvgsooIcFE0gXWnq0gOf3ueJHRDribQFzsS8T6iIBhEAMSFx_CJFL43_5iiDaN5o-Ik4nVeIJ7lrcRcPZF9xOhVLmPQGKvHXWdgktZ0huZF3R9oof5Nq2f4yQ6M25FiTlkKZtp42vwfUElcm7x-RmqEKhTeUiA94JdzVAdCQUy8YLImY4JHwf2xEUDHGEHMq9PHIPFJ7l9ohZ7iz9imBsfMZhK3BFBiQrlwChB1DiGWq2bOugXyA";
    x.RegisterServicesFromAssemblyContaining<GetActivityList.Handler>();
    x.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfiles).Assembly);
builder.Services.AddValidatorsFromAssemblyContaining<CreateActivityValidator>();
builder.Services.AddTransient<ExceptionMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Order for middleware is important
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
    .WithOrigins("http://localhost:3000", "https://localhost:3000"));

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    // This block creates a database if there isn't one
    // and takes care of pending migrations and seeding data
    var context = services.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();
    await DbInitializer.SeedData(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Error occurred during migration");
}

app.Run();
