using EmployeeDetails.BasicAuth;
using EmployeeDetails.DB;
using EmployeeDetails.Repository;
using EmployeeDetails.Services;
using Microsoft.AspNetCore.Authentication;
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
builder.Services.AddScoped<IProjectRepository, MySQLProjectRepository>();
builder.Services.AddScoped<IEmpProjRepository, MySQLEmpProj>();
builder.Services.AddScoped<ISignUpRepo, MySQLSignUpRepo>();
builder.Services.AddScoped<IRoleRepo, MySQLRoleRepo>();
builder.Services.AddScoped<ISignupRolesRepo, MySQLSignupRoleRepo>();
builder.Services.AddSingleton<Mail>();




builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

builder.Services.AddControllers();

builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

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

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();
