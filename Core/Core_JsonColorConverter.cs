using System.Text.Json;
using System.Text.Json.Serialization;

namespace MTM_WIP_Application_Winforms.Core;

/// <summary>
/// JSON converter for nullable Color types
/// </summary>
public class JsonColorConverter : JsonConverter<Color?>
{
    #region JsonConverter Implementation

    /// <summary>
    /// Read Color value from JSON
    /// </summary>
    /// <param name="reader">JSON reader</param>
    /// <param name="typeToConvert">Type being converted</param>
    /// <param name="options">Serializer options</param>
    /// <returns>Nullable Color value</returns>
    public override Color? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            // Handle null token type explicitly
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            var colorString = reader.GetString();
            if (string.IsNullOrWhiteSpace(colorString)) return null;
            
            if (colorString.Equals("Transparent", StringComparison.OrdinalIgnoreCase))
                return Color.Transparent;
            
            // Validate color string before parsing to prevent MySql.Data NullReferenceException
            if (!colorString.StartsWith("#") && !colorString.StartsWith("rgb", StringComparison.OrdinalIgnoreCase))
            {
                // Try to convert named colors
                try
                {
                    return Color.FromName(colorString);
                }
                catch
                {
                    return null;
                }
            }
            
            return ColorTranslator.FromHtml(colorString);
        }
        catch (Exception ex)
        {
            // Log but don't throw - return null for invalid colors
            System.Diagnostics.Debug.WriteLine($"[JsonColorConverter] Failed to parse color: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Write Color value to JSON
    /// </summary>
    /// <param name="writer">JSON writer</param>
    /// <param name="value">Color value to write</param>
    /// <param name="options">Serializer options</param>
    public override void Write(Utf8JsonWriter writer, Color? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
            writer.WriteStringValue(ColorTranslator.ToHtml(value.Value));
        else
            writer.WriteNullValue();
    }

    #endregion
}