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
    public sealed class BoxStairsEditor : Editor
    {
        private const int StepsNumberLabelFieldWidth = 115;
        private const int StepsNumberValueLabelFieldWidth = 30;

        SerializedProperty Pivot;
        SerializedProperty StairsWidth;
        SerializedProperty StairsHeight;
        SerializedProperty StairsDepth;
        SerializedProperty StepsNumber;
        SerializedProperty ThreeSides;
        SerializedProperty StairsHeightDrivedBySteps;
        SerializedProperty HeightFoldout;
        SerializedProperty StepsHeight;
        SerializedProperty KeepCustomHeightValues;
        SerializedProperty StairsDepthDrivedBySteps;
        SerializedProperty DepthFoldout;
        SerializedProperty StepsDepth;
        SerializedProperty KeepCustomDepthValues;
        SerializedProperty StairsMaterial;
        SerializedProperty MaterialsFoldout;
        SerializedProperty StepsMaterials;

        private const string DefaultName = "BoxStairs";
        private bool FinalizeButtonPressed = false;

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
            Pivot                     = serializedObject.FindProperty("Pivot");
            StairsWidth               = serializedObject.FindProperty("StairsWidth");
            StairsHeight              = serializedObject.FindProperty("StairsHeight");
            StairsDepth               = serializedObject.FindProperty("StairsDepth");
            StepsNumber               = serializedObject.FindProperty("StepsNumber");
            ThreeSides                = serializedObject.FindProperty("ThreeSides");
            StairsHeightDrivedBySteps = serializedObject.FindProperty("StairsHeightDrivedBySteps");
            HeightFoldout             = serializedObject.FindProperty("HeightFoldout");
            StepsHeight               = serializedObject.FindProperty("StepsHeight");
            KeepCustomHeightValues    = serializedObject.FindProperty("KeepCustomHeightValues");
            StairsDepthDrivedBySteps  = serializedObject.FindProperty("StairsDepthDrivedBySteps");
            DepthFoldout              = serializedObject.FindProperty("DepthFoldout");
            StepsDepth                = serializedObject.FindProperty("StepsDepth");
            KeepCustomDepthValues     = serializedObject.FindProperty("KeepCustomDepthValues");
            StairsMaterial            = serializedObject.FindProperty("StairsMaterial");
            MaterialsFoldout          = serializedObject.FindProperty("MaterialsFoldout");
            StepsMaterials            = serializedObject.FindProperty("StepsMaterials");

            EditorApplication.update += Update;
        }

        public override void OnInspectorGUI ()
        {
            serializedObject.Update();

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(Pivot); // Property: Pivot
            EditorGUILayout.PropertyField(StairsWidth); // Property: StairsWidth

            if (StairsHeightDrivedBySteps.boolValue) { GUI.enabled = false; }
            EditorGUILayout.PropertyField(StairsHeight);  // Property: StairsHeight
            GUI.enabled = true; // Restore GUI default state

            if (StairsDepthDrivedBySteps.boolValue) { GUI.enabled = false; }
            EditorGUILayout.PropertyField(StairsDepth); // Property: StairsDepth
            GUI.enabled = true; // Restore GUI default state

            GUI.enabled = false; // Only show the steps number
            EditorGUILayout.PropertyField(StepsNumber); // Property: StepsNumber
            GUI.enabled = true; // Restore GUI default state

            // Show buttons to control steps number

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+1" )) { StepsNumber.intValue++;     }
            if (GUILayout.Button("+10")) { StepsNumber.intValue += 10; }
            if (GUILayout.Button("-1" )) { StepsNumber.intValue--;     }
            if (GUILayout.Button("-10")) { StepsNumber.intValue -= 10; }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(ThreeSides); // Property: ThreeSides

            // Steps height

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            bool heightFoldout = HeightFoldout.boolValue;
            HeightFoldout.boolValue = EditorGUILayout.Foldout(heightFoldout, "Steps Height");

            if (HeightFoldout.boolValue)
            {
                EditorGUILayout.PropertyField(StairsHeightDrivedBySteps); // Property: StairsHeightDrivedBySteps

                for (int i = 0; i < StepsHeight.arraySize - 1; i++)
                {
                    EditorGUILayout.PropertyField(StepsHeight.GetArrayElementAtIndex(i)); // Property: StepsHeight
                }

                if (!StairsHeightDrivedBySteps.boolValue) { GUI.enabled = false; }
                EditorGUILayout.PropertyField(StepsHeight.GetArrayElementAtIndex(StepsHeight.arraySize - 1));
                GUI.enabled = true; // Restore GUI default state

                EditorGUILayout.PropertyField(KeepCustomHeightValues); // Property: KeepCustomHeightValues
            }

            // Steps depth

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            bool depthFoldout = DepthFoldout.boolValue;
            DepthFoldout.boolValue = EditorGUILayout.Foldout(depthFoldout, "Steps Depth");

            if (DepthFoldout.boolValue)
            {
                EditorGUILayout.PropertyField(StairsDepthDrivedBySteps); // Property: StairsDepthDrivedBySteps

                for (int i = 0; i < StepsDepth.arraySize - 1; i++)
                {
                    EditorGUILayout.PropertyField(StepsDepth.GetArrayElementAtIndex(i)); // Property: StepsDepth
                }

                if (!StairsDepthDrivedBySteps.boolValue) { GUI.enabled = false; }
                EditorGUILayout.PropertyField(StepsDepth.GetArrayElementAtIndex(StepsDepth.arraySize - 1));
                GUI.enabled = true; // Restore GUI default state

                EditorGUILayout.PropertyField(KeepCustomDepthValues); // Property: KeepCustomDepthValues
            }

            // Stairs Material

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(StairsMaterial); // Property: StairsMaterial

            // Steps Materials

            bool materialsfoldout = MaterialsFoldout.boolValue;
            MaterialsFoldout.boolValue = EditorGUILayout.Foldout(materialsfoldout, "Steps Materials");

            if (MaterialsFoldout.boolValue)
            {
                for (int i = 0; i < StepsMaterials.arraySize; i++)
                {
                    EditorGUILayout.PropertyField(StepsMaterials.GetArrayElementAtIndex(i)); // Property: StepsMaterials
                }
            }

            serializedObject.ApplyModifiedProperties();

            if (EditorGUI.EndChangeCheck())
            {
                BoxStairs script = (BoxStairs)target;
                Undo.SetCurrentGroupName("BoxStairs parameter change");
                Undo.undoRedoPerformed += script.CreateStairs;
                script.CreateStairs();
            }

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            if (GUILayout.Button("Finalize stairs"))
            {
                FinalizeButtonPressed = true;
            }
        }

        private void FinalizeStairs ()
        {
            Undo.SetCurrentGroupName("Finalize stairs");
            BoxStairs script = (BoxStairs)target;
            GameObject go = script.gameObject;
            BoxCollider bc = go.GetComponent<BoxCollider>();

            if (bc != null)
            {
                Undo.DestroyObjectImmediate(bc);
            }

            Undo.DestroyObjectImmediate(target);
        }

        private void Update ()
        {
            if (FinalizeButtonPressed)
            {
                FinalizeStairs();
                FinalizeButtonPressed = false;
                EditorApplication.update -= Update;
            }
        }
    }
}
