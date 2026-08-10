using HarmonyLib;
using MGSC;

namespace QOL_bundle.Features
{
    [HarmonyPatch(typeof(RewardsGrid),"AddIconToGrid")]
    public static class ClickItemForStock
    {
        static void Postfix(TooltipItemIcon test)
        {
            test.MakeRed();//really just for testing to see if the mod gets loaded
        }
    }
}
