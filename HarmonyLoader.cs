using System;
using System.Reflection;
using HarmonyLib;

namespace HarmonyLoader
{
    public class HarmonyLoader : IMod
    {
        public string Name => "Harmony Library Loader";
        public string Description => "Loads Harmony, so your mods don't have to (enable first!)";
        public string Identifier => "Harmony@ParkitectMods";

        private static AppDomain appDomain;
        private static Assembly harmonyAssembly;
        private static string executingDirectory;

        public void onDisabled()
        {
        }

        public void onEnabled()
        {
            appDomain = AppDomain.CurrentDomain;

            var assemblyList = appDomain.GetAssemblies();

            if (Array.Exists<Assembly>(assemblyList, match: element => element.GetName().Name == "0Harmony"))
            {
                FileLog.Log("Harmony already present in app domain."); //Harmony's logger
                return;
            }

            executingDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            harmonyAssembly = Assembly.LoadFrom(executingDirectory + "\\0Harmony.dll");
            appDomain.Load(harmonyAssembly.GetName());

            if (Array.Exists<Assembly>(assemblyList, match: element => element.GetName().Name == "0Harmony"))
            {
                FileLog.Log("Harmony successfully loaded into app domain.");
            }
        }
    }
}
