using UnityEngine;
using UnityEngine.EventSystems;

namespace Mitzmell
{
    class GazingButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IGvrPointerHoverHandler
    {
        bool isGazing;
        float gazingSeconds;

        public void OnPointerEnter(PointerEventData eventData)
        {
            gazingSeconds = 0f;
            isGazing = true;
        }

        public void OnGvrPointerHover(PointerEventData eventData)
        {
            if (!isGazing) return;

            gazingSeconds += Time.deltaTime;
            if (gazingSeconds < 1f) return;

            print("gazed!");
            isGazing = false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isGazing = false;
        }
    }
}
