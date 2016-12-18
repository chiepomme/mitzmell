using UnityEngine;

namespace Mitzmell
{
    partial class StageInfo : ScriptableObject
    {
        public string Name;
        public Texture SmallThumbnail;
        public Texture LargeThumbnail;
        public string AuthorUrl;
        public string DownloadPath;
        public AudioClip PreviewClip;
    }
}
