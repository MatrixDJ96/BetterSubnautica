using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BetterSubnautica.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> SafeGetTypes(this Assembly __instance)
        {
            try
            {
                return __instance.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(t => t != null);
            }
        }
    }
}
