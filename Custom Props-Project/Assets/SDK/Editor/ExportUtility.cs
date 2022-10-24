using CustomProps;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ExportUtility
{
    [MenuItem("Custom Props/Export SDK")]
    static void Export()
    {
        var path = EditorUtility.SaveFilePanel("Export Package",
            Directory.GetCurrentDirectory(),
            "Custom Props SDK.unitypackage",
            "unitypackage");

        if (!string.IsNullOrEmpty(path))
        {
            AssetDatabase.ExportPackage("Assets/SDK", "Custom Props SDK.unitypackage",
                ExportPackageOptions.Recurse | ExportPackageOptions.Interactive);
        }
    }

    public static void ExportProp(CustomProp prop, string path)
    {
        GameObject clonedProp = null;

        try
        {
            var fileName = Path.GetFileName(path);
            var folderPath = Path.GetDirectoryName(path);


            clonedProp = Object.Instantiate(prop.gameObject);
            PrefabUtility.SaveAsPrefabAsset(clonedProp, "Assets/prop.prefab");
            AssetBundleBuild assetBundleBuild = default;
            assetBundleBuild.assetBundleName = fileName;
            assetBundleBuild.assetNames = new string[] { "Assets/prop.prefab" };

            BuildTargetGroup selectedBuildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            BuildTarget activeBuildTarget = EditorUserBuildSettings.activeBuildTarget;

            BuildPipeline.BuildAssetBundles(Application.temporaryCachePath,
                            new AssetBundleBuild[] { assetBundleBuild }, BuildAssetBundleOptions.ForceRebuildAssetBundle,
                            EditorUserBuildSettings.activeBuildTarget);
            EditorPrefs.SetString("currentBuildingAssetBundlePath", folderPath);
            EditorUserBuildSettings.SwitchActiveBuildTarget(selectedBuildTargetGroup, activeBuildTarget);

            AssetDatabase.DeleteAsset("Assets/prop.prefab");

            if (File.Exists(path)) File.Delete(path);

            File.Move(Path.Combine(Application.temporaryCachePath, fileName.ToLowerInvariant()), path);

            AssetDatabase.Refresh();

            EditorUtility.DisplayDialog("Prop Export", "Export Successful!", "OK");

            if (clonedProp != null) Object.DestroyImmediate(clonedProp);
        }
        catch (System.Exception e)
        {
            EditorUtility.DisplayDialog("Prop Export", $"Export Unsuccessful!\n{e.Message}\n{e.StackTrace}", "OK");

            if (clonedProp) Object.DestroyImmediate(clonedProp);
        }
    }
}
