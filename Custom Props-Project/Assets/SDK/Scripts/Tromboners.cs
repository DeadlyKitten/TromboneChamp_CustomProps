using CustomProps;
using CustomProps.Extensions;
using SandolkakosDigital.EditorUtils;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tromboners : MonoBehaviour
{
#if UNITY_EDITOR
    const string MALE_0 = "Male 0";
    const string MALE_1 = "Male 1";
    const string FEMALE_0 = "Female 0";
    const string FEMALE_1 = "Female 1";

    GameObject male0;
    GameObject male1;
    GameObject female0;
    GameObject female1;

    public ModelType activeTromboner;
    ModelType previousTromboner = (ModelType)(-1);

    private void OnValidate()
    {
        ResolveTromboners();
        DisableAllTromboners();
        CollapseAll();

        switch (activeTromboner)
        {
            case ModelType.Male0:
                male0?.SetActive(true);
                Expand(male0);
                UpdateProps(male0);
                break;
            case ModelType.Male1:
                male1?.SetActive(true);
                Expand(male1);
                UpdateProps(male1);
                break;
            case ModelType.Female0:
                female0?.SetActive(true);
                Expand(female0);
                UpdateProps(female0);
                break;
            case ModelType.Female1:
                female1?.SetActive(true);
                Expand(female1);
                UpdateProps(female1);
                break;
        }

        previousTromboner = activeTromboner;
    }

    void UpdateProps(GameObject tromboner)
    {
        var props = new List<CustomProp>();

        foreach (var prop in Resources.FindObjectsOfTypeAll<CustomProp>())
        {
            if (!EditorUtility.IsPersistent(prop.transform.root.gameObject) && !(prop.hideFlags == HideFlags.NotEditable || prop.hideFlags == HideFlags.HideAndDontSave))
                props.Add(prop);
        }


        foreach (var prop in props)
        {
            if (prop.transform.root != transform) continue;

            if ((int)previousTromboner != -1)
            {
                prop.positionOffsets[(int)previousTromboner] = prop.transform.localPosition;
                prop.rotationOffsets[(int)previousTromboner] = prop.transform.localRotation.eulerAngles;
                prop.scaleOffsets[(int)previousTromboner] = prop.transform.localScale;
                prop.attachBone = prop.transform.parent.name;
            }

            var oldTransformName = prop.transform.parent.name;

            prop.transform.parent = tromboner.transform.FindRecursive(oldTransformName);

            prop.transform.localPosition = prop.positionOffsets[(int) activeTromboner];
            prop.transform.localRotation = Quaternion.Euler(prop.rotationOffsets[(int) activeTromboner]);
            prop.transform.localScale = prop.scaleOffsets[(int) activeTromboner];
        }
    }

    private void ResolveTromboners()
    {
        if (!male0) male0 = transform.Find(MALE_0).gameObject;
        if (!male1) male1 = transform.Find(MALE_1).gameObject;
        if (!female0) female0 = transform.Find(FEMALE_0).gameObject;
        if (!female1) female1 = transform.Find(FEMALE_1).gameObject;
    }

    void DisableAllTromboners()
    {
        male0.SetActive(false);
        male1.SetActive(false);
        female0.SetActive(false);
        female1.SetActive(false);
    }

    void CollapseAll()
    {
        SceneHierarchyUtility.SetExpanded(male0, false);
        SceneHierarchyUtility.SetExpanded(male1, false);
        SceneHierarchyUtility.SetExpanded(female0, false);
        SceneHierarchyUtility.SetExpanded(female1, false);
    }

    void Expand(GameObject go) => SceneHierarchyUtility.SetExpanded(go, true);
#endif
}
