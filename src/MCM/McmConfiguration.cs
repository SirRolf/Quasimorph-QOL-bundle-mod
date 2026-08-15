using System.Collections.Generic;
using ModConfigMenu;
using ModConfigMenu.Contracts;

namespace QOL_bundle.MCM
{
	internal class McmConfiguration : McmConfigurationBase
	{

		public McmConfiguration(ModConfig config) : base (config) { }

		public override void Configure()
		{
			ModConfig defaults = new ModConfig();

			ModConfigMenuAPI.RegisterModConfig("Extra Deploy Checks", new List<IConfigValue>()
			{
				CreateConfigProperty(nameof(ModConfig.ClickItemsForStock),
					"Click the items in the upgrade confirmation screen to go to the stock exchange"),
				CreateConfigProperty(nameof(ModConfig.Hotkeys),
					"Turn on hotkeys"),
				CreateConfigProperty(nameof(ModConfig.HotkeyUse),
					"Hotkey for using item"),
				CreateConfigProperty(nameof(ModConfig.HotkeyDrop),
					"Hotkey for Dropping item"),
				CreateConfigProperty(nameof(ModConfig.HotkeyDisassemble),
					"Hotkey for Disassembling item"),

			}, OnSave);
		}
         
	}
}