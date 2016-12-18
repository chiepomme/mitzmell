#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Mitzmell
{
    partial class StageInfo : ScriptableObject
    {
        [MenuItem("Assets/Create/StageInfo")]
        public static void CreateAsset()
        {
            CreateAsset<StageInfo>();
        }

        public static void CreateAsset<T>() where T : ScriptableObject
        {
            var info = CreateInstance<T>();
            var path = AssetDatabase.GenerateUniqueAssetPath("Assets/" + typeof(T).Name + ".asset");

            AssetDatabase.CreateAsset(info, path);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = info;
        }
    }
}
#endif
