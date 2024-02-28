using System.Buffers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace WebApplication1;

public static class ServiceExtension
{
    public static IMvcBuilder AddMultiNewtonsoftJsonOptions(
        this IMvcBuilder builder,
        string settingsName,
        Action<MvcNewtonsoftJsonOptions> configure)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(configure);
        builder.Services.Configure(settingsName, configure);
        builder.Services.AddSingleton<IConfigureOptions<MvcOptions>>(sp =>
        {
            var options = sp.GetRequiredService<IOptionsMonitor<MvcNewtonsoftJsonOptions>>();
            var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
            var charPool = sp.GetRequiredService<ArrayPool<char> >();
            var objectPoolProvider = sp.GetRequiredService<ObjectPoolProvider >();
            var mvcNewtonsoftJsonOptions = sp.GetRequiredService<IOptions<MvcNewtonsoftJsonOptions> >();
                // , ArrayPool<char> charPool, ObjectPoolProvider objectPoolProvider,MvcNewtonsoftJsonOptions newtonsoftJsonOptions
            return new ConfigureMvcJsonOptions(settingsName, options, loggerFactory,charPool,objectPoolProvider,mvcNewtonsoftJsonOptions.Value);
        });
        return builder;
    }
    public static string? GetJsonSettingsName(this HttpContext context)
    {
        var attr=context.GetEndpoint()
            ?.Metadata
            .GetMetadata<JsonSettingsNameAttribute>();
          return attr ?.Name;
    }
}