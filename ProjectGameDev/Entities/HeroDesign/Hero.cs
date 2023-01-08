using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectGameDev.Animations;
using ProjectGameDev.Enemies;
using ProjectGameDev.Entities;
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

enum Action
{
    idle,
    run,
    jump,
    fall,
    death,
    hit,
    attack
}
namespace ProjectGameDev.Entities.HeroDesign
{
    internal class Hero : Entity, IGameObject
    {
        private Texture2D blokTexture;

        private Texture2D textureIdle;
        private Texture2D textureRun;
        private Texture2D textureJump;
        private Texture2D textureFall;
        private Texture2D textureAttack;

        private Texture2D textureDeath;
        private Texture2D textureTakeHit;


        public int NLoopsHit = 0, NLoopsDeath = 0;


        private int screenHeight = 720;
        private int screenWidth = 1280;

        

        private Animation idle;
        private Animation run;
        private Animation jump;
        private Animation fall;
        private Animation attack;
        private Animation death;
        private Animation takeHit;



        private int Counter = 0, lastCount = 0;

        public HealthBar Healthbar;



        private int attackWidth = 110;
        private int attackHeight = 75;

        private int startY = 50;
        private int startX = 60;





        private int schuifOp_X = 150;


        public Hero(Texture2D textureIdle, Texture2D textureRun, Texture2D textureJump, Texture2D textureFall, Texture2D textureAttack1, Texture2D textureAttack2, Texture2D textureAttack3, Texture2D textureAttack4, Texture2D textureDeath, Texture2D textureTakeHit, Texture2D blokTexture, HealthBar Healthbar)
        {
            RunningSpeed = 8;
            Width = 30;
            Height = 45;
            this.Healthbar = Healthbar;
            this.blokTexture = blokTexture;

            Position = new Vector2(700, 300);
            Speed = new Vector2(1, 1);
            int a = Width * 3;
            int b = Height * 3;
            BoundingBox = new Rectangle(Convert.ToInt16(Position.X), Convert.ToInt16(Position.Y), Width * 3, Height * 3);
            AttackHitbox = new Rectangle(Convert.ToInt16(BoundingBox.Right), Convert.ToInt16(BoundingBox.Y), 30, 50);

            Action = Action.idle;
            
            Gravity = new Vector2(0, 1);
            GravityAcceleration = new Vector2(0, 0.1f);
            TerminalVelocity = new Vector2(0, 2.5f);

            //idle
            this.textureIdle = textureIdle;
            idle = new Animation();
            idle.AddFrame(new AnimationFrame(new Rectangle(startX, startY, Width, Height)));
            idle.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X, startY, Width, Height)));
            idle.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 2, startY, Width, Height)));
            idle.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 3, startY, Width, Height)));
            idle.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 4, startY, Width, Height)));
            idle.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 5, startY, Width, Height)));
            idle.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 6, startY, Width, Height)));
            idle.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 7, startY, Width, Height)));

            //run
            this.textureRun = textureRun;
            run = new Animation();
            run.AddFrame(new AnimationFrame(new Rectangle(startX, startY, Width, Height)));
            run.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X, startY, Width, Height)));
            run.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 2, startY, Width, Height)));
            run.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 3, startY, Width, Height)));
            run.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 4, startY, Width, Height)));
            run.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 5, startY, Width, Height)));
            run.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 6, startY, Width, Height)));
            run.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 7, startY, Width, Height)));

            //jump
            this.textureJump = textureJump;
            jump = new Animation();
            jump.AddFrame(new AnimationFrame(new Rectangle(startX, startY, Width, Height)));
            jump.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X, startY, Width, Height)));


            //fall
            this.textureFall = textureFall;
            fall = new Animation();
            fall.AddFrame(new AnimationFrame(new Rectangle(startX, startY, Width, Height)));
            fall.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X, startY, Width, Height)));


            //attack
            this.textureAttack = textureAttack1;
            attack = new Animation();
            attack.AddFrame(new AnimationFrame(new Rectangle(30, 20, attackWidth, attackHeight)));
            attack.AddFrame(new AnimationFrame(new Rectangle(30 + schuifOp_X, 20, attackWidth, attackHeight)));
            attack.AddFrame(new AnimationFrame(new Rectangle(30 + schuifOp_X * 2, 20, attackWidth, attackHeight)));
            attack.AddFrame(new AnimationFrame(new Rectangle(30 + schuifOp_X * 3, 20, attackWidth, attackHeight)));


            //death
            this.textureDeath = textureDeath;
            death = new Animation();
            death.AddFrame(new AnimationFrame(new Rectangle(startX, startY, 53, Height)));
            death.AddFrame(new AnimationFrame(new Rectangle(startX + 143, startY, 53, Height)));
            death.AddFrame(new AnimationFrame(new Rectangle(startX + 143 * 2, startY, 53, Height)));
            death.AddFrame(new AnimationFrame(new Rectangle(startX + 143 * 3, startY, 53, Height)));
            death.AddFrame(new AnimationFrame(new Rectangle(startX + 143 * 4, startY, 53, Height)));
            death.AddFrame(new AnimationFrame(new Rectangle(startX + 143 * 5, startY, 53, Height)));



            //take hit
            this.textureTakeHit = textureTakeHit;
            takeHit = new Animation();
            takeHit.AddFrame(new AnimationFrame(new Rectangle(startX, startY, Width, Height)));
            takeHit.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X, startY, Width, Height)));
            takeHit.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 2, startY, Width, Height)));
            takeHit.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 3, startY, Width, Height)));

        }

        public void Reset(HealthBar Healthbar)
        {
            Position = new Vector2(700, 300);
            this.Healthbar = Healthbar;
            Action = Action.idle;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(blokTexture, BoundingBox, Color.Red);
            switch (Action)
            {
                case Action.idle:
                    spriteBatch.Draw(textureIdle, Position, idle.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    break;
                case Action.run:
                    spriteBatch.Draw(textureRun, Position, run.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    break;
                case Action.jump:
                    spriteBatch.Draw(textureJump, Position, jump.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    break;
                case Action.fall:
                    spriteBatch.Draw(textureFall, Position, fall.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    break;
                case Action.attack:
                    //spriteBatch.Draw(blokTexture, AttackHitbox, Color.Red);
                    if (SpriteEffect == SpriteEffects.FlipHorizontally)
                    {
                        spriteBatch.Draw(textureAttack, new Vector2(Position.X - 145, Position.Y - 2), attack.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(textureAttack, new Vector2(Position.X - 90, Position.Y - 2), attack.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    }
                    break;
                case Action.hit:
                    spriteBatch.Draw(textureTakeHit, Position, takeHit.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    break;
                case Action.death:
                    spriteBatch.Draw(textureDeath, Position, death.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
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

            //snelheid = Vector2.Zero;


            KeyboardState State = Keyboard.GetState();
            if (State.IsKeyUp(Keys.W) && Action == Action.attack)
            {
                if (attack.NLoops >= 1)
                {
                    attack.NLoops = 0;
                    attack.CurrentFrame = attack.Frames[0];
                    Action = Action.idle;
                }
            }
            if (Action != Action.attack)
            {
                if (State.IsKeyDown(Keys.Left))
                {
                    Speed.X = -RunningSpeed;
                    SpriteEffect = SpriteEffects.FlipHorizontally;
                }

                if (State.IsKeyDown(Keys.Right))
                {
                    Speed.X = RunningSpeed;
                    SpriteEffect = SpriteEffects.None;
                }

                if (State.IsKeyUp(Keys.Left) && State.IsKeyUp(Keys.Right))
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
            }
            if (State.IsKeyDown(Keys.Space) && Action != Action.hit && Action != Action.jump && Action != Action.fall)
            {
                Action = Action.attack;
            }
            Move();
            UpdateBoundingBox();
            Fall();





            switch (Action)
            {
                case Action.idle:
                    idle.Update(gameTime);
                    break;
                case Action.run:
                    run.Update(gameTime);
                    break;
                case Action.jump:
                    jump.Update(gameTime);
                    break;
                case Action.fall:
                    fall.Update(gameTime);
                    break;
                case Action.attack:
                    attack.Update(gameTime);
                    break;
                case Action.hit:
                    if (!(takeHit.NLoops >= 2))
                    {
                        takeHit.Update(gameTime);
                    }
                    else
                    {
                        takeHit.NLoops = 0;
                        Action = Action.idle;
                    }
                    break;
                case Action.death:
                    if (!(death.NLoops >= 1))
                    {
                        death.Update(gameTime);

                    }
                    else
                    {
                        death.NLoops = 0;
                        Game1.GameState = GameState.death;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }





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
            if (Action == Action.attack)
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
            if (Action != Action.hit)
            {
                Action = Action.hit;

                if (!Healthbar.LowerHealth())
                {
                    Die();
                }
                
            }
        }

        

        public override void AttackEnemy(Enemy target)
        {
            if (Action == Action.attack)
            {
                base.AttackEnemy(target);
            }

        }

        private void Die()
        {
            Action = Action.death;
        }


    }
}
