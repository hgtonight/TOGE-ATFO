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
    /// based on linear interpolation between key frames
    /// </summary>
    public class KeyedAnimatable
    {
        const float FrameTime = 1f / 24f;
        protected SpriteBatch spriteBatch;
        protected float FrameTimer;
        protected bool Animating;
        protected int CurrentFrame;
        protected Vector2 CurrentPosition;
        protected byte CurrentAlpha;
        protected float CurrentRotation, CurrentScale;
        protected SortedList<int, Vector2> PositionKeys;
        protected SortedList<int, byte> AlphaKeys;
        protected SortedList<int, float> RotationKeys, ScaleKeys;

        /// <summary>
        /// Initializes to sensible defaults
        /// </summary>
        /// <param name="game">Passed to GameComponent</param>
        public KeyedAnimatable()
        {
            this.Reset();
        }

        /// <summary>
        /// Set a position animation key frame
        /// </summary>
        /// <param name="key">The frame number to set</param>
        /// <param name="position">The position to be at key's frame</param>
        public void AddPositionKey(int key, Vector2 position) {
            PositionKeys[key] = position;
        }

        public void AddRotationKey(int key, float rotation)
        {
            RotationKeys[key] = rotation;
        }

        public void AddAlphaKey(int key, byte alpha)
        {
            AlphaKeys[key] = alpha;
        }

        public void SetScalePoints(int key, float scale)
        {
            ScaleKeys[key] = scale;
        }

        /// <summary>
        /// Starts the update of the set animation
        /// </summary>
        public void Start()
        {
            Animating = true;
        }

        /// <summary>
        /// Pauses the animation
        /// </summary>
        public void Pause()
        {
            Animating = false;
        }

        /// <summary>
        /// Restarts the current animation from the beginning
        /// </summary>
        public void Restart()
        {
            this.Stop();
            this.Start();
        }
        
        /// <summary>
        /// Ends the current animations immediately and resets it
        /// </summary>
        public void Stop()
        {
            Animating = false;
            CurrentFrame = 0;
        }

        private void Reset()
        {
            this.Stop();
            PositionKeys.Clear();
            AlphaKeys.Clear();
            RotationKeys.Clear();
            ScaleKeys.Clear();
        }

        /// <summary>
        /// Update the animation frame if necessary
        /// </summary>
        public void Update(int Frame)
        {
            if (Animating)
            {
                // Update the current variables with a linear interpolation using
                // key frames to determine length
                
                // find the previous keyframe and the next key frame
                KeyValuePair<int, Vector2> LastKeyFrame = PositionKeys.Last(pair => pair.Key < Frame);
                KeyValuePair<int, Vector2> NextKeyFrame = PositionKeys.First(pair => pair.Key > Frame);
                int AnimationLength = NextKeyFrame.Key - LastKeyFrame.Key;

                float interp = (float)CurrentFrame / (float)AnimationLength;
                PositionKeys.Last(CurrentFrame <
                CurrentAlpha = (byte)(StartAlpha + ((float)(EndAlpha - StartAlpha) * interp));
                CurrentPosition = Vector2.Lerp(StartPosition, EndPosition, interp);
                CurrentRotation = StartRotation + ((EndRotation - StartRotation) * interp);
                CurrentScale = StartScale + ((EndScale - StartScale) * interp);
            }
                
        }

    }

    public class AnimatableTexture2D : KeyedAnimatable
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
            if (Animating)
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

    public class AnimatableText : KeyedAnimatable
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
            if (Animating)
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
