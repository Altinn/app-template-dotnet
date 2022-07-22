using System;
using Altinn.App.Core.Interface;
using Microsoft.Extensions.Logging;

namespace Altinn.App.AppLogic;

/// <summary>
/// Implementation of IAppModel used to get and create this applications datamodel.
/// </summary>
public class AppModel : IAppModel
{
    private readonly ILogger<AppModel> _logger;
    
    /// <summary>
    /// Initialize new instance of AppModel
    /// </summary>
    /// <param name="logger">Logger for AppModle</param>
    public AppModel(ILogger<AppModel> logger)
    {
        _logger = logger;
    }
    
    /// <inheritdoc />
    public object Create(string classRef)
    {
        _logger.LogInformation($"CreateNewAppModel {classRef}");

        return Activator.CreateInstance(GetModelType(classRef));
    }

    /// <inheritdoc />
    public Type GetModelType(string classRef)
    {
        _logger.LogInformation($"GetAppModelType {classRef}");

        return Type.GetType(classRef);
    }
}