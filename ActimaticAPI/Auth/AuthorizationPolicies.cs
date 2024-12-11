using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.Authorization;

public static class AuthorizationPolicies
{
    public static void AddPolicies(IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy("MustBeModerator", a => a.RequireAuthenticatedUser().RequireClaim("Role", "Moderator"));
            options.AddPolicy("MustBeStudent", a => a.RequireAuthenticatedUser().RequireClaim("Role", "Student"));
            options.AddPolicy("MustBeEmployee", a =>
               a.RequireAuthenticatedUser().RequireClaim("Role", "Employee"));
            options.AddPolicy("MustBeHr", a => a.RequireAuthenticatedUser().RequireClaim("Role", "Hr"));
            options.AddPolicy("IsApproved", a => a.RequireAuthenticatedUser().RequireClaim("IsApproved", "True"));
        });
    }
}