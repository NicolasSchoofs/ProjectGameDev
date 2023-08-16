using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectGameDev.Animations;
using ProjectGameDev.Enemies;
using ProjectGameDev.Entities.Animation;
using ProjectGameDev.Entities.HeroDesign;
using ProjectGameDev.Levels;
using SharpDX;
using SharpDX.Direct3D9;
using SharpDX.MediaFoundation;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ProjectGameDev
{

    internal class Minotaur: Enemy
    {
        private AnimationManager animationManager = new AnimationManager();


        private int _startX = 25, _startY = 115;
        private int _schuifOpX = 96, _schuifOpY = 0;

        private int _attackStartX = 5, _attackStartY = 290;
        private int _hitStartX = 5, _hitStartY = 788;
        private int _deathStartX = 5, _deathStartY = 885;


        public Minotaur(Vector2 spawnLocation, ContentManager content)
        {
            Width = 55;
            Height = 45;
            State = State.right;
            GroundLevel = 9999;
            WallRight = 9999;
            WallLeft = 0;
            AttackHitbox = new Rectangle(Convert.ToInt16(BoundingBox.Right), Convert.ToInt16(BoundingBox.Y), 30, 50);
            Position = spawnLocation;
            Speed = new Vector2(1, 1);
            SpriteEffect = SpriteEffects.None;
            RunningSpeed = 10;



            BoundingBox = new Rectangle(Convert.ToInt16(Position.X), Convert.ToInt16(Position.Y), Width * 3, Height * 3);
            

            RunningSpeed = 10;

            
            
            Gravity = new Vector2(0, 1);
            GravityAcceleration = new Vector2(0, 0.1f);
            TerminalVelocity = new Vector2(0, 6);

            Action = ActionState.run;

            //Run
            animationManager.AddAnimation(content.Load<Texture2D>("Enemies/Minotaur"), ActionState.run, _startX, _startY, _schuifOpX, Width, Height, 8, BoundingBox);
    

            animationManager.AddAnimation(content.Load<Texture2D>("Enemies/Minotaur"), ActionState.attack, _attackStartX, _attackStartY, _schuifOpX, 90, 70, 9, BoundingBox);


            animationManager.AddAnimation(content.Load<Texture2D>("Enemies/Minotaur"), ActionState.hit, _hitStartX, _hitStartY, _schuifOpX, 90, 70, 3, BoundingBox);
    

            animationManager.AddAnimation(content.Load<Texture2D>("Enemies/Minotaur"), ActionState.death, _deathStartX, _deathStartY, _schuifOpX, 90, 70, 6, BoundingBox);
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

      

        public override void CheckForHero(Hero target)
        {
            if (Action != ActionState.death && Action != ActionState.hit)
            {
            
                if (State == State.right)
                {
                    if (target.Position.X >= Position.X && target.Position.X <= Position.X + 250)
                    {
                        Action = ActionState.attack;
                    }
                    else
                    {
                        Action = ActionState.run;
                    }
                }

                if (State == State.left)
                {
                    if (target.Position.X <= Position.X && target.Position.X >= Position.X - 250)
                    {
                        Action = ActionState.attack;
                    }
                    else
                    {
                        Action = ActionState.run;
                    }
                }
            }
        }
       
        public override void UpdateBoundingBox()
        {
            base.UpdateBoundingBox();
            if (State == State.left)
            {
                AttackHitbox = new Rectangle(BoundingBox.Left - 30, BoundingBox.Y- 40, 130, 170);
            }
            else
            {
                AttackHitbox = new Rectangle(BoundingBox.Right - 100, BoundingBox.Y - 40, 130, 170);
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
                    spriteBatch.Draw(animationManager.currentAnimation.texture, new Vector2(Position.X - 50, Position.Y- 50), animationManager.currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    break;
                case ActionState.hit:
                    spriteBatch.Draw(animationManager.currentAnimation.texture, new Vector2(Position.X - 50, Position.Y), animationManager.currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    break;
                case ActionState.death:
                    spriteBatch.Draw(animationManager.currentAnimation.texture, Position, animationManager.currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
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
        public override void Reset()
        {
            Health = 3;
            Action = ActionState.run;
        }

    }
}
