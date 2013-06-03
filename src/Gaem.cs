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
    /// This is the main type for your game
    /// </summary>
    public class Gaem : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ScreenManager screenManager;
        ScreenFactory screenFactory;
        Color NullColor;
        SplashScreen Splash;
        Menu Menu;
        Tertis Tetris;
        int State;

        public Gaem()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            // lets me make a screen factory
            screenFactory = new screenFactory();
            Services.AddService(typeof(IScreenFactory), screenFactory);
            
            // lets me make drawable components
            screenManager = new screenManager(this);
            Components.Add(screenManager);

            // Run the initial screen
            AddScreens();
        }


        private void AddScreens()
        {
            // background screen for some cool effects
            screenManager.AddScreen(new BackgroundScreen(), null);

            // actual splash screen
            screenManager.AddScreen(new SplashScreen(), null);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            // The actual drawing will be done in the screen manager
            base.Draw(gameTime);
        }
    }
}
