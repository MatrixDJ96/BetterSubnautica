#if BELOWZERO
using BetterSubnautica.Attributes;
using BetterSubnautica.Extensions;
using HarmonyLib;
using System.Reflection;
using UnityEngine;

namespace BetterSubnautica.Patches
{
    [PrePatch]
    [HarmonyPatch(typeof(EarlyAccessDisclaimer))]
    [HarmonyPatch(nameof(EarlyAccessDisclaimer.SetText))]
    class EarlyAccessDisclaimerSetTextPatch
    {
        static void Postfix(EarlyAccessDisclaimer __instance)
        {
            __instance.text.text =
                $"<size=25>" +
                    $"Modded with <size=35><color={RandomHexColor()}><b>BetterSubnautica</b></size>\n" +
                    $"v{Assembly.GetExecutingAssembly().GetName().Version}</color>\n\n" +
                    $"Created by <size=30><color={RandomHexColor()}><b>MatrixDJ96</b></color></size>" +
                $"</size>";
        }

        private static string RandomHexColor()
        {
            return "#" + Random.RandomRange(0, 255).ToHex() + Random.RandomRange(0, 255).ToHex() + Random.RandomRange(0, 255).ToHex() + "FF";
        }
    }
}
#endif
