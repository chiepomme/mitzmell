using Mitzmell.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Mitzmell
{
    class GazingButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IGvrPointerHoverHandler, IHasGazingProgress
    {
        const float SecondsNeededToComplete = 1.5f;

        float gazingSeconds;

        public readonly UnityEvent OnGazeCompleted = new UnityEvent();
        public readonly UnityEvent OnGazeStarted = new UnityEvent();
        public readonly UnityEvent OnGazeEnded = new UnityEvent();

        bool GazeInProgress { get; set; }
        public float GazeProgressionRate { get { return gazingSeconds / SecondsNeededToComplete; } }

        public void OnPointerEnter(PointerEventData eventData)
        {
            gazingSeconds = 0f;
            GazeInProgress = true;
            OnGazeStarted.Invoke();
        }

        public void OnGvrPointerHover(PointerEventData eventData)
        {
            if (!GazeInProgress) return;

            gazingSeconds += Time.deltaTime;
            if (gazingSeconds < SecondsNeededToComplete) return;

            OnGazeCompleted.Invoke();
            GazeInProgress = false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            GazeInProgress = false;
            OnGazeEnded.Invoke();
        }
    }
}
