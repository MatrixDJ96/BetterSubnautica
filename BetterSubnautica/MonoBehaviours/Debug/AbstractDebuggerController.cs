using BetterSubnautica.Enums;
using BetterSubnautica.Utility;
using System.Collections;
using UnityEngine;

namespace BetterSubnautica.MonoBehaviours.Debug
{
    public abstract partial class AbstractDebuggerController<T> : MonoBehaviour where T : Component
    {
        private bool lastEnabled = false;
        public abstract bool ShowDebugInfo { get; }

        private T component;
        protected virtual T Component
        {
            get
            {
                if (component == null)
                {
                    component = gameObject.GetComponent<T>();
                }
                return component;
            }
        }

        protected virtual bool ShowLights { get; } = true;

        protected abstract LightsType LightsType { get; }
        protected abstract bool LightsActive { get; }

        private float energyPerSecond = 0f;
        protected virtual float EnergyPerSecond
        {
            get
            {
                if (energyPerSecond > -0.0001f && energyPerSecond < 0.0001f)
                {
                    energyPerSecond = 0f;
                }
                return energyPerSecond;
            }
            set => energyPerSecond = value;
        }

        protected virtual float LastCapacity { get; set; } = 0f;
        protected abstract float Capacity { get; }

        protected virtual float LastCharge { get; set; } = 0f;
        protected abstract float Charge { get; }

        protected virtual float LastUpdate { get; set; } = 0f;

        protected virtual float PercentCharge => (Charge * 100) / Capacity;

        protected virtual void Start()
        {
            StartCoroutine(StartAsync());
        }

        protected virtual IEnumerator StartAsync()
        {
            yield return new WaitUntil(() => Component != null);

            LastCharge = Charge;
            LastCapacity = Capacity;
            LastUpdate = Time.time;
        }

        protected virtual void Update()
        {
            var showDebugInfo = Core.Settings.ShowDebugInfo && ShowDebugInfo;

            if (showDebugInfo)
            {
                if (LastUpdate > 0f)
                {
                    if (ShowLights)
                    {
                        DebuggerUtility.ShowMessage($"{LightsActive:0.##} ({LightsType})", $"({GetInstanceID()}) {GetType().Name}.LightsStatus");
                    }
                    DebuggerUtility.ShowMessage($"{EnergyPerSecond:+0.####;-0.####;0.####}", $"({GetInstanceID()}) {GetType().Name}.EnergyPerSecond");
                    DebuggerUtility.ShowMessage($"{Charge:0.##}/{Capacity:0.##} ({PercentCharge:0.##}%)", $"({GetInstanceID()}) {GetType().Name}.AvailableEnergy");
                    DebuggerUtility.ShowMessage("", $"({GetInstanceID()}) {GetType().Name}.ZZZ");

                    if (LastUpdate + 1f < Time.time)
                    {
                        EnergyPerSecond = Charge - LastCharge;

                        LastCharge = Charge;
                        LastCapacity = Capacity;
                        LastUpdate = Time.time;
                    }
                }
            }
            else
            {
                if (lastEnabled != showDebugInfo)
                {
                    if (ShowLights)
                    {
                        DebuggerController.Instance.RemoveMessage($"({GetInstanceID()}) {GetType().Name}.LightsStatus");
                    }
                    DebuggerController.Instance.RemoveMessage($"({GetInstanceID()}) {GetType().Name}.EnergyPerSecond");
                    DebuggerController.Instance.RemoveMessage($"({GetInstanceID()}) {GetType().Name}.AvailableEnergy");
                    DebuggerController.Instance.RemoveMessage($"({GetInstanceID()}) {GetType().Name}.ZZZ");
                }
            }

            lastEnabled = showDebugInfo;
        }
    }
}
