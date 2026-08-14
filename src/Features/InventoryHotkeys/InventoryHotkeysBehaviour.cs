using System;
using MGSC;
using UnityEngine;
using UnityEngine.EventSystems;

namespace QOL_bundle.Features.InventoryHotkeys
{
	public class InventoryHotkeysBehaviour : MonoBehaviour ,IPointerEnterHandler, IPointerExitHandler
	{
		private ItemSlot _itemSlot;
		private InventoryScreen _inventoryScreen;
		
		private bool _isHovering;

		public InventoryHotkeysBehaviour Configure(ItemSlot itemSlot, InventoryScreen inventoryScreen)
		{
			_itemSlot = itemSlot;
			_inventoryScreen =  inventoryScreen;
			return this;
		}
		
		private void Update()
		{
			if (!_isHovering || !InputHelper.GetKeyDown(KeyCode.E) || _itemSlot.Item == null) return; //probably should change to a keybind but not sure how to do that yet
			_inventoryScreen.InteractWithCharacter(_itemSlot.Item,true);
		}

		public void OnPointerEnter(PointerEventData eventData) => _isHovering = true; 

		public void OnPointerExit(PointerEventData eventData) => _isHovering = false;
	}
}