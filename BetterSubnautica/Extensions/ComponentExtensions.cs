using BetterSubnautica.Components;
using BetterSubnautica.Enums;
using HarmonyLib;
using System;
using UnityEngine;

namespace BetterSubnautica.Extensions
{
    public static class ComponentExtensions
    {
        public static ToggleLights GetToggleLights<T>(this T __instance) where T : Component
        {
            return __instance.gameObject.GetComponentInChildren<ToggleLights>();
        }
        public static GameObject GetLightsParent<T>(this T __instance) where T : Component
        {
            if (__instance.gameObject.transform.Find("lights_parent") is Transform transform)
            {
                return transform.gameObject;
            }

            return null;
        }

        public static EnergyMixin GetEnergyMixin<T>(this T __instance) where T : Component
        {
            return __instance.gameObject.GetComponent<EnergyMixin>();
        }
        public static EnergyInterface GetEnergyInterface<T>(this T __instance) where T : Component
        {
            return __instance.gameObject.GetComponent<EnergyInterface>();
        }

        public static PowerRelay GetPowerRelay<T>(this T __instance) where T : Component
        {
            return __instance.gameObject.GetComponent<PowerRelay>();
        }

        public static IEnergySource GetEnergySource<T>(this T __instance) where T : Component
        {
            if (__instance.GetEnergyMixin() is EnergyMixin energyMixin)
            {
                return new EnergyMixinSource() { Component = energyMixin };
            }

            if (__instance.GetEnergyInterface() is EnergyInterface energyInterface)
            {
                return new EnergyInterfaceSource() { Component = energyInterface };
            }

            if (__instance.GetPowerRelay() is PowerRelay powerRelay)
            {
                return new PowerRelaySource() { Component = powerRelay };
            }

            return null;
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
