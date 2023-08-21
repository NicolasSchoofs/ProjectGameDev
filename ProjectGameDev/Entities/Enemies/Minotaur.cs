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

        private int _animationDimentions = 96;

        Texture2D pixelTexture;

        public Minotaur(Vector2 spawnLocation, ContentManager content)
        {
            offset = new Vector2(70, 0);
            BoundingBox = new Rectangle(Convert.ToInt16(Position.X) + Convert.ToInt16(offset.X), Convert.ToInt16(Position.Y) + Convert.ToInt16(offset.Y), Width * 3, Height * 3);
            Width = 35;
            Height = 65;
            State = State.right;
            GroundLevel = 9999;
            WallRight = 9999;
            WallLeft = 0;
            AttackHitbox = new Rectangle(Convert.ToInt16(BoundingBox.Right), Convert.ToInt16(BoundingBox.Y), 30, 50);
            Position = spawnLocation;
            Speed = new Vector2(1, 1);
            SpriteEffect = SpriteEffects.None;
            RunningSpeed = 10;


            
            Gravity = new Vector2(0, 1);
            GravityAcceleration = new Vector2(0, 0.1f);
            TerminalVelocity = new Vector2(0, 6);

            Action = ActionState.run;

            _animationManager.AddAnimation(content.Load<Texture2D>("Enemies/Minotaur"), ActionState.run, 0, _animationDimentions, _animationDimentions, _animationDimentions, _animationDimentions, 8);


            _animationManager.AddAnimation(content.Load<Texture2D>("Enemies/Minotaur"), ActionState.attack, 0, _animationDimentions * 3, _animationDimentions, _animationDimentions, _animationDimentions, 9);


            _animationManager.AddAnimation(content.Load<Texture2D>("Enemies/Minotaur"), ActionState.hit, 0, _animationDimentions * 8, _animationDimentions, _animationDimentions, _animationDimentions, 3);


            _animationManager.AddAnimation(content.Load<Texture2D>("Enemies/Minotaur"), ActionState.death, 0, _animationDimentions * 9, _animationDimentions, _animationDimentions, _animationDimentions, 6);
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


        public override void AttackHero(Hero target)
        {
            if (Action == ActionState.attack)
            {
                base.AttackHero(target);
            }

        }
    

    }
}
