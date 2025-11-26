using BepInEx;
using BetterSubnautica.Plugins;
using Nautilus.Handlers;

namespace BetterSubnautica
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : SubnauticaPlugin
    {
        public Settings Settings { get; } = OptionsPanelHandler.RegisterModOptions<Settings>();
        
        public static Plugin Core { get; private set; }
        
        protected override void Awake()
        {
            Core = this;
            base.Awake();
        }
    }
}
