using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TOGE
{
    /// <summary>
    /// This displays a sweet splashscreen
    /// </summary>
    public class SplashScreen : GameStateManagement.GameScreen
    {
        ContentManager content;
        AnimatableTexture2D DaKlutz, Logo;
        AnimatableText Presents, The, OliveGarden, Experience;
        SpriteFont OldeFont;
        Color bgColor;
        
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
            Logo = new AnimatableTexture2D(content.Load<Texture2D>("Logo"));
            Logo.SetAlphaPoints(0, 255);
            Logo.SetPositionPoints(new Vector2(300, 150), new Vector2(320, 150));
            Logo.SetAnimationLength(96);
            Logo.Start();
            DaKlutz = new AnimatableTexture2D(content.Load<Texture2D>("daklutz"));
            DaKlutz.SetAlphaPoints(0, 255);
            DaKlutz.SetPositionPoints(new Vector2(120, 180), new Vector2(120, 170));
            DaKlutz.SetScalePoints(0.99f, 1.0f);
            DaKlutz.SetAnimationLength(48);
            //DaKlutz.Start();
            Presents = new AnimatableText("PRESENTS", ScreenManager.Font);
            Presents.SetPositionPoints(new Vector2(-100, 400), new Vector2(300, 400));
            Presents.SetScalePoints(2.0f, 2.0f);
            Presents.SetAlphaPoints(64, 212);
            Presents.SetAnimationLength(48);
            //Presents.Start();
            OldeFont = content.Load<SpriteFont>("YeOldeFont");
            The = new AnimatableText("The", OldeFont);
            The.SetPositionPoints(new Vector2(300, -25), new Vector2(300, 50));
            The.SetAnimationLength(96);
            The.Start();
            OliveGarden = new AnimatableText("Olive Garden", OldeFont);
            OliveGarden.SetPositionPoints(new Vector2(0, 150), new Vector2(280, 150));
            OliveGarden.SetAnimationLength(96);
            OliveGarden.Start();
            Experience = new AnimatableText("Experience", OldeFont);
            Experience.SetPositionPoints(new Vector2(600, 400), new Vector2(300, 400));
            Experience.SetAnimationLength(96);
            Experience.Start();
            bgColor = Color.Gray;
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
            Logo.Update(gameTime);
            DaKlutz.Update(gameTime);
            Presents.Update(gameTime);
            The.Update(gameTime);
            OliveGarden.Update(gameTime);
            Experience.Update(gameTime);
        }

        /// <summary>
        /// Draws the splash screen animation
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(bgColor);
            Logo.Draw(ScreenManager.SpriteBatch, gameTime);
            DaKlutz.Draw(ScreenManager.SpriteBatch, gameTime);
            Presents.Draw(ScreenManager.SpriteBatch, gameTime);
            The.Draw(ScreenManager.SpriteBatch, gameTime);
            OliveGarden.Draw(ScreenManager.SpriteBatch, gameTime);
            Experience.Draw(ScreenManager.SpriteBatch, gameTime);
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
                this.ExitScreen();
            }
        }
    }
}
