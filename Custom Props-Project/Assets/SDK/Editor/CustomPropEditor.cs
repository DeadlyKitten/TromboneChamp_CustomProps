using CustomProps;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CustomProp))]
public class CustomPropEditor : Editor
{
    CustomProp prop;

    private void OnEnable() => prop = (CustomProp)target;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(20);

        EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(prop.propName) || string.IsNullOrEmpty(prop.authorName) || string.IsNullOrEmpty(prop.attachBone));

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Save Offset"))
        {
            var tromboners = FindObjectOfType<Tromboners>();
            var modelType = (int)tromboners.activeTromboner;
            prop.SaveOffset(modelType);
            Debug.Log("Offset Saved!");
        }

        if (GUILayout.Button("Save All Offsets"))
        {
            prop.SaveAllOffsets();
            Debug.Log("Offsets Saved!");

        }

        EditorGUILayout.EndHorizontal();
        
        if (GUILayout.Button("Export Custom Prop"))
        {
            var path = EditorUtility.SaveFilePanel("Save Custom Prop", string.Empty, $"{prop.propName}.prop", "prop");

            if (!string.IsNullOrEmpty(path))
                ExportUtility.ExportProp(prop, path);
        }
        EditorGUI.EndDisabledGroup();
    }

    
}
