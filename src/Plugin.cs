using HarmonyLib;
using MGSC;
using System.IO;
using QOL_bundle.MCM;

namespace QOL_bundle
{
    public static class Plugin
    {

        public static readonly ConfigDirectories ConfigDirectories = new ConfigDirectories();

        public static ModConfig Config { get; private set; }

        public static readonly Logger Logger = new Logger();
        
        private static McmConfiguration McmConfiguration { get; set; }

        [Hook(ModHookType.AfterConfigsLoaded)]
        public static void AfterConfig(IModContext context)
        {
            //MCM Config
            Directory.CreateDirectory(ConfigDirectories.ModPersistenceFolder);

            Config = ModConfig.LoadConfig();
            
            McmConfiguration = new McmConfiguration(Config);
            if (!McmConfiguration.TryConfigure())
            {
                Logger.LogError("Failed to configure MCM for QOL Bundle, QOL Bundle will continue with default configuration");
            }

            //Harmony
            new Harmony("SirRolf_" + ConfigDirectories.ModAssemblyName).PatchAll();
        }
     
    }
}
