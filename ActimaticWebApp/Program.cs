using ActimaticWebApp.Components;
using Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Authorization;
using AppInterfaces;
using AppServices;
using Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5205/") });
builder.Services.AddScoped<AppInterfaces.IAuthService, JwtAuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ActivitiesService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<ISavingFoodService, ActivitiesService>();
builder.Services.AddScoped<IStairsService, ActivitiesService>();
builder.Services.AddScoped<ITransportService, ActivitiesService>();
builder.Services.AddScoped<IVolunteeringService, ActivitiesService>();
builder.Services.AddScoped<IRewardService, RewardsService>();
builder.Services.AddScoped<IRedeemedRewardService, RedeemedRewardsService>();
builder.Services.AddScoped<ICarPoolService, ActivitiesService>();
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
builder.Services.AddScoped<IRewardService, RewardService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();



AuthorizationPolicies.AddPolicies(builder.Services);

builder.Services.AddAuthentication().AddCookie(options =>
{
    options.LoginPath = "/login";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
