#if !SUBNAUTICA_STABLE
using HarmonyLib;

namespace BetterPDA.Patches
{
    [HarmonyPatch(typeof(GameSettings))]
    [HarmonyPatch(nameof(GameSettings.SerializeSettings))]
    class GameSettingsSerializeSettingsPatch
    {
        static void Prefix(GameSettings __instance, GameSettings.ISerializer serializer)
        {
            MiscSettings.pdaPause = Core.Settings.EnablePDAPause;
        }
    }
}
#endif
