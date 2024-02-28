using System.Globalization;
using Newtonsoft.Json;

namespace WebApplication1;

/// <summary>
/// double序列化自动保留两位小数
/// </summary>
public class DoubleTwoDigitalConverter : JsonConverter
{
    private readonly int _digital;

    public DoubleTwoDigitalConverter(int digital = 2)
    {
        _digital = digital;
    }
    public override bool CanConvert(Type objectType)
    {
        return typeof(double).IsAssignableFrom(objectType) || typeof(double?).IsAssignableFrom(objectType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        double value = reader.Value == null ? 0 : Convert.ToDouble(reader.Value);
        if (double.IsPositiveInfinity(value) || double.IsNegativeInfinity(value))
            value = Math.Round(value, _digital, MidpointRounding.AwayFromZero);
        return value;
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        double doubleValue = (double)value;
        string stringValue = doubleValue.ToString($"F{_digital}", CultureInfo.InvariantCulture);
        writer.WriteRawValue(stringValue);
    }
}