using HarmonyLib;
using MGSC;

namespace QOL_bundle.Features.ClickForStock
{
    [HarmonyPatch(typeof(ConfirmMagnumUpgradeWindow),nameof(ConfirmMagnumUpgradeWindow.Configure))]
    public static class ClickForStockPatch
    {
        public static void Postfix(MagnumPerkRecord perkRecord,ConfirmMagnumUpgradeWindow __instance)
        {
            for (int i = 0; i < __instance._rewardsGrid._rewardsRoot.childCount; i++)
            {
                __instance._rewardsGrid._rewardsRoot.GetChild(i).gameObject.AddComponent<ClickItemForStockBehaviour>().Configure(perkRecord.UpgradePrice[i]);//using index here can be somewhat unsafe but i don't know of an alternative. Feel free to suggest one
            }
        }
    }
}
