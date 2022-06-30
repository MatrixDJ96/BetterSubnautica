using BetterSubnautica.Extensions;
using System.Collections;
using UnityEngine;

namespace BetterLights.MonoBehaviours.ToggleLights
{
    public class ExosuitToggleLightsController : AbstractToggleLightsController<Exosuit>
    {
        private EnergyInterface energyInterface = null;

        protected override bool KeyDown => Input.GetKeyDown(Core.ExosuitSettings.LightsButtonToggle);

        public override float EnergyConsumption => Core.ExosuitSettings.LightsConsumption;

        protected override void Awake()
        {
            base.Awake();

            if (component != null)
            {
                StartCoroutine(CreateToggleLightsAsync());
            }
        }

        private IEnumerator CreateToggleLightsAsync()
        {
            var request = CraftData.GetPrefabForTechTypeAsync(TechType.Seamoth);
            yield return request;
            GameObject gameObject = request.GetResult();

            if (gameObject.GetComponent<SeaMoth>() is SeaMoth seamoth)
            {
                energyInterface = component.GetEnergyInterface();

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
            return base.CanToggleLightsActive() && component.GetPilotingMode();
        }

        protected override bool IsPowered()
        {
            if (energyInterface != null)
            {
                return energyInterface.hasCharge;
            }

            return false;
        }

        protected override void ConsumeEnergy(float amount)
        {
            if (energyInterface != null)
            {
                energyInterface.ConsumeEnergy(amount);
            }
        }
    }
}
