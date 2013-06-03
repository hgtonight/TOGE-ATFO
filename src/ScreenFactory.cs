using System;

namespace TOGE_ATFO
{
    /// <summary>
    /// Lets us swap out screens like underwear
    /// </summary>
    public class ScreenFactory : IScreenFactory
    {
        public GameScreen CreateScreen(Type screenType)
        {
            // some Activator magic thanks to empty constructors
            return Activator.CreateInstance(screenType) as GameScreen;
        }
    }
}
