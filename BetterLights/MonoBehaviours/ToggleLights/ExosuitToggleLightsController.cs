using System.Collections;
using UnityEngine;

namespace BetterLights.MonoBehaviours.ToggleLights
{
    public class ExosuitToggleLightsController : AbstractToggleLightsController<Exosuit>
    {
        public override bool MandatoryToggleLights { get; } = false;

        public override bool KeyDown => Input.GetKeyDown(Core.ExosuitSettings.LightsButtonToggle);

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
            SeaMoth seamoth = request.GetResult().GetComponent<SeaMoth>();

            if (seamoth == null || !InitializeToggleLights(seamoth))
            {
                Destroy(this);
                yield break;
            }

            Start();
        }

        public override bool CanToggleLightsActive()
        {
            return base.CanToggleLightsActive() && component.GetPilotingMode();
        }
    }
}
