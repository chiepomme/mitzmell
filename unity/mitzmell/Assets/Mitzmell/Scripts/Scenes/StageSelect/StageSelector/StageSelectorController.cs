using UnityEngine;
using UnityEngine.UI;

namespace Mitzmell
{
    class StageSelectorController : MonoBehaviour
    {
        [SerializeField]
        StageSelectorButton[] Buttons;
        [SerializeField]
        RawImage SelectedImage;
        [SerializeField]
        AudioSource PreviewSource;

        void Start()
        {
            AddButtonListeners();
        }

        void AddButtonListeners()
        {
            foreach (var button in Buttons)
            {
                button.OnSelected.AddListener(stageInfo =>
                {
                    SelectedImage.texture = stageInfo.LargeThumbnail;
                    PreviewSource.clip = stageInfo.PreviewClip;
                    PreviewSource.Play();
                });
            }
        }
    }
}
