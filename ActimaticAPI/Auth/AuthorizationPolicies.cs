using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;

public static class AuthorizationPolicies{
    public static void AddPolicies(IServiceCollection services){
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy("MustBeModerator",a => a.RequireAuthenticatedUser().RequireClaim("Role", "Moderator"));
            options.AddPolicy("MustBeAdmin", a => a.RequireAuthenticatedUser().RequireClaim("Role", "Admin"));
            options.AddPolicy("MustBeHr", a => a.RequireAuthenticatedUser().RequireClaim("Role", "Hr"));
        });
    }
}