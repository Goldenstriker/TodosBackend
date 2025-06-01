using Microsoft.EntityFrameworkCore;
using TodoBackend.Context;
using TodoBackend.Context.Managers;
using TodoBackend.Context.Managers.TodosList;

var builder = WebApplication.CreateBuilder(args);


DbContextOptionsBuilder<TodoContext> optionsBuilder = new DbContextOptionsBuilder<TodoContext>();
DbContextOptions<TodoContext> options = optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("SQLConnection")).Options;

using (TodoContext todoContext = new TodoContext(options))
{
    todoContext.Migrate();
    SeedAudit.SeedDatabase(todoContext);
}


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
builder.Services.AddTransient<ITodosProvider, TodosProvider>();

builder.Services.AddDbContext<TodoContext>((options) =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("SQLConnection"));
    options.EnableDetailedErrors();
});


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
