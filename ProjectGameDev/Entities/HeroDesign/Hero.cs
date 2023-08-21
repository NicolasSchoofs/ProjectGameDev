using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectGameDev.Animations;
using ProjectGameDev.Enemies;
using ProjectGameDev.Entities;
using ProjectGameDev.Entities.Animation;
using ProjectGameDev.Interfaces;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.MediaFoundation;
using SharpDX.XAudio2;
using BoundingBox = Microsoft.Xna.Framework.BoundingBox;
using Color = Microsoft.Xna.Framework.Color;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using Timer = SharpDX.MediaFoundation.Timer;
using Vector2 = Microsoft.Xna.Framework.Vector2;


namespace ProjectGameDev.Entities.HeroDesign
{
    internal class Hero : Entity, IGameObject
    {
        


        public int NLoopsHit = 0, NLoopsDeath = 0;


        private int screenHeight = 720;
        private int screenWidth = 1280;






        private int Counter = 0, lastCount = 0;

        public HealthBar Healthbar;



        private int attackWidth = 110;
        private int attackHeight = 75;

        private int startY = 20;
        private int startX = 20;


        private int schuifOp_X = 150;


        public Hero(HealthBar Healthbar, ContentManager content)
        {

            offset = new Vector2(120, 115);
            RunningSpeed = 8;
            Width = 30;
            Height = 37;
            this.Healthbar = Healthbar;

            Position = new Vector2(700, 400);
            Speed = new Vector2(1, 1);
            int a = Width * 3;
            int b = Height * 3;
            BoundingBox = new Rectangle(Convert.ToInt16(Position.X) + Convert.ToInt16(offset.X), Convert.ToInt16(Position.Y) + Convert.ToInt16(offset.Y), Width * 3, Height * 3);
            AttackHitbox = new Rectangle(Convert.ToInt16(BoundingBox.Right), Convert.ToInt16(BoundingBox.Y), 30, 50);

            Action = ActionState.idle;
            
            Gravity = new Vector2(0, 1);
            GravityAcceleration = new Vector2(0, 0.1f);
            TerminalVelocity = new Vector2(0, 2.5f);

            this._animationManager.AddAnimation(content.Load<Texture2D>("Hero/Idle"),ActionState.idle, startX, startY, schuifOp_X, attackWidth, attackHeight, 8);



            this._animationManager.AddAnimation(content.Load<Texture2D>("Hero/Run"), ActionState.run, startX, startY, schuifOp_X, attackWidth, attackHeight, 8);


            this._animationManager.AddAnimation(content.Load<Texture2D>("Hero/Jump"), ActionState.jump, startX, startY, schuifOp_X, attackWidth, attackHeight, 2);
      


            this._animationManager.AddAnimation(content.Load<Texture2D>("Hero/Fall"), ActionState.fall, startX, startY, schuifOp_X, attackWidth, attackHeight, 2);
          


            this._animationManager.AddAnimation(content.Load<Texture2D>("Hero/Attack1"), ActionState.attack, startX, startY, schuifOp_X, attackWidth, attackHeight, 4);
    


            this._animationManager.AddAnimation(content.Load<Texture2D>("Hero/Death"), ActionState.death, startX, startY, 143, attackWidth, attackHeight, 6);
     

            this._animationManager.AddAnimation(content.Load<Texture2D>("Hero/Take Hit"), ActionState.hit, startX, startY, 150, attackWidth, attackHeight, 4);
      

        }

        public void Reset(HealthBar Healthbar)
        {
            Position = new Vector2(700, 200);
            this.Healthbar = Healthbar;
            Speed = new Vector2(1, 1);
            Gravity = new Vector2(0, 1);
            GravityAcceleration = new Vector2(0, 0.1f);
            TerminalVelocity = new Vector2(0, 2.5f);
            Action = ActionState.idle;
        }
    

        public override void Update(GameTime gameTime)
        {

            Counter++;
            if (!Collided)
            {
                GroundLevel = 9999;
                WallRight = 9999;
                WallLeft = 0;
            }

            Collided = false;


            KeyboardState State = Keyboard.GetState();
           
            if (Action != ActionState.attack)
            {
                if (Action == ActionState.hit)
                {
                    if (_animationManager.currentAnimation.NLoops >= 2)
                    {
                        _animationManager.currentAnimation.NLoops = 0;
                        _animationManager.currentAnimation.IsComplete = false;
                        Action = ActionState.idle;
                        _animationManager.currentAnimation = _animationManager.GetAnimation(ActionState.idle); 
                    }
                }

                if (_animationManager.currentAnimation.IsComplete)
                {
                    _animationManager.currentAnimation.IsComplete = false;
                    _animationManager.currentAnimation.NLoops = 0;
                }


                if (State.IsKeyDown(Keys.Left))
                {
                    Speed.X = -RunningSpeed;
                    SpriteEffect = SpriteEffects.FlipHorizontally;
                }
                else if (State.IsKeyDown(Keys.Right))
                {
                    Speed.X = RunningSpeed;
                    SpriteEffect = SpriteEffects.None;
                }
                else
                {
                    Speed.X = 0;
                }

                if (State.IsKeyDown(Keys.Up) && Speed.Y == 0)
                {
                    Speed.Y = -30;
                }

                if (State.IsKeyDown(Keys.Escape))
                {
                    Game1.GameState = GameState.pauze;
                }

                if (State.IsKeyDown(Keys.Space) && Action != ActionState.hit && Action != ActionState.jump && Action != ActionState.fall)
                {
                    Action = ActionState.attack;
                }
            }
            else 
            {
                if (_animationManager.currentAnimation.NLoops >= 1)
                {
                    _animationManager.currentAnimation.NLoops = 0;
                    _animationManager.currentAnimation.IsComplete = false;
                    Action = ActionState.idle;
                    _animationManager.currentAnimation = _animationManager.GetAnimation(ActionState.idle); 
                }

            }
            
            Move();
            UpdateBoundingBox();
            Fall();



            _animationManager.Update(gameTime, Action);
            this.Healthbar.Update(gameTime);

        }

        public override void UpdateBoundingBox()
        {
            base.UpdateBoundingBox();
            if (SpriteEffect == SpriteEffects.FlipHorizontally)
            {
                AttackHitbox = new Rectangle(BoundingBox.Left - 120, BoundingBox.Y, 180, 130);
            }
            else
            {
                AttackHitbox = new Rectangle(BoundingBox.Right + 60, BoundingBox.Y, 180, 130);
            }


        }

       

        public override void TakeDamage()
        {
            if (Action != ActionState.hit)
            {
                Action = ActionState.hit;

                if (!Healthbar.LowerHealth())
                {
                    Die();
                }
                
            }
        }

        

        public override void Attack(Entity target)
        {
            if (Action == ActionState.attack)
            {
                base.Attack(target);
            }

        }

        private void Die()
        {
            Action = ActionState.death;
        }


    }
}
