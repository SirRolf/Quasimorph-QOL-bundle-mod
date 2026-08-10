using HarmonyLib;
using MGSC;
using System.IO;

namespace QOL_bundle
{
    public static class Plugin
    {

        public static ConfigDirectories ConfigDirectories = new ConfigDirectories();

        public static ModConfig Config { get; private set; }

        public static Logger Logger = new Logger();

        [Hook(ModHookType.AfterConfigsLoaded)]
        public static void AfterConfig(IModContext context)
        {

            Directory.CreateDirectory(ConfigDirectories.ModPersistenceFolder);

            Config = ModConfig.LoadConfig(ConfigDirectories.ConfigPath);

            new Harmony("SirRolf_" + ConfigDirectories.ModAssemblyName).PatchAll();
        }
     
    }
}
