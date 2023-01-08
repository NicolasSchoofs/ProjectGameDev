using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectGameDev.Entities.HeroDesign;
using ProjectGameDev.Levels;
using SharpDX.XAudio2;

namespace ProjectGameDev.Extra
{
    internal class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow(Hero target)
        {
            var Position = Matrix.CreateTranslation(
                -target.Position.X - target.Width * 3 / 2,
                -target.Position.Y - target.Height * 3 / 2,
                0);

            var offset = Matrix.CreateTranslation(
                Game1.ScreenWidth / 2,
                Game1.ScreenHeight / 2,
                0);

            Transform = Position * offset;




        }
    }
}
