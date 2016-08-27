using UnityEngine;
using System.Collections.Generic;

namespace BoxStairsTool
{
    [ExecuteInEditMode]
    public sealed class BoxStairs : MonoBehaviour
    {
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
                Step.transform.localPosition = new Vector3(0, halfStepHeight + (i * stepHeight), halfStairsDepth + (i * halfStepDepth));
                Step.transform.localRotation = Quaternion.identity;

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
