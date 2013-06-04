using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;

namespace TOGE.ATFO
{
    class Tertimino
    {
        Color Color;
        int Rotation;
        Vector2 Position;
        int[,] Shape;
        public extern int[,] RotateCW();
        public extern int[,] RotateCCW();
    }
}
