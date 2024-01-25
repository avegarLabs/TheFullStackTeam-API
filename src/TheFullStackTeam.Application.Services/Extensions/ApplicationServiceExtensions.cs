using Microsoft.Extensions.DependencyInjection;
using TheFullStackTeam.Application.Services.Abstract;

namespace TheFullStackTeam.Application.Services.Extensions;

public static class ApplicationServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.Scan(scan =>
            scan.FromAssembliesOf(typeof(BaseService))
                .AddClasses(classes => classes.AssignableTo(typeof(IService)))
                .AsImplementedInterfaces().WithTransientLifetime());
    }
}