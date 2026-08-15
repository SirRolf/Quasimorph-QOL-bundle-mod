using System;
using System.IO;
using Newtonsoft.Json;
using QOL_bundle.MCM;
using UnityEngine;

namespace QOL_bundle
{
    public class ModConfig : ISave
    {
        public bool ClickItemsForStock { get; set; } = true;
        public bool Hotkeys { get; set; } = true;
        public KeyCode HotkeyUse { get; set; } = KeyCode.E;
        public KeyCode HotkeyDrop { get; set; } = KeyCode.F;
        public KeyCode HotkeyDisassemble { get; set; } = KeyCode.X;
        

        [JsonIgnore]
        private static JsonSerializerSettings SerializerSettings { get; } = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
        };


        [JsonIgnore]
        private static string ConfigPath { get; } = Plugin.ConfigDirectories.ConfigPath;

        public static ModConfig LoadConfig()
        {
            ModConfig config;


            if (File.Exists(ConfigPath))
            {
                try
                {
                    string sourceJson = File.ReadAllText(ConfigPath);

                    config = JsonConvert.DeserializeObject<ModConfig>(sourceJson, SerializerSettings);

                    //Add any new elements that have been added since the last mod version the user had.
                    string upgradeConfig = JsonConvert.SerializeObject(config, SerializerSettings);

                    if (upgradeConfig != sourceJson)
                    {
                        Plugin.Logger.Log("Updating config with missing elements");
                        //re-write
                        File.WriteAllText(ConfigPath, upgradeConfig);
                    }
                }
                catch (Exception ex)
                {
                    Plugin.Logger.LogError($"Error parsing configuration.  Ignoring config file and using defaults: {ex}");

                    //Not overwriting in case the user just made a typo.
                    config = new ModConfig();
                }
            }
            else
            {
                //Use the defaults.
                config = Save(new ModConfig());
            }

            return config;
        }


        public ModConfig Save() 
        {
            return Save(this);
        }

        private static ModConfig Save(ModConfig config)
        {
            string json = JsonConvert.SerializeObject(config, SerializerSettings);
            File.WriteAllText(ConfigPath, json);
            return config;
        }

        void ISave.Save()
        {
            Save(this);
        }
    }
}