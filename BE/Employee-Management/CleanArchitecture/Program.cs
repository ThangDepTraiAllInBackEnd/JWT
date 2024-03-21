using CleanArchitecture.Core.Auth;
using CleanArchitecture.Core.Exeptions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Mapper;
using CleanArchitecture.Core.Resources;
using CleanArchitecture.Core.Services;
using CleanArchitecture.Infrastructure.Context;
using CleanArchitecture.Infrastructure.Context.DatabaseContext;
using CleanArchitecture.Infrastructure.Interfaces;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;


// For Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseMySql(configuration.GetConnectionString("MariaDb"), new MySqlServerVersion(new Version(10, 3, 38))));



// For Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();


// Adding Authentication
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
 {
	 options.SaveToken = true;
	 options.RequireHttpsMetadata = false;
	 options.TokenValidationParameters = new TokenValidationParameters()
	 {
		 ValidateIssuer = true,
		 ValidateAudience = true,
		 ValidateLifetime = true,
		 ValidateIssuerSigningKey = true,
		 ClockSkew = TimeSpan.Zero,

		 ValidAudience = configuration["JWT:ValidAudience"],
		 ValidIssuer = configuration["JWT:ValidIssuer"],
		 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
	 };
 });


// change name from t... to T...
builder.Services.AddControllers().AddJsonOptions(o =>
{
	o.JsonSerializerOptions.PropertyNamingPolicy = null;
	o.JsonSerializerOptions.PropertyNamingPolicy = null;
});


builder.Services.AddCors(options =>
{
	options.AddPolicy(name: MyAllowSpecificOrigins,
					  policy =>
					  {
						  policy.WithOrigins("*")
						  .AllowAnyHeader()
						   .AllowAnyMethod();
					  });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DI Configuration
builder.Services.AddScoped(typeof(IEmployeeService), typeof(EmployeeService));
builder.Services.AddScoped(typeof(IEmployeeRepository), typeof(EmployeeRepository));
builder.Services.AddScoped(typeof(IDepartmentService), typeof(DepartmentService));
builder.Services.AddScoped(typeof(IDepartmentRepository), typeof(DepartmentRepository));
builder.Services.AddScoped(typeof(IDBContext), typeof(MariaDbContext));
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(DBContext<>));
builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
builder.Services.AddScoped<ICacheRepository, CacheRepository>();

// mapper
builder.Services.AddAutoMapper
	(typeof(AutoMapperProfile));
//builder.Services.AddAutoMapper()

builder.Services.AddMemoryCache();
var app = builder.Build();

//MiddleWare config
app.UseMiddleware<HandleExeptionMiddleWare>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
