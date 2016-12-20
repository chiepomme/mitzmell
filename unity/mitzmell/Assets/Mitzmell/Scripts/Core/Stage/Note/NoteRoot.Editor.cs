#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Mitzmell
{
    partial class NoteRoot : MonoBehaviour
    {
        class NotePair
        {
            public readonly Note note;
            public readonly NoteView view;

            public NotePair(Note note, NoteView view)
            {
                this.note = note;
                this.view = view;
            }
        }

        [ContextMenu("Read note timings from smf")]
        public void ReadSmf()
        {
            var noteList = AskFilePathAndReadNoteList();
            if (noteList == null) return;

            var notePairs = GetComponentsInChildren<NoteView>(includeInactive: true)
                                .Select((v, i) => new NotePair(noteList.ElementAtOrDefault(i), v));

            EraceAllMeasureSeparators();
            RenameOverflowedNoteViews(notePairs);
            AlertIfNoteOverflows(noteList, notePairs);

            var validNotePair = notePairs.Where(p => p.note != null);
            SetSecondsToNoteViews(validNotePair);
            DrawMeasureSeparators(validNotePair);
        }

        NoteList AskFilePathAndReadNoteList()
        {
            var path = EditorUtility.OpenFilePanel("Select smf file", "", "mid");
            if (string.IsNullOrEmpty(path)) return null;

            return new SmfReader().Read(path);
        }

        void RenameOverflowedNoteViews(IEnumerable<NotePair> pairs)
        {
            var overflowedViews = pairs.Where(p => p.note == null)
                                       .Select(p => p.view);
            foreach (var view in overflowedViews)
            {
                view.gameObject.name = "[----:--:----]";
                view.seconds = 10000f;
            }
        }

        void AlertIfNoteOverflows(NoteList noteList, IEnumerable<NotePair> pairs)
        {
            var overflowedNoteCount = noteList.Count() - pairs.Count();
            if (overflowedNoteCount > 0)
            {
                EditorUtility.DisplayDialog(overflowedNoteCount + " notes overflowed", overflowedNoteCount + " notes overflowed. Create more NoteViews.", "OK");
            }
        }

        void SetSecondsToNoteViews(IEnumerable<NotePair> pairs)
        {
            foreach (var pair in pairs)
            {
                pair.view.seconds = pair.note != null ? pair.note.seconds : 0f;
                pair.view.gameObject.name = pair.note.musicalTime.ToString();
            }
        }

        void EraceAllMeasureSeparators()
        {
            foreach (var separator in GetComponentsInChildren<MeasureSeparator>(includeInactive: true))
            {
                DestroyImmediate(separator.gameObject);
            }
        }

        void DrawMeasureSeparators(IEnumerable<NotePair> pairs)
        {
            EraceAllMeasureSeparators();

            var orderedGameObjects = new List<GameObject>();

            int prevMeasures = 0;
            foreach (var pair in pairs)
            {
                var measures = pair.note.musicalTime.measures;
                var measureDiff = measures - prevMeasures;

                for (var i = 0; i < measureDiff; i++)
                {
                    var separator = CreateMeasureSeparator(prevMeasures + i + 1);
                    orderedGameObjects.Add(separator.gameObject);
                }

                orderedGameObjects.Add(pair.view.gameObject);
                prevMeasures = measures;
            }

            // sort objects including separators
            for (var i = 0; i < orderedGameObjects.Count; i++)
            {
                orderedGameObjects[i].transform.SetSiblingIndex(i);
            }
        }

        MeasureSeparator CreateMeasureSeparator(int measures)
        {
            var separator = new GameObject("-- measure " + measures + " -----------------");
            separator.transform.SetParent(transform, false);
            return separator.AddComponent<MeasureSeparator>();
        }
    }
}
#endif