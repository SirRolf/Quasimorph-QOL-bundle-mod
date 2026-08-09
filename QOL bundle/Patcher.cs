using HarmonyLib;
using MGSC;

namespace QOL_bundle
{
	public class Patcher
	{
		[Hook(ModHookType.AfterConfigsLoaded)]
		public static void AfterConfigsLoaded(IModContext context)
		{
			Harmony harmony = new Harmony("Test_Mod");
			harmony.PatchAll();
		}
	}
}