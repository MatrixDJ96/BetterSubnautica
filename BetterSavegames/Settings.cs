using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;

namespace BetterSavegames
{
    [Menu("Better Savegames")]
    public class Settings : ConfigFile
    {
        [Toggle("Autoload Latest Savegame")]
        public bool AutoloadLatestSavegame { get; set; } = true;

        [Toggle("Maximize Loading Speed", Tooltip = "Unlock FPS while loading to maximize speed")]
        public bool MaximizeLoadingSpeed { get; set; } = true;
    }
}
