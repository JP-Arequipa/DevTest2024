using Microsoft.EntityFrameworkCore;
using polls_app.API.Data;
using polls_app.API.Repositories;
using polls_app.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(connection));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy  =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});
//builder.Services.AddSingleton<IPollRepository, InMemoryPollRepository>();
builder.Services.AddScoped<IPollRepository, EntityFrameworkPollRepository>();
builder.Services.AddScoped<IPollService, PollService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();
app.MapControllers();

app.Run();

