using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TOGE_ATFO
{
    class Baord
    {
        int Speed;
        int Lines;
        Vector2 Dimension;
        int[,] CollisionGrid;
        public extern void CheckForLines();
    }
}
