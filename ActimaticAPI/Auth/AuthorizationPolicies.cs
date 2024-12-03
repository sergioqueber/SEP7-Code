using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;

public static class AuthorizationPolicies{
    public static void AddPolicies(IServiceCollection services){
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy("MustBeModerator",a => a.RequireAuthenticatedUser().RequireClaim("Role", "Moderator"));
            options.AddPolicy("MustBeStudent", a => a.RequireAuthenticatedUser().RequireClaim("Role", "Student"));
        });
    }
}