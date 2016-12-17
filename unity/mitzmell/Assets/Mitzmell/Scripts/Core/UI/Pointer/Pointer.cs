using Mitzmell.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Mitzmell
{
    class Pointer : GvrBasePointer
    {
        [SerializeField]
        Slider ProgressSlider;

        IHasGazingProgress CurrentGazingProgress { get; set; }

        protected override void Start()
        {
            base.Start();
            ProgressSlider.gameObject.SetActive(false);
        }

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
            var newProgress = targetObject.GetComponent<IHasGazingProgress>();
            if (CurrentGazingProgress == newProgress) return;

            if (newProgress == null)
            {
                ProgressSlider.gameObject.SetActive(false);
            }
            else
            {
                CurrentGazingProgress = newProgress;
                ProgressSlider.gameObject.SetActive(true);
            }
        }

        public override void OnPointerExit(GameObject targetObject)
        {
            CurrentGazingProgress = null;
            ProgressSlider.gameObject.SetActive(false);
        }

        public override void OnPointerHover(GameObject targetObject, Vector3 intersectionPosition, Ray intersectionRay, bool isInteractive)
        {

        }

        void Update()
        {
            UpdateProgressSlider();
        }

        void UpdateProgressSlider()
        {
            if (CurrentGazingProgress == null) return;
            ProgressSlider.normalizedValue = CurrentGazingProgress.GazeProgressionRate;
        }
    }
}
