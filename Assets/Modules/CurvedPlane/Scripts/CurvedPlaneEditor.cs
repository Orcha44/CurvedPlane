using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(CurvedPlane))]
[CanEditMultipleObjects]
public class CurvedPlaneEditor : Editor
{

    SerializedProperty Poligons;
    SerializedProperty CurveCoefX;
    SerializedProperty CustomMaterial;
    SerializedProperty Target;
    SerializedProperty TexturesAspectRatio;
    SerializedProperty Size;
    SerializedProperty Width;
    SerializedProperty Height;
    /* 
        void OnEnable()
        {
            Poligons = serializedObject.FindProperty("quality");
            CurveCoefX = serializedObject.FindProperty("m_CurveCoeficientX");
            CustomMaterial = serializedObject.FindProperty("CustomMaterial");
            Target = serializedObject.FindProperty("m_Target");
            TexturesAspectRatio = serializedObject.FindProperty("TexturesSize");
            Size = serializedObject.FindProperty("Scale");
            Width = serializedObject.FindProperty("width");
            Height = serializedObject.FindProperty("height");
        }

            public override void OnInspectorGUI()
            {
                serializedObject.Update();

                EditorGUILayout.PropertyField(useFixedUpdate);

                EditorGUILayout.PropertyField(CustomMaterial);
                if (CustomMaterial.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Will create standard material instead", MessageType.Info);
                }
                EditorGUILayout.PropertyField(Target);
                if (Target.objectReferenceValue == null)
                {
                    EditorGUILayout.HelpBox("Will use Camera.main instead", MessageType.Info);
                }

                EditorGUILayout.PropertyField(TexturesAspectRatio);
                if (TexturesAspectRatio.boolValue)
                {
                    EditorGUILayout.PropertyField(Size);
                }
                else
                {
                    EditorGUILayout.PropertyField(Width);
                    EditorGUILayout.PropertyField(Height);
                }





                serializedObject.ApplyModifiedProperties();
            }
            */
}
