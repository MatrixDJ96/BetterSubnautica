namespace BetterSubnautica.Utility
{
    public static class InventoryUtility
    {
#if BELOWZERO
        public static ItemAction GetEatUseItemAction(InventoryItem item)
        {
            var result = ItemAction.None;

            if (item != null)
            {
                var pickupable = item.item;
                var techType = pickupable.GetTechType();

                GameModeUtils.GetGameMode(out var mode, out var _);

                var survival = !GameModeUtils.IsOptionActive(mode, GameModeOption.NoSurvival);
                var oxygen = !GameModeUtils.IsOptionActive(mode, GameModeOption.NoOxygen);
                var cold = !GameModeUtils.IsOptionActive(mode, GameModeOption.NoCold);

                if (pickupable != null && pickupable.GetComponentInParent<Planter>() == null && pickupable.GetComponent<Eatable>() is Eatable eatable)
                {
                    if (survival || (oxygen && techType == TechType.Bladderfish) || (cold && eatable.coldMeterValue < 0f))
                    {
                        result |= ItemAction.Eat;
                    }
                }

                if (techType == TechType.FirstAidKit || techType == TechType.WaterPurificationTablet)
                {
                    result |= ItemAction.Use;
                }
            }

            return result;
        }
#endif
    }
}
