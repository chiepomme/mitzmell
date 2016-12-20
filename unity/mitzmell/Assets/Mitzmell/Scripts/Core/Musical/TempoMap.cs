using System.Collections.Generic;
using System.Linq;

namespace Mitzmell
{
    class TempoMap
    {
        class Tempo
        {
            public readonly int ticks;
            public readonly float bpm;
            public readonly float seconds;

            public Tempo(int ticks, float bpm, float seconds)
            {
                this.ticks = ticks;
                this.bpm = bpm;
                this.seconds = seconds;
            }
        }

        readonly int division;
        readonly List<Tempo> Tempos = new List<Tempo>();

        public TempoMap(int division)
        {
            this.division = division;
        }

        public void Add(int ticks, float bpm)
        {
            var totalElapsedSeconds = 0f;

            for (var i = 0; i < Tempos.Count; i++)
            {
                if (i + 1 < Tempos.Count)
                {
                    totalElapsedSeconds += CalculateElapsedSeconds(Tempos[i], Tempos[i + 1].ticks);
                }
                else
                {
                    totalElapsedSeconds += CalculateElapsedSeconds(Tempos[i], ticks);
                }
            }

            Tempos.Add(new Tempo(ticks, bpm, totalElapsedSeconds));
        }

        public float GetElapsedSeconds(int ticks)
        {
            var currentTempo = Tempos.Last(t => t.ticks <= ticks);
            var elapsedSecondsFromTempoChange = CalculateElapsedSeconds(currentTempo, ticks);

            return currentTempo.seconds + elapsedSecondsFromTempoChange;
        }

        public int GetTicks(float elapsedSeconds)
        {
            var currentTempo = Tempos.Last(t => t.seconds <= elapsedSeconds);
            var elapsedSecondsFromTempoChange = elapsedSeconds - currentTempo.seconds;

            return (int)((elapsedSecondsFromTempoChange * division) / currentTempo.bpm);
        }

        float CalculateElapsedSeconds(Tempo from, int toTicks)
        {
            var elapsedTick = toTicks - from.ticks;
            return ((float)elapsedTick / division) * (60f / from.bpm);
        }
    }
}
