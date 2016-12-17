using System;
using UnityEngine;
using UnityEngine.UI;

namespace Mitzmell.UI
{
    class CircularLayoutGroup : MonoBehaviour, ILayoutGroup
    {
        [SerializeField]
        int MinItemCount;
        [SerializeField]
        float Radius;
        [SerializeField]
        bool Clockwise;

        Vector3 CalculatePosition(int index)
        {
            var placeCount = Math.Max(MinItemCount, transform.childCount);
            var step = (float)360 / placeCount;
            var normalizedPos = Clockwise ? placeCount - index - 0.5f : index + 0.5f;
            var degree = step * normalizedPos + 90f;
            var radian = degree * Mathf.Deg2Rad;

            var direction = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian));
            return direction * Radius;
        }

        [ContextMenu("Reposition")]
        public void Reposition()
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                var rectTransform = child as RectTransform;
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition = CalculatePosition(i);
                }
                else
                {
                    child.localPosition = CalculatePosition(i);
                }
            }
        }

        public void SetLayoutHorizontal() { Reposition(); }
        public void SetLayoutVertical() { Reposition(); }

        void OnDrawGizmosSelected()
        {
            // draw item positions with page switcher
            var placeCount = Math.Max(MinItemCount, transform.childCount);
            for (var i = 0; i < placeCount; i++)
            {
                var localPos = CalculatePosition(i);
                var worldPos = transform.TransformPoint(localPos);
                Debug.DrawLine(worldPos + Vector3.up * 0.1f, worldPos + Vector3.down * 0.1f, Color.magenta);
                Debug.DrawLine(worldPos + Vector3.left * 0.1f, worldPos + Vector3.right * 0.1f, Color.magenta);
            }
        }
    }
}
