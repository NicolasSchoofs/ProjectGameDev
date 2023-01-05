using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectGameDev
{
    internal class BlockFactory
    {
        public static Block CreateBlock(
            string type, int x, int y, GraphicsDevice graphics, Texture2D texture)
        {
            Block newBlock = null;
            type = type.ToUpper();
            //if (type == "NORMAL")
            //{
            //    newBlock = new Block(x, y, graphics);
            //}

            //if (type == "TRAP")
            //{
            //    newBlock = new TrapBlock(x, y, graphics);
            //}
            //if (type == "SLIME")
            //{
            //    newBlock = new SlimeBlock(x, y, graphics);
            //}
            return newBlock;
        }

    }
}
