using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TOGE
{
    /// <summary>
    /// An object that automatically calculates the proper position, alpha, and rotation
    /// based on linear interpolation
    /// </summary>
    public class Animatable
    {
        const float FrameTime = 1f / 24f;
        protected SpriteBatch spriteBatch;
        protected Vector2 StartPosition, EndPosition, CurrentPosition;
        protected byte StartAlpha, EndAlpha, CurrentAlpha;
        protected float StartRotation, EndRotation, CurrentRotation, StartScale, EndScale, CurrentScale, FrameTimer;
        protected int AnimationLength, CurrentFrame;
        protected bool Active;

        /// <summary>
        /// Initializes to sensible defaults
        /// </summary>
        /// <param name="game">Passed to GameComponent</param>
        public Animatable()
        {
            this.Reset();
        }

        /// <summary>
        /// Set up position animatipm
        /// </summary>
        /// <param name="start">Position the object should start it's animation</param>
        /// <param name="end">Position the object should end it's animation</param>
        public void SetPositionPoints(Vector2 start, Vector2 end) {
            StartPosition = CurrentPosition = start;
            EndPosition = end;
        }

        /// <summary>
        /// Set up rotation animation
        /// </summary>
        /// <param name="start">Rotation the object should start it's animation</param>
        /// <param name="end">Rotation the object should end it's animation</param>
        public void SetRotationPoints(float start, float end)
        {
            StartRotation = CurrentRotation = start;
            EndRotation = end;
        }

        /// <summary>
        /// Set up alpha animation
        /// </summary>
        /// <param name="start">Alpha the object should start it's animation</param>
        /// <param name="end">Alpha the object should end it's animation</param>
        public void SetAlphaPoints(byte start, byte end)
        {
            StartAlpha = CurrentAlpha = start;
            EndAlpha = end;
        }

        /// <summary>
        /// Set up scale animation
        /// </summary>
        /// <param name="start">Scale the object should start it's animation</param>
        /// <param name="end">Scale the object should end it's animation</param>
        public void SetScalePoints(float start, float end)
        {
            StartScale = CurrentScale = start;
            EndScale = end;
        }

        /// <summary>
        /// Set up the lenth of animation
        /// </summary>
        /// <param name="length">How many frames should animation last</param>
        /// <param name="start">What frame should animation start</param>
        public void SetAnimationLength(int length)
        {
            AnimationLength = length;
        }

        /// <summary>
        /// Starts the update of the set animation
        /// </summary>
        public void Start()
        {
            Active = true;
        }

        /// <summary>
        /// Pauses the animation
        /// </summary>
        public void Pause()
        {
            Active = false;
        }

        /// <summary>
        /// Restarts the current animation from the beginning
        /// </summary>
        public void Restart()
        {
            Active = false;
            CurrentAlpha = StartAlpha;
            CurrentPosition = StartPosition;
            CurrentRotation = StartRotation;
            CurrentScale = StartScale;
            CurrentFrame = 0;
            Active = true;
        }
        
        /// <summary>
        /// Ends the current animations immediately and resets it
        /// </summary>
        public void Stop()
        {
            this.Reset();
        }

        private void Reset()
        {
            StartPosition = EndPosition = CurrentPosition = new Vector2(0, 0);
            StartAlpha = EndAlpha = CurrentAlpha = 255;
            StartRotation = EndRotation = CurrentRotation = 0.0f;
            AnimationLength = 1;
            StartScale = EndScale = CurrentScale = 1.0f;
            CurrentFrame = 1;
            Active = false;
        }

        /// <summary>
        /// Update the animation frame if necessary
        /// </summary>
        public void Update(GameTime gameTime)
        {
            if (Active)
            {
                FrameTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (FrameTimer <= 0)
                {
                    CurrentFrame++;
                    FrameTimer = FrameTime;

                    if (CurrentFrame < AnimationLength)
                    {
                        // Update the current variables with a linear interpolation using the 
                        // current frame and the total length as the 
                        float interp = (float)CurrentFrame / (float)AnimationLength;

                        CurrentAlpha = (byte)(StartAlpha + ((float)(EndAlpha - StartAlpha) * interp));
                        CurrentPosition = Vector2.Lerp(StartPosition, EndPosition, interp);
                        CurrentRotation = StartRotation + ((EndRotation - StartRotation) * interp);
                        CurrentScale = StartScale + ((EndScale - StartScale) * interp);
                    }
                    else
                    {
                        this.Restart();
                    }
                }
            }
                
        }

    }

    public class AnimatableTexture2D : Animatable
    {
        public Texture2D Texture;

        public AnimatableTexture2D(Texture2D texture)
        {
            Texture = texture;
        }

        /// <summary>
        /// Draw the current frame of animation
        /// </summary>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Active)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(
                     Texture,
                     CurrentPosition,
                     null,
                     new Color(CurrentAlpha, CurrentAlpha, CurrentAlpha, CurrentAlpha),
                     CurrentRotation,
                     new Vector2(0, 0),
                     CurrentScale,
                     SpriteEffects.None,
                     0
                );
                spriteBatch.End();
            }
        }
    }

    public class AnimatableText : Animatable
    {
        String Text;
        SpriteFont Font;

        public AnimatableText(String text, SpriteFont font)
        {
            Text = text;
            Font = font;
        }

        /// <summary>
        /// Draw the current frame of animation
        /// </summary>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Active)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(
                     Font,
                     Text,
                     CurrentPosition,
                     new Color(255, 255, 255, CurrentAlpha),
                     CurrentRotation,
                     new Vector2(0, 0),
                     CurrentScale,
                     SpriteEffects.None,
                     0
                );
                spriteBatch.End();
            }
        }
    }
}
