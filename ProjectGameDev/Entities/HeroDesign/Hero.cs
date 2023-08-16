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
        private AnimationManager animationManager = new AnimationManager();


        public int NLoopsHit = 0, NLoopsDeath = 0;


        private int screenHeight = 720;
        private int screenWidth = 1280;




        private int Counter = 0, lastCount = 0;

        public HealthBar Healthbar;



        private int attackWidth = 110;
        private int attackHeight = 75;

        private int startY = 50;
        private int startX = 60;





        private int schuifOp_X = 150;


        public Hero(HealthBar Healthbar, ContentManager content)
        {
            RunningSpeed = 8;
            Width = 30;
            Height = 45;
            this.Healthbar = Healthbar;

            Position = new Vector2(700, 300);
            Speed = new Vector2(1, 1);
            int a = Width * 3;
            int b = Height * 3;
            BoundingBox = new Rectangle(Convert.ToInt16(Position.X), Convert.ToInt16(Position.Y), Width * 3, Height * 3);
            AttackHitbox = new Rectangle(Convert.ToInt16(BoundingBox.Right), Convert.ToInt16(BoundingBox.Y), 30, 50);

            Action = ActionState.idle;
            
            Gravity = new Vector2(0, 1);
            GravityAcceleration = new Vector2(0, 0.1f);
            TerminalVelocity = new Vector2(0, 2.5f);

            this.animationManager.AddAnimation(content.Load<Texture2D>("Hero/Idle"),ActionState.idle, startX, startY, schuifOp_X, Width, Height, 8, BoundingBox);



            this.animationManager.AddAnimation(content.Load<Texture2D>("Hero/Run"), ActionState.run, startX, startY, schuifOp_X, Width, Height, 8, BoundingBox);


            this.animationManager.AddAnimation(content.Load<Texture2D>("Hero/Jump"), ActionState.jump, startX, startY, schuifOp_X, Width, Height, 2, BoundingBox);
      


            this.animationManager.AddAnimation(content.Load<Texture2D>("Hero/Fall"), ActionState.fall, startX, startY, schuifOp_X, Width, Height, 2, BoundingBox);
          


            this.animationManager.AddAnimation(content.Load<Texture2D>("Hero/Attack1"), ActionState.attack, 30, 20, schuifOp_X, attackWidth, attackHeight, 4, BoundingBox);
    


            this.animationManager.AddAnimation(content.Load<Texture2D>("Hero/Death"), ActionState.death, startX, startY, 143, 53, Height, 6, BoundingBox);
     

            this.animationManager.AddAnimation(content.Load<Texture2D>("Hero/Take Hit"), ActionState.hit, startX, startY, 143, 53, Height, 4, BoundingBox);
      

        }

        public void Reset(HealthBar Healthbar)
        {
            Position = new Vector2(700, 200);
            this.Healthbar = Healthbar;
            Action = ActionState.idle;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Action)
            {
                case ActionState.idle:
                    spriteBatch.Draw(animationManager.currentAnimation.texture, Position, animationManager.currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    break;
                case ActionState.run:
                    spriteBatch.Draw(animationManager.currentAnimation.texture, Position, animationManager.currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    break;
                case ActionState.jump:
                    spriteBatch.Draw(animationManager.currentAnimation.texture, Position, animationManager.currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    break;
                case ActionState.fall:
                    spriteBatch.Draw(animationManager.currentAnimation.texture, Position, animationManager.currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    break;
                case ActionState.attack:
                    if (SpriteEffect == SpriteEffects.FlipHorizontally)
                    {
                        spriteBatch.Draw(animationManager.currentAnimation.texture, new Vector2(Position.X - 145, Position.Y - 2), animationManager.currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(animationManager.currentAnimation.texture, new Vector2(Position.X - 90, Position.Y - 2), animationManager.currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    }
                    break;
                case ActionState.hit:
                    spriteBatch.Draw(animationManager.currentAnimation.texture, Position, animationManager.currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    break;
                case ActionState.death:
                    spriteBatch.Draw(animationManager.currentAnimation.texture, Position, animationManager.currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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
                    if (animationManager.currentAnimation.NLoops >= 2)
                    {
                        animationManager.currentAnimation.NLoops = 0;
                        animationManager.currentAnimation.IsComplete = false;
                        Action = ActionState.idle;
                        animationManager.currentAnimation = animationManager.GetAnimation(ActionState.idle); // Set idle animation
                    }
                }

                if (animationManager.currentAnimation.IsComplete)
                {
                    animationManager.currentAnimation.IsComplete = false;
                    animationManager.currentAnimation.NLoops = 0;
                }

              
               

                // Handle movement and other actions
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
                if (animationManager.currentAnimation.NLoops >= 1)
                {
                    animationManager.currentAnimation.NLoops = 0;
                    animationManager.currentAnimation.IsComplete = false;
                    Action = ActionState.idle;
                    animationManager.currentAnimation = animationManager.GetAnimation(ActionState.idle); // Set idle animation
                }



            }
            Move();
            UpdateBoundingBox();
            Fall();



            animationManager.Update(gameTime, Action);





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
                AttackHitbox = new Rectangle(BoundingBox.Right - 60, BoundingBox.Y, 180, 130);
            }

        }



        public void Fall()
        {
            if (Action == ActionState.attack)
            {
                if (Position.Y == GroundLevel - attackHeight * 3) return;

                if (Position.Y > GroundLevel - attackHeight * 3)
                {
                    Gravity = new Vector2(0, 1);
                    Position.Y = GroundLevel - attackHeight * 3;
                    Speed.Y = 0;
                    GravityAcceleration = new Vector2(0, 0.1f);
                }
                else
                {
                    if (Gravity.Y < TerminalVelocity.Y)
                    {
                        Gravity += GravityAcceleration;
                    }

                    Speed += Gravity;


                }

            }
            else
            {
                base.Fall();

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

        

        public override void AttackEnemy(Enemy target)
        {
            if (Action == ActionState.attack)
            {
                base.AttackEnemy(target);
            }

        }

        private void Die()
        {
            Action = ActionState.death;
        }


    }
}
