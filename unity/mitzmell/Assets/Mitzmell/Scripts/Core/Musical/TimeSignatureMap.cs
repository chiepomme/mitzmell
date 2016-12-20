using System.Collections.Generic;
using System.Linq;

namespace Mitzmell
{
    class TimeSignatureMap
    {
        class TimeSignature
        {
            public readonly int ticks;
            public readonly int numerator;
            public readonly int denominator;
            public readonly MusicalTime musicalTime;

            public TimeSignature(int ticks, int numerator, int denominator, MusicalTime musicalTime)
            {
                this.ticks = ticks;
                this.numerator = numerator;
                this.denominator = denominator;
                this.musicalTime = musicalTime;
            }
        }

        readonly int division;
        readonly List<TimeSignature> TimeSignatures = new List<TimeSignature>();

        public TimeSignatureMap(int division)
        {
            this.division = division;
        }

        public void Add(int ticks, int numerator, int denominator)
        {
            var measures = 1;

            for (var i = 0; i < TimeSignatures.Count; i++)
            {
                if (i + 1 < TimeSignatures.Count)
                {
                    measures += CalculateElapsedMeasures(TimeSignatures[i], TimeSignatures[i + 1].ticks);
                }
                else
                {
                    measures += CalculateElapsedMeasures(TimeSignatures[i], ticks);
                }
            }

            TimeSignatures.Add(new TimeSignature(ticks, numerator, denominator, new MusicalTime(measures, 1, 0)));
        }

        int CalculateElapsedMeasures(TimeSignature from, int ticks)
        {
            int ticksInBeat, ticksInMeasure;
            CalculateMusicalUnits(from, out ticksInBeat, out ticksInMeasure);

            var elapsedTicks = ticks - from.ticks;
            var elapsedMeasures = elapsedTicks / ticksInMeasure;

            return elapsedMeasures;
        }

        public MusicalTime GetMusicalTime(int tick)
        {
            var currentSignature = TimeSignatures.Last(t => t.ticks <= tick);

            int ticksInBeat, ticksInMeasure;
            CalculateMusicalUnits(currentSignature, out ticksInBeat, out ticksInMeasure);

            var elapsedTicks = tick - currentSignature.ticks;
            var elapsedMeasures = elapsedTicks / ticksInMeasure;

            var measures = currentSignature.musicalTime.measures + elapsedMeasures;
            var beats = currentSignature.musicalTime.beats + (elapsedTicks % ticksInMeasure) / ticksInBeat;
            var ticks = currentSignature.musicalTime.ticks + (elapsedTicks % ticksInMeasure) % ticksInBeat;

            return new MusicalTime(measures, beats, ticks);
        }

        void CalculateMusicalUnits(TimeSignature timeSignature, out int ticksInBeat, out int ticksInMeasure)
        {
            // denominator and division are quarter note based
            ticksInBeat = (int)((1 / (timeSignature.denominator / 4f)) * division);
            ticksInMeasure = ticksInBeat * timeSignature.numerator;
        }
    }
}
