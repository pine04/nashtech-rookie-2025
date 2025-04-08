using System.ComponentModel;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace aspnetcoreapi2.Models;

[JsonConverter(typeof(GenderSerializer))]
[TypeConverter(typeof(GenderTypeConverter))]
public enum Gender
{
    Male,
    Female,
    NonBinary
}

public class GenderSerializer : JsonConverter<Gender>
{
    public override Gender Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string value = reader.GetString();

        return value switch
        {
            "Male" => Gender.Male,
            "Female" => Gender.Female,
            "Non-binary" => Gender.NonBinary,
            _ => throw new JsonException($"{value} is not a valid gender value. Valid genders: 'Male', 'Female', 'Non-binary'.")
        };
    }

    public override void Write(Utf8JsonWriter writer, Gender value, JsonSerializerOptions options)
    {
        var stringValue = value switch
        {
            Gender.Male => "Male",
            Gender.Female => "Female",
            Gender.NonBinary => "Non-binary",
            _ => throw new JsonException("Unknown gender value.")
        };

        writer.WriteStringValue(stringValue);
    }
}

public class GenderTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        => sourceType == typeof(string);

    public override object ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        var str = value?.ToString();

        return str switch
        {
            "Male" => Gender.Male,
            "Female" => Gender.Female,
            "Non-binary" => Gender.NonBinary,
            _ => throw new ArgumentException($"{value} is not a valid gender value. Valid genders: 'Male', 'Female', 'Non-binary'.")
        };
    }
}