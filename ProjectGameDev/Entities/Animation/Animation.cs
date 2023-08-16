using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Entities.Animation;
using SharpDX.DXGI;

namespace ProjectGameDev.Animations
{
    internal class Animation
    {
        public AnimationFrame CurrentFrame { get; set; }
        public List<AnimationFrame> Frames;
        public int Counter;
        public double SecondCounter = 0;
        public int NLoops = 0;
        public int MaxLoops = 1; 
        public Texture2D texture;
        public ActionState ActionState;
        public bool IsComplete;

        public Animation(Texture2D texture)
        {
            this.texture = texture;
            Frames = new List<AnimationFrame>();
        }

        public void AddFrame(AnimationFrame frame)
        {
            Frames.Add(frame);
            CurrentFrame = Frames[0];
        }

        public void AddFrames(int startX, int startY, int schuifOp, int width, int height, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                this.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp * i, startY, width, height)));
            }
        }

        public void Update(GameTime gameTime)
        {
            CurrentFrame = Frames[Counter];

            SecondCounter += gameTime.ElapsedGameTime.TotalSeconds;
            int fps = 15;
            if (SecondCounter >= 1d / fps)
            {
                Counter++;
                SecondCounter = 0;
            }

            if (Counter >= Frames.Count)
            {
                NLoops++;
                Counter = 0;

                if (NLoops >= MaxLoops)
                {
                }
                IsComplete = NLoops >= MaxLoops && Counter == 0;
            }
        }
    }
}
