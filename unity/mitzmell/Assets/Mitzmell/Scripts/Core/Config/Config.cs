namespace Mitzmell
{
    class Config
    {
        public static bool Is3D { get; set; }

        static Config()
        {
            Is3D = true;
        }
    }
}
