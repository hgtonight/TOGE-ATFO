using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;

namespace TOGE_ATFO
{
    /// <summary>
    /// This maintains a stack of screens, calling their Update and Draw methods if appropriate
    /// also routes input to the topmost active screen
    /// </summary>
    public class ScreenManager : DrawableGameComponent
    {
        private const string StateFilename = "ScreenManagerState.xml";

        List<GameScreen> screens = new List<GameScreen>();
        List<GameScreen> tempScreensList = new List<GameScreen>();

        InputState input = new InputState();

        SpriteBatch spriteBatch;
        SpriteFont font;
        Texture2D blankTexture;

        bool isInitialized;

        bool traceEnabled;

        /// <summary>
        /// Use a single spritebatch to save resources
        /// </summary>
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        /// <summary>
        /// Use a single font to save resources
        /// </summary>
        public SpriteFont Font
        {
            get { return font; }
        }

        /// <summary>
        /// For debugging
        /// </summary>
        public bool TraceEnabled
        {
            get { return traceEnabled; }
            set { traceEnabled = value; }
        }
        
        /// <summary>
        /// Gets a blank texture that can be used by the screens
        /// </summary>
        public Texture2D BlankTexture
        {
            get { return blankTexture; }
        }
        
        /// <summary>
        /// Constructs a new screen manager component
        /// </summary>
        public ScreenManager(Game game)
            : base(game)
        {

        }

        /// <summary>
        /// Initializes the screen manager component
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            isInitialized = true;
        }
        
        /// <summary>
        /// Load your graphics content
        /// </summary>
        protected override void LoadContent()
        {
            // Load content belonging to the screen manager.
            ContentManager content = Game.Content;

            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = content.Load<SpriteFont>("Calibri");
            blankTexture = content.Load<Texture2D>("blank");

            // Tell each of the screens to load their content.
            foreach (GameScreen screen in screens)
            {
                screen.Activate(false);
            }
        }
        
        /// <summary>
        /// Unload your graphics content
        /// </summary>
        protected override void UnloadContent()
        {
            // Tell each of the screens to unload their content.
            foreach (GameScreen screen in screens)
            {
                screen.Unload();
            }
        }

        /// <summary>
        /// Allows each screen to run logic
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            // Read the keyboard and gamepad.
            input.Update();

            // keep a copy of the current screens
            tempScreensList.Clear();

            foreach (GameScreen screen in screens)
                tempScreensList.Add(screen);

            bool otherScreenHasFocus = !Game.IsActive;
            bool coveredByOtherScreen = false;

            // Loop as long as there are screens waiting to be updated.
            while (tempScreensList.Count > 0)
            {
                // Pop the topmost screen off the waiting list.
                GameScreen screen = tempScreensList[tempScreensList.Count - 1];

                tempScreensList.RemoveAt(tempScreensList.Count - 1);

                // Update the screen.
                screen.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

                if (screen.ScreenState == ScreenState.TransitionOn ||
                    screen.ScreenState == ScreenState.Active)
                {
                    // give it a chance to handle input.
                    if (!otherScreenHasFocus)
                    {
                        screen.HandleInput(gameTime, input);

                        otherScreenHasFocus = true;
                    }

                    // inform any subsequent screens that they are covered by it.
                    if (!screen.IsPopup)
                        coveredByOtherScreen = true;
                }
            }

            // Print debug
            if (traceEnabled)
                TraceScreens();
        }
        
        /// <summary>
        /// Prints a list of all the screens, for debugging
        /// </summary>
        void TraceScreens()
        {
            List<string> screenNames = new List<string>();

            foreach (GameScreen screen in screens)
                screenNames.Add(screen.GetType().Name);

            Debug.WriteLine(string.Join(", ", screenNames.ToArray()));
        }
        
        /// <summary>
        /// Tells each screen to draw itself
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            foreach (GameScreen screen in screens)
            {
                if (screen.ScreenState == ScreenState.Hidden)
                    continue;

                screen.Draw(gameTime);
            }
        }


        /// <summary>
        /// Adds a new screen to the screen manager
        /// </summary>
        public void AddScreen(GameScreen screen, PlayerIndex? controllingPlayer)
        {
            screen.ControllingPlayer = controllingPlayer;
            screen.ScreenManager = this;
            screen.IsExiting = false;

            // If we have a graphics device, tell the screen to load content.
            if (isInitialized)
            {
                screen.Activate(false);
            }

            screens.Add(screen);
        }

        /// <summary>
        /// Removes a screen from the screen manager
        /// Only use on non showing screens
        /// Use GameScreen.ExitScreen for a transition
        /// </summary>
        public void RemoveScreen(GameScreen screen)
        {
            // If we have a graphics device, tell the screen to unload content.
            if (isInitialized)
            {
                screen.Unload();
            }

            screens.Remove(screen);
            tempScreensList.Remove(screen);
        }

        /// <summary>
        /// Get a copy of an array of all the screens
        /// </summary>
        public GameScreen[] GetScreens()
        {
            return screens.ToArray();
        }

        /// <summary>
        /// Helper draws a translucent black fullscreen sprite, used for fading
        /// screens in and out, and for darkening the background behind popups.
        /// </summary>
        public void FadeBackBufferToBlack(float alpha)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(blankTexture, GraphicsDevice.Viewport.Bounds, Color.Black * alpha);
            spriteBatch.End();
        }

        /// <summary>
        /// Informs the screen manager to serialize its state to disk
        /// </summary>
        public void Deactivate()
        {
            return;
        }

        public bool Activate(bool instancePreserved)
        {
            return false;
        }
    }
}