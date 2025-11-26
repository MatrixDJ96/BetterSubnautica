using System;
using System.IO;
using System.Reflection;
using BepInEx;
using BetterSubnautica.Plugins;

namespace BetterNitrox
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : SubnauticaPlugin
    {
        public static Plugin Core { get; private set; }

#if BELOWZERO_MULTI
        public static event Action BootstrapLoaded;

        public static bool IsBootstrapLoaded => SubnauticaBootstrap.IsLoaded;
#endif

        protected override void Awake()
        {
            Core = this;

            try
            {
#if BELOWZERO_MULTI
                if (!SubnauticaBootstrap.IsLoaded)
                {
                    Logger.LogInfo("Loading Subnautica Bootstrap...");

                    var loaderPath = SubnauticaBootstrap.GetLoaderPath();
                    var apiFilePath = SubnauticaBootstrap.GetAPIFilePath();

                    if (File.Exists(loaderPath) && File.Exists(apiFilePath))
                    {
                        Logger.LogInfo("Patching Subnautica Bootstrap...");

                        var loaderAssembly = Assembly.Load(File.ReadAllBytes(loaderPath!));
                        var apiAssembly = Assembly.Load(File.ReadAllBytes(apiFilePath!));

                        var loader = loaderAssembly.GetType("Subnautica.Loader.Loader");

                        if (loader != null)
                        {
                            Harmony.PatchAll();
                            loader.GetMethod("Run")?.Invoke(null, null);

                            Logger.LogInfo("Subnautica Bootstrap Patched!");

                            SubnauticaBootstrap.IsLoaded = true;
                            BootstrapLoaded?.Invoke();
                        }
                        else
                        {
                            Logger.LogWarning("Unable to patch Subnautica Bootstrap: assembly not found!");
                        }
                    }
                    else
                    {
                        Logger.LogWarning("Unable to patch Subnautica Bootstrap: missing files!");
                    }
                }
                else
                {
                    Logger.LogWarning("Unable to patch Subnautica Bootstrap: already loaded!");
                }
#endif
            }
            catch (Exception e)
            {
                Logger.LogError($"Unable to patch Subnautica Bootstrap: ${e}");
            }
        }
    }
}
