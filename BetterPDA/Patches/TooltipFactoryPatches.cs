#if BELOWZERO
using BetterSubnautica.Utility;
using HarmonyLib;
using System.Text;

namespace BetterPDA.Patches
{
    [HarmonyPatch(typeof(TooltipFactory))]
    [HarmonyPatch(nameof(TooltipFactory.ItemActions))]
    class TooltipFactoryItemActionsPatch
    {
        static void Postfix(StringBuilder sb, InventoryItem item)
        {
            if (Inventory.main is Inventory inventory && item != null)
            {
                if ((inventory.GetAllItemActions(item) & ItemAction.Eat) == ItemAction.None)
                {
                    var itemAction = InventoryUtility.GetEatUseItemAction(item);

                    if ((itemAction & ItemAction.Eat) != ItemAction.None)
                    {
                        TooltipFactory.WriteAction(sb, KeyCodeUtility.GetName(Core.Settings.EatUse), TooltipFactory.GetUseActionString(ItemAction.Eat));
                    }

                    if ((itemAction & ItemAction.Use) != ItemAction.None)
                    {
                        TooltipFactory.WriteAction(sb, KeyCodeUtility.GetName(Core.Settings.EatUse), TooltipFactory.GetUseActionString(ItemAction.Use));
                    }
                }
                else
                {
                    var lines = sb.ToString().Split('\n');
                    sb.Clear();

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].Contains(TooltipFactory.stringButton0) && (lines[i].Contains(TooltipFactory.stringEat) || lines[i].Contains(TooltipFactory.stringUse)))
                        {
                            lines[i] = lines[i].Replace(TooltipFactory.stringButton0, TooltipFactory.stringButton0 + "/" + KeyCodeUtility.GetName(Core.Settings.EatUse));
                        }

                        if (sb.Length > 0)
                        {
                            sb.Append('\n');
                        }
                        sb.Append(lines[i]);
                    }
                }
            }
        }
    }
}
#endif
