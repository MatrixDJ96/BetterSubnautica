using BepInEx;
using BetterSubnautica.Plugins;

// TODO: Gestire Subnautica Nitrox

namespace BetterMap
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : SubnauticaPlugin
    {
        public static Plugin Core { get; private set; }

        protected override void Awake()
        {
            Core = this;

#if BELOWZERO_MULTI
            BetterNitrox.Plugin.BootstrapLoaded += OnBootstrapLoaded;

            if (BetterNitrox.Plugin.IsBootstrapLoaded)
            {
                OnBootstrapLoaded();
            }
#else
            base.Awake();
#endif
        }

#if BELOWZERO_MULTI
        private void OnBootstrapLoaded()
        {
            BetterNitrox.Plugin.BootstrapLoaded -= OnBootstrapLoaded;

            base.Awake();
        }
#endif
    }
}
