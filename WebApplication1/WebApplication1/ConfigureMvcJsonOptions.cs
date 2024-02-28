using System.Buffers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebApplication1;

public class ConfigureMvcJsonOptions : IConfigureOptions<MvcOptions>
{
    private readonly string _jsonSettingsName;
    private readonly IOptionsMonitor<MvcNewtonsoftJsonOptions> _jsonOptions;
    private readonly ILoggerFactory _loggerFactory;
    private readonly ArrayPool<char> _charPool;
    private readonly ObjectPoolProvider _objectPoolProvider;
    private readonly MvcNewtonsoftJsonOptions _newtonsoftJsonOptions;

    public ConfigureMvcJsonOptions(
        string jsonSettingsName,
        IOptionsMonitor<MvcNewtonsoftJsonOptions> jsonOptions,
        ILoggerFactory loggerFactory, ArrayPool<char> charPool, ObjectPoolProvider objectPoolProvider,MvcNewtonsoftJsonOptions newtonsoftJsonOptions)
    {
        _jsonSettingsName = jsonSettingsName;
        _jsonOptions = jsonOptions;
        _loggerFactory = loggerFactory;
        _charPool = charPool;
        _objectPoolProvider = objectPoolProvider;
        _newtonsoftJsonOptions = newtonsoftJsonOptions;
    }

    public void Configure(MvcOptions options)
    {
        var jsonOptions = _jsonOptions.Get(_jsonSettingsName);
        var logger = _loggerFactory.CreateLogger<SpecificNewtonSoftJsonInputFormatter>();
        options.InputFormatters.Insert(
            0,
            new SpecificNewtonSoftJsonInputFormatter(
                _jsonSettingsName,logger,jsonOptions.SerializerSettings,_charPool,_objectPoolProvider,options,_newtonsoftJsonOptions));
        options.OutputFormatters.Insert(
            0,
            new SpecificNewtonSoftJsonOutputFormatter(
                _jsonSettingsName,jsonOptions.SerializerSettings,_charPool,options));
    }
}