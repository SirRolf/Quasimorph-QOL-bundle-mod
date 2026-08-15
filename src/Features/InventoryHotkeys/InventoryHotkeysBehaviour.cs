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
			if (!Plugin.Config.Hotkeys) return;
			if (!_itemSlot.IsPointerInside || _itemSlot.Item == null) return;
			if (InputHelper.GetKeyDown(Plugin.Config.HotkeyUse))
			{
				_inventoryScreen.InteractWithCharacter(_itemSlot.Item, true);
			}

			if (_itemSlot.Storage.Source != ItemStorageSource.Floor && InputHelper.GetKeyDown(Plugin.Config.HotkeyDrop))
			{
				_inventoryScreen.DragControllerDropOutsideCallback(_itemSlot.Item);
				_inventoryScreen.DragControllerInteractionCallback(InventoryInteractionType.DropOutside);
				_inventoryScreen.DragControllerRefreshCallback();
			}

			if (InputHelper.GetKeyDown(Plugin.Config.HotkeyDisassemble))
			{
				_inventoryScreen.DisassembleItem(_itemSlot.Item, (short) -1, true);
				_inventoryScreen.TryUnloadWeapon(_itemSlot.Item);
				_inventoryScreen._creatures.Player.CreatureData.EffectsController.PropagateAction(PlayerActionHappened.HandAction);
			}

		}
	}
}