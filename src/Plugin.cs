using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace BookmarkTravel
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource Log { get; set; }
        public static ConfigEntry<int> DoubleClickMilliseconds { get; private set; }    

        public static ConfigEntry<bool> UseDoubleClick{ get; private set; }

        public static ConfigEntry<bool> ShowCardDialog{ get; private set; }


        private void Awake()
        {

            Log = Logger;

            DoubleClickMilliseconds = Config.Bind("General", nameof(DoubleClickMilliseconds), 250, "The maximum milliseconds for a key press to be considered a double click");

            UseDoubleClick = Config.Bind("General", nameof(UseDoubleClick), true, "If true, requires the bookmark hotkey to be pressed twice to activate.  Otherwise a single press will activate.");

            ShowCardDialog = Config.Bind("General", nameof(ShowCardDialog), false, "If true, will show the information dialog for the card.  If false, will instantly travel");

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