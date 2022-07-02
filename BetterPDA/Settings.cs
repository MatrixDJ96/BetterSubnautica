using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;
using UnityEngine;

namespace BetterPDA
{
    [Menu("Better PDA")]
    public class Settings : ConfigFile
    {
        [Toggle("PDA Pause")]
        public bool EnablePDAPause { get; set; }

        [Keybind("Eat/Use Button")]
        public KeyCode EatUse { get; set; } = KeyCode.Mouse2;
    }
}
