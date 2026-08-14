using System.Collections.Generic;
using HarmonyLib;
using MGSC;

namespace QOL_bundle.Features.InventoryHotkeys
{
	public static class InventoryHotkeysPatch
	{
		public static InventoryScreen InventoryScreen;
	}

	[HarmonyPatch(typeof(InventoryScreen), nameof(InventoryScreen.Awake))]
	public class InventoryHotkeysPatchInventory
	{
		public static void Postfix(InventoryScreen __instance)
		{
			InventoryHotkeysPatch.InventoryScreen = __instance;
		}
	}
	
	[HarmonyPatch(typeof(ItemSlot),nameof(ItemSlot.Initialize))]
	public class InventoryHotkeysPatchItemSlot
	{
		public static void Postfix(ItemSlot __instance)
		{
			if (__instance.gameObject.TryGetComponent(out InventoryHotkeysBehaviour _))
			{
				return;
			}
			__instance.gameObject.AddComponent<InventoryHotkeysBehaviour>().Configure(__instance, InventoryHotkeysPatch.InventoryScreen);
			Plugin.Logger.Log("InventoryHotkeysPatch applied");
		}
	}
}