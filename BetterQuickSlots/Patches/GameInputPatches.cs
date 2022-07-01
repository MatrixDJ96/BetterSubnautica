using BetterQuickSlots.MonoBehaviours;
using HarmonyLib;
using static GameInput;

namespace BetterQuickSlots.Patches
{
    [HarmonyPatch(typeof(GameInput))]
    [HarmonyPatch(nameof(GameInput.SetBindingInternal), new[] { typeof(Device), typeof(Button), typeof(BindingSet), typeof(int) })]
    class GameInputSetBindingInternalPatch
    {
        static void Postfix(Device device, Button button, BindingSet bindingSet, int inputIndex)
        {
            if (QuickSlotsController.Instance != null)
            {
                QuickSlotsController.Instance.ForceUpdate = true;
            }
        }
    }
}
