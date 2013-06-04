using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TOGE_ATFO
{
    class MenuItem
    {
        SpriteFont menuFont = Content.Load<SpriteFont>("Calibri");

        String text;
        int value;
        Rectangle location;
        Color color;

        public Rectangle Location
        {
            get { return location; }
        }

        public MenuItem(String text, int value, Rectangle location, Color color)
        {
            this.text = text;
            this.value = value;
            this.location = location;
            this.color = color;
        }
        public void Draw(SpriteBatch item)
        {
            item.DrawString(menuFont, text, new Vector2(location), color);
        }
    }
}
