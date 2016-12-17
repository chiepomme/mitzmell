namespace Mitzmell
{
    static class Scenes
    {
        const string RootFolder = "Mitzmell/Scenes/";

        public static readonly string Title = GetScenePathByName("Title");
        public static readonly string MainMenu = GetScenePathByName("MainMenu");
        public static readonly string Transit = GetScenePathByName("Transit");

        static string GetScenePathByName(string name)
        {
            return RootFolder + name;
        }
    }
}
