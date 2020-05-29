using System;
using System.Reflection;
using HarmonyLib;

namespace HarmonyLoaderTester
{
    public class HarmonyLoaderTester : IMod
    {
        public string Name => "Harmony Loaded Tester";

        public string Description => "Tests if Harmony is loaded";

        public string Identifier => "HarmonyLoaderTester@Topodic";

        public void onDisabled()
        {
        }

        public void onEnabled()
        {
            Assembly[] currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            if (Array.Exists<Assembly>(currentAssemblies, match: element => element.GetName().Name == "0Harmony"))
            {
                FileLog.Log("Harmony is loaded.");
            }
        }
    }
}
