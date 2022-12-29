using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace MoreFilteredCards
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource Log { get; set; }
        public static ConfigEntry<bool> IncludeBlueprints { get; set; }
        public static ConfigEntry<bool> IncludeContainers { get; set; }
        public static ConfigEntry<bool> IncludeTravelLocations { get; set; }


        private void Awake()
        {

            Log = Logger;

            IncludeBlueprints = Config.Bind("General", nameof(IncludeBlueprints), true, "Includes blueprint instances");
            IncludeContainers = Config.Bind("General", nameof(IncludeContainers), true, "Includes containers such as baskets");
            IncludeTravelLocations = Config.Bind("General", nameof(IncludeTravelLocations), true, "Includes cards that move the current " +
                "location.  Includes sheds, cellars, beaches, etc.");

            Harmony harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            harmony.PatchAll();

        }

        public static void LogInfo(string text)
        {
            Plugin.Log.LogInfo(text);
        }

        public static string GetGameObjectPath(GameObject obj)
        {
            GameObject searchObject = obj;

            string path = "/" + searchObject.name;
            while (searchObject.transform.parent != null)
            {
                searchObject = searchObject.transform.parent.gameObject;
                path = "/" + searchObject.name + path;
            }
            return path;
        }

    }
}