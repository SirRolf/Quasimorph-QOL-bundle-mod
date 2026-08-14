using System;
using MGSC;
using UnityEngine;
using UnityEngine.EventSystems;

namespace QOL_bundle.Features.InventoryHotkeys
{
	public class InventoryHotkeysBehaviour : MonoBehaviour ,IPointerEnterHandler, IPointerExitHandler
	{
		public BasePickupItem _item { private get; set; }
		private InventoryScreen _inventoryScreen;
		
		private bool _isHovering;

		public InventoryHotkeysBehaviour Configure(BasePickupItem item, InventoryScreen inventoryScreen)
		{
			//Plugin.Logger.Log($"item: {item},InventoryScreen: {inventoryScreen}");
			_item = item;
			_inventoryScreen =  inventoryScreen;
			return this;
		}
		
		private void Update()
		{
			if (_isHovering && InputHelper.GetKeyDown(KeyCode.E))//probably should change to a keybind but not sure how to do that yet
			{
				UsableItemComponent usableItemComponent = _item.Comp<UsableItemComponent>();
				//Plugin.Logger.Log($"Trying to consume: {_item.Id} amount before: {_item.StackCount}. got component: {usableItemComponent}");
				_inventoryScreen.InteractWithCharacter(_item,true);//probably an issue with not removing this
				//Plugin.Logger.Log($"amount after: {_item.StackCount}");
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			Plugin.Logger.Log($"{_item.Id} at position: {_item.InventoryPos}");
			//SEEMS INDEED THE TABS GET RESET WHEN OPENING AND CLOSING
			_isHovering = true;
		}

		public void OnPointerExit(PointerEventData eventData) => _isHovering = false;
	}
}