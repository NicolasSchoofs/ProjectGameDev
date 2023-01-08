using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ProjectGameDev.Animations
{
    internal class Animation
    {
        public AnimationFrame CurrentFrame { get; set; }
        public List<AnimationFrame> Frames;
        public int Counter;
        public double SecondCounter = 0;
        public int NLoops = 0;

        public Animation()
        {
            Frames = new List<AnimationFrame>();
        }

        public void AddFrame(AnimationFrame frame)
        {
            Frames.Add(frame);
            CurrentFrame = Frames[0];
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
            }
        }
    }
}
