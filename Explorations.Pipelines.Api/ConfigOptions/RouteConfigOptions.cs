using Microsoft.Extensions.Options;

namespace Explorations.Pipelines.Api.ConfigOptions;

public class RouteConfigOptions : IConfigureOptions<RouteOptions>
{
    public void Configure(RouteOptions options)
    {
        options.LowercaseUrls = true;
    }
}