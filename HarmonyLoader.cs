using System;
using System.Reflection;
using UnityEngine;

namespace HarmonyLoader
{
    public class HarmonyLoader : IMod
    {
        public static Version HarmonyVersion = default;
        public string Name => "Harmony Library Loader";
        public string Description => "Loads Harmony, so your mods don't have to (enable first!)";
        string IMod.Identifier => "Harmony@ParkitectMods";

        public HarmonyLoader()
        {
            var appDomain = AppDomain.CurrentDomain;
            var assemblyList = appDomain.GetAssemblies();
            if (Array.Exists<Assembly>(assemblyList, match: element => element.GetName().Name == "0Harmony"))
            {
                // FileLog.Log("Harmony already present in app domain."); //Harmony's logger
                Debug.Log("Harmony already present in app domain.");
                return;
            }

            String harmonyPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                         "/packages/Lib.Harmony.2.0.1/lib/net35/0Harmony.dll";
            String pmcPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                                 "/packages/PMC.Mod.1.0.0/lib/net35/PMC.Mod.dll";

            Debug.Log("Assembly path: " + harmonyPath);
            Debug.Log("Assembly path: " + pmcPath);
            appDomain.Load(Assembly.LoadFrom(harmonyPath).GetName());
            appDomain.Load(Assembly.LoadFrom(pmcPath).GetName());
            Debug.Log("Loaded Assembly");
        }

        public void onDisabled()
        {
        }

        public void onEnabled()
        {
        }
    }
}
