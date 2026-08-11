using HarmonyLib;
using MGSC;
using UnityEngine;

namespace QOL_bundle.Features.ClickForStock
{
    [HarmonyPatch(typeof(ConfirmMagnumUpgradeWindow),nameof(ConfirmMagnumUpgradeWindow.Configure))]
    public static class ClickForStockPatch
    {
        public static void Postfix(ConfirmMagnumUpgradeWindow __instance)
        {
            for (int i = 0; i < __instance._rewardsGrid._rewardsRoot.childCount; i++)
            {
                GameObject gameObject = __instance._rewardsGrid._rewardsRoot.GetChild(i).gameObject;//Wish i could add all ItemTooltipHandlers in a list in the RewardsGrid
                string itemId = gameObject.GetComponent<ItemTooltipHandler>()._itemRecord.Id;
                gameObject.AddComponent<ClickItemForStockBehaviour>().Configure(itemId);
            }
        }
    }
}
