using System.Reflection;
using auther.Identity.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Nauther.Identity.Application.Services.Implementations;
using Nauther.Identity.Application.Services.Interfaces;
using Nauther.Identity.Domain.ExternalContract;

namespace Nauther.Identity.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddScoped<IUserService, UserService<External_AIED_UserModel>>();
        services.AddScoped<IRoleService, RoleService<External_AIED_UserModel>>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IGroupService, GroupService>();
        services.AddScoped<IUserRoleService, UserRoleService>();
        services.AddScoped<IUserGroupService, UserGroupService>();
        services.AddScoped<IRolePermissionService, RolePermissionService>();
        services.AddScoped<IUserPermissionService, UserPermissionService>();
        services.AddScoped<IGroupPermissionService, GroupPermissionService>();
        services.AddScoped<IOtpService, OtpService>();
        
        return services;
    }
}