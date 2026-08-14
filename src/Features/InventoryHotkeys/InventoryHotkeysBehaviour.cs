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
			if (_itemSlot.IsPointerInside && _itemSlot.Item != null) //probably should change to a keybind but not sure how to do that yet
			{
				if (InputHelper.GetKeyDown(KeyCode.E))
				{
					_inventoryScreen.InteractWithCharacter(_itemSlot.Item, true);
				}

				if (_itemSlot.Storage.Source != ItemStorageSource.Floor && InputHelper.GetKeyDown(KeyCode.F))
				{
					_inventoryScreen.DragControllerDropOutsideCallback(_itemSlot.Item);
					_inventoryScreen.DragControllerInteractionCallback(InventoryInteractionType.DropOutside);
					_inventoryScreen.DragControllerRefreshCallback();
				}
			}

		}
	}
}