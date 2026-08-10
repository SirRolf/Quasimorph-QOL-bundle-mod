using HarmonyLib;
using MGSC;

namespace QOL_bundle.Features.ClickForStock
{
    [HarmonyPatch(typeof(RewardsGrid),nameof(RewardsGrid.AddIconToGrid))]
    public static class ClickForStockPatch
    {
        public static void Prefix(TooltipItemIcon icon, MainMenuScreen __instance)
        {
            icon.gameObject.AddComponent<ClickItemForStockBehaviour>();//probably use MGSC.CommonButton
        }
    }
}
