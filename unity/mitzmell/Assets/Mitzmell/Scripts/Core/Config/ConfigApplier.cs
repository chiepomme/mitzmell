using UnityEngine;

namespace Mitzmell
{
    class ConfigApplier : MonoBehaviour
    {
        [SerializeField]
        GvrViewer VRViewer;

        void Start()
        {
            if (VRViewer != null) VRViewer.VRModeEnabled = Config.Is3D;
        }
    }
}
