using System;
using Altinn.App.Core.Interface;

namespace Altinn.App;

/// <summary>
/// Implementation of IAppModel used to get and create this applications datamodel.
/// </summary>
public class AppModel : IAppModel
{
    /// <inheritdoc />
    public object Create(string classRef)
    {
        return Activator.CreateInstance(GetModelType(classRef));
    }

    /// <inheritdoc />
    public Type GetModelType(string classRef)
    {
        return Type.GetType(classRef);
    }
}
