#if BELOWZERO
using BetterSubnautica.Utility;
using HarmonyLib;
using UnityEngine;

namespace BetterPDA.Patches
{
    [HarmonyPatch(typeof(uGUI_InventoryTab))]
    [HarmonyPatch(nameof(uGUI_InventoryTab.OnUpdate))]
    class uGUIInventoryTabOnButtonDownPatch
    {
        static void Postfix(uGUI_InventoryTab __instance, bool isOpen)
        {
            if (isOpen && ItemDragManager.hoveredItem is InventoryItem item && Input.GetKeyDown(Core.Settings.EatUse))
            {
                if (InventoryUtility.GetEatUseItemAction(item) is ItemAction itemAction && itemAction != ItemAction.None)
                {
                    var pickupable = item.item;
                    var inventory = Inventory.main;
                    var survival = Player.main.GetComponent<Survival>();

                    if (pickupable != null && inventory != null && survival != null)
                    {
                        switch (itemAction)
                        {
                            case ItemAction.Eat:
                                if (survival.Eat(pickupable.gameObject))
                                {
                                    inventory.TryRemoveItem(pickupable);
                                    Object.Destroy(pickupable.gameObject);
                                }
                                break;
                            case ItemAction.Use:
                                if (survival.Use(pickupable.gameObject, inventory))
                                {
                                    inventory.TryRemoveItem(pickupable);
                                    Object.Destroy(pickupable.gameObject);
                                }
                                break;
                        }
                    }
                }
            }
        }
    }
}
#endif
