using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TOGE_ATFO
{
    /// <summary>
    /// This displays a sweet splashscreen
    /// </summary>
    public class SplashScreen : GameStateManagement.GameScreen
    {
        ContentManager content;
        Texture2D Logo;
        Color bgColor;
        Vector2 LogoPosition;

        public SplashScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        /// <summary>
        /// Get dat content in a separate content manager
        /// </summary>
        public override void LoadContent()
        {
            if (content == null) {
                content = new ContentManager(ScreenManager.Game.Services, "Content");
            }
            Logo = content.Load<Texture2D>("Grissini");
            bgColor = Color.Black;
            LogoPosition.X = 0;
            LogoPosition.Y = 0;

        }

        /// <summary>
        /// Unloads graphics content for this screen.
        /// </summary>
        public override void UnloadContent()
        {
            content.Unload();
        }

        /// <summary>
        /// Updates the splash screen animation
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            
            bgColor.R = (byte)(Math.Max(0, Math.Sin(gameTime.TotalGameTime.TotalSeconds)) * 255);
            bgColor.G = (byte)(Math.Max(0, Math.Cos(gameTime.TotalGameTime.TotalSeconds)) * 255);
            bgColor.B = (byte)(Math.Max(0, Math.Sin(gameTime.TotalGameTime.TotalSeconds + Math.PI)) * 255);

            LogoPosition.X = (float)(120 + Math.Sin(gameTime.TotalGameTime.TotalSeconds) * 100);
            LogoPosition.Y = (float)(120 + Math.Cos(gameTime.TotalGameTime.TotalSeconds) * 100);
        }

        /// <summary>
        /// Draws the splash screen animation
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(bgColor);
            ScreenManager.SpriteBatch.Begin();
            ScreenManager.SpriteBatch.Draw(Logo, LogoPosition, Color.Pink);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, "Welcome to my gaem!", new Vector2(16, 16), Color.White);
            ScreenManager.SpriteBatch.End();
        }

        /// <summary>
        /// Responds to user input, changing the selected entry and accepting
        /// or cancelling the menu.
        /// </summary>
        public override void HandleInput(GameStateManagement.InputState input)
        {
            // Skip splash screen?
            if (input.CurrentKeyboardStates[0].IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Space) && input.LastKeyboardStates[0].IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                ScreenManager.AddScreen(new MenuScreen(), null);
            }
        }
    }
}
