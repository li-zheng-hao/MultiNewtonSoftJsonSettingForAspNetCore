using System.Buffers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;

namespace WebApplication1;

public class SpecificNewtonSoftJsonInputFormatter : NewtonsoftJsonInputFormatter
{
    public SpecificNewtonSoftJsonInputFormatter(string settingsName, ILogger logger, JsonSerializerSettings serializerSettings, ArrayPool<char> charPool, ObjectPoolProvider objectPoolProvider, MvcOptions options, MvcNewtonsoftJsonOptions jsonOptions) : base(logger, serializerSettings, charPool, objectPoolProvider, options, jsonOptions)
    {
        SettingsName = settingsName;
    }
    public string SettingsName { get; }

    public override bool CanRead(InputFormatterContext context)
    {
        if (context.HttpContext.GetJsonSettingsName() != SettingsName)
            return false;

        return base.CanRead(context);
    }

    
}

public class SpecificNewtonSoftJsonOutputFormatter : NewtonsoftJsonOutputFormatter
{
  
    public string SettingsName { get; }

    public override bool CanWriteResult(OutputFormatterCanWriteContext context)
    {
        if (context.HttpContext.GetJsonSettingsName() != SettingsName)
            return false;
            
        return base.CanWriteResult(context);
    }

    public SpecificNewtonSoftJsonOutputFormatter(string settingsName,JsonSerializerSettings serializerSettings, ArrayPool<char> charPool, MvcOptions mvcOptions) : base(serializerSettings, charPool, mvcOptions)
    {
        SettingsName = settingsName;
    }

    public SpecificNewtonSoftJsonOutputFormatter(JsonSerializerSettings serializerSettings, ArrayPool<char> charPool, MvcOptions mvcOptions, MvcNewtonsoftJsonOptions? jsonOptions) : base(serializerSettings, charPool, mvcOptions, jsonOptions)
    {
    }
}