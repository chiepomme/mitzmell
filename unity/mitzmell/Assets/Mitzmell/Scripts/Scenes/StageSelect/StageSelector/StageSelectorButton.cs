using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Mitzmell
{
    class StageSelectorButton : MonoBehaviour
    {
        [SerializeField]
        GazingButton Button;
        [SerializeField]
        RawImage StageThumbnail;
        [SerializeField]
        StageInfo StageInfo;

        public class StageSelectEvent : UnityEvent<StageInfo> { }
        public StageSelectEvent OnSelected = new StageSelectEvent();

        void Start()
        {
            Button.OnGazeStarted.AddListener(() => OnSelected.Invoke(StageInfo));

            if (StageInfo != null)
            {
                StageThumbnail.texture = StageInfo.SmallThumbnail;
            }
        }
    }
}