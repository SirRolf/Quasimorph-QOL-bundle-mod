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
		private static readonly Dictionary<ItemSlot, InventoryHotkeysBehaviour> _behaviourInstances = new Dictionary<ItemSlot, InventoryHotkeysBehaviour>();
		
		public static void Postfix(ItemSlot __instance)
		{
			if (_behaviourInstances.ContainsKey(__instance)) return;//gotta do some cleanup here
			_behaviourInstances.Add(__instance, __instance.gameObject.AddComponent<InventoryHotkeysBehaviour>().Configure(__instance.Item, InventoryHotkeysPatch.InventoryScreen));
			Plugin.Logger.Log("InventoryHotkeysPatch applied");
		}
	}
}