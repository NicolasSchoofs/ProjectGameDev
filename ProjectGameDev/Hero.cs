﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

enum Action {
   idle,
   run,
   jump,
   fall,
   death,
   hit,
   attack1,
   attack2,
   attack3,
   attack4
}
namespace ProjectGameDev
{
    internal class Hero:Sprite, IGameObject
    {
        private Texture2D blokTexture;
        private Rectangle boundingBox;

        private Texture2D textureIdle;
        private Texture2D textureRun;
        private Texture2D textureJump;
        private Texture2D textureFall;
        private Texture2D textureAttack1;
        private Texture2D textureAttack2;
        private Texture2D textureAttack3;
        private Texture2D textureAttack4;
        private Texture2D textureDeath;
        private Texture2D textureTakeHit;

        private int health = 10;

        private int nLoopsHit = 0, nLoopsDeath= 0;


        private int screenHeight = 720;
        private int groundLevel = 9999;
        private int screenWidth = 1280;
        private int wallRight = 9999;
        private int wallLeft = 0;

        private SpriteEffects spriteEffect = SpriteEffects.None;

        private Animation idle;
        private Animation run;
        private Animation jump;
        private Animation fall;
        private Animation attack1;
        private Animation attack2;
        private Animation attack3;
        private Animation attack4;
        private Animation death;
        private Animation takeHit;



        private int counter = 0, lastCount = 0;

        private HealthBar healthbar;




        public Vector2 positie;
        private Vector2 snelheid;
        private Vector2 gravity;
        private Vector2 gravityAcceleration;
        private Vector2 terminalVelocity;
        public int width = 30;
        public int height = 45;
        private int attackWidth = 110;
        private int attackHeight = 47;

        private int startY = 50;
        private int startX = 60;


        private Action action;
        


        private int schuifOp_X = 150;
        

        public Hero(Texture2D textureIdle, Texture2D textureRun, Texture2D textureJump, Texture2D textureFall, Texture2D textureAttack1, Texture2D textureAttack2, Texture2D textureAttack3, Texture2D textureAttack4, Texture2D textureDeath, Texture2D textureTakeHit, Texture2D blokTexture, HealthBar healthbar)
        {
            this.healthbar = healthbar;
            this.blokTexture = blokTexture;
            int a = width * 3;
            int b = height * 3;
            boundingBox = new Rectangle(Convert.ToInt16(positie.X), Convert.ToInt16(positie.Y), width * 3,height * 3);
            
            action = Action.idle;
            positie = new Vector2(0, 0);
            snelheid = new Vector2(1, 1);
            gravity = new Vector2(0, 1);
            gravityAcceleration = new Vector2(0, 0.1f);
            terminalVelocity = new Vector2(0, 6);

            //idle
            this.textureIdle = textureIdle;
            idle = new Animation();
            idle.AddFrame(new AnimationFrame(new Rectangle(startX, startY, width, height)));
            idle.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X, startY, width, height)));
            idle.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 2, startY, width, height)));
            idle.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 3, startY, width, height)));
            idle.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 4, startY, width, height)));
            idle.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 5, startY, width, height)));
            idle.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 6, startY, width, height)));
            idle.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 7, startY, width, height)));

            //run
            this.textureRun = textureRun;
            run = new Animation();
            run.AddFrame(new AnimationFrame(new Rectangle(startX, startY, width, height)));
            run.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X, startY, width, height)));
            run.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 2, startY, width, height)));
            run.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 3, startY, width, height)));
            run.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 4, startY, width, height)));
            run.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 5, startY, width, height)));
            run.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 6, startY, width, height)));
            run.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 7, startY, width, height)));

            //jump
            this.textureJump = textureJump;
            jump = new Animation();
            jump.AddFrame(new AnimationFrame(new Rectangle(startX, startY, width, height)));
            jump.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X, startY, width, height)));


            //fall
            this.textureFall = textureFall;
            fall = new Animation();
            fall.AddFrame(new AnimationFrame(new Rectangle(startX, startY, width, height)));
            fall.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X, startY, width, height)));


            //attack1
            this.textureAttack1 = textureAttack1;
            attack1 = new Animation();
            attack1.AddFrame(new AnimationFrame(new Rectangle(50, startY, attackWidth, attackHeight)));
            attack1.AddFrame(new AnimationFrame(new Rectangle(50 + schuifOp_X, startY, attackWidth, attackHeight)));
            attack1.AddFrame(new AnimationFrame(new Rectangle(50 + schuifOp_X * 2, startY, attackWidth, attackHeight)));
            attack1.AddFrame(new AnimationFrame(new Rectangle(50 + schuifOp_X * 3, startY, attackWidth, attackHeight)));

            //attack2
            this.textureAttack2 = textureAttack2;
            attack2 = new Animation();
            attack2.AddFrame(new AnimationFrame(new Rectangle(60, 50, width, height)));
            attack2.AddFrame(new AnimationFrame(new Rectangle(210, 50, width, height)));

            //attack3
            //attack4

            //death
            this.textureDeath = textureDeath;
            death = new Animation();
            death.AddFrame(new AnimationFrame(new Rectangle(startX, startY, 53, height)));
            death.AddFrame(new AnimationFrame(new Rectangle(startX + 143, startY, 53, height)));
            death.AddFrame(new AnimationFrame(new Rectangle(startX + 143 * 2, startY, 53, height)));
            death.AddFrame(new AnimationFrame(new Rectangle(startX + 143 * 3, startY, 53, height)));
            death.AddFrame(new AnimationFrame(new Rectangle(startX + 143 * 4, startY, 53, height)));
            death.AddFrame(new AnimationFrame(new Rectangle(startX + 143 * 5, startY, 53, height)));



            //take hit
            this.textureTakeHit = textureTakeHit;
            takeHit = new Animation();
            takeHit.AddFrame(new AnimationFrame(new Rectangle(startX, startY, width, height)));
            takeHit.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X, startY, width, height)));
            takeHit.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 2, startY, width, height)));
            takeHit.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 3, startY, width, height)));

        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            //spriteBatch.Draw(blokTexture, boundingBox, Color.Red);
            switch (action)
            {
                case Action.idle:
                    spriteBatch.Draw(textureIdle, positie, idle.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), spriteEffect, 0);
                    break;
                case Action.run:
                    spriteBatch.Draw(textureRun, positie, run.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), spriteEffect, 0);
                    break;
                case Action.jump:
                    spriteBatch.Draw(textureJump, positie, jump.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), spriteEffect, 0);
                    break;
                case Action.fall:
                    spriteBatch.Draw(textureFall, positie, fall.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), spriteEffect, 0);
                    break;
                case Action.attack1:
                    spriteBatch.Draw(textureAttack1, positie, attack1.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), spriteEffect, 0);
                    break;
                case Action.hit:
                    spriteBatch.Draw(textureTakeHit, positie, takeHit.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), spriteEffect, 0);
                    break;
                case Action.death:
                    spriteBatch.Draw(textureDeath, positie, death.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), spriteEffect, 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Update(GameTime gameTime)
        {
           
            
            if (action == Action.death)
            {
                if (nLoopsDeath + 1 <= death.nLoops)
                {
                    Game1.pauze = true;
                }
                death.Update(gameTime);
                return;
            }
            counter++;
            if (!collided)
            {
                groundLevel = 9999;
                wallRight = 9999;
                wallLeft = 0;
            }

            if (health <= 0)
            {
                System.Environment.Exit(0);
            }

           
            collided = false;
            
           //snelheid = Vector2.Zero;
           
           
           
           

           KeyboardState state = Keyboard.GetState();

           if (state.IsKeyDown(Keys.M))
           {
               
           }
           if (state.IsKeyDown(Keys.Left))
           {
               snelheid.X = -8;
               spriteEffect = SpriteEffects.FlipHorizontally;
           }

           if (state.IsKeyDown(Keys.Right))
           {
               snelheid.X = 8;
               spriteEffect = SpriteEffects.None;
           }

           if (state.IsKeyUp(Keys.Left) && state.IsKeyUp(Keys.Right))
           {
               snelheid.X = 0;
           }

           if (state.IsKeyDown(Keys.Up) && snelheid.Y == 0)
           {
               snelheid.Y = -30;
           }

           if (state.IsKeyDown(Keys.W))
           {
               Attack1();
           }

           if (state.IsKeyUp(Keys.W) && action == Action.attack1)
           {
               positie.Y = screenHeight - height * 3;
               action = Action.idle;
           }

            Move();
            UpdateBoundingBox();
            Fall();

            
           
            
            
            
            
            switch (action)
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
                case Action.attack1:
                    attack1.Update(gameTime);
                    break;
                case Action.hit:
                    takeHit.Update(gameTime);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (action == Action.hit && nLoopsHit + 1 <= takeHit.nLoops)
            {
                action = Action.idle;
                nLoopsHit = takeHit.nLoops;
            }




        }

        private void UpdateBoundingBox()
        {
            boundingBox = new Rectangle(Convert.ToInt16(positie.X), Convert.ToInt16(positie.Y), width * 3, height * 3);
        }

        private void Attack1()
        {
            
            action = Action.attack1;
        }

        private void Move()
        {
            if (action != Action.hit)
            {
                if (snelheid != Vector2.Zero)
                {
                    if (snelheid.Y < 0)
                    {
                        action = Action.jump;
                    }
                    else if (snelheid.Y > 0)
                    {
                        action = Action.fall;
                    }
                    else
                    {
                        action = Action.run;
                    }

                    
                }

                else if (action != Action.attack1)
                {
                    action = Action.idle;
                }

            }
            positie += snelheid;
            if (boundingBox.Right > wallRight)
            {
                positie.X = wallRight - width * 3;
            }

            if (boundingBox.Left < wallLeft)
            {
                positie.X = wallLeft;
            }


        }

        private void Fall()
        {
            if (positie.Y > 3000)
            {
                System.Environment.Exit(0);
            }
            if (action == Action.attack1)
            {
                if (positie.Y == groundLevel - attackHeight * 3)
                {

                }
                else if (positie.Y > groundLevel - attackHeight * 3)
                {
                    gravity = new Vector2(0, 1);
                    positie.Y = groundLevel - attackHeight * 3;
                    snelheid.Y = 0;
                    gravityAcceleration = new Vector2(0, 0.1f);
                }
                else
                {
                    if (gravity.Y < terminalVelocity.Y)
                    {
                        gravity += gravityAcceleration;
                    }

                    snelheid += gravity;


                }

            }
            else
            {
                if (positie.Y == groundLevel - height * 3)
                {

                }
                else if (positie.Y > groundLevel - height * 3)
                {
                    gravity = new Vector2(0, 1);
                    positie.Y = groundLevel - height * 3;
                    snelheid.Y = 0;
                    gravityAcceleration = new Vector2(0, 0.1f);
                }
                else
                {
                    if (gravity.Y < terminalVelocity.Y)
                    {
                        gravity += gravityAcceleration;
                    }

                    snelheid += gravity;


                }

            }
            
        }

        public bool Collision(Block target)
        {
            bool intersects = boundingBox.Intersects(target.boundingBox);
            if (intersects)
            {
                collided = true;
            }
            return intersects;
        }
      

        public void CollisionWithBlock(Block target)
        {
            //landen op
            if (positie.Y + height * 3 - 50 <= target.Position.Y)
            {
                positie.Y = target.Position.Y - height * 3 + 1;
                groundLevel = Convert.ToInt16(target.Position.Y);

            }
            //rechts
            // && boundingBox.Right <= target.boundingBox.Left + 30
            else if (boundingBox.Right >= target.boundingBox.Left && boundingBox.Right <= target.boundingBox.Left + 30)
            {
                wallRight = Convert.ToInt16(target.boundingBox.Left);

            }

            //links
            else if (boundingBox.Left <= target.boundingBox.Right && positie.X >= target.boundingBox.Right - 20)
            {
                wallLeft = Convert.ToInt16(target.boundingBox.Right);
            }

            
        }
        public bool Collision(Slime target)
        {
            bool intersects = boundingBox.Intersects(target.boundingBox);
            if (intersects)
            {
                collided = true;
            }
            return intersects;
        }
        public void CollisionWithSlime(Slime target)
        {
            if (action != Action.hit)
            {
                action = Action.hit;

                if (!healthbar.LowerHealth())
                {
                    //DIE
                    action = Action.death;
                }
                
                lastCount = counter;
            }
            
        }




    }
}
