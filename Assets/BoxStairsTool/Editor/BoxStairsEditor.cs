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
        SerializedProperty ThreeSides;
        SerializedProperty StairsMaterial;
        SerializedProperty StepsMaterials;

        private const string DefaultName = "BoxStairs";

        [MenuItem("GameObject/3D Object/BoxStairs")]
        private static void CreateBoxStairsGO ()
        {
            if (Selection.transforms.Length > 1)
            {
                if (EditorUtility.DisplayDialog("Create several "+ DefaultName + "?", "You have SEVERAL GameObject SELECTED. Are you sure you want to create one "+ DefaultName + " for each selected GameObject?", "Yes", "No"))
                {
                    GameObject[] selection = new GameObject[Selection.transforms.Length];

                    for (int i = 0; i < Selection.transforms.Length; i++)
                    {
                        GameObject BoxStairs = new GameObject(DefaultName);
                        BoxStairs.AddComponent<BoxStairs>();
                        BoxStairs.transform.SetParent(Selection.transforms[i]);
                        BoxStairs.transform.localPosition = new Vector3(0, 0, 0);
                        selection[i] = BoxStairs;
                        Undo.RegisterCreatedObjectUndo(BoxStairs, "Create BoxStairs");
                    }

                    Selection.objects = selection;
                }
            }
            else
            {
                GameObject BoxStairs = new GameObject(DefaultName);
                BoxStairs.AddComponent<BoxStairs>();

                if (Selection.transforms.Length == 1)
                {
                    BoxStairs.transform.SetParent(Selection.transforms[0]);
                    BoxStairs.transform.localPosition = new Vector3(0,0,0);
                }

                Selection.activeGameObject = BoxStairs;
                Undo.RegisterCreatedObjectUndo(BoxStairs, "Create BoxStairs");
            }
        }

        private void OnEnable ()
        {
            Pivot = serializedObject.FindProperty("Pivot");
            StairsWidth = serializedObject.FindProperty("StairsWidth");
            StairsHeight = serializedObject.FindProperty("StairsHeight");
            StairsDepth = serializedObject.FindProperty("StairsDepth");
            StepsNumber = serializedObject.FindProperty("StepsNumber");
            ThreeSides = serializedObject.FindProperty("ThreeSides");
            StairsMaterial = serializedObject.FindProperty("StairsMaterial");
            StepsMaterials = serializedObject.FindProperty("StepsMaterials");
        }

        public override void OnInspectorGUI ()
        {
            serializedObject.Update();

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(Pivot);
            EditorGUILayout.PropertyField(StairsWidth);
            EditorGUILayout.PropertyField(StairsHeight);
            EditorGUILayout.PropertyField(StairsDepth);
            EditorGUILayout.PropertyField(StepsNumber);
            EditorGUILayout.PropertyField(ThreeSides);
            EditorGUILayout.LabelField("Step Height: " + (StairsHeight.floatValue / StepsNumber.intValue));
            EditorGUILayout.PropertyField(StairsMaterial);
            EditorGUILayout.PropertyField(StepsMaterials, true);
            serializedObject.ApplyModifiedProperties();

            if (EditorGUI.EndChangeCheck())
            {
                BoxStairs script;

                for (int i = 0; i < targets.Length; i++)
                {
                    script = (BoxStairs)targets[i];
                    Undo.SetCurrentGroupName("BoxStairs parameter change");
                    Undo.undoRedoPerformed += script.CreateStairs;
                    script.CreateStairs();
                }
            }

            if (GUILayout.Button("Finalize stairs"))
            {
                FinalizeStairs();
            }
        }

        private void FinalizeStairs ()
        {
            Undo.SetCurrentGroupName("Finalize stairs");

            if (targets.Length == 1)
            {
                Undo.DestroyObjectImmediate(target);
            }
            else
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    Undo.DestroyObjectImmediate(targets[i]);
                }
            }
        }
    }
}
