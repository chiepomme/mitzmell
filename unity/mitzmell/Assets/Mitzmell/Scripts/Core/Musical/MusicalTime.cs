namespace Mitzmell
{
    struct MusicalTime
    {
        public readonly int measures;
        public readonly int beats;
        public readonly int ticks;

        public MusicalTime(int measures, int beats, int ticks)
        {
            this.measures = measures;
            this.beats = beats;
            this.ticks = ticks;
        }

        public override string ToString()
        {
            return string.Format("[{0:0000}:{1:00}:{2:0000}]", measures, beats, ticks);
        }
    }
}
