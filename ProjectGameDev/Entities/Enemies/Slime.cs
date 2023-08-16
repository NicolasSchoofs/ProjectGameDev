using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Animations;
using ProjectGameDev.Enemies;
using ProjectGameDev.Entities.Animation;
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
        private AnimationManager animationManager = new AnimationManager();


        public Slime(Vector2 spawnLocation, ContentManager content)
        {
            GroundLevel = 720;
            WallRight = 9999;
            WallLeft = 0;
            Width = 25;
            Height = 18;
            RunningSpeed = 10;
            SpriteEffect = SpriteEffects.FlipHorizontally;
            Position = spawnLocation;
            Speed = new Vector2(1, 1);
            RunningSpeed = 2;
            
            BoundingBox = new Rectangle(Convert.ToInt16(Position.X), Convert.ToInt16(Position.Y), Width * 3, Height * 3);
            AttackHitbox = BoundingBox;
            Action = ActionState.run;
            

            Gravity = new Vector2(0, 1);
            GravityAcceleration = new Vector2(0, 0.1f);
            TerminalVelocity = new Vector2(0, 10);


            animationManager.AddAnimation(content.Load<Texture2D>("Enemies/slime_idle2"), ActionState.run, 20, 30, 80, Width, Height, 7, BoundingBox);
            animationManager.currentAnimation = animationManager.GetAnimation(ActionState.run);

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


            
            switch (Action)
            {
                case ActionState.run:
                    //movement
                    Move();
                    Fall();
                    UpdateBoundingBox();
                    animationManager.currentAnimation.Update(gameTime);
                    break;
                case ActionState.hit:
                    Action = ActionState.death;
                    break;
                case ActionState.death:
                    break;

            }

            animationManager.Update(gameTime, Action);
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
                case ActionState.run:
                    spriteBatch.Draw(animationManager.currentAnimation.texture, Position, animationManager.currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    break;
                case ActionState.death:
                    break;
            }
            
        }
            
        public override void Reset()
        {
            Health = 1;
            Action = ActionState.run;
        }

        public override void CheckForHero(Hero target)
        {
        }

     
    }
}
