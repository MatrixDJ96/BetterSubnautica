namespace BetterSubnautica.Extensions
{
    public static class VehicleExtensions
    {
        public static bool HasStorage(this Vehicle __instance, TechType techType)
        {
            for (int i = 0; i < __instance.GetSlotCount(); i++)
            {
                if (__instance.GetStorageInSlot(i, techType) is ItemsContainer)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
