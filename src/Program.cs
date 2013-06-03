#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace TOGE_ATFO
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // TODO: This makes two different windows, probably need to move it to one game with different render loops
            
            using (var splash = new SplashScreen())
                splash.Run();

            using (var game = new Gaem())
                game.Run();
        }
    }
#endif
}
