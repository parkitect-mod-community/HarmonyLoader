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
            String pmcPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) +
                                 "/packages/PMC.Mod.1.0.0/lib/net35/PMC.Mod.dll";

            Debug.Log("Assembly path: " + harmonyPath);
            Debug.Log("Assembly path: " + pmcPath);
            var asm = Assembly.LoadFrom(harmonyPath);
            var asm1 = Assembly.LoadFrom(pmcPath);
            appDomain.Load(asm.GetName());
            appDomain.Load(asm1.GetName());
            Debug.Log("Loaded Assembly");
            LoadModPatcher();

        }

        public bool LoadModPatcher()
        {
            var appDomain = AppDomain.CurrentDomain;
            List<string> stringList = new List<string>();
            stringList.Add(GameController.modsPath);
            stringList.AddRange((IEnumerable<string>) Directory.GetDirectories(GameController.modsPath));
            foreach (string path in stringList)
            {
                string[] files1 = Directory.GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly);
                if (files1.Length != 0)
                {
                    if (System.IO.Path.GetFileName(files1[0]).Contains("PMC.ModPatcher"))
                    {
                        var asm2 = Assembly.LoadFrom(files1[0]);
                        appDomain.Load(asm2.GetName());
                        foreach (Type exportedType in asm2.GetExportedTypes())
                        {
                            if (!exportedType.IsAbstract && typeof(IMod).IsAssignableFrom(exportedType) &&
                                Activator.CreateInstance(exportedType) is IMod instance)
                            {
                                instance.onEnabled();
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public void onDisabled()
        {
        }

        public void onEnabled()
        {

        }
    }
}
