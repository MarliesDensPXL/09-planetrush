using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRush.Sounds
{
    internal static class SoundBoard
    {
        private static Random rng = new Random();

        private static void PlaySound(int frequency, int duration)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.Beep(frequency, duration); // Works only on Windows
            }
            else
            {
                Console.Beep();
            }
        }

        public static void PlayUISound()
        {
            int freq = rng.Next(1000, 1200);
            int dur = rng.Next(60, 100);
            PlaySound(freq, dur);
        }

        public static void PlayConfirmSound()
        {
            PlaySound(1800, 120);
        }

        public static void PlayFailSound()
        {
            PlaySound(400, 150);
            PlaySound(300, 250);
        }

        public static void PlayVictorySound()
        {
            int[][] melody = new int[][]
            {
                new[] { 800, 120 },
                new[] { 1000, 120 },
                new[] { 1200, 150 },
                new[] { 1400, 200 },
                new[] { 1600, 300 }
            };

            foreach (var note in melody)
            {
                PlaySound(note[0], note[1]);
                Thread.Sleep(10);
            }
        }
    }
}
