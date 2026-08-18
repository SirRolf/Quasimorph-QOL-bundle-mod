using HarmonyLib;
using MGSC;

namespace QOL_bundle.Features.TravelTimeTradeStation
{
	public class TravelTimeTradeStationPatch
	{
		[HarmonyPatch(typeof(TooltipFactory),nameof(TooltipFactory.BuildStationTooltip))]
		public class InventoryHotkeysPatchItemSlot
		{
			public static void Postfix(Station station, Mission mission, TooltipFactory __instance)
			{
				if (!Plugin.Config.TravelTimeTradeStation || mission != null) return;
				
				Stations stations = __instance._state.Get<Stations>();
				TravelMetadata travelMetadata = __instance._state.Get<TravelMetadata>();
				string currentSpaceObject = travelMetadata.CurrentSpaceObject;
				string spaceObjectId = stations.Get(station.Id).SpaceObjectId;
				
				if (currentSpaceObject == spaceObjectId) return;
				
				SpaceObjects spaceObjects = __instance._state.Get<SpaceObjects>();
				
				__instance.AddBreakLine();

				double hoursBetweenPoints = TravelSystem.GetTravelHoursBetweenPoints(travelMetadata, spaceObjects, currentSpaceObject, spaceObjectId);
				__instance.AddPanelToTooltip().SetIcon("common_travel_time").LocalizeName("tooltip.TravelTime").SetValue(FormatHelper.ToLocalizedDaysAndHours(hoursBetweenPoints));
			}
		}
	}
}