using System;
using MGSC;
using UnityEngine;
using UnityEngine.EventSystems;

namespace QOL_bundle.Features.ClickForStock
{
	public class ClickItemForStockBehaviour : MonoBehaviour, IPointerClickHandler
	{
		private string _itemId;
		
		public void Configure(string itemId)
		{
			_itemId = itemId;
		}
		
		public void OnPointerClick(PointerEventData eventData)
		{    
			if (!Plugin.Config.ClickItemsForStock || eventData.button != PointerEventData.InputButton.Left) return;
			SingletonMonoBehaviour<SoundController>.Instance.PlayUiSound(SingletonMonoBehaviour<SoundsStorage>.Instance.ButtonClick);
			UI.Chain<FactionsScreen>().HideAll().Show();
			MGSC.UI.Chain<TradeWindow>().Invoke(v => v.Configure(_itemId)).Show().AttachToGroup<FactionsScreen>();
		}
	}
}