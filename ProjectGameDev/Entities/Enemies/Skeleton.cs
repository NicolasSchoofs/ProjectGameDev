using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Animations;
using ProjectGameDev.Enemies;
using ProjectGameDev.Entities.Animation;
using ProjectGameDev.Entities.HeroDesign;
using ProjectGameDev.Levels;
using SharpDX.XAudio2;

namespace ProjectGameDev
{
    internal class Skeleton: Enemy
    {
        private AnimationManager animationManager = new AnimationManager();



        private int _startX = 0, _startY = 140;
        private int _schuifOpX = 64, _schuifOpY = 0;

        
        private int _attackStartX = 5, _attackStartY = 10;



        public Skeleton(Vector2 spawnLocation, ContentManager content)
        {
            Width = 45;
            Height = 37;
            State = State.right;
            GroundLevel = 9999;
            WallRight = 9999;
            WallLeft = 0;
            

            SpriteEffect = SpriteEffects.FlipHorizontally;
            RunningSpeed =  5;

            Position = spawnLocation;
            BoundingBox = new Rectangle(Convert.ToInt16(Position.X), Convert.ToInt16(Position.Y), Width * 3, Height * 3);
            AttackHitbox = new Rectangle(Convert.ToInt16(BoundingBox.Right), Convert.ToInt16(BoundingBox.Y), 30, 50);
            Speed = new Vector2(1, 1);
            Gravity = new Vector2(0, 1);
            GravityAcceleration = new Vector2(0, 0.1f);
            TerminalVelocity = new Vector2(0, 6);
            Action = ActionState.run;



            animationManager.AddAnimation(content.Load<Texture2D>("Enemies/Skeleton"), ActionState.run, _startX, _startY, _schuifOpX, Width, Height, 12, BoundingBox);
            animationManager.AddAnimation(content.Load<Texture2D>("Enemies/Skeleton"), ActionState.attack, _startX, _attackStartY, _schuifOpX, 65, Height, 9, BoundingBox);
            animationManager.AddAnimation(content.Load<Texture2D>("Enemies/Skeleton"), ActionState.hit, _startX, 268, _schuifOpX, 65, Height, 3, BoundingBox);
            animationManager.AddAnimation(content.Load<Texture2D>("Enemies/Skeleton"), ActionState.death, _startX, 75, _schuifOpX, 70, Height, 12, BoundingBox);
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
                    Fall();
                    Move();
                    UpdateBoundingBox();
                    animationManager.currentAnimation.Update(gameTime);
                    break;
                case ActionState.attack:
                    animationManager.currentAnimation.Update(gameTime);
                    break;
                case ActionState.hit:
                    if (!(animationManager.currentAnimation.NLoops >= 6))
                    {
                        animationManager.currentAnimation.Update(gameTime);
                    }
                    else
                    {
                        animationManager.currentAnimation.NLoops = 0;
                        if (Health > 0)
                        {
                            if (animationManager.currentAnimation.IsComplete)
                            {
                                animationManager.currentAnimation.NLoops = 0;
                                animationManager.currentAnimation.IsComplete = false;
                                Action = ActionState.run;
                            }
                        }
                        else
                        {
                            Die();
                        }

                    }
                    break;
                case ActionState.death:
                    if (!(animationManager.currentAnimation.NLoops >= 1))
                    {
                        animationManager.currentAnimation.Update(gameTime);
                    }
                    break;
            }


            animationManager.Update(gameTime, Action);
        }


   
        public override void UpdateBoundingBox()
        {

            base.UpdateBoundingBox();

            if (State == State.left)
            {
                AttackHitbox = new Rectangle(BoundingBox.Left - 50, BoundingBox.Y, 130, 130);
            }
            else
            {
                AttackHitbox = new Rectangle(BoundingBox.Right - 60, BoundingBox.Y, 130, 130);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Action)
            {
                case ActionState.run:
                    spriteBatch.Draw(animationManager.currentAnimation.texture, Position, animationManager.currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    break;
                case ActionState.attack:
                    if (State == State.left)
                    {
                        spriteBatch.Draw(animationManager.currentAnimation.texture, new Vector2(Position.X - 40, Position.Y), animationManager.currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(animationManager.currentAnimation.texture, Position, animationManager.currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    }
                    break;
                case ActionState.death:
                    if (State == State.left)
                    {
                        spriteBatch.Draw(animationManager.currentAnimation.texture, new Vector2(Position.X - 50, Position.Y), animationManager.currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(animationManager.currentAnimation.texture, Position, animationManager.currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    }
                    break;
                case ActionState.hit:
                    if (State == State.left)
                    {
                        spriteBatch.Draw(animationManager.currentAnimation.texture, new Vector2(Position.X - 50, Position.Y), animationManager.currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(animationManager.currentAnimation.texture, Position, animationManager.currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    }
                    
                    break;
            }
        }



        public override void AttackHero(Hero target)
        {
            if (Action == ActionState.attack)
            {
                base.AttackHero(target);
            }

        }

    }
}
