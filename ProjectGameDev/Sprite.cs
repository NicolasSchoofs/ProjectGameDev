using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace ProjectGameDev
{
    internal class Sprite
    {
        private int width;
        private int height;
        private Vector2 position;
        public Rectangle boundingBox;
        public bool collided;

        //public bool Collision(Sprite target)
        //{
        //    bool intersects = boundingBox.Intersects(target.boundingBox);
        //    collided = intersects;
        //    return intersects;
        //}
    }
}
