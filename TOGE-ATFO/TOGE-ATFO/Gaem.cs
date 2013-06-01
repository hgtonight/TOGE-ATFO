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
        Color bgColor;
        Vector2 grissiniPosition = new Vector2(0, 0);
        Vector2 moPosition = new Vector2(30, 30);

        Texture2D grissiniSpriteTexture;
        Texture2D moSpriteTexture;

        public Gaem()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Assets";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            bgColor = Color.Black;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            grissiniSpriteTexture = this.Content.Load<Texture2D>("Grissini");
          //  moSpriteTexture = this.Content.Load<Texture2D>("Mo.xnb");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            bgColor.R++;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(bgColor);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(grissiniSpriteTexture, grissiniPosition, Color.Pink);
            spriteBatch.Draw(moSpriteTexture, moPosition, Color.PaleGoldenrod);
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
