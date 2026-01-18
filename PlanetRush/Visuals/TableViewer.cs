using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRush.Visuals
{
    public static class TableViewer
    {
        public static void ViewTable(List<string[]> rows, string[] headers, bool autoColorStyling = true)
        {
            if (headers == null || headers.Length == 0)
                throw new ArgumentException("Headers required.");

            if (rows.Any(r => r.Length != headers.Length))
                throw new ArgumentException("Each row must match header length.");

            int[] columnWidths = CalculateColumnWidths(rows, headers);

            int selectedRow = 0;
            int topRow = 0;

            ConsoleKey key;
            do
            {
                Console.Clear();
                DrawTable(rows, headers, columnWidths, topRow, selectedRow, autoColorStyling);

                key = Console.ReadKey(true).Key;

                int visibleRowCount = GetVisibleRowCount();

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedRow > 0) selectedRow--;
                        if (selectedRow < topRow) topRow--;
                        break;

                    case ConsoleKey.DownArrow:
                        if (selectedRow < rows.Count - 1) selectedRow++;
                        if (selectedRow >= topRow + visibleRowCount) topRow++;
                        break;
                }

            } while (key != ConsoleKey.Q);
        }

        private static int[] CalculateColumnWidths(List<string[]> rows, string[] headers)
        {
            int cols = headers.Length;
            int[] widths = new int[cols];

            for (int c = 0; c < cols; c++)
            {
                int maxCell = rows
                    .Select(r => r[c]?.Length ?? 0)
                    .DefaultIfEmpty(0)
                    .Max();

                widths[c] = Math.Max(headers[c].Length, maxCell) + 2;
            }

            return widths;
        }

        private static void DrawTable(List<string[]> rows, string[] headers, int[] widths, 
                                            int topRow, int selectedRow, bool autoColor)
        {
            PrintSeparator(widths);
            PrintHeader(headers, widths);
            PrintSeparator(widths);

            int visibleRows = GetVisibleRowCount();

            for (int i = 0; i < visibleRows && topRow + i < rows.Count; i++)
            {
                int rowIndex = topRow + i;
                bool isSelected = rowIndex == selectedRow;

                PrintRow(rows[rowIndex], widths, autoColor, isSelected);
            }

            PrintSeparator(widths);

            Console.WriteLine("↑↓ Move  q Quit");
        }

        private static void PrintSeparator(int[] widths)
        {
            Console.Write("+");
            foreach (var w in widths)
                Console.Write(new string('-', w) + "+");
            Console.WriteLine();
        }

        private static void PrintHeader(string[] headers, int[] widths)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("|");
            for (int i = 0; i < headers.Length; i++)
            {
                Console.Write(" " + headers[i].PadRight(widths[i] - 1));
                Console.Write("|");
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        private static void PrintRow(string[] cells, int[] widths, bool autoColor, bool selected)
        {
            if (selected)
                Console.BackgroundColor = ConsoleColor.DarkGray;

            Console.Write("|");

            for (int i = 0; i < cells.Length; i++)
            {
                if (autoColor && !selected)
                    ApplyColor(cells[i]);

                Console.Write(" " + (cells[i] ?? "").PadRight(widths[i] - 1));
                Console.ResetColor();
                Console.Write("|");

                if (selected)
                    Console.BackgroundColor = ConsoleColor.DarkGray;
            }

            Console.WriteLine();
            Console.ResetColor();
        }

        private static void ApplyColor(string value)
        {
            if (bool.TryParse(value, out bool b))
            {
                Console.ForegroundColor = b ? ConsoleColor.Green : ConsoleColor.Red;
                return;
            }

            if (double.TryParse(value, out double n))
            {
                if (n > 0) Console.ForegroundColor = ConsoleColor.Green;
                else if (n < 0) Console.ForegroundColor = ConsoleColor.Red;
                else Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private static int GetVisibleRowCount()
        {
            return Math.Max(1, Console.WindowHeight - 6);
        }
    }
}
