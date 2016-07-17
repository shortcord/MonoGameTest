using System;
using System.Diagnostics;

namespace ShortCord.MonoGame {
    /// <summary>
    /// Static Logger class, will print to Visual Studio's Output if debugger is attached
    /// </summary>
    public static class Logger {

        static bool consoleExists;

        static Logger() {
            try {
                var c = Console.WindowHeight;
                consoleExists = true;
            } catch {
                consoleExists = false;
            }
        }

        /// <summary>
        /// Write a message to the Output Window
        /// </summary>
        /// <param name="message">Message</param>
        public static void WriteLine<T>(T message) {
            if (consoleExists) {
                Console.WriteLine($"[DEBUG LOG] {message?.ToString()}");
            }

            if (!consoleExists && Debugger.IsAttached) {
                Debug.WriteLine(message?.ToString());
            } 
        }
    }
}
