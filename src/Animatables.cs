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
        protected bool? Animating;
        protected bool PositionFinished, AlphaFinished, RotationFinished, ScaleFinished;
        protected Vector2 CurrentPosition;
        protected byte CurrentAlpha;
        protected float CurrentRotation, CurrentScale;
        protected int AlphaIndex, RotationIndex, ScaleIndex, PositionIndex, StartFrame, EndFrame;
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
            PositionKeys.Add(key, position);
        }

        public void AddRotationKey(int key, float rotation)
        {
            RotationKeys.Add(key, rotation);
        }

        /// <summary>
        /// Add an alpha key frame.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="alpha"></param>
        public void AddAlphaKey(int key, byte alpha)
        {
            AlphaKeys.Add(key, alpha);
        }

        /// <summary>
        /// Add a scale key frame.
        /// </summary>
        /// <param name="key">The scale will be set to scale on this frame.</param>
        /// <param name="scale">The scale value.</param>
        public void AddScaleKey(int key, float scale)
        {
            ScaleKeys.Add(key, scale);
        }

        /// <summary>
        /// Set the automatic stop frame for all animation.
        /// </summary>
        /// <param name="key">The frame all animation will stop on.</param>
        public void StopPoint(int key)
        {
            EndFrame = key;
        }

        /// <summary>
        /// Set the automatice start frame for all animation.
        /// </summary>
        /// <param name="key">The frame all animation will start on.</param>
        public void StartPoint(int key)
        {
            StartFrame = key;
        }

        /// <summary>
        /// Convenience method used to reset all values to default
        /// </summary>
        private void Reset()
        {
            CurrentPosition = new Vector2(0, 0);
            CurrentAlpha = 255;
            CurrentRotation = 0.0f;
            CurrentScale = 1.0f;
            Animating = null;
            StartFrame = 0;
            EndFrame = 2147483647;            
            PositionFinished = AlphaFinished = RotationFinished = ScaleFinished = false;
            PositionKeys.Clear();
            PositionIndex = 0;
            AlphaKeys.Clear();
            AlphaIndex = 0;
            RotationKeys.Clear();
            RotationIndex = 0;
            ScaleKeys.Clear();
            ScaleIndex = 0;
        }

        /// <summary>
        /// Update the animation frame if necessary
        /// </summary>
        public void Update(int Frame)
        {
            // This holds the offset frame for ease of adding keyframes
            int AnimationFrame = Frame - StartFrame;

            if(Frame >= StartFrame && Frame <= EndFrame) {
                Animating = true;
            }
            else {
                Animating = false;
            }

            if (Animating == true)
            {
                int AnimationLength;
                float interp;

                // TODO: DRY this shit up

                // Only update the position if it won't put us out of bounds
                if (PositionKeys.Count() != 0
                    && PositionIndex + 1 != PositionKeys.Count()
                    && PositionFinished == false)
                {
                    // Calculate the difference from the current index to the next index in terms of frames
                    AnimationLength = PositionKeys.Keys[PositionIndex + 1] - PositionKeys.Keys[PositionIndex];
                       
                    // We can't divide by 0 so just set the interp to 0
                    if (AnimationLength < 1)
                    {
                        interp = 0.0f;
                    }
                    else
                    {
                        interp = ((float)AnimationFrame - (float)PositionKeys.Keys[PositionIndex]) / (float)AnimationLength;
                    }

                    // Interpolate by using the last key frame value and next as end points
                    // Linear interpolation is the easiest                       
                    // TODO: Implement easing functions
                    CurrentPosition = Vector2.Lerp(PositionKeys.Values[PositionIndex], PositionKeys.Values[PositionIndex + 1], interp);

                    if (AnimationFrame > PositionKeys.Keys[PositionIndex + 1])
                    {
                        PositionIndex++;
                    }
                }
                else
                {
                    PositionFinished = true;
                }

                // TODO: DRY this shit up

                // Only update the alpha if it won't put us out of bounds
                if (AlphaKeys.Count() != 0
                    && AlphaIndex + 1 != AlphaKeys.Count()
                    && AlphaFinished == false)
                {
                    // Calculate the difference from the current index to the next index in terms of frames
                    AnimationLength = AlphaKeys.Keys[AlphaIndex + 1] - AlphaKeys.Keys[AlphaIndex];

                    // We can't divide by 0 so just set the interp to 0
                    if (AnimationLength < 1)
                    {
                        interp = 0.0f;
                    }
                    else
                    {
                        interp = ((float)AnimationFrame - (float)AlphaKeys.Keys[AlphaIndex]) / (float)AnimationLength;
                    }

                    // Interpolate by using the last key frame value and next as end points
                    // Linear interpolation is the easiest                       
                    // TODO: Implement easing functions
                    CurrentAlpha = (byte)(AlphaKeys.Values[AlphaIndex] + ((float)(AlphaKeys.Values[AlphaIndex + 1] - AlphaKeys.Values[AlphaIndex]) * interp));

                    if (AnimationFrame > AlphaKeys.Keys[AlphaIndex + 1])
                    {
                        AlphaIndex++;
                    }
                }
                else
                {
                    AlphaFinished = true;
                }

                // TODO: DRY this shit up

                // Only update the rotation if it won't put us out of bounds
                if (RotationKeys.Count() != 0
                    && RotationIndex + 1 != RotationKeys.Count()
                    && RotationFinished == false)
                {
                    // Calculate the difference from the current index to the next index in terms of frames
                    AnimationLength = RotationKeys.Keys[RotationIndex + 1] - RotationKeys.Keys[RotationIndex];

                    // We can't divide by 0 so just set the interp to 0
                    if (AnimationLength < 1)
                    {
                        interp = 0.0f;
                    }
                    else
                    {
                        interp = ((float)AnimationFrame - (float)RotationKeys.Keys[RotationIndex]) / (float)AnimationLength;
                    }

                    // Interpolate by using the last key frame value and next as end points
                    // Linear interpolation is the easiest                       
                    // TODO: Implement easing functions
                    CurrentRotation = RotationKeys.Values[RotationIndex] + ((RotationKeys.Values[RotationIndex + 1] - RotationKeys.Values[RotationIndex]) * interp);
                    if (AnimationFrame > RotationKeys.Keys[RotationIndex + 1])
                    {
                        RotationIndex++;
                    }
                }
                else
                {
                    RotationFinished = true;
                }

                // TODO: DRY this shit up

                // Only update the scale if it won't put us out of bounds
                if (ScaleKeys.Count() != 0
                    && ScaleIndex + 1 != ScaleKeys.Count()
                    && ScaleFinished == false)
                {
                    // Calculate the difference from the current index to the next index in terms of frames
                    AnimationLength = ScaleKeys.Keys[ScaleIndex + 1] - ScaleKeys.Keys[ScaleIndex];

                    // We can't divide by 0 so just set the interp to 0
                    if (AnimationLength < 1)
                    {
                        interp = 0.0f;
                    }
                    else
                    {
                        interp = ((float)AnimationFrame - (float)ScaleKeys.Keys[ScaleIndex]) / (float)AnimationLength;
                    }

                    // Interpolate by using the last key frame value and next as end points
                    // Linear interpolation is the easiest                       
                    // TODO: Implement easing functions
                    CurrentScale = ScaleKeys.Values[ScaleIndex] + ((ScaleKeys.Values[ScaleIndex + 1] - ScaleKeys.Values[ScaleIndex]) * interp);
                    if (AnimationFrame > ScaleKeys.Keys[ScaleIndex + 1])
                    {
                        ScaleIndex++;
                    }
                }
                else
                {
                    ScaleFinished = true;
                }

            }
                
        }

    }

    public class AnimatableTexture2D : KeyedAnimatable
    {
        public Texture2D Texture;

        public AnimatableTexture2D(Texture2D texture) : base()
        {
            Texture = texture;
        }

        /// <summary>
        /// Draw the current frame of animation
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Animating == true)
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

        public AnimatableText(String text, SpriteFont font) : base()
        {
            Text = text;
            Font = font;
        }

        /// <summary>
        /// Draw the current frame of animation
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Animating == true)
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
