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

            float stepHeight = StairsHeight / StepsNumber;
            float stepDepth = StairsDepth / StepsNumber;

            for (int i = 0; i < StepsNumber; i++)
            {
                GameObject Step = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Step.name = "Step " + i;
                Step.transform.SetParent(Root.transform);
                Step.transform.localScale = new Vector3(StairsWidth, stepHeight, StairsDepth - (i * stepDepth));
                Step.transform.position = new Vector3(0, i * stepHeight, i * (stepDepth / 2));

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
    }
}
