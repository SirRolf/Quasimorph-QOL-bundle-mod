using System.Collections.Generic;
using HarmonyLib;
using MGSC;

namespace QOL_bundle.Features.InventoryHotkeys
{
	[HarmonyPatch(typeof(ItemSlot),nameof(ItemSlot.Initialize))]
	public class InventoryHotkeysPatch
	{
		private static readonly Dictionary<ItemSlot, InventoryHotkeysBehaviour> _behaviourInstances = new Dictionary<ItemSlot, InventoryHotkeysBehaviour>();
		
		public static void Postfix(ItemSlot __instance)
		{
			if (_behaviourInstances.ContainsKey(__instance)) return;//gotta do some cleanup here
			_behaviourInstances.Add(__instance, __instance.gameObject.AddComponent<InventoryHotkeysBehaviour>().Configure(__instance.Item));
			Plugin.Logger.Log("InventoryHotkeysPatch applied");
		}
	}
}