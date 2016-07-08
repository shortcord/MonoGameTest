using System;
using System.Diagnostics;

namespace ShortCord.MonoGame {
    /// <summary>
    /// Static Logger class, will print to Visual Studio's Output if debugger is attached
    /// </summary>
    public static class Logger {
        /// <summary>
        /// Write a message to the Output Window
        /// </summary>
        /// <param name="message">Message</param>
        public static void WriteLine<T>(T message) {
            if (Debugger.IsAttached) {
                Debug.WriteLine(message?.ToString());
            } 
        }
    }
}
