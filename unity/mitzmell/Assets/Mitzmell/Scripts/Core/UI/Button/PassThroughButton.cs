using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Mitzmell.UI
{
    class PassThroughButton : MonoBehaviour
    {
        [SerializeField]
        public GazingButton LeftButton;
        [SerializeField]
        public GazingButton RightButton;

        bool GazingLeft { get; set; }
        bool GazingRight { get; set; }

        public UnityEvent OnPassedToLeft = new UnityEvent();
        public UnityEvent OnPassedToRight = new UnityEvent();

        void Start()
        {
            AddButtonListeners();
        }

        void AddButtonListeners()
        {
            LeftButton.OnGazeStarted.AddListener(() =>
            {
                if (GazingRight)
                {
                    OnPassedToLeft.Invoke();
                    GazingRight = false;
                }

                StopCoroutine("WaitAndFinishGazingLeft");
                GazingLeft = true;
            });

            RightButton.OnGazeStarted.AddListener(() =>
            {
                if (GazingLeft)
                {
                    OnPassedToRight.Invoke();
                    GazingLeft = false;
                }

                StopCoroutine("WaitAndFinishGazingRight");
                GazingRight = true;
            });

            LeftButton.OnGazeEnded.AddListener(() =>
            {
                StopCoroutine("WaitAndFinishGazingLeft");
                StartCoroutine("WaitAndFinishGazingLeft");
            });

            RightButton.OnGazeEnded.AddListener(() =>
            {
                StopCoroutine("WaitAndFinishGazingRight");
                StartCoroutine("WaitAndFinishGazingRight");
            });
        }

        IEnumerator WaitAndFinishGazingLeft()
        {
            yield return new WaitForSeconds(1f);
            GazingLeft = false;
        }

        IEnumerator WaitAndFinishGazingRight()
        {
            yield return new WaitForSeconds(1f);
            GazingRight = false;
        }
    }
}
