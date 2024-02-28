namespace WebApplication1;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class JsonSettingsNameAttribute : Attribute
{
    public JsonSettingsNameAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}