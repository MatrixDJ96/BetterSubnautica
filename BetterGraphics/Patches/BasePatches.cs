#if BELOWZERO
using BetterSubnautica.Extensions;
using HarmonyLib;

namespace BetterGraphics.Patches
{
    [HarmonyPatch(typeof(Base))]
    [HarmonyPatch(nameof(Base.UpdateSkyAppliers))]
    class BaseUpdateSkyAppliersPatch
    {
        static bool Prefix(Base __instance)
        {
            foreach (var component in __instance.gameObject.GetComponentsInChildren<SkyApplier>())
            {
                // Send environment only to objects inside base
                if (component != null && __instance.GetCellIndex(component.transform.position) > -1)
                {
                    SkyEnvironmentChanged.Send(component.gameObject, __instance);
                }
            }

            return false;
        }
    }
}
#endif
