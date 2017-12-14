﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zork.UIContext
{
    /// <summary>
    /// Sets the ConsoleColour as specified, and resets the colours upon disposal.
    /// </summary>
    public class ColourContext : IDisposable
    {
        public static ConsoleColor HeaderColor = ConsoleColor.DarkMagenta;
        public static ConsoleColor FailureColor = ConsoleColor.Red;

        private ConsoleColor _originalForegroundColor;
        private ConsoleColor _originalBackgroundColor;

        public ColourContext(ConsoleColor foregroundColor, ConsoleColor backgroundColor = ConsoleColor.Black)
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

        public void Dispose()
        {
            Console.ForegroundColor = _originalForegroundColor;
            Console.BackgroundColor = _originalBackgroundColor;
        }
    }
}
