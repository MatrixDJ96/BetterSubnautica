using BetterLights.MonoBehaviours.VolumetricLights;
using HarmonyLib;
using System;
using UnityEngine;

namespace BetterLights.Patches
{
    [HarmonyPatch(typeof(VFXVolumetricLight))]
    [HarmonyPatch(nameof(VFXVolumetricLight.UpdateMaterial), new[] { typeof(bool) })]
    class VFXVolumetricLightUpdateMaterialPatch
    {
        static bool Prefix(VFXVolumetricLight __instance, bool forceUpdate)
        {
            VolumetricLightsContainer.Dict.TryGetValue(__instance.GetInstanceID(), out var volumetricLightsController);

            if (volumetricLightsController != null)
            {
                volumetricLightsController.UpdateMaterial(__instance, forceUpdate);
            }

            return volumetricLightsController == null;
        }
    }

    [HarmonyPatch(typeof(VFXVolumetricLight))]
    [HarmonyPatch(nameof(VFXVolumetricLight.UpdateScale))]
    class VFXVolumetricLightUpdateScalePatch
    {
        static bool Prefix(VFXVolumetricLight __instance)
        {
            if (__instance.gameObject.GetComponentInParent<Exosuit>() != null)
            {
                float scale = Mathf.Tan((float)Math.PI / 180f * __instance.lightSource.spotAngle / 2f) * __instance.lightSource.range * 2f;
                __instance.volumGO.transform.localScale = new Vector3(scale, scale, __instance.lightSource.range);

                return false;
            }

            return true;
        }
    }
}
