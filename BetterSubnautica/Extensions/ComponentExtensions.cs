using BetterSubnautica.Enums;
using HarmonyLib;
using System;
using UnityEngine;

namespace BetterSubnautica.Extensions
{
    public static class ComponentExtensions
    {
        public static GameObject GetLightsParent<T>(this T __instance) where T : Component
        {
            return __instance.gameObject.transform.Find("lights_parent")?.gameObject;
        }

        public static EnergyInterface GetEnergyInterface<T>(this T __instance) where T : Component
        {
            return __instance.gameObject.GetComponent<EnergyInterface>();
        }

        public static EnergyMixin GetEnergyMixin<T>(this T __instance) where T : Component
        {
            return __instance.gameObject.GetComponent<EnergyMixin>();
        }

        public static PowerRelay GetPowerRelay<T>(this T __instance) where T : Component
        {
            return __instance.gameObject.GetComponent<PowerRelay>();
        }

        public static void CopyValues<T>(this T __instance, T component, CopyType copyType = CopyType.All) where T : Component
        {
            var instanceTraverse = Traverse.Create(__instance);
            var componentTraverse = Traverse.Create(component);

            if (copyType.HasFlag(CopyType.Fields))
            {
                foreach (var field in instanceTraverse.Fields())
                {
                    try
                    {
                        instanceTraverse.Field(field).SetValue(componentTraverse.Field(field).GetValue());
                    }
                    catch (Exception) { }
                }
            }

            if (copyType.HasFlag(CopyType.Properties))
            {
                foreach (var property in instanceTraverse.Properties())
                {
                    try
                    {
                        instanceTraverse.Property(property).SetValue(componentTraverse.Property(property).GetValue());
                    }
                    catch (Exception) { }
                }
            }
        }
    }
}
