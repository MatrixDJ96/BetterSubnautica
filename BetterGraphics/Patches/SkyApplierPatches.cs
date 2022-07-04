#if BELOWZERO
using BetterSubnautica.Extensions;
using HarmonyLib;
using UnityEngine;

namespace BetterGraphics.Patches
{
    [HarmonyPatch(typeof(Base))]
    [HarmonyPatch(nameof(Base.UpdateSkyAppliers))]
    class BaseUpdateSkyAppliersPatch
    {
        static bool Prefix(Base __instance)
        {
            foreach (var component in __instance.GetComponentsInChildren<SkyApplier>())
            {
                if (__instance.IsInside(component.transform.position))
                {
                    SkyEnvironmentChanged.Send(component.gameObject, __instance);
                }
            }

            return false;
        }
    }

    [HarmonyPatch(typeof(SkyApplier))]
    [HarmonyPatch(nameof(SkyApplier.OnEnvironmentChanged))]
    class SkyApplierOnEnvironmentChangedPatch
    {
        static void Postfix(SkyApplier __instance, GameObject environment)
        {
            if (environment != null && environment.GetComponent<Base>() is Base component)
            {
                var baseCellLighting = component.GetCellLightingFor(__instance.transform.position);

                if (baseCellLighting != null)
                {
                    baseCellLighting.RegisterSkyApplier(__instance, true);
                }
            }
        }
    }
}
#endif
