#if BELOWZERO
using BetterSubnautica.Attributes;
using HarmonyLib;
using System.Reflection;

namespace BetterSubnautica.Patches
{
    [PrePatch]
    [HarmonyPatch(typeof(EarlyAccessDisclaimer))]
    [HarmonyPatch(nameof(EarlyAccessDisclaimer.SetText))]
    class EarlyAccessDisclaimerSetTextPatch
    {
        static void Postfix(EarlyAccessDisclaimer __instance)
        {
            __instance.text.text = "Modded with <color=#3399ffff><b>BetterSubnautica v" + Assembly.GetExecutingAssembly().GetName().Version + "<b></color>\n\nCreated by <color=#ff0000ff><b>MatrixDJ96<b></color>";
        }
    }
}
#endif