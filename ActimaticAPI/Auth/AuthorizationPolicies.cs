using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.Authorization;

public static class AuthorizationPolicies{
    public static void AddPolicies(IServiceCollection services){
        services.AddAuthorizationCore(options =>
        {
          options.AddPolicy("MustBeManager", a =>
                a.RequireAuthenticatedUser().RequireClaim("Role", "Manager"));

            options.AddPolicy("MustBeEmployee", a =>
                a.RequireAuthenticatedUser().RequireClaim("Role", "Employee"));
        });
    }
}