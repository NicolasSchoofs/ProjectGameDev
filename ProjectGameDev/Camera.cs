using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.XAudio2;

namespace ProjectGameDev
{
    //STOLEN CODE
    internal class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow(Hero target)
        {
            var position = Matrix.CreateTranslation(
                -target.positie.X - (target.width * 3 / 2),
                -target.positie.Y - (target.height * 3 / 2),
                0);

            var offset = Matrix.CreateTranslation(
                Game1.screenwidth / 2,
                Game1.screenHeight / 2,
                0);

            Transform = position * offset;




        }
    }
}
