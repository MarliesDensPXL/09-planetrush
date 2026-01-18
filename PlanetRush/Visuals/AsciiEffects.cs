using PlanetRush.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRush.Visuals
{
    public static class AsciiEffects
    {
        #region RocketAnimations
        private static readonly string[] Rocket =
        {
            "                                            ,:",
            "                                          ,' |",
            "                                         /   :",
            "                                      --'   /",
            "                                      \\/ /:/",
            "                                      / ://_\\",
            "                                   __/   /",
            "                                   )'-. /",
            "                                   ./  :\\",
            "                                    /.' '"
        };

        private static Random rng = new Random();

        public static void LaunchRocket(int frames = 20, int msDelay = 90)
        {
            Console.CursorVisible = false;

            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            int rocketHeight = Rocket.Length;
            int rocketWidth = Rocket.Max(l => l.Length);

            int baseX = Math.Min(Math.Max(0, (int)(width * 0.8)), Math.Max(0, width - rocketWidth - 1));
            int baseY = Math.Min((int)(height * 0.05), height - rocketHeight - 4); // leave some space for exhaust

            for (int frame = 0; frame < frames; frame++)
            {
                Console.Clear();

                int jitterX = rng.Next(-1, 2);
                int jitterY = rng.Next(-1, 2);

                Console.ForegroundColor = ConsoleColor.Red;
                for (int i = 0; i < Rocket.Length; i++)
                {
                    int drawY = baseY + jitterY + i;
                    if (drawY < 0 || drawY >= height) continue;

                    int targetX = baseX + jitterX;
                    string line = Rocket[i];

                    if (targetX < 0) targetX = 0;
                    if (targetX + line.Length >= width) targetX = Math.Max(0, width - line.Length - 1);

                    Console.SetCursorPosition(targetX, drawY);
                    Console.Write(line);
                }

                DrawCoreExhaust(baseX + 20, baseY + rocketHeight + jitterY, width, height);
                DrawBlowingDust(baseX, baseY + rocketHeight, width, height);
                Thread.Sleep(msDelay);
            }

            Console.ResetColor();
            Console.Clear();
            Console.CursorVisible = true;
        }

        private static void DrawCoreExhaust(int rocketX, int topY, int consoleWidth, int consoleHeight)
        {
            Console.ForegroundColor = ConsoleColor.White;

            int coreWidth = 8;
            int coreStartX = Math.Max(0, rocketX + 6);
            coreStartX = Math.Min(coreStartX, Math.Max(0, consoleWidth - 1));

            for (int row = 0; row < 5; row++)
            {
                int y = topY + row;
                if (y < 0 || y >= consoleHeight) continue;

                int density = 6 + rng.Next(0, 6);
                for (int p = 0; p < density; p++)
                {
                    int x = coreStartX + rng.Next(0, coreWidth) - rng.Next(0, 2);
                    if (x < 0 || x >= consoleWidth) continue;

                    char ch = rng.Next(0, 3) switch
                    {
                        0 => '.',
                        1 => ',',
                        _ => '\''
                    };

                    Console.SetCursorPosition(x, y);
                    Console.Write(ch);
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void DrawBlowingDust(int rocketBaseX, int rocketBaseY, int consoleWidth, int consoleHeight)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;

            int particleCount = 28 + rng.Next(0, 20);

            for (int i = 0; i < particleCount; i++)
            {
                int dx = rocketBaseX - 2 - rng.Next(0, Math.Max(10, rocketBaseX));
                int dy = rocketBaseY + rng.Next(-1, 6);

                if (dy < 0 || dy >= consoleHeight) continue;
                if (dx < 0) dx = rng.Next(0, Math.Min(4, consoleWidth - 1)); // wrap a bit if we went negative

                char ch = rng.Next(0, 4) switch
                {
                    0 => '.',
                    1 => ',',
                    2 => '`',
                    _ => '*'
                };

                Console.SetCursorPosition(Math.Max(0, Math.Min(consoleWidth - 1, dx)), dy);
                Console.Write(ch);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        #endregion

        public static void DrawPlanet(Planet planet, int topOffset = 2)
        {
            int radius = planet.Radius;
            rng = new Random(planet.Seed);
            double xScale = 2.0;
            int planetWidth = (int)(radius * xScale * 2);

            int consoleWidth = Console.WindowWidth;
            int consoleHeight = Console.WindowHeight;
            int startX = Math.Max(0, consoleWidth - planetWidth - 2);

            // Random characters
            char[] seaOptions = { '~', '.', ',', '`', '≈', ':' };
            char[] landOptions = { '#', '@', '%', 'A', 'v', 'M', '▲', '^' };
            char seaChar = seaOptions[rng.Next(seaOptions.Length)];
            char landChar = landOptions[rng.Next(landOptions.Length)];

            // Random colors
            ConsoleColor[] possibleColors =
            {
                ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.DarkBlue, ConsoleColor.DarkCyan,
                ConsoleColor.Green, ConsoleColor.DarkGreen, ConsoleColor.Yellow, ConsoleColor.Magenta
            };
            ConsoleColor seaColor = possibleColors[rng.Next(4)];
            ConsoleColor landColor = possibleColors[rng.Next(4, possibleColors.Length)];

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            for (int y = -radius; y <= radius; y++)
            {
                int cursorY = topOffset + y + radius;
                if (cursorY >= consoleHeight - 1) break;

                for (int x = (int)(-radius * xScale); x <= radius * xScale; x++)
                {
                    double dx = x / xScale;
                    double dy = y;
                    double distance = Math.Sqrt(dx * dx + dy * dy);

                    if (distance <= radius)
                    {
                        double noise = rng.NextDouble()
                            + 0.3 * Math.Sin((x + rng.NextDouble()) * 0.3)
                            + 0.3 * Math.Cos((y + rng.NextDouble()) * 0.3);
                        bool isLand = noise < 0.5;

                        Console.ForegroundColor = isLand ? landColor : seaColor;
                        int cursorX = startX + x + (int)(radius * xScale);
                        if (cursorX >= 0 && cursorX < consoleWidth)
                        {
                            Console.SetCursorPosition(cursorX, cursorY);
                            Console.Write(isLand ? landChar : seaChar);
                        }
                    }
                }
            }
            Console.SetCursorPosition(0,0);
            Console.ResetColor();
        }

        public static void WriteMenuOption(string optionText, bool isCurrentlyChosen = false)
        {
            if (isCurrentlyChosen)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                int padding = 2; // spaces around text
                int boxWidth = optionText.Length + padding * 2;
                int consoleWidth = Console.WindowWidth;
                boxWidth = Math.Min(boxWidth, consoleWidth-1);
                Console.WriteLine(" " + new string('-', boxWidth));
                Console.WriteLine("|" + new string(' ', padding) + optionText + new string(' ', padding) + "|");
                Console.WriteLine(" " + new string('-', boxWidth));
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("   " + optionText);
                Console.WriteLine();
            }
        }

        public static async Task RevealTextAsync(string text, int startX, int startY, int delayMs = 50)
        {
            const int minAscii = 33;  // '!'
            const int maxAscii = 126; // '~'
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= text.Length; i++)
            {
                sb.Append(text.Substring(0, i));
                for (int j = i; j < text.Length; j++)
                {
                    char randomChar = (char)rng.Next(minAscii, maxAscii + 1);
                    sb.Append(randomChar);
                }
                Console.SetCursorPosition(startX, startY);
                Console.Write(sb.ToString());
                await Task.Delay(delayMs);
                sb.Clear();
            }
        }

    }
}