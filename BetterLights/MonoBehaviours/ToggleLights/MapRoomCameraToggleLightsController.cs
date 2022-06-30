using BetterSubnautica.Extensions;
using System.Collections;
using UnityEngine;

namespace BetterLights.MonoBehaviours.ToggleLights
{
    public class MapRoomCameraToggleLightsController : AbstractToggleLightsController<MapRoomCamera>
    {
        private EnergyMixin energyMixin = null;

        protected override bool KeyDown => Input.GetKeyDown(Core.MapRoomCameraSettings.LightsButtonToggle);

        public override float EnergyConsumption => Core.MapRoomCameraSettings.LightsConsumption;

        protected override void Awake()
        {
            base.Awake();

            if (component != null)
            {
                if (component != null)
                {
                    StartCoroutine(CreateToggleLightsAsync());
                }
            }
        }

        private IEnumerator CreateToggleLightsAsync()
        {
            var request = CraftData.GetPrefabForTechTypeAsync(TechType.Seamoth);
            yield return request;
            GameObject gameObject = request.GetResult();

            if (gameObject.GetComponent<SeaMoth>() is SeaMoth seamoth)
            {
                energyMixin = component.GetEnergyMixin();

                lightsOnSound = seamoth.toggleLights.lightsOnSound;
                onSound = seamoth.toggleLights.onSound;
                lightsOffSound = seamoth.toggleLights.lightsOffSound;
                offSound = seamoth.toggleLights.offSound;

                Start();
            }
            else
            {
                Destroy(this);
            }
        }

        protected override bool CanToggleLightsActive()
        {
            return base.CanToggleLightsActive() && component.controllingPlayer != null;
        }

        protected override bool IsPowered()
        {
            if (energyMixin != null)
            {
                return energyMixin.charge > 0f;
            }

            return false;
        }

        protected override void ConsumeEnergy(float amount)
        {
            if (energyMixin != null)
            {
                energyMixin.ConsumeEnergy(amount);
            }
        }
    }
}
