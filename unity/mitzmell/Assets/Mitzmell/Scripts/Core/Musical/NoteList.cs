using System;
using System.Collections;
using System.Collections.Generic;

namespace Mitzmell
{
    class NoteList : IEnumerable<Note>
    {
        public readonly TempoMap tempoMap;
        public readonly TimeSignatureMap timeSignatureMap;
        readonly List<Note> notes = new List<Note>();

        public NoteList(TempoMap tempoMap, TimeSignatureMap timeSignatureMap)
        {
            this.tempoMap = tempoMap;
            this.timeSignatureMap = timeSignatureMap;
        }

        public void Add(int ticks)
        {
            var seconds = tempoMap.GetElapsedSeconds(ticks);
            var musicalTime = timeSignatureMap.GetMusicalTime(ticks);
            notes.Add(new Note(ticks, seconds, musicalTime));
        }

        public IEnumerator<Note> GetEnumerator()
        {
            return notes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return notes.GetEnumerator();
        }

        internal float GetElementAt()
        {
            throw new NotImplementedException();
        }
    }
}
