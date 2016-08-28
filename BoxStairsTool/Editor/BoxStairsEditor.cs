/*
MIT License

Copyright (c) 2016 Juan Antonio Fajardo Serrano

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
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
            EditorGUILayout.LabelField("Step Height: " + (StairsHeight.floatValue / StepsNumber.intValue));
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
