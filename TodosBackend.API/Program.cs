using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TodoBackend.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TodoContext>((options) =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("SQLConnection"));
});

{
    DbContextOptionsBuilder<TodoContext> optionsBuilder = new DbContextOptionsBuilder<TodoContext>();
    DbContextOptions<TodoContext> options = optionsBuilder.UseSqlite(builder.Configuration.GetConnectionString("SQLConnection")).Options;

    using (TodoContext todoContext = new TodoContext(options))
    {
        await todoContext.Database.MigrateAsync();
    }
}

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

app.Run();
