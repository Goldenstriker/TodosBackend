using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TodoBackend.Context;
using TodoBackend.Context.Managers;

var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
                      {
                          policy.WithOrigins("http://localhost:3000");
                      });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ITodoManager, TodoManager>();

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

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
