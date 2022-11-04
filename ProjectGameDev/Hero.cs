using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using SharpDX.XAudio2;
using Keys = Microsoft.Xna.Framework.Input.Keys;

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
    internal class Hero: IGameObject
    {
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

        private int screenHeight = 720;
        private int screenWidth = 1280;

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




        private Vector2 positie;
        private Vector2 snelheid;
        private Vector2 gravity;
        private Vector2 gravityAcceleration;
        private Vector2 terminalVelocity;
        private int width = 30;
        private int height = 45;
        private int attackWidth = 110;
        private int attackHeight = 47;

        private int startY = 50;
        private int startX = 60;


        private Action action;
        


        private int schuifOp_X = 150;
        

        public Hero(Texture2D textureIdle, Texture2D textureRun, Texture2D textureJump, Texture2D textureFall, Texture2D textureAttack1, Texture2D textureAttack2, Texture2D textureAttack3, Texture2D textureAttack4, Texture2D textureDeath, Texture2D textureTakeHit)
        {
            action = Action.idle;
            positie = new Vector2(0, 0);
            snelheid = new Vector2(1, 1);
            gravity = new Vector2(0, 1);
            gravityAcceleration = new Vector2(0, 0.1f);
            terminalVelocity = new Vector2(0, 10);

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

            //take hit




        }

        public void Draw(SpriteBatch spriteBatch) 
        {
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
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Update(GameTime gameTime)
        {
           
           idle.Update(gameTime);
           run.Update(gameTime);
           jump.Update(gameTime);
           fall.Update(gameTime);
           attack1.Update(gameTime);
           //snelheid = Vector2.Zero;
           
           

            KeyboardState state = Keyboard.GetState();

           if (state.IsKeyDown(Keys.Left))
           {
               snelheid.X = -5;
               spriteEffect = SpriteEffects.FlipHorizontally;
           }

           if (state.IsKeyDown(Keys.Right))
           {
               snelheid.X = 5;
               spriteEffect = SpriteEffects.None;
           }

           if (state.IsKeyUp(Keys.Left) && state.IsKeyUp(Keys.Right))
           {
               snelheid.X = 0;
           }

           if (state.IsKeyDown(Keys.Up) && snelheid.Y == 0)
           {
               snelheid.Y = -25;
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

       
           Fall();
           Move();
         
        }

        private void Attack1()
        {
            
            action = Action.attack1;
        }

        private void Move()
        {
            //if (action != Action.attack1)
            //{
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
                    positie += snelheid;
                }
                else if (action != Action.attack1)
                {
                action = Action.idle;
                }
               
            //}
            
            
        }

        private void Fall()
        {
            if (action == Action.attack1)
            {
                if (positie.Y == screenHeight - attackHeight * 3)
                {

                }
                else if (positie.Y > screenHeight - attackHeight * 3)
                {
                    gravity = new Vector2(0, 1);
                    positie.Y = screenHeight - attackHeight * 3;
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
                if (positie.Y == screenHeight - height * 3)
                {

                }
                else if (positie.Y > screenHeight - height * 3)
                {
                    gravity = new Vector2(0, 1);
                    positie.Y = screenHeight - height * 3;
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

    
}
}
