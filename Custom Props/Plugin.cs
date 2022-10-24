using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace CustomProps
{
    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private static Plugin Instance;

        const string PLUGIN_GUID = "com.steven.trombone.customprops";
        const string PLUGIN_NAME = "Custom Props";
        const string PLUGIN_VERSION = "1.0.0";
        const string FOLDER_NAME = "CustomProps";
        const string PROP_ASSET_PATH = "assets/prop.prefab";
        const string PROP_EXTENSION = ".prop";

        readonly string folderPath = Path.Combine(Paths.BepInExRootPath, FOLDER_NAME);

        public static List<CustomProp> AllProps = new List<CustomProp>();

        void Awake()
        {
            Instance = this;

            var harmony = new Harmony(PLUGIN_GUID);
            harmony.PatchAll();

            StartCoroutine(LoadAllProps());
        }

        IEnumerator LoadAllProps()
        {
            Directory.CreateDirectory(folderPath);

            var bundleLoadRequests = new List<AssetBundleCreateRequest>();

            foreach(var file in Directory.GetFiles(folderPath).Where((x) => Path.GetExtension(x).ToLower() == PROP_EXTENSION))
                bundleLoadRequests.Add(AssetBundle.LoadFromFileAsync(file));

            yield return new WaitUntil(() => bundleLoadRequests.All((x) => x.isDone));

            var assetLoadRequests = new List<AssetBundleRequest>();
            foreach (var request in bundleLoadRequests)
            {
                var bundle = request.assetBundle;

                if (bundle == null)
                {
                    LogError("Failed to load AssetBundle.");
                    continue;
                }

                foreach (var asset in bundle.GetAllAssetNames())
                    LogInfo(asset);

                assetLoadRequests.Add(bundle.LoadAssetAsync<GameObject>(PROP_ASSET_PATH));
            }

            yield return new WaitUntil(() => assetLoadRequests.All((x) => x.isDone));

            foreach (var request in assetLoadRequests)
            {
                var go = request.asset as GameObject;
                AllProps.Add(go.GetComponent<CustomProp>());
            }

            foreach(var request in bundleLoadRequests)
            {
                var bundle = request.assetBundle;
                bundle.Unload(false);
            }

            LogInfo($"Loaded {AllProps.Count()} custom props!");
        }

        #region logging
        internal static void LogDebug(string message) => Instance.Log(message, LogLevel.Debug);
        internal static void LogInfo(string message) => Instance.Log(message, LogLevel.Info);
        internal static void LogWarning(string message) => Instance.Log(message, LogLevel.Warning);
        internal static void LogError(string message) => Instance.Log(message, LogLevel.Error);
        internal static void LogError(Exception ex) => Instance.Log($"{ex.Message}\n{ex.StackTrace}", LogLevel.Error);
        private void Log(string message, LogLevel logLevel) => Logger.Log(logLevel, message);
        #endregion
    }
}
