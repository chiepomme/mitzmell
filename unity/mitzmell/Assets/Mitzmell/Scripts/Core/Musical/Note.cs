namespace Mitzmell
{
    class Note
    {
        public int ticks;
        public float seconds;
        public MusicalTime musicalTime;

        public Note(int ticks, float seconds, MusicalTime musicalTime)
        {
            this.ticks = ticks;
            this.seconds = seconds;
            this.musicalTime = musicalTime;
        }
    }
}
