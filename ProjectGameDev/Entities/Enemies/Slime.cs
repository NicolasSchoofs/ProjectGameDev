using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Animations;
using ProjectGameDev.Enemies;
using ProjectGameDev.Entities.HeroDesign;
using ProjectGameDev.Levels;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct3D9;
using SharpDX.MediaFoundation;
using SharpDX.XAudio2;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ProjectGameDev
{
    enum State
    {
        left,
        right
    }
    internal class Slime:Enemy
    {
        private Texture2D _textureIdle;

        //private int GroundLevel = 720;
        //private int WallRight = 9999;
        //private int WallLeft = 0;

        private Texture2D _blokTexture;



        //private int Width = 25;
        //private int Height = 18;

        //private Vector2 Gravity, GravityAcceleration, TerminalVelocity;


        //private State State = State.Right;

        //private SpriteEffects SpriteEffect = SpriteEffects.None;
        private Animation _idle;

        //private Vector2 positie;
        //private Vector2 snelheid;

        public Slime(Texture2D textuIdle, Texture2D blokTexture, Vector2 spawnLocation)
        {
            GroundLevel = 720;
            WallRight = 9999;
            WallLeft = 0;
            Width = 25;
            Height = 18;
            RunningSpeed = 10;
            //
            SpriteEffect = SpriteEffects.FlipHorizontally;
            //
            Position = spawnLocation;
            Speed = new Vector2(1, 1);
            RunningSpeed = 2;
            //
            this._blokTexture = blokTexture;
            //collision box
            BoundingBox = new Rectangle(Convert.ToInt16(Position.X), Convert.ToInt16(Position.Y), Width * 3, Height * 3);
            AttackHitbox = BoundingBox;
            Action = Action.run;
            

            Gravity = new Vector2(0, 1);
            GravityAcceleration = new Vector2(0, 0.1f);
            TerminalVelocity = new Vector2(0, 10);

            this._textureIdle = textuIdle;
            _idle = new Animation();
            _idle.AddFrame(new AnimationFrame(new Rectangle(20, 30, Width, Height)));
            _idle.AddFrame(new AnimationFrame(new Rectangle(100, 30, Width, Height)));
            _idle.AddFrame(new AnimationFrame(new Rectangle(180, 30, Width, Height)));
            _idle.AddFrame(new AnimationFrame(new Rectangle(260, 30, Width, Height)));
            _idle.AddFrame(new AnimationFrame(new Rectangle(340, 30, Width, Height)));
            _idle.AddFrame(new AnimationFrame(new Rectangle(420, 30, Width, Height)));
            _idle.AddFrame(new AnimationFrame(new Rectangle(500, 30, Width, Height)));
        }

        public override void Update(GameTime gameTime)
        {
            if (!Collided)
            {
                GroundLevel = 9999;
                WallRight = 9999;
                WallLeft = 0;
            }
            Collided = false;


            //animations
            switch (Action)
            {
                case Action.run:
                    //movement
                    Move();
                    Fall();
                    UpdateBoundingBox();
                    _idle.Update(gameTime);
                    break;
                case Action.hit:
                    Action = Action.death;
                    break;
                case Action.death:
                    break;

            }
            
        }

        public override void UpdateBoundingBox()
        {
            base.UpdateBoundingBox();
            AttackHitbox = BoundingBox;
        }



        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Action)
            {
                case Action.run:
                    spriteBatch.Draw(_textureIdle, Position, _idle.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    break;
                case Action.death:
                    break;
            }
            
        }
            
        public override void Reset()
        {
            Health = 1;
            Action = Action.run;
        }

        public override void CheckForHero(Hero target)
        {
        }

     
    }
}
