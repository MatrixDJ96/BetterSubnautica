#if !SUBNAUTICA_STABLE
using UWE;
#endif

namespace BetterSubnautica.Utility
{
    public static class PDAUtility
    {
#if SUBNAUTICA_STABLE
        public static string FreezeId { get => "PDAPause"; }
#else
        public static FreezeTime.Id FreezeId { get => FreezeTime.Id.PDA; }
#endif
    }
}
