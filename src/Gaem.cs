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
        SpriteFont basicFont;
        Vector2 grissini = new Vector2(30, 60);
        Texture2D grisspic;

        public Gaem()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

            basicFont = Content.Load<SpriteFont>("Calibri");
            grisspic = Content.Load<Texture2D>("Grissini");

            // TODO: use this.Content to load your game content here
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
            bgColor.R = (byte)(Math.Sin(gameTime.TotalGameTime.TotalSeconds) * 255);
            bgColor.G = (byte)(Math.Cos(gameTime.TotalGameTime.TotalSeconds) * 255);
            bgColor.B = (byte)(Math.Cos(gameTime.TotalGameTime.TotalSeconds + (Math.PI/2)) * 255);

            grissini.X = (float)(120 + Math.Sin(gameTime.TotalGameTime.TotalSeconds) * 100);
            grissini.Y = (float)(120 + Math.Cos(gameTime.TotalGameTime.TotalSeconds) * 100);
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(bgColor);
            spriteBatch.Begin();
            spriteBatch.Draw(grisspic, grissini, Color.Pink);
            spriteBatch.DrawString(basicFont, "Welcome to my gaem!", new Vector2(16, 16), Color.White);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
