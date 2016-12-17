using UnityEngine;

namespace Mitzmell
{
    class Pointer : GvrBasePointer
    {
        public override float GetMaxPointerDistance() { return 50f; }

        public override void GetPointerRadius(out float innerRadius, out float outerRadius)
        {
            innerRadius = 0.01f;
            outerRadius = 0.1f;
        }

        public override void OnInputModuleDisabled()
        {

        }

        public override void OnInputModuleEnabled()
        {

        }

        public override void OnPointerClickDown()
        {

        }

        public override void OnPointerClickUp()
        {

        }

        public override void OnPointerEnter(GameObject targetObject, Vector3 intersectionPosition, Ray intersectionRay, bool isInteractive)
        {

        }

        public override void OnPointerExit(GameObject targetObject)
        {

        }

        public override void OnPointerHover(GameObject targetObject, Vector3 intersectionPosition, Ray intersectionRay, bool isInteractive)
        {

        }
    }
}
