using BetterSubnautica.Enums;
using BetterSubnautica.Utility;
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

        protected virtual void OnDisable()
        {
            DeleteMessages();
        }

        protected virtual void OnDestroy()
        {
            DeleteMessages();
        }

        protected virtual void Awake()
        {
            if (Component == null)
            {
                Destroy(this);
                return;
            }

            UpdateInfo(false);
        }

        protected virtual void Update()
        {
            var showDebugInfo = Core.Settings.ShowDebugInfo && ShowDebugInfo;

            if (showDebugInfo)
            {
                ShowMessages();

                if (LastUpdate + 1f < Time.time)
                {
                    UpdateInfo();
                }
            }
            else
            {
                if (lastEnabled != showDebugInfo)
                {
                    DeleteMessages();
                }
            }

            lastEnabled = showDebugInfo;
        }

        protected virtual void UpdateInfo(bool withEnergy = true)
        {
            if (withEnergy)
            {
                EnergyPerSecond = Charge - LastCharge;
            }

            LastCharge = Charge;
            LastCapacity = Capacity;
            LastUpdate = Time.time;
        }

        protected virtual void ShowMessages()
        {
            if (ShowLights)
            {
                DebuggerUtility.ShowMessage($"{LightsActive:0.##} ({LightsType})", $"({GetInstanceID()}) {GetType().Name}.LightsStatus");
            }

            DebuggerUtility.ShowMessage($"{EnergyPerSecond:+0.####;-0.####;0.####}", $"({GetInstanceID()}) {GetType().Name}.EnergyPerSecond");
            DebuggerUtility.ShowMessage($"{Charge:0.##}/{Capacity:0.##} ({PercentCharge:0.##}%)", $"({GetInstanceID()}) {GetType().Name}.AvailableEnergy");
            DebuggerUtility.ShowMessage("", $"({GetInstanceID()}) {GetType().Name}.ZZZ");
        }

        protected virtual void DeleteMessages()
        {
            if (ShowLights)
            {
                DebuggerUtility.RemoveMessage($"({GetInstanceID()}) {GetType().Name}.LightsStatus");
            }

            DebuggerUtility.RemoveMessage($"({GetInstanceID()}) {GetType().Name}.EnergyPerSecond");
            DebuggerUtility.RemoveMessage($"({GetInstanceID()}) {GetType().Name}.AvailableEnergy");
            DebuggerUtility.RemoveMessage($"({GetInstanceID()}) {GetType().Name}.ZZZ");
        }
    }
}
