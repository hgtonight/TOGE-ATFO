#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace TOGE_ATFO
{
    /// <summary>
    /// This displays a crappy Menu
    /// </summary>
    public class MenuScreen : GameStateManagement.GameScreen
    {
        ContentManager content;
        Color bgColor;
        
        public MenuScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        /// <summary>
        /// Get dat content
        /// </summary>
        public override void LoadContent()
        {
            if (content == null) {
                content = new ContentManager(ScreenManager.Game.Services, "Content");
            }
            bgColor = Color.Black;
        }

        /// <summary>
        /// Unloads graphics content for this screen.
        /// </summary>
        public override void UnloadContent()
        {
            content.Unload();
        }

        /// <summary>
        /// Updates the menu screen state
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        /// <summary>
        /// Draws the menu screen
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(bgColor);
            ScreenManager.SpriteBatch.Begin();
            ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, "Press 'Space' to Start", new Vector2(120, 120), Color.White);
            ScreenManager.SpriteBatch.End();
        }

        /// <summary>
        /// Responds to user input, changing the selected entry and accepting
        /// or cancelling the menu.
        /// </summary>
        public override void HandleInput(GameStateManagement.InputState input)
        {
            // Start game
            if (input.CurrentKeyboardStates[0].IsKeyUp(Keys.Space) && input.LastKeyboardStates[0].IsKeyDown(Keys.Space))
            {
                ScreenManager.AddScreen(new GameplayScreen(), null);
            }

            // Return to splash screen
            if (input.CurrentKeyboardStates[0].IsKeyUp(Keys.Escape) && input.LastKeyboardStates[0].IsKeyDown(Keys.Escape))
            {
                ScreenManager.AddScreen(new SplashScreen(), null);
            }
        }
    }
}
