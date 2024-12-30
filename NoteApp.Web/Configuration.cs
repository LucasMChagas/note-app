using MudBlazor;
using MudBlazor.Utilities;

namespace NoteApp.Web;

public static class Configuration
{
    public const string ApiBaseUrl = "http://localhost:5164";
    public const string HttpClientName = "NoteApp";
    public static bool IsDarkMode { get; set; } = false;
    public static readonly MudTheme Theme = new()
    {
        Typography = new Typography()
        {
            Default = new Default()
            {
                FontFamily = ["Ubuntu", "sans-serif"],
            }
        },
        PaletteLight = new PaletteLight()
        {
            Primary = new MudColor("#3B0A77"),
            Secondary = new MudColor("#e0e0e0"),
            Tertiary = new MudColor("#6310C0"),
            AppbarBackground = new MudColor("#e0e0e0"),
            Background = new MudColor("#3B0A77"),
            TextPrimary = new MudColor("3B0A77"),
            TextSecondary = new MudColor("3B0A77"),
            PrimaryContrastText = new MudColor("#e0e0e0"),
            
            
        },
        PaletteDark = new PaletteDark()
        {
            Primary = new MudColor("#e0e0e0"),
            Secondary = new MudColor("#3B0A77"),
            AppbarBackground = new MudColor("#3B0A77"),
            Background = new MudColor("#e0e0e0"),
            TextPrimary = new MudColor(Colors.Shades.Black),
            TextSecondary = new MudColor("#e0e0e0"),
            PrimaryContrastText = new MudColor("#3B0A77"),
            Surface = new MudColor("#3B0A77"),
        }
    };
}