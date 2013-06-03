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
    /// This displays a sweet splashscreen
    /// </summary>
    public class Menu
    {
        Color bgColor;
        SpriteFont Font;
        Vector2 LogoPosition = new Vector2(30, 60);
        Texture2D Logo;

        public Menu()
        {
            
        }

        /// <summary>
        /// Get dat content
        /// </summary>
        public void LoadContent(ContentManager Content)
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            if(Content.
            Font = Content.Load<SpriteFont>("Calibri");
            Logo = Content.Load<Texture2D>("Grissini");

            bgColor = Color.Black;

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// Updates the splash screen animation and returns the state
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public int Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                return 1;

            bgColor.R = (byte)(Math.Max(0, Math.Sin(gameTime.TotalGameTime.TotalSeconds)) * 255);
            bgColor.G = (byte)(Math.Max(0, Math.Cos(gameTime.TotalGameTime.TotalSeconds)) * 255);
            bgColor.B = (byte)(Math.Max(0, Math.Sin(gameTime.TotalGameTime.TotalSeconds + Math.PI)) * 255);

            LogoPosition.X = (float)(120 + Math.Sin(gameTime.TotalGameTime.TotalSeconds) * 100);
            LogoPosition.Y = (float)(120 + Math.Cos(gameTime.TotalGameTime.TotalSeconds) * 100);

            return 0;
        }

        /// <summary>
        /// Draws the current splash screen animation
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(bgColor);
            spriteBatch.Begin();
            spriteBatch.Draw(Logo, LogoPosition, Color.Pink);
            spriteBatch.DrawString(Font, "Welcome to my gaem!", new Vector2(16, 16), Color.White);
            spriteBatch.End();
        }
    }
}
