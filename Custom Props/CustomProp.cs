using CustomProps.Attributes;
using System.Linq;
using UnityEngine;

namespace CustomProps
{
    [ExecuteAlways]
    public class CustomProp : MonoBehaviour
    {
        [Tooltip("Name of the prop.")]
        public string propName;
        [Tooltip("Your name goes here!")]
        public string authorName;
        [Tooltip("If your model came with a license, feel free to attach it here.")]
        public TextAsset license;

        const string GET_BONES_METHOD_NAME = "GetBones";

        [Space(20)]
        [Tooltip("Bone the prop will attach itself to.")]
        [Dropdown(typeof(CustomProp), GET_BONES_METHOD_NAME)]
        public string attachBone;

        [LabeledArray(typeof(ModelType))]
        public Vector3[] positionOffsets = new Vector3[4];

        [LabeledArray(typeof(ModelType))]
        public Vector3[] rotationOffsets = new Vector3[4];

        [LabeledArray(typeof(ModelType))]
        public Vector3[] scaleOffsets = Enumerable.Repeat(Vector3.one, 4).ToArray();

#if PLUGIN
        public void ApplyOffsets()
        {
            var modelType = (int)GetModelType();

            transform.localPosition = positionOffsets[modelType];
            transform.localRotation = Quaternion.Euler(rotationOffsets[modelType]);
            transform.localScale = scaleOffsets[modelType];
        }

        private ModelType GetModelType()
        {
            switch (GlobalVariables.chosen_character)
            {
                case 1:
                case 2:
                    return ModelType.Female0;
                case 0:
                case 3:
                    return ModelType.Female1;
                case 4:
                case 5:
                default:
                    return ModelType.Male0;
                case 6:
                case 7:
                    return ModelType.Male1;
            }
        }
#endif
#if EDITOR
        void OnValidate()
        {
            if (positionOffsets.Length != 4) System.Array.Resize(ref positionOffsets, 4);
            if (rotationOffsets.Length != 4) System.Array.Resize(ref rotationOffsets, 4);
            if (scaleOffsets.Length != 4) System.Array.Resize(ref scaleOffsets, 4);
        }

        public void SaveOffset(int modelType)
        {
            positionOffsets[modelType] = transform.localPosition;
            rotationOffsets[modelType] = transform.localRotation.eulerAngles;
            scaleOffsets[modelType] = transform.localScale;
        }

        public void SaveAllOffsets()
        {
            for (int i = 0; i < 4; i++)
                positionOffsets[i] = transform.localPosition;
            for (int i = 0; i < 4; i++)
                rotationOffsets[i] = transform.localRotation.eulerAngles;
            for (int i = 0; i < 4; i++)
                scaleOffsets[i] = transform.localScale;
        }

        public static string[] GetBones()
        {
            return new string[]
            {
                "basic_rig",
                "basic_rig Pelvis",
                "basic_rig L Thigh",
                "basic_rig L Calf",
                "basic_rig L Foot",
                "basic_rig L Toe0",
                "basic_rig R Thigh",
                "basic_rig R Calf",
                "basic_rig R Foot",
                "basic_rig R Toe0",
                "basic_rig Spine",
                "basic_rig Spine1",
                "basic_rig L Clavicle",
                "basic_rig L UpperArm",
                "basic_rig L Forearm",
                "basic_rig L Hand",
                "basic_rig L Finger0",
                "basic_rig L Finger1",
                "basic_rig L Finger2",
                "L_hand_grab_point",
                "basic_rig R Clavicle",
                "basic_rig R UpperArm",
                "basic_rig R Forearm",
                "basic_rig R Hand",
                "basic_rig R Finger0",
                "basic_rig R Finger1",
                "basic_rig R Finger2",
                "R_hand_grab_point",
                "basic_rig Neck",
                "basic_rig Head",
                "tromboneMAIN",
                "bellmesh",
                "tromboneTUBE"
            };
        }
#endif
    }
}
