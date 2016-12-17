using UnityEngine;

namespace Mitzmell
{
    [ExecuteInEditMode]
    class LookAtCamera : MonoBehaviour
    {
        public bool Flip;

        void LateUpdate()
        {
            if (Flip)
            {
                transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
            }
            else
            {
                transform.rotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position);
            }
        }
    }
}
