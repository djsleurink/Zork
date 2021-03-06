﻿using System;

namespace Zork.UIContext
{
    /// <summary>
    /// Sets the ConsoleColor as specified, and resets the colors upon disposal.
    /// </summary>
    public sealed class ColorContext : IDisposable
    {
        public static ConsoleColor HeaderColor = ConsoleColor.DarkMagenta;
        public static ConsoleColor FailureColor = ConsoleColor.Red;
        public static ConsoleColor PreambleColor = ConsoleColor.DarkGreen;
        public static ConsoleColor InstructionsColor = ConsoleColor.Yellow;
        public static ConsoleColor DirectionsColor = ConsoleColor.Gray;
        public static ConsoleColor KeyCodeColor = ConsoleColor.Green;
        public static ConsoleColor MapPlayerLocation = ConsoleColor.Red;
        public static ConsoleColor MapAvailableSquare = ConsoleColor.White;
        public static ConsoleColor MapWall = ConsoleColor.DarkGray;
        public static ConsoleColor BattleHit = ConsoleColor.DarkGreen;
        public static ConsoleColor BattleDamage = ConsoleColor.DarkRed;
        public static ConsoleColor BattleWin = ConsoleColor.Green;
        public static ConsoleColor BattleLose = ConsoleColor.Red;
        public static ConsoleColor MapNPCLocation = ConsoleColor.Red;

        private ConsoleColor _originalForegroundColor;
        private ConsoleColor _originalBackgroundColor;

        public ColorContext(ConsoleColor foregroundColor, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            _originalForegroundColor = Console.ForegroundColor;
            _originalBackgroundColor = Console.BackgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
        }

        /// <summary>
        /// Write the value padded to the window width, so that the backgroundcolor applies to the whole line,
        /// not just the printed text.
        /// </summary>
        /// <param name="value">A string to print</param>
        public static void WriteFullLine(string value)
        {
            Console.WriteLine(value.PadRight(Console.WindowWidth - 1));
        }

        public static void PrintWithKeyCodes(string value)
        {
            ConsoleColor startColor = Console.ForegroundColor;
            for(int i = 0; i < value.Length; i++)
            {
                if(value[i] == ']')
                {
                    Console.ForegroundColor = startColor;
                }
                Console.Write(value[i]);
                if (value[i] == '[')
                {
                    Console.ForegroundColor = KeyCodeColor;
                }
            }
        }

        public void Dispose()
        {
            Console.ForegroundColor = _originalForegroundColor;
            Console.BackgroundColor = _originalBackgroundColor;
        }
    }
}
