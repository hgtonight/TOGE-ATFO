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
        const float FrameTime = 1f / 24f;
        float FrameTimer;
        int CurrentFrame;
        
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
            FrameTimer = 0.0f;
            CurrentFrame = 0;
                        
            DaKlutz = new AnimatableTexture2D(content.Load<Texture2D>("daklutz"));
            DaKlutz.AddAlphaKey(0, 0);
            DaKlutz.AddAlphaKey(48, 255);
            DaKlutz.AddPositionKey(0, new Vector2(120, 180));
            DaKlutz.AddPositionKey(48, new Vector2(120, 170));
            DaKlutz.AddScaleKey(0, 0.99f);
            DaKlutz.AddScaleKey(48, 1.0f);
                        
            Presents = new AnimatableText("PRESENTS", ScreenManager.Font);
            Presents.AddPositionKey(12, new Vector2(-100, 400));
            Presents.AddPositionKey(48, new Vector2(300, 400));
            Presents.AddScaleKey(0, 2.0f);
            Presents.AddAlphaKey(12, 64);
            Presents.AddAlphaKey(48, 212);

            OldeFont = content.Load<SpriteFont>("YeOldeFont");
            
            Logo = new AnimatableTexture2D(content.Load<Texture2D>("Logo"));
            Logo.AddAlphaKey(0, 0);
            Logo.AddAlphaKey(96, 255);
            Logo.AddPositionKey(0, new Vector2(300, 150));
            Logo.AddPositionKey(96, new Vector2(320, 150));

            //The = new AnimatableText("The", OldeFont);
            //The.SetPositionPoints(new Vector2(300, -25), new Vector2(300, 50));
            //The.SetAnimationLength(96);
            
            //OliveGarden = new AnimatableText("Olive Garden", OldeFont);
            //OliveGarden.SetPositionPoints(new Vector2(0, 150), new Vector2(280, 150));
            //OliveGarden.SetAnimationLength(96);
            
            //Experience = new AnimatableText("Experience", OldeFont);
            //Experience.SetPositionPoints(new Vector2(600, 400), new Vector2(300, 400));
            //Experience.SetAnimationLength(96);
            
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
            
            FrameTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (FrameTimer <= 0)
            {
                CurrentFrame++;
                FrameTimer = FrameTime;
            }

            Logo.Update(CurrentFrame);
            DaKlutz.Update(CurrentFrame);
            Presents.Update(CurrentFrame);
            //The.Update(CurrentFrame);
            //OliveGarden.Update(CurrentFrame);
            //Experience.Update(CurrentFrame);
        }

        /// <summary>
        /// Draws the splash screen animation
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(bgColor);
            Logo.Draw(ScreenManager.SpriteBatch);
            DaKlutz.Draw(ScreenManager.SpriteBatch);
            Presents.Draw(ScreenManager.SpriteBatch);

            ScreenManager.SpriteBatch.Begin();
            ScreenManager.SpriteBatch.DrawString(OldeFont, CurrentFrame.ToString(), new Vector2(0, 0), Color.Black);
            ScreenManager.SpriteBatch.End();
            
            //The.Draw(ScreenManager.SpriteBatch);
            //OliveGarden.Draw(ScreenManager.SpriteBatch);
            //Experience.Draw(ScreenManager.SpriteBatch);
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
