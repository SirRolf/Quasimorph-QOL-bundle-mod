using HarmonyLib;
using MGSC;
using System.IO;
using ModConfigMenu;
using QOL_bundle.MCM;

namespace QOL_bundle
{
    public static class Plugin
    {

        public static ConfigDirectories ConfigDirectories = new ConfigDirectories();

        public static ModConfig Config { get; private set; }

        public static Logger Logger = new Logger();
        
        private static McmConfiguration McmConfiguration { get; set; }

        [Hook(ModHookType.AfterConfigsLoaded)]
        public static void AfterConfig(IModContext context)
        {
            Directory.CreateDirectory(ConfigDirectories.ModPersistenceFolder);

            Config = ModConfig.LoadConfig();
            
            McmConfiguration = new McmConfiguration(Config);
            McmConfiguration.TryConfigure();

            new Harmony("SirRolf_" + ConfigDirectories.ModAssemblyName).PatchAll();
        }
     
    }
}
