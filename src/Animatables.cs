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
        protected SpriteBatch spriteBatch;
        protected bool Animating;
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
            PositionKeys = new SortedList<int,Vector2>();
            AlphaKeys = new SortedList<int,byte>();
            RotationKeys = new SortedList<int,float>();
            ScaleKeys = new SortedList<int,float>();

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

        public void AddScaleKey(int key, float scale)
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
        }

        private void Reset()
        {
            this.Stop();
            PositionKeys.Clear();
            PositionKeys.Add(0, new Vector2(0, 0));
            AlphaKeys.Clear();
            AlphaKeys.Add(0, 255);
            RotationKeys.Clear();
            RotationKeys.Add(0, 0.0f);
            ScaleKeys.Clear();
            ScaleKeys.Add(0, 1.0f);
        }

        /// <summary>
        /// Update the animation frame if necessary
        /// </summary>
        public void Update(int Frame)
        {
            if (Animating)
            {
                // Update the current variables with a linear interpolation using key frames
                int AnimationLength = 1;
                float interp = 0.0f;

                // Position
                KeyValuePair<int, Vector2> LastKeyFrameP = PositionKeys.Last(pair => pair.Key < Frame);
                KeyValuePair<int, Vector2> NextKeyFrameP = PositionKeys.First(pair => pair.Key > Frame);
                AnimationLength = NextKeyFrameP.Key - LastKeyFrameP.Key;
                interp = (Frame - LastKeyFrameP.Key) / AnimationLength;
                CurrentPosition = Vector2.Lerp(LastKeyFrameP.Value, NextKeyFrameP.Value, interp);

                KeyValuePair<int, byte> LastKeyFrameA = AlphaKeys.Last(pair => pair.Key < Frame);
                KeyValuePair<int, byte> NextKeyFrameA = AlphaKeys.First(pair => pair.Key > Frame);
                AnimationLength = NextKeyFrameA.Key - LastKeyFrameA.Key;
                interp = (Frame - LastKeyFrameA.Key) / AnimationLength;
                CurrentAlpha = (byte)(LastKeyFrameA.Value + ((float)(NextKeyFrameA.Value - LastKeyFrameA.Value) * interp));

                KeyValuePair<int, float> LastKeyFrameR = RotationKeys.Last(pair => pair.Key < Frame);
                KeyValuePair<int, float> NextKeyFrameR = RotationKeys.First(pair => pair.Key > Frame);
                AnimationLength = NextKeyFrameR.Key - LastKeyFrameR.Key;
                interp = (Frame - LastKeyFrameR.Key) / AnimationLength;
                CurrentRotation = LastKeyFrameR.Value + ((NextKeyFrameR.Value - LastKeyFrameR.Value) * interp);

                KeyValuePair<int, float> LastKeyFrameS = ScaleKeys.Last(pair => pair.Key < Frame);
                KeyValuePair<int, float> NextKeyFrameS = ScaleKeys.First(pair => pair.Key > Frame);
                AnimationLength = NextKeyFrameS.Key - LastKeyFrameS.Key;
                interp = (Frame - LastKeyFrameS.Key) / AnimationLength;
                CurrentScale = LastKeyFrameS.Value + ((NextKeyFrameS.Value - LastKeyFrameS.Value) * interp);
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
        public void Draw(SpriteBatch spriteBatch)
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
        public void Draw(SpriteBatch spriteBatch)
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
