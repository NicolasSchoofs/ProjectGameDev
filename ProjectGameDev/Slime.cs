using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using SharpDX.Direct3D9;
using SharpDX.MediaFoundation;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace ProjectGameDev
{
    internal class Slime : IGameObject
    {
        private Texture2D textureIdle;

        private SpriteEffects spriteEffect = SpriteEffects.None;
        private Animation idle;

        private Vector2 positie;
        private Vector2 snelheid;

        public Slime(Texture2D textuIdle)
        {
            positie = new Vector2(0, 0);
            snelheid = new Vector2(1, 1);

            this.textureIdle = textuIdle;
            idle = new Animation();
            idle.AddFrame(new AnimationFrame(new Rectangle(20, 30, 50, 50)));
            idle.AddFrame(new AnimationFrame(new Rectangle(100, 30, 50, 50)));
            idle.AddFrame(new AnimationFrame(new Rectangle(180, 30, 50, 50)));
            idle.AddFrame(new AnimationFrame(new Rectangle(260, 30, 50, 50)));
            idle.AddFrame(new AnimationFrame(new Rectangle(340, 30, 50, 50)));
            idle.AddFrame(new AnimationFrame(new Rectangle(420, 30, 50, 50)));
            idle.AddFrame(new AnimationFrame(new Rectangle(500, 30, 50, 50)));
        }

        public void Update(GameTime gameTime)
        {
            idle.Update(gameTime);
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureIdle, positie, idle.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), spriteEffect, 0);
        }
    }
}
