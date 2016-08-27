using UnityEngine;
using UnityEditor;

namespace BoxStairsTool
{
    [CustomEditor(typeof(BoxStairs))]
    public sealed class BoxStairsEditor : Editor
    {
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
            EditorGUILayout.PropertyField(StairsWidth);
            EditorGUILayout.PropertyField(StairsHeight);
            EditorGUILayout.PropertyField(StairsDepth);
            EditorGUILayout.PropertyField(StepsNumber);
            EditorGUILayout.PropertyField(StairsMaterial);
            serializedObject.ApplyModifiedProperties();
            
            if (EditorGUI.EndChangeCheck())
            {
                BoxStairs script = (BoxStairs)target;

                script.CreateStairs();
            }
        }
    }
}
