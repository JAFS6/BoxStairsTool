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
    public sealed class BoxStairs : MonoBehaviour
    {
        [SerializeField]
        private PivotType Pivot;
        [SerializeField]
        private float StairsWidth;
        [SerializeField]
        private float StairsHeight;
        [SerializeField]
        private float StairsDepth;
        [SerializeField]
        private int StepsNumber;
        [SerializeField]
        private Material StairsMaterial;

        private GameObject Root;

        private const float MinimumLength = 0.000000001f; // Minimum Length for a length unit

        private void Start()
        {
            Root = this.gameObject;

            this.CreateStairs();
        }

        public BoxStairs()
        {
            StairsWidth = 1.0f;
            StairsHeight = 0.5f;
            StairsDepth = 1.0f;
            StepsNumber = 2;
            StairsMaterial = null;
        }

        public void CreateStairs()
        {
            // Validate parameters
            StairsWidth = GuaranteeMinimumLength(StairsWidth);
            StairsHeight = GuaranteeMinimumLength(StairsHeight);
            StairsDepth = GuaranteeMinimumLength(StairsDepth);

            if (StepsNumber < 1)
            {
                StepsNumber = 1;
            }

            // If any child has been created, destroy it

            List<GameObject> children = new List<GameObject>();
            children.Clear();

            foreach (Transform child in Root.transform)
            {
                children.Add(child.gameObject);
            }

            while (children.Count > 0)
            {
                DestroyImmediate(children[0]);
                children.RemoveAt(0);
            }

            // Create the new childs

            float halfStairsDepth = StairsDepth / 2;
            float stepHeight = StairsHeight / StepsNumber;
            float halfStepHeight = stepHeight / 2;
            float stepDepth = StairsDepth / StepsNumber;
            float halfStepDepth = stepDepth / 2;

            for (int i = 0; i < StepsNumber; i++)
            {
                GameObject Step = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Step.name = "Step " + i;
                Step.transform.SetParent(Root.transform);
                Step.transform.localScale = new Vector3(StairsWidth, stepHeight, StairsDepth - (i * stepDepth));
                Step.transform.localRotation = Quaternion.identity;

                switch (Pivot)
                {
                    case PivotType.Downstairs:
                        Step.transform.localPosition = new Vector3(0, halfStepHeight + (i * stepHeight), halfStairsDepth + (i * halfStepDepth));
                        break;

                    case PivotType.Upstairs:
                        int fixedIndex = StepsNumber - 1 - i;
                        Step.transform.localPosition = new Vector3(0, -halfStepHeight - (fixedIndex * stepHeight), -halfStepDepth - (fixedIndex * halfStepDepth));
                        break;
                }

                if (StairsMaterial != null)
                {
                    Renderer renderer = Step.GetComponent<Renderer>();

                    if (renderer != null)
                    {
                        renderer.material = StairsMaterial;
                    }
                }
            }
        }

        /*
         * This methods checks a value which represents a length unit, if the value is below a MinimumLength the returned value will be MinimumLength, in another case it returns the value.
         */
        private float GuaranteeMinimumLength (float value)
        {
            if (value < MinimumLength)
            {
                return MinimumLength;
            }

            return value;
        }
    }
}
