using System;
using System.Collections.Generic;
using System.IO;
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
            Debug.Log("Harmony path: " + harmonyPath);
            var asm = Assembly.LoadFrom(harmonyPath);
            appDomain.Load(asm.GetName());
            Debug.Log("Harmony Loaded!");

        }

        public void onDisabled()
        {
        }

        public void onEnabled()
        {

        }
    }
}
