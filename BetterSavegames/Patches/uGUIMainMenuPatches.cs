using HarmonyLib;

namespace BetterSavegames.Patches
{
    [HarmonyPatch(typeof(uGUI_MainMenu))]
    [HarmonyPatch(nameof(uGUI_MainMenu.Start))]
    class uGUI_MainMenuStartPatch
    {
        static bool FirstRun { get; set; } = true;

        static void Prefix(uGUI_MainMenu __instance)
        {
            if (Core.Settings.AutoloadLatestSavegame && FirstRun)
            {
                FirstRun = false;

                if (__instance.HasSavedGames())
                {
                    __instance.LoadMostRecentSavedGame();
                }
            }
        }
    }
}
