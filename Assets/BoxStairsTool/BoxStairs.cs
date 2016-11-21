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
using System.Collections.Generic;

namespace BoxStairsTool
{
    enum PivotType : byte { Downstairs, Upstairs };

    [ExecuteInEditMode]
    [SelectionBase]
    public sealed class BoxStairs : MonoBehaviour
    {
        private const float MinimumLength = 0.000000001f; // Minimum Length for a length unit

        [SerializeField]
        private PivotType Pivot;
        private PivotType LastPivot;
        [SerializeField]
        private float StairsWidth;
        private float LastStairsWidth;
        [SerializeField]
        private float StairsHeight;
        [SerializeField]
        private float StairsDepth;
        private float LastStairsDepth;
        [SerializeField]
        private int StepsNumber;
        private int LastStepsNumber;
        [SerializeField]
        private bool ThreeSides;
        private bool LastThreeSides;
        #pragma warning disable 414
        [SerializeField]
        private bool StepsFoldout; // The value of this property will be used and changed on BoxStairsEditor class
        #pragma warning restore 414
        [SerializeField]
        private float[] StepsDepth;
        [SerializeField]
        private bool KeepCustomDepthValues;
        [SerializeField]
        private Material StairsMaterial;
        private Material LastStairsMaterial;
        #pragma warning disable 414
        [SerializeField]
        private bool MaterialsFoldout; // The value of this property will be used and changed on BoxStairsEditor class
        #pragma warning restore 414
        [SerializeField]
        private Material[] StepsMaterials;

        private GameObject Root;

        private void Start ()
        {
            Root = this.gameObject;
            this.CreateStairs();
        }

        public BoxStairs ()
        {
            // Initialization of parameters and structures
            Pivot = PivotType.Downstairs;
            LastPivot = PivotType.Downstairs;
            StairsWidth = 1.0f;
            LastStairsWidth = StairsWidth;
            StairsHeight = 0.5f;
            StairsDepth = 1.0f;
            LastStairsDepth = StairsDepth;
            StepsNumber = 4;
            LastStepsNumber = StepsNumber;
            ThreeSides = false;
            LastThreeSides = ThreeSides;

            StepsFoldout = false;
            StepsDepth = new float[StepsNumber];
            float defaultStepsDepth = StairsDepth / StepsNumber;

            for (int i = 0; i < StepsNumber; i++)
            {
                StepsDepth[i] = defaultStepsDepth;
            }

            KeepCustomDepthValues = false;

            StairsMaterial = null;
            LastStairsMaterial = StairsMaterial;
            MaterialsFoldout = false;
            StepsMaterials = new Material[StepsNumber];

            for (int i = 0; i < StepsNumber; i++)
            {
                StepsMaterials[i] = null;
            }
        }

        /*
         * This method validates the parameters and prepare the structures for the creation of the stairs
         */
        public void CreateStairs ()
        {
            // Validate global parameters
            StairsWidth = GuaranteeMinimumLength(StairsWidth);
            StairsHeight = GuaranteeMinimumLength(StairsHeight);
            StairsDepth = GuaranteeMinimumLength(StairsDepth);

            if (StepsNumber < 1)
            {
                StepsNumber = 1;
            }

            if (StepsNumber != LastStepsNumber) // If the number of steps has changed
            {
                LastStepsNumber = StepsNumber;
                // Update arrays
                StepsDepth = UpdateStepsDepthArray();
                StepsMaterials = UpdateStepsMaterialsArray();
            }

            if (ThreeSides == true && LastThreeSides == false) // If ThreeSides option has been enabled
            {
                LastThreeSides = ThreeSides;

                // Force StairsWidth and StairsDepth to have a relation 1:2

                StairsWidth = StairsDepth * 2;
                LastStairsWidth = StairsWidth;
            }

            if (ThreeSides && LastStairsWidth != StairsWidth) // If StairsWidth has changed and ThreeSides is enabled
            {
                LastStairsWidth = StairsWidth;

                StairsDepth = StairsWidth * 0.5f;
                LastStairsDepth = StairsDepth;
            }

            if (StairsDepth != LastStairsDepth)
            {
                LastStairsDepth = StairsDepth;

                if (ThreeSides)
                {
                    StairsWidth = StairsDepth * 2;
                    LastStairsWidth = StairsWidth;
                }
            }

            ConsolidateStepsDepth();

            // If any child has been created, destroy it
            ClearChildObjects();

            // Create the new childs
            CreateBoxes();
            
            AddSelectionBox();
        }

        /*
         * This method is in charge of create the boxes for the stairs
         */
        private void CreateBoxes ()
        {
            float stepHeight = StairsHeight / StepsNumber;
            float halfStepHeight = stepHeight / 2;

            // Calculate the cumulative steps depth

            float[] cumulativeStepsDepth = new float[StepsNumber];
            float[] doubleCumulativeStepsDepth = new float[StepsNumber];
            cumulativeStepsDepth[0] = StepsDepth[0];
            doubleCumulativeStepsDepth[0] = StepsDepth[0] * 2;

            for (int i = 1; i < StepsNumber; i++)
            {
                cumulativeStepsDepth[i] = cumulativeStepsDepth[i-1] + StepsDepth[i];
                doubleCumulativeStepsDepth[i] = doubleCumulativeStepsDepth[i-1] + (StepsDepth[i] * 2);
            }

            for (int i = 0; i < StepsNumber; i++)
            {
                GameObject Step = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Step.name = "Step " + i;
                Step.transform.SetParent(Root.transform);

                float threeSidesWidth = (i == 0) ? StairsWidth : StairsWidth - doubleCumulativeStepsDepth[i-1];

                float boxXscale = (ThreeSides) ? threeSidesWidth : StairsWidth;
                float boxZscale = (i == 0) ? StairsDepth : StairsDepth - cumulativeStepsDepth[i-1];

                Step.transform.localScale = new Vector3(boxXscale, stepHeight, boxZscale);
                Step.transform.localRotation = Quaternion.identity;

                switch (Pivot)
                {
                    case PivotType.Downstairs:
                        Step.transform.localPosition = new Vector3(0, halfStepHeight + (i * stepHeight), StairsDepth - (boxZscale * 0.5f));
                        break;

                    case PivotType.Upstairs:
                        int fixedIndex = StepsNumber - 1 - i;
                        Step.transform.localPosition = new Vector3(0, -halfStepHeight - (fixedIndex * stepHeight), -(boxZscale * 0.5f));
                        break;
                }

                if (StairsMaterial != LastStairsMaterial)
                {
                    LastStairsMaterial = StairsMaterial;

                    for (int stepIndex = 0; stepIndex < StepsNumber; stepIndex++)
                    {
                        StepsMaterials[stepIndex] = StairsMaterial;
                    }
                }

                Renderer renderer = Step.GetComponent<Renderer>();

                if (renderer != null)
                {
                    if (StepsMaterials[i] != null)
                    {
                        renderer.material = StepsMaterials[i];
                    }
                    else
                    {
                        renderer.material = new Material(Shader.Find("Diffuse"));
                    }
                }
            }

            if (Pivot != LastPivot)
            {
                if (Pivot == PivotType.Downstairs && LastPivot == PivotType.Upstairs)
                {
                    Root.transform.position = Root.transform.position - (StairsDepth * Root.transform.forward);
                    Root.transform.position = Root.transform.position - (StairsHeight * Root.transform.up);
                }
                else // if (Pivot == PivotType.Upstairs && LastPivot == PivotType.Downstairs)
                {
                    Root.transform.position = Root.transform.position + (StairsDepth * Root.transform.forward);
                    Root.transform.position = Root.transform.position + (StairsHeight * Root.transform.up);
                }

                LastPivot = Pivot;
            }
        }

        /*
         * This method checks a value which represents a length unit,
         * if the value is below a MinimumLength the returned value will be MinimumLength,
         * in another case it returns the value.
         */
        private float GuaranteeMinimumLength (float value)
        {
            return (value < MinimumLength) ? MinimumLength : value;
        }

        /*
         * This method checks a value which represents a length unit,
         * if the value is above upperLimit the returned value will be upperLimit,
         * in another case it returns the value.
         */
        private float GuaranteeMaximumLength (float value, float upperLimit)
        {
            return (value > upperLimit) ? upperLimit : value;
        }

        /*
         * This method creates a new array for the new number of steps and conserve the step's depth
         * from the old array on the lower positions. If the new steps number is lower than before,
         * the upper steps info will be discarded, if the new steps number is greater than before,
         * a default depth will be assigned to the new ones. This default depth will be equal to
         * half depth of the last step.
         */
        private float[] UpdateStepsDepthArray ()
        {
            float[] NewStepsDepth = new float[StepsNumber];

            if (!KeepCustomDepthValues)
            {
                float defaultStepsDepth = StairsDepth / StepsNumber;

                for (int i=0; i < StepsNumber; i++)
                {
                    NewStepsDepth[i] = defaultStepsDepth;
                }
            }
            else
            {
                int OldIndex = 0, NewIndex = 0;

                while (OldIndex < StepsDepth.Length && NewIndex < StepsNumber)
                {
                    NewStepsDepth[NewIndex] = StepsDepth[OldIndex];

                    NewIndex++;
                    OldIndex++;
                }

                if (StepsNumber > StepsDepth.Length)
                {
                    float defaultStepsDepth = StepsDepth[StepsDepth.Length - 1] / 2;

                    while (NewIndex < StepsNumber)
                    {
                        NewStepsDepth[NewIndex] = defaultStepsDepth;
                        NewStepsDepth[NewIndex - 1] -= defaultStepsDepth;

                        NewIndex++;
                    }
                }
            }

            return NewStepsDepth;
        }

        /*
         * This method creates a new array for the new number of steps and conserve the materials
         * from the old array on the lower positions. If the new steps number is lower than before,
         * the upper steps info will be discarded, if the new steps number is greater than before,
         * the global material (Stairs Material) will be assigned to the new ones.
         */
        private Material[] UpdateStepsMaterialsArray ()
        {
            Material[] NewStepsMaterials = new Material[StepsNumber];

            int OldIndex = 0, NewIndex = 0;

            while (OldIndex < StepsMaterials.Length && NewIndex < StepsNumber)
            {
                NewStepsMaterials[NewIndex] = StepsMaterials[OldIndex];

                NewIndex++;
                OldIndex++;
            }

            while (NewIndex < StepsNumber)
            {
                NewStepsMaterials[NewIndex] = StairsMaterial;

                NewIndex++;
            }

            return NewStepsMaterials;
        }

        /*
         * This method destroys all children GameObject which could has been created under Root
         */
        private void ClearChildObjects ()
        {
            List<GameObject> children = new List<GameObject>();
            children.Clear();

            for (int i = 0; i < Root.transform.childCount; i++)
            {
                children.Add(Root.transform.GetChild(i).gameObject);
            }

            while (children.Count > 0)
            {
                DestroyImmediate(children[0]);
                children.RemoveAt(0);
            }
        }

        /*
         * This method consolidates the steps depth to allow "push" steps back and forth.
         */
        private void ConsolidateStepsDepth ()
        {
            for (int i=0; i < StepsNumber; i++)
            {
                if (StepsDepth[i] < 0)
                {
                    PushBack(i);
                    StepsDepth[i] = 0;
                }
                StepsDepth[i] = GuaranteeMaximumLength(StepsDepth[i], StairsDepth);
                PushForth(i);
            }
        }

        /*
         * This method fixes the depth of the steps affected by the depth of step i, from the first to stepIndex
         */
        private void PushBack (int stepIndex)
        {
            if (stepIndex > 0 && StepsDepth[stepIndex] < 0)
            {
                float depthDifference = Mathf.Abs(StepsDepth[stepIndex]);
                StepsDepth[stepIndex] = 0;

                for (int i = stepIndex - 1; i >= 0 && depthDifference > 0; i--)
                {
                    if (StepsDepth[i] > depthDifference)
                    {
                        StepsDepth[i] -= depthDifference;
                        depthDifference = 0;
                    }
                    else if (StepsDepth[i] < depthDifference)
                    {
                        depthDifference -= StepsDepth[i];
                        StepsDepth[i] = 0;
                    }
                    else
                    {
                        depthDifference = 0;
                        StepsDepth[i] = 0;
                    }
                }
            }
        }

        /*
         * This method fixes the depth of the steps affected by the depth of step i, from the last to stepIndex
         */
        private void PushForth (int stepIndex)
        {
            if (stepIndex == StepsNumber - 1)
            {
                // Calculate the sum of all steps depth except the last ones
                float cumulativeDepth = 0;

                for (int i = 0; i < StepsNumber - 1; i++)
                {
                    cumulativeDepth += StepsDepth[i];
                }

                StepsDepth[StepsNumber - 1] = StairsDepth - cumulativeDepth;
            }
            else
            {
                // Check if the total depth is greater than the StairsDepth

                float totalDepth = 0;

                for (int i = 0; i < StepsNumber; i++)
                {
                    totalDepth += StepsDepth[i];
                }

                if (totalDepth > StairsDepth)
                {
                    /* If totalDepth is greater than StairsDepth, the depth of each step starting from
                    the upper most step must be decreased. If that step's depth reach 0, the next step
                    will reduce its depth */

                    float depthDifference = totalDepth - StairsDepth;

                    for (int i = StepsNumber - 1; i > stepIndex && depthDifference > 0; i--)
                    {
                        if (StepsDepth[i] > depthDifference)
                        {
                            StepsDepth[i] -= depthDifference;
                            depthDifference = 0;
                        }
                        else if (StepsDepth[i] < depthDifference)
                        {
                            depthDifference -= StepsDepth[i];
                            StepsDepth[i] = 0;
                        }
                        else
                        {
                            depthDifference = 0;
                            StepsDepth[i] = 0;
                        }
                    }
                }
            }
        }
        
        /*
         * This method creates a disabled BoxCollider which marks the volume defined by 
         * StairsWidth, StairsHeight, StairsDepth.
         */
        private void AddSelectionBox ()
        {
            BoxCollider VolumeBox = Root.GetComponent<BoxCollider>();

            if (VolumeBox == null)
            {
                VolumeBox = Root.AddComponent<BoxCollider>();
            }

            if (Pivot == PivotType.Downstairs)
            {
                VolumeBox.center = new Vector3(0, StairsHeight * 0.5f, StairsDepth * 0.5f);
            }
            else
            {
                VolumeBox.center = new Vector3(0, -StairsHeight * 0.5f, -StairsDepth * 0.5f);
            }

            VolumeBox.size = new Vector3(StairsWidth, StairsHeight, StairsDepth);

            VolumeBox.enabled = false;
        }
    }
}
