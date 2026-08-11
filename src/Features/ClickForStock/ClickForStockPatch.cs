using HarmonyLib;
using MGSC;

namespace QOL_bundle.Features.ClickForStock
{
    [HarmonyPatch(typeof(TooltipItemIcon),nameof(TooltipItemIcon.Initialize))]
    public static class ClickForStockPatch
    {
        public static void Prefix(string itemId, TooltipItemIcon __instance)
        {
	        __instance.gameObject.AddComponent<ClickItemForStockBehaviour>().Configure(itemId);//probably use MGSC.CommonButton
        }
    }
}
