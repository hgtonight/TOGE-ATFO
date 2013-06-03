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
    public class Menu
    {
        Color bgColor;
        SpriteFont Font;
        
        public Menu()
        {

        }

        /// <summary>
        /// Get dat content
        /// </summary>
        public void LoadContent(ContentManager Content)
        {
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// Updates the menu object and returns the state
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public int Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                return 2;

            return 1;
        }

        /// <summary>
        /// Draws the menu
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.DrawString(Font, "Press any key to play my gaem!", new Vector2(16, 16), Color.White);
            spriteBatch.End();
        }
    }
}
