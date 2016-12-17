using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Mitzmell
{
    class TitleController : MonoBehaviour
    {
        [SerializeField]
        float WaitSeconds;
        [SerializeField]
        Button GoNextButton;

        IEnumerator Start()
        {
            AddButtonListeners();
            yield return new WaitForSeconds(WaitSeconds);
            GoNext();
        }

        void AddButtonListeners()
        {
            GoNextButton.onClick.AddListener(() => GoNext());
        }

        public void GoNext()
        {
            SceneTransit.TransitTo(Scenes.Title);
        }
    }
}
