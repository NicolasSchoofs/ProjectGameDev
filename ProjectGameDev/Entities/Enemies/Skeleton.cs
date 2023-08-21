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
        private int animationDimentions = 64;

        public Skeleton(Vector2 spawnLocation, ContentManager content)
        {
            offset = new Vector2(60, 40);
            Width = 15;
            Height = 37;
            State = State.right;
            GroundLevel = 9999;
            WallRight = 9999;
            WallLeft = 0;



            SpriteEffect = SpriteEffects.FlipHorizontally;
            RunningSpeed =  5;

            Position = spawnLocation;
            BoundingBox = new Rectangle(Convert.ToInt16(Position.X) + Convert.ToInt16(offset.X), Convert.ToInt16(Position.Y) + Convert.ToInt16(offset.Y), Width * 3, Height * 3);
            AttackHitbox = new Rectangle(Convert.ToInt16(BoundingBox.Right), Convert.ToInt16(BoundingBox.Y), 30, 50);
            Speed = new Vector2(1, 1);
            Gravity = new Vector2(0, 1);
            GravityAcceleration = new Vector2(0, 0.1f);
            TerminalVelocity = new Vector2(0, 6);
            Action = ActionState.run;



            _animationManager.AddAnimation(content.Load<Texture2D>("Enemies/Skeleton"), ActionState.run, 0, animationDimentions * 2, animationDimentions, animationDimentions, animationDimentions, 12);
            _animationManager.AddAnimation(content.Load<Texture2D>("Enemies/Skeleton"), ActionState.attack, 0, 0, animationDimentions, animationDimentions, animationDimentions, 9);
            _animationManager.AddAnimation(content.Load<Texture2D>("Enemies/Skeleton"), ActionState.hit, 0, animationDimentions * 4, animationDimentions, animationDimentions, animationDimentions, 3);
            _animationManager.AddAnimation(content.Load<Texture2D>("Enemies/Skeleton"), ActionState.death, 0, animationDimentions, animationDimentions, animationDimentions, animationDimentions, 12);
            _animationManager.currentAnimation = _animationManager.GetAnimation(ActionState.run);

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
                    _animationManager.currentAnimation.Update(gameTime);
                    break;
                case ActionState.attack:
                    _animationManager.currentAnimation.Update(gameTime);
                    break;
                case ActionState.hit:
                    if (!(_animationManager.currentAnimation.NLoops >= 6))
                    {
                        _animationManager.currentAnimation.Update(gameTime);
                    }
                    else
                    {
                        _animationManager.currentAnimation.NLoops = 0;
                        if (Health > 0)
                        {
                            if (_animationManager.currentAnimation.IsComplete)
                            {
                                _animationManager.currentAnimation.NLoops = 0;
                                _animationManager.currentAnimation.IsComplete = false;
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
                    if (!(_animationManager.currentAnimation.NLoops >= 1))
                    {
                        _animationManager.currentAnimation.Update(gameTime);
                    }
                    break;
            }


            _animationManager.Update(gameTime, Action);
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



        public override void AttackHero(Hero target)
        {
            if (Action == ActionState.attack)
            {
                base.AttackHero(target);
            }

        }

    }
}
