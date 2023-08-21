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

        private float deadZoneWidthPercent = 0.7f; 

        public void Follow(Hero target)
        {
            float deadZoneWidth = Game1.ScreenWidth * deadZoneWidthPercent;

            float targetX = target.Position.X - deadZoneWidth / 2;
            float targetY = target.Position.Y - Game1.ScreenHeight / 2;

            
            float cameraX = MathHelper.Clamp(targetX, 0, float.MaxValue);
            float cameraY = MathHelper.Clamp(targetY, 0, float.MaxValue);

            var Position = Matrix.CreateTranslation(-cameraX, -cameraY, 0);

            Transform = Position;
        }
    }
}
