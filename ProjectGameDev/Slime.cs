using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        Left,
        Right
    }
    internal class Slime:Enemy
    {
        private Texture2D textureIdle;

        private int groundLevel = 720;
        private int wallRight = 9999;
        private int wallLeft = 0;

        private Texture2D blokTexture;



        private int width = 25;
        private int height = 18;

        private Vector2 gravity, gravityAcceleration, terminalVelocity;


        private State state = State.Right;

        private SpriteEffects spriteEffect = SpriteEffects.None;
        private Animation idle;

        private Vector2 positie;
        private Vector2 snelheid;

        public Slime(Texture2D textuIdle, Texture2D blokTexture, Vector2 spawnLocation)
        {
            this.blokTexture = blokTexture;
            //collision box
            boundingBox = new Rectangle(Convert.ToInt16(positie.X), Convert.ToInt16(positie.Y), width * 3, height * 3);
            action = Action.run;
            //
            positie = spawnLocation;
            snelheid = new Vector2(1, 1);

            gravity = new Vector2(0, 1);
            gravityAcceleration = new Vector2(0, 0.1f);
            terminalVelocity = new Vector2(0, 10);

            this.textureIdle = textuIdle;
            idle = new Animation();
            idle.AddFrame(new AnimationFrame(new Rectangle(20, 30, width, height)));
            idle.AddFrame(new AnimationFrame(new Rectangle(100, 30, width, height)));
            idle.AddFrame(new AnimationFrame(new Rectangle(180, 30, width, height)));
            idle.AddFrame(new AnimationFrame(new Rectangle(260, 30, width, height)));
            idle.AddFrame(new AnimationFrame(new Rectangle(340, 30, width, height)));
            idle.AddFrame(new AnimationFrame(new Rectangle(420, 30, width, height)));
            idle.AddFrame(new AnimationFrame(new Rectangle(500, 30, width, height)));
        }

        public override void Update(GameTime gameTime)
        {
            if (!collided)
            {
                groundLevel = 9999;
                wallRight = 9999;
                wallLeft = 0;
            }
            collided = false;




            //animations
            switch (action)
            {
                case Action.run:
                    //movement
                    Move();
                    Fall();
                    UpdateBoundingBox();
                    idle.Update(gameTime);
                    break;
                case Action.hit:
                    action = Action.death;
                    break;
                case Action.death:
                    break;

            }
            
            
            
        }

        private void UpdateBoundingBox()
        {
            boundingBox = new Rectangle(Convert.ToInt16(positie.X), Convert.ToInt16(positie.Y), width * 3, height * 3);
        }


        private void Fall()
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

        private void Move()
        {
            switch (state)
            {
                case State.Left:
                    snelheid.X = -2;
                    break;
                case State.Right:
                    snelheid.X = 2;
                    break;
            }

            if (snelheid != Vector2.Zero)
            {
                positie += snelheid;
                if (boundingBox.Right > wallRight)
                {
                    state = State.Left;
                }

                if (boundingBox.Left < wallLeft)
                {
                    state = State.Right;
                    spriteEffect = SpriteEffects.None;
                }
                
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(textureIdle, positie, idle.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), spriteEffect, 0);
            //spriteBatch.Draw(blokTexture, boundingBox, Color.Red);
            switch (action)
            {
                case Action.run:
                    spriteBatch.Draw(textureIdle, positie, idle.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), spriteEffect, 0);
                    break;
                case Action.death:
                    break;
            }
            
        }
            
        



        public override void CollisionWithBlock(Block target)
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
                state = State.Left;
                spriteEffect = SpriteEffects.FlipHorizontally;
            }

            //links
            else if (boundingBox.Left <= target.boundingBox.Right && positie.X >= target.boundingBox.Right - 20)
            {
                state = State.Right;
                spriteEffect = SpriteEffects.None;
            }

        }

        public override void Reset()
        {
            health = 3;
            action = Action.run;
        }





    }
}
