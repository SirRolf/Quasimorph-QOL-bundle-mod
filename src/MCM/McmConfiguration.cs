using HarmonyLib;
using ModConfigMenu.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using ModConfigMenu;
using ModConfigMenu.Contracts;
using UnityEngine;

namespace QOL_bundle.MCM
{
    internal class McmConfiguration
    {
        private ISave Config { get; }
        
        private ModConfig Defaults { get; } = new ModConfig();

        public McmConfiguration(ISave config)
        {
            Config = config;
        }
        
        public bool TryConfigure()
        {
            try
            {
                Configure();
                return true;
            }
            catch (FileNotFoundException)
            {
                Plugin.Logger.Log("Bypassing MCM. The 'Mod Configuration Menu' mod is not loaded. ");
            }
            catch (Exception ex)
            {
                Plugin.Logger.LogError($"An error occurred when configuring MCM: {ex}");
            }

            return false;
        }
        
        private void Configure()
        {
            ModConfigMenuAPI.RegisterModConfig("QOL Bundle", new List<IConfigValue>()
            {
                CreateConfigProperty(nameof(ModConfig.ClickItemsForStock),
                    "Click the items in the upgrade confirmation screen to go to the stock exchange"),
                CreateConfigProperty(nameof(ModConfig.Hotkeys),
                    "Turn on hotkeys")

            }, OnSave);
        }
        
        private static string FormatUpperCaseSpaces(string propertyName)
        {
            //Since the UI uppercases the text, add spaces to make it easier to read.
            Regex regex = new Regex(@"([A-Z0-9])");
            string formattedPropertyName = regex.Replace(propertyName, " $1").TrimStart();
            return formattedPropertyName;
        }

        private ConfigValue CreateConfigProperty(string propertyName,
            string tooltip, string label = "", string header = "General")
        {
            object defaultValue = AccessTools.Property(typeof(ModConfig), propertyName).GetValue(Defaults);
            object propertyValue = AccessTools.Property(typeof(ModConfig), propertyName).GetValue(Config);

            string formattedLabel = label == "" ? FormatUpperCaseSpaces(propertyName) : label;

            return new ConfigValue(propertyName, propertyValue, header, defaultValue, tooltip, formattedLabel);
        }

        private bool TrySetModConfigValue(string key, object value)
        {
            MethodInfo setter = AccessTools.PropertySetter(typeof(ModConfig), key);
            if (setter == null) return false;

            setter.Invoke(Config, new[] { value});
            return true;
        }

        private bool OnSave(Dictionary<string, object> currentConfig, out string feedbackMessage)
        {
            feedbackMessage = "";

            foreach (KeyValuePair<string, object> entry in currentConfig)
            {
                if (!TrySetModConfigValue(entry.Key, entry.Value))
                {
                    Debug.LogWarning($"could not configure value for {entry.Key}");
                }
            }

            Config.Save();

            return true;
        }
    }
}