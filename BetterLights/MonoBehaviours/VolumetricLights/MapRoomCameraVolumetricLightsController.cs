using BetterSubnautica.Enums;
using BetterSubnautica.Extensions;
using System.Collections;
using UnityEngine;

namespace BetterLights.MonoBehaviours.VolumetricLights
{
    public class MapRoomCameraVolumetricLightsController : AbstractVolumetricLightsController<MapRoomCamera>
    {
        protected override void Awake()
        {
            base.Awake();

            if (component != null)
            {
                StartCoroutine(CreateVolumetricLightsAsync());
            }
        }

        private IEnumerator CreateVolumetricLightsAsync()
        {
            var request = CraftData.GetPrefabForTechTypeAsync(TechType.Seamoth);
            yield return request;
            GameObject gameObject = request.GetResult();

            if (gameObject.GetComponent<SeaMoth>() is SeaMoth seamoth)
            {
                var lights = component.gameObject.GetComponentsInChildren<Light>(true);
                var seamothLights = seamoth.gameObject.GetComponentsInChildren<Light>(true);

                var volumetricLights = new VFXVolumetricLight[lights.Length];
                var seamothVolumetricLights = seamoth.volumeticLights;

                if (seamothLights.Length == lights.Length && seamothVolumetricLights.Length == volumetricLights.Length)
                {
                    for (int i = 0; i < lights.Length; i++)
                    {
                        volumetricLights[i] = lights[i].gameObject.GetComponent<VFXVolumetricLight>();

                        if (volumetricLights[i] == null)
                        {
                            volumetricLights[i] = lights[i].gameObject.AddComponent<VFXVolumetricLight>();
                            volumetricLights[i].CopyValues(seamothVolumetricLights[i], CopyType.Fields);
                            volumetricLights[i].lightSource = lights[i];
                            volumetricLights[i].block = null;

                            volumetricLights[i].volumGO = Instantiate(seamothVolumetricLights[i].volumGO, lights[i].transform);

                            volumetricLights[i].Init();
                            volumetricLights[i].InitMaterialBlock();
                            volumetricLights[i].UpdateMaterial();
                            volumetricLights[i].UpdateScale();
                        }
                    }

                    this.volumetricLights = volumetricLights;
                    Start();
                }
            }
        }

        protected override void UpdateSettings()
        {
            IntensityOffset = Core.MapRoomCameraSettings.VolumetricLightIntensityOffset;
        }
    }
}
