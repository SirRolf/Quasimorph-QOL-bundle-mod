using System;
using MGSC;
using UnityEngine;
using UnityEngine.EventSystems;

namespace QOL_bundle.Features.InventoryHotkeys
{
	public class InventoryHotkeysBehaviour : MonoBehaviour ,IPointerEnterHandler, IPointerExitHandler
	{
		private bool _isHovering;
		private BasePickupItem _item;

		public InventoryHotkeysBehaviour Configure(BasePickupItem item)
		{
			_item = item;
			return this;
		}
		
		private void Update()
		{
			if (_isHovering && InputHelper.GetKeyDown(KeyCode.E))//probably should change to a keybind but not sure how to do that yet
			{
				Plugin.Logger.Log($"Trying to consume: {_item.Id}");
				ItemInteractionSystem.ConsumeItem(_item);
			}
		}

		public void OnPointerEnter(PointerEventData eventData) => _isHovering = true;

		public void OnPointerExit(PointerEventData eventData) => _isHovering = false;
	}
}