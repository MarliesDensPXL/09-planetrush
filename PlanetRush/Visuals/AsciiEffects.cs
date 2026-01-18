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

        #region Planet
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
            Console.SetCursorPosition(0, 0);
            Console.ResetColor();
        }
        #endregion

        #region Star
        public static void DrawStar(Random rand)
        {
            double p = rand.NextDouble() * 0.17 + 0.15;
            int numberOfSpikes = rand.Next(2, 7);
            double spikeStrength = Math.Sqrt(rand.NextDouble() * 2) / 10 + 0.05;
            int[] allowedColors = { 4, 11, 12, 14, 14, 14, 15, 15 };
            ConsoleColor color = (ConsoleColor)allowedColors[rand.Next(allowedColors.Length)];

            int charStartX = Console.WindowWidth / 2;
            int charWidth = Console.WindowWidth - charStartX - 5;
            int charHeight = Console.WindowHeight - 5;
            charStartX += 15;
            int size = Math.Min(charWidth, charHeight);
            charHeight = size;
            charWidth = size * 2;

            DrawPSquircle(charStartX, 3, charWidth, charHeight, p, color, numberOfSpikes, spikeStrength);

            Console.SetCursorPosition(0, 7);
            Console.ResetColor();
        }


        static void DrawPSquircle(int xStart, int yStart, int xWidth, int yHeight, double p, ConsoleColor color, int numberOfSpikes, double spikeStrength)
        {
            int pixelWidth = xWidth * 2;
            int pixelHeight = yHeight * 4;

            double centerX = pixelWidth / 2.0;
            double centerY = pixelHeight / 2.0;

            bool[,] pixels = new bool[pixelWidth, pixelHeight];

            int samples = 1024;
            double[] rawR = new double[samples];
            double maxRawR = 0;
            for (int i = 0; i < samples; i++)
            {
                double theta = (i / (double)samples) * Math.PI * numberOfSpikes;
                double cosT = Math.Cos(theta);
                double sinT = Math.Sin(theta);
                double r = Math.Pow(Math.Pow(Math.Abs(cosT), p) + Math.Pow(Math.Abs(sinT), p), -1.0 / p);
                rawR[i] = r;
                if (r > maxRawR) maxRawR = r;
            }

            if (maxRawR <= 0) maxRawR = 1.0;

            double bias = 0.01;
            double gamma = 0.7;
            for (int i = 0; i < samples; i++)
            {
                double norm = rawR[i] / maxRawR;
                rawR[i] = bias + (1.0 - bias) * Math.Pow(norm, gamma);
            }

            int radialSteps = Math.Max(20, (int)(pixelWidth * 0.8));
            double rxPixels = pixelWidth / 2.0;
            double ryPixels = pixelHeight / 2.0;
            int spikes = numberOfSpikes;

            for (int i = 0; i < samples; i++)
            {
                double theta = (i / (double)samples) * Math.PI * 2;
                double cosT = Math.Cos(theta);
                double sinT = Math.Sin(theta);

                double baseR = rawR[i];
                double spikeWave = Math.Pow(Math.Abs(Math.Sin(spikes * theta)), 5.0);
                double rMod = baseR * (1.0 + spikeStrength * spikeWave);

                for (int s = 0; s <= radialSteps; s++)
                {
                    double t = s / (double)radialSteps;
                    double curR = t * rMod;
                    double fx = centerX + curR * rxPixels * cosT;
                    double fy = centerY + curR * ryPixels * sinT;

                    int px = (int)Math.Round(fx);
                    int py = (int)Math.Round(fy);
                    if (px >= 0 && px < pixelWidth && py >= 0 && py < pixelHeight)
                        pixels[px, py] = true;
                }
            }

            DrawBrailleBuffer(pixels, xStart, yStart, xWidth, yHeight, color);
        }

        static void DrawBrailleBuffer(bool[,] pixels, int xStart, int yStart, int xWidth, int yHeight, ConsoleColor color)
        {
            int pixelWidth = pixels.GetLength(0);
            int pixelHeight = pixels.GetLength(1);

            Console.ForegroundColor = color;

            for (int by = 0; by < yHeight; by++)
            {
                for (int bx = 0; bx < xWidth; bx++)
                {
                    int code = 0;
                    for (int dy = 0; dy < 4; dy++)
                    {
                        for (int dx = 0; dx < 2; dx++)
                        {
                            int px = bx * 2 + dx;
                            int py = by * 4 + dy;
                            if (px >= 0 && px < pixelWidth && py >= 0 && py < pixelHeight && pixels[px, py])
                                code |= DotMask(dx, dy);
                        }
                    }

                    if (code != 0)
                    {
                        int drawX = xStart + bx;
                        int drawY = yStart + by;
                        if (drawX >= 0 && drawX < Console.WindowWidth &&
                            drawY >= 0 && drawY < Console.WindowHeight)
                        {
                            Console.SetCursorPosition(drawX, drawY);
                            Console.Write((char)(0x2800 + code));
                        }
                    }
                }
            }

            Console.ResetColor();
        }

        static int DotMask(int dx, int dy)
        {
            int[,] dotNumbers = {
                {1,4},
                {2,5},
                {3,6},
                {7,8}
            };
            int dot = dotNumbers[dy, dx];
            return 1 << (dot - 1);
        }
        #endregion

        public static void WriteMenuOption(string optionText, bool isCurrentlyChosen = false)
        {
            if (isCurrentlyChosen)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                int padding = 2;
                int boxWidth = optionText.Length + padding * 2;
                int consoleWidth = Console.WindowWidth;
                boxWidth = Math.Min(boxWidth, consoleWidth - 1);
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
            const int minAscii = 33;
            const int maxAscii = 126;
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

        public static void PressEnterToContinue()
        {
            Console.WriteLine("\nPress Enter to continue...");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
        }

    }
}