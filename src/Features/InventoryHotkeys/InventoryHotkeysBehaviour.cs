using System;
using MGSC;
using UnityEngine;

namespace QOL_bundle.Features.InventoryHotkeys
{
	public class InventoryHotkeysBehaviour : MonoBehaviour
	{
		private ItemSlot _itemSlot;
		private InventoryScreen _inventoryScreen;

		public void Configure(ItemSlot itemSlot, InventoryScreen inventoryScreen)
		{
			_itemSlot = itemSlot;
			_inventoryScreen =  inventoryScreen;
		}
		
		private void Update()
		{
			if (!Plugin.Config.Hotkeys || !_itemSlot.IsPointerInside || _itemSlot.Item == null) return;
			
			CheckKeyInput(KeyCode.E, UseItem);
			CheckKeyInput(KeyCode.F, DropItem);
			CheckKeyInput(KeyCode.X, DisassembleItem);
		}

		private static void CheckKeyInput(KeyCode keyCode, Action action)
		{
			if (!InputHelper.GetKeyDown(keyCode)) return;
			
			action.Invoke();
		}

		private void UseItem()
		{
			_inventoryScreen.InteractWithCharacter(_itemSlot.Item, true);
		}

		private void DropItem()
		{
			if (_itemSlot.Storage.Source == ItemStorageSource.Floor) return;
			_inventoryScreen.DragControllerDropOutsideCallback(_itemSlot.Item);
			_inventoryScreen.DragControllerInteractionCallback(InventoryInteractionType.DropOutside);
			_inventoryScreen.DragControllerRefreshCallback();
		}

		private void DisassembleItem()
		{
			_inventoryScreen.DisassembleItem(_itemSlot.Item, (short) -1, true);
			_inventoryScreen.TryUnloadWeapon(_itemSlot.Item);
			_inventoryScreen._creatures.Player.CreatureData.EffectsController.PropagateAction(PlayerActionHappened.HandAction);
		}
	}
}