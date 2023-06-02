using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UIElements;

namespace BookmarkTravel
{

    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private const string LatestConfigVersion = "2.0";

        public static ManualLogSource Log { get; set; }
        public static ConfigEntry<int> DoubleClickMilliseconds { get; private set; }    

        public static ConfigEntry<TravelMode> TravelMode { get; private set; }


        public static ConfigEntry<bool> ShowCardDialog{ get; private set; }

        /// <summary>
        /// Used internally in case there is a need for a settings conversion in the future.
        /// </summary>
        public static ConfigEntry<string> ConfigVersion { get; private set; }


        private void Awake()
        {
            Log = Logger;


            //Check if this is a 1.0 configuration.  The ConfigVersion will be missing.
            //Must do before the Bind as it will write the entry if missing.
            bool isConfigVersionMissing = IsConfigVersionMissing();

            DoubleClickMilliseconds = Config.Bind("General", nameof(DoubleClickMilliseconds), 250, "The maximum milliseconds for a key press to be considered a double click");

            TravelMode = Config.Bind("General", nameof(TravelMode), BookmarkTravel.TravelMode.DoubleClick, "Determines if a single click, double click, or holding a key will invoke a travel.");

            ShowCardDialog = Config.Bind("General", nameof(ShowCardDialog), false, "If true, will show the information dialog for the card.  If false, will instantly travel");

            ConfigVersion = Config.Bind("Version", nameof(ConfigVersion), LatestConfigVersion, "If true, requires the bookmark hotkey to be pressed twice to activate.  Otherwise a single press will activate.");

            if (isConfigVersionMissing)
            {
                //Version 1.0
                UpgradeConfigTo1_1();
            }

            Harmony harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            harmony.PatchAll();
        }


        /// <summary>
        /// Returns true if there is an existing config file but does not havea  ConfigVersion entry.
        /// </summary>
        /// <param name="newConfig"></param>
        /// <param name="missingVersion"></param>
        /// <returns>False if no config exists or the file has a ConfigVersion entry</returns>
        private bool IsConfigVersionMissing()
        {
            string configPath = Config.ConfigFilePath;

            if (File.Exists(configPath))
            {
                string configVersionPrefix = $"{nameof(ConfigVersion)} = ";
                return File.ReadAllLines(configPath).Any(x => x.StartsWith(configVersionPrefix)) == false;
            }
            else
            {
                //Assume a new config.
                return false;
            }

        }

        /// <summary>
        /// Upgrades the configuration from a version prior to 1.2
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void UpgradeConfigTo1_1()
        {
            LogInfo("Converting configuration to ConfigVersion 2.0");

            //Have to define old config or it won't be in the Config member.
            var useDoubleClick = Config.Bind("General", "UseDoubleClick", true, "If true, requires the bookmark hotkey to be pressed twice to activate.  Otherwise a single press will activate.");

            TravelMode.Value = useDoubleClick.Value ? BookmarkTravel.TravelMode.DoubleClick : BookmarkTravel.TravelMode.SingleClick;

            Config.Remove(useDoubleClick.Definition);
            Config.Save();
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