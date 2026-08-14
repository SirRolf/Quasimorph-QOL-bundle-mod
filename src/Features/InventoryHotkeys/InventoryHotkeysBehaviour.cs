using System;
using MGSC;
using UnityEngine;
using UnityEngine.EventSystems;

namespace QOL_bundle.Features.InventoryHotkeys
{
	public class InventoryHotkeysBehaviour : MonoBehaviour
	{
		private ItemSlot _itemSlot;
		private InventoryScreen _inventoryScreen;

		public InventoryHotkeysBehaviour Configure(ItemSlot itemSlot, InventoryScreen inventoryScreen)
		{
			_itemSlot = itemSlot;
			_inventoryScreen =  inventoryScreen;
			return this;
		}
		
		private void Update()
		{
			if (_itemSlot.IsPointerInside && _itemSlot.Item != null && InputHelper.GetKeyDown(KeyCode.E))//probably should change to a keybind but not sure how to do that yet
				_inventoryScreen.InteractWithCharacter(_itemSlot.Item, true);
		}
	}
}