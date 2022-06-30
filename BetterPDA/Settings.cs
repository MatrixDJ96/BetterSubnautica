using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;

namespace BetterPDA
{
    [Menu("Better PDA")]
    public class Settings : ConfigFile
    {
        [Toggle("PDA Pause")]
        public bool EnablePDAPause { get; set; }
    }
}
