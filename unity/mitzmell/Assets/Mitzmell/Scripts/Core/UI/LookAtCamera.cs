using UnityEngine;

namespace Mitzmell
{
    [ExecuteInEditMode]
    class LookAtCamera : MonoBehaviour
    {
        void LateUpdate()
        {
            transform.LookAt(Camera.main.transform.position, Vector3.up);
        }
    }
}
