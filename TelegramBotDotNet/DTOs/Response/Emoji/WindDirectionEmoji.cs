namespace TelegramBotDotNet.DTOs.Response.Emoji;

public class WindDirectionEmoji
{
    public static readonly WindDirectionEmoji North = new (char.ConvertFromUtf32(0x2B07), "N");
    public static readonly WindDirectionEmoji Northeast = new(char.ConvertFromUtf32(0x2199), "NE");
    public static readonly WindDirectionEmoji East = new(char.ConvertFromUtf32(0x2B05), "E");
    public static readonly WindDirectionEmoji Southeast = new(char.ConvertFromUtf32(0x2196), "SE");
    public static readonly WindDirectionEmoji South = new(char.ConvertFromUtf32(0x2B06), "S");
    public static readonly WindDirectionEmoji Southwest = new(char.ConvertFromUtf32(0x2197), "SW");
    public static readonly WindDirectionEmoji West = new(char.ConvertFromUtf32(0x27A1), "W");
    public static readonly WindDirectionEmoji Northwest = new (char.ConvertFromUtf32(0x2198), "NW");

    public string Emoji { get; }
    public string Abbreviation { get; }

    private WindDirectionEmoji(string emoji, string abbreviation)
    {
        Emoji = emoji;
        Abbreviation = abbreviation;
    }
}