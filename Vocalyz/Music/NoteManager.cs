using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocalyz.Music
{
    public class NotesManager
    {
        /// <summary>
        /// Ordered by frequency
        /// </summary>
        public static string[] NOTES_SYMBOLS = new string[]
        {
            "A","A#","B","C","C#","D","D#","E","F","F#","G","G#"
        };

        public const int PIANO_KEYS_COUNT = 88;

        public static Note[] Notes;

        public static void Initialize()
        {
            Notes = GenerateNotes();
        }
        static Note[] GenerateNotes()
        {
            Note[] result = new Note[PIANO_KEYS_COUNT];

            int octave = 0;

            for (int n = 1; n < result.Length + 1; n++)
            {
                int i2 = (n - 1) % NOTES_SYMBOLS.Length;

                if ((n - 3) % NOTES_SYMBOLS.Length == 0)
                {
                    octave++;
                }
                result[n - 1] = new Note(n, NOTES_SYMBOLS[i2], octave, GetNoteFrequency(n));
            }

            return result;
        }
        public static Note FindNote(int n)
        {
            return Notes.FirstOrDefault(x => x.N == n);
        }
        public static Note FindNote(double frequency, int frequencyGap = 6)
        {
            return Notes.FirstOrDefault(x => x.Frequency > frequency - frequencyGap && x.Frequency < frequency + frequencyGap);
        }
        /// <summary>
        /// Symbol + Octave
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public static Note FindNote(string note)
        {
            return Notes.FirstOrDefault(x => x.ToString() == note);
        }
        /// <summary>
        /// https://en.wikipedia.org/wiki/Piano_key_frequencies
        /// </summary>
        /// <param name="n">number of the key [1-88]</param>
        /// <returns></returns>
        public static double GetNoteFrequency(int n)
        {
            return Math.Pow(2d, ((n - 49d) / 12d)) * 440d;
        }
        public static double GetNoteNumber(int frequency)
        {
            return 12 * Math.Log(frequency / 440d) + 49;
        }


    }

    /// <summary>
    /// To struct?
    /// </summary>
    public class Note
    {
        /// <summary>
        /// Number of the key , [1-88]
        /// </summary>
        public int N { get; set; }

        public string Symbol { get; set; }

        /// Theorical frequency of the note  , hertz
        /// </summary>
        public double Frequency { get; set; }

        public int Octave { get; set; }

        public Note(int n, string symbol, int octave, double frequency)
        {
            this.N = n;
            this.Symbol = symbol;
            this.Octave = octave;
            this.Frequency = frequency;
        }
        public override string ToString()
        {
            return Symbol + Octave;
        }
    }
}
