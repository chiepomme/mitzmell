using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mitzmell
{
    class SceneTransitRunner : MonoBehaviour
    {
        static SceneTransitRunner instance;
        public static SceneTransitRunner Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameObject(typeof(SceneTransitRunner).Name).AddComponent<SceneTransitRunner>();
                    DontDestroyOnLoad(instance.gameObject);
                }
                return instance;
            }
        }

        public static void TransitTo(string destination)
        {
            var coroutine = Instance.TransitToAsync(destination);
            Instance.StartCoroutine(coroutine);
        }

        IEnumerator TransitToAsync(string destination)
        {
            yield return SceneManager.LoadSceneAsync(Scenes.Transit, LoadSceneMode.Single);
            yield return SceneManager.LoadSceneAsync(destination, LoadSceneMode.Single);
        }
    }
}
