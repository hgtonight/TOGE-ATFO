using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TOGE_ATFO
{
    class MenuItem
    {
        SpriteFont menuFont; //Not used yet -- to be handled by Content Manager

        String text;
        int value;
        Vector2 location;
        Color color;

        public Vector2 Location
        {
            get { return location; }
        }

        public MenuItem(String text, int value, Vector2 location, Color color)
        {
            this.text = text;
            this.value = value;
            this.location = location;
            this.color = color;
        }
        public void Draw(SpriteBatch item)
        {
            item.DrawString(menuFont, text, location, color);
        }
    }
}
