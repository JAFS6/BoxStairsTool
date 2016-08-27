using UnityEngine;
using UnityEditor;

namespace BoxStairsTool
{
    [CustomEditor(typeof(BoxStairs))]
    [CanEditMultipleObjects]
    public sealed class BoxStairsEditor : Editor
    {
        SerializedProperty Pivot;
        SerializedProperty StairsWidth;
        SerializedProperty StairsHeight;
        SerializedProperty StairsDepth;
        SerializedProperty StepsNumber;
        SerializedProperty StairsMaterial;

        [MenuItem("BoxStairs Tool/Create BoxStairs")]
        private static void CreateBoxStairsGO()
        {
            GameObject BoxStairs = new GameObject("BoxStairs");
            BoxStairs.AddComponent<BoxStairs>();
            Selection.activeGameObject = BoxStairs;
        }

        private void OnEnable()
        {
            Pivot = serializedObject.FindProperty("Pivot");
            StairsWidth = serializedObject.FindProperty("StairsWidth");
            StairsHeight = serializedObject.FindProperty("StairsHeight");
            StairsDepth = serializedObject.FindProperty("StairsDepth");
            StepsNumber = serializedObject.FindProperty("StepsNumber");
            StairsMaterial = serializedObject.FindProperty("StairsMaterial");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(Pivot);
            EditorGUILayout.PropertyField(StairsWidth);
            EditorGUILayout.PropertyField(StairsHeight);
            EditorGUILayout.PropertyField(StairsDepth);
            EditorGUILayout.PropertyField(StepsNumber);
            EditorGUILayout.PropertyField(StairsMaterial);
            serializedObject.ApplyModifiedProperties();
            
            if (EditorGUI.EndChangeCheck())
            {
                BoxStairs script;

                for (int i = 0; i < targets.Length; i++)
                {
                    script = (BoxStairs)targets[i];
                    script.CreateStairs();
                }
            }

            if (GUILayout.Button("Finalize stairs"))
            {
                FinalizeStairs();
            }
        }

        private void FinalizeStairs()
        {
            if (EditorUtility.DisplayDialog("Finalize stairs?", "This action can't be undo. Are you sure you want to finalize the stairs?", "Yes", "No"))
            {
                if (targets.Length == 1)
                {
                    DestroyImmediate(target);
                }
                else if (EditorUtility.DisplayDialog("Finalize ALL SELECTED stairs?", "You have SEVERAL stairs SELECTED. This action can't be undo. Are you sure you want to finalize the stairs?", "Yes", "No"))
                {
                    for (int i = 0; i < targets.Length; i++)
                    {
                        DestroyImmediate(targets[i]);
                    }
                }
            }
        }
    }
}
