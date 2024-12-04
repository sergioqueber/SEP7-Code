using Interfaces;
using Microsoft.AspNetCore.Builder;
using Services;
using Storage;
using Controllers;
using Storage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>();
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITransportService, TransportService>();
builder.Services.AddScoped<ICarPoolService, CarPoolService>();
builder.Services.AddScoped<ISavingFoodService, SavingFoodService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IRewardService, RewardService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IVolunteeringService, VolunteeringService>();
builder.Services.AddScoped<IStairsService, StairsService>();
builder.Services.AddScoped<IAuthService, AuthService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.MapInboundClaims = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "")),
        ClockSkew = TimeSpan.Zero,
    };
});


AuthorizationPolicies.AddPolicies(builder.Services);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", builder =>
        builder.WithOrigins("http://localhost:5048") // Blazor app URL
               .AllowAnyMethod()
               .AllowAnyHeader());
});

var app = builder.Build();

//Add services to the container.


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazorClient"); // Apply CORS policy
app.UseAuthorization();
app.MapControllers();
app.Run();


