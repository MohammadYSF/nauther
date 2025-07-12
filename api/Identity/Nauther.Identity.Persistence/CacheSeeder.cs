using EasyCaching.Core;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nauther.Identity.Application.Features.Auth.Commands.Register;

namespace Nauther.Identity.Persistence;

public class CacheSeeder(IRedisCachingProvider redisCachingProvider, IMediator mediator, IServiceProvider serviceProvider) : IHostedService
{
    private readonly IRedisCachingProvider _redisCachingProvider = redisCachingProvider;
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IMediator _mediator = mediator;
    public Task StartAsync(CancellationToken cancellationToken)
    {

        _redisCachingProvider.HSet("ids:userbasicinform", "123e4567-e89b-12d3-a456-426614174000", """
            {
              "id": "123e4567-e89b-12d3-a456-426614174000",
              "userCode": "USR-001",
              "profileImage": "https://example.com/images/user001.png",
              "username": "lord_of_typo",
              "phoneNumber": "+1234567890",
              "isActive": true
            }

            """);
        var x = new Dima_RegisterUserCommand(
        new Dima_RegisterUserCommand_Dto
        {
            Id = "123e4567-e89b-12d3-a456-426614174000",
            Password = "123!@#qweQWE",
            ConfirmPassword = "123!@#qweQWE",
            Permissions = [],
            Roles = []
        }, false);//or maybe true . don't know :)
        _redisCachingProvider.HSet("ids:userbasicinform", "987f6543-a21b-34c2-b789-526734190abc", """
            {
              "id": "987f6543-a21b-34c2-b789-526734190abc",
              "userCode": "USR-007",
              "profileImage": "https://example.com/images/bond007.png",
              "username": "not_james_bond",
              "phoneNumber": "+447700900123",
              "isActive": false
            }
            
            """);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}