using EmployeeDetails.DB;
using EmployeeDetails.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
    
string connectionString = "server=localhost;port=3306;database=EmployeeDB;user=rishav;password=rishav";
builder.Services.AddDbContextPool<EmployeeDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


//Add services to the container.

//builder.Services.AddSingleton<IEmployeeRepsitory, MockEmployeeRepository>();
builder.Services.AddScoped<IEmployeeRepsitory, MySQLEmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, MySQLDepartmentRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

builder.Services.AddControllers();


//Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
