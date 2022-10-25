using CustomProps;
using UnityEditor;
using UnityEngine;
using CustomProps.Extensions;

[CustomEditor(typeof(CustomProp))]
public class CustomPropEditor : Editor
{
    CustomProp prop;

    private void OnEnable() => prop = (CustomProp)target;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(20);


        EditorGUI.BeginChangeCheck();

        EditorGUI.BeginDisabledGroup(prop.gameObject.scene.name == null);
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Reset to Offset"))
        {
            var tromboners = FindObjectOfType<Tromboners>();
            var modelType = (int)tromboners.activeTromboner;
            Undo.RecordObject(prop, "Reset to Offset");
            PrefabUtility.RecordPrefabInstancePropertyModifications(prop);

            prop.transform.parent = tromboners.GetActiveTrombonerTransform().FindRecursive(prop.attachBone);
            EditorGUIUtility.PingObject(prop);

            prop.transform.localPosition = prop.positionOffsets[modelType];
            prop.transform.localRotation = Quaternion.Euler(prop.rotationOffsets[modelType]);
            prop.transform.localScale = prop.scaleOffsets[modelType];
        }

        if (GUILayout.Button("Reset to 0"))
        {
            Undo.RecordObject(prop, "Reset to Offset");
            PrefabUtility.RecordPrefabInstancePropertyModifications(prop);

            var tromboners = FindObjectOfType<Tromboners>();
            prop.transform.parent = tromboners.GetActiveTrombonerTransform().FindRecursive(prop.attachBone);
            EditorGUIUtility.PingObject(prop);

            prop.transform.localPosition = Vector3.zero; ;
            prop.transform.localRotation = Quaternion.identity;
            prop.transform.localScale = Vector3.one;
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Save Offset"))
        {
            var tromboners = FindObjectOfType<Tromboners>();
            var modelType = (int)tromboners.activeTromboner;
            Undo.RecordObject(prop, "Saved Offset");
            PrefabUtility.RecordPrefabInstancePropertyModifications(prop);
            prop.SaveOffset(modelType);
            Debug.Log("Offset Saved!");
        }

        if (GUILayout.Button("Save All Offsets"))
        {
            Undo.RecordObject(prop, "Saved All Offsets");
            PrefabUtility.RecordPrefabInstancePropertyModifications(prop);
            prop.SaveAllOffsets();
            Debug.Log("Offsets Saved!");

        }

        EditorGUILayout.EndHorizontal();
        EditorGUI.EndDisabledGroup();

        EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(prop.propName) || string.IsNullOrEmpty(prop.authorName) || string.IsNullOrEmpty(prop.attachBone));
        if (GUILayout.Button("Export Custom Prop"))
        {
            var path = EditorUtility.SaveFilePanel("Save Custom Prop", string.Empty, $"{prop.propName}.prop", "prop");

            if (!string.IsNullOrEmpty(path))
                ExportUtility.ExportProp(prop, path);
        }
        EditorGUI.EndDisabledGroup();
    }

    
}
