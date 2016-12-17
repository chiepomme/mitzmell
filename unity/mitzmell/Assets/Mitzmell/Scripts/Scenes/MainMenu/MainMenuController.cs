using UnityEngine;
using UnityEngine.UI;

namespace Mitzmell
{
    class MainMenuController : MonoBehaviour
    {
        [SerializeField]
        Button TwoDButton;
        [SerializeField]
        Button ThreeDButton;

        void Start()
        {
            AddButtonListeners();
        }

        void AddButtonListeners()
        {
            TwoDButton.onClick.AddListener(() =>
            {
                Config.Is3D = false;
                GoNext();
            });

            ThreeDButton.onClick.AddListener(() =>
            {
                Config.Is3D = true;
                GoNext();
            });
        }

        public void GoNext()
        {
            SceneTransit.TransitTo(Scenes.Title);
        }
    }
}
