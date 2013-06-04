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

namespace TOGE
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Gaem : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameStateManagement.ScreenManager screenManager;

        static readonly string[] preloadAssets =
        {
            "Calibri",
            "Logo",
        };

        public Gaem() : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            // lets me make drawable components
            screenManager = new GameStateManagement.ScreenManager(this);
            Components.Add(screenManager);

            // Run the initial screen
            screenManager.AddScreen(new SplashScreen(), null);
        }

        /// <summary>
        /// Loads graphics content.
        /// </summary>
        protected override void LoadContent()
        {
            foreach (string asset in preloadAssets)
            {
                Content.Load<object>(asset);
            }
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

    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new Gaem())
                game.Run();
        }
    }
}
