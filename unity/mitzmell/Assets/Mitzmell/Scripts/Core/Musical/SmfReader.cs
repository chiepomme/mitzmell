using SmfLitePlus;
using System;
using System.IO;

namespace Mitzmell
{
    class SmfReader
    {
        public NoteList Read(string path)
        {
            var smf = MidiFileLoader.Load(File.ReadAllBytes(path));

            var conductorTrack = smf.tracks[0];
            var noteTrack = smf.tracks[1];

            var tempoMap = new TempoMap(smf.division);
            var timeSignatureMap = new TimeSignatureMap(smf.division);

            ParseConductorTrack(conductorTrack, tempoMap, timeSignatureMap);

            var noteList = new NoteList(tempoMap, timeSignatureMap);
            ParseNoteTrack(smf, noteTrack, noteList);

            return noteList;
        }

        void ParseNoteTrack(MidiFileContainer smf, MidiTrack noteTrack, NoteList noteList)
        {
            var ticks = 0;
            foreach (var ev in noteTrack)
            {
                ticks += ev.delta;
                if (!ev.midiEvent.HasValue) continue;
                if (ev.midiEvent.Value.statusType != MidiEvent.StatusType.NOTE_ON) continue;

                noteList.Add(ticks);
            }
        }

        void ParseConductorTrack(MidiTrack conductorTrack, TempoMap tempoMap, TimeSignatureMap timeSignatureMap)
        {
            var ticks = 0;
            foreach (var ev in conductorTrack)
            {
                ticks += ev.delta;
                if (!ev.metaEvent.HasValue) continue;

                var meta = ev.metaEvent.Value;
                if (meta.type == MetaEvent.Type.SET_TEMPO)
                {
                    tempoMap.Add(ticks, ParseTempo(meta));
                }

                if (meta.type == MetaEvent.Type.TIME_SIGNATURE)
                {
                    int numerator, denominator;
                    ParseTimeSignature(meta, out numerator, out denominator);
                    timeSignatureMap.Add(ticks, numerator, denominator);
                }
            }
        }

        float ParseTempo(MetaEvent ev)
        {
            var microSec = (ev.data[0] << 16) + (ev.data[1] << 8) + ev.data[2];
            var bpm = (60 * 1000 * 1000) / (float)microSec;
            return bpm;
        }

        void ParseTimeSignature(MetaEvent ev, out int numerator, out int denominator)
        {
            numerator = ev.data[0];
            denominator = (int)Math.Pow(2, ev.data[1]);
        }
    }
}

