using HarmonyLib;
using UnityEngine;
using CustomProps.Extensions;

namespace CustomProps.Patches
{
    [HarmonyPatch(typeof(HumanPuppetController), nameof(HumanPuppetController.setTromboneTex))]
    class HumanPuppetController_SetTromboneTex
    {
        static void Postfix(HumanPuppetController __instance)
        {
            foreach (var prop in Plugin.AllProps)
            {
                var bone = __instance.transform.FindRecursive(prop.attachBone);

                if (!bone)
                {
                    Plugin.LogError($"Could not find bone: '{prop.attachBone}'\nfor prop: '{prop.propName}");
                    continue;
                }

                Plugin.LogDebug($"Attaching prop '{prop.propName}' to bone '{prop.attachBone}'");

                var propInstance = Object.Instantiate(prop, bone);
                propInstance.gameObject.name = prop.propName;
                propInstance.ApplyOffsets();
            }
        }
    }
}
