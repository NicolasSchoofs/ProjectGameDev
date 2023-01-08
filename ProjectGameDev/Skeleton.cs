using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.XAudio2;

namespace ProjectGameDev
{
    internal class Skeleton: Enemy
    {
        private Texture2D texture;
        private Animation run;
        private Animation attack;
        private Animation hit;
        private Animation death;

        private int width = 45, height = 37;
        private State state = State.Right;
        private int groundLevel = 9999;
        private int wallRight = 9999;
        private int wallLeft = 0;

        private int startX = 0, startY = 140;
        private int schuifOp_X = 64, schuifOp_Y = 0;

        private Texture2D blokTexture2D;
        private int attackStartX = 5, attackStartY = 10;

        private Rectangle attackHitbox;



        private Vector2 position;

        private Vector2 snelheid;
        private Vector2 gravity;
        private Vector2 gravityAcceleration;
        private Vector2 terminalVelocity;

        private SpriteEffects spriteEffect = SpriteEffects.None;


        public Skeleton(Texture2D skeletonTexture, Texture2D blokTexture, Vector2 spawnLocation)
        {
            blokTexture2D = blokTexture;


            position = spawnLocation;
            boundingBox = new Rectangle(Convert.ToInt16(position.X), Convert.ToInt16(position.Y), width * 3, height * 3);
            attackHitbox = new Rectangle(Convert.ToInt16(boundingBox.Right), Convert.ToInt16(boundingBox.Y), 30, 50);
            snelheid = new Vector2(1, 1);
            gravity = new Vector2(0, 1);
            gravityAcceleration = new Vector2(0, 0.1f);
            terminalVelocity = new Vector2(0, 6);

            action = Action.run;
            texture = skeletonTexture;



            //run
            run = new Animation();
            run.AddFrame(new AnimationFrame(new Rectangle(startX, startY, width, height)));
            run.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X, startY, width, height)));
            run.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 2, startY, width, height)));
            run.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 3, startY, width, height)));
            run.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 4, startY, width, height)));
            run.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 5, startY, width, height)));
            run.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 6, startY, width, height)));
            run.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 7, startY, width, height)));
            run.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 8, startY, width, height)));
            run.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 9, startY, width, height)));
            run.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 10, startY, width, height)));
            run.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 11, startY, width, height)));

            //attack
            attack = new Animation();
            attack.AddFrame(new AnimationFrame(new Rectangle(startX, attackStartY, 65, height)));
            attack.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X, attackStartY, 65, height)));
            attack.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 2, attackStartY, 65, height)));
            attack.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 3, attackStartY, 65, height)));
            attack.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 4, attackStartY, 65, height)));
            attack.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 5, attackStartY, 65, height)));
            attack.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 6, attackStartY, 65, height)));
            attack.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 7, attackStartY, 65, height)));
            attack.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 8, attackStartY, 65, height)));

            //hit
            hit = new Animation();
            hit.AddFrame(new AnimationFrame(new Rectangle(startX, 268, 65, height)));
            hit.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X, 268, 65, height)));
            hit.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 2, 268, 65, height)));

            //death
            death = new Animation();
            death.AddFrame(new AnimationFrame(new Rectangle(startX, 75, 70, height)));
            death.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X, 75, 70, height)));
            death.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 2, 75, 70, height)));
            death.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 3, 75, 70, height)));
            death.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 4, 75, 70, height)));
            death.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 5, 75, 70, height)));
            death.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 6, 75, 70, height)));
            death.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 7, 75, 70, height)));
            death.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 8, 75, 70, height)));
            death.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 9, 75, 70, height)));
            death.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 10, 75, 70, height)));
            death.AddFrame(new AnimationFrame(new Rectangle(startX + schuifOp_X * 11, 75, 70, height)));
        }
        public override void Update(GameTime gameTime)
        {

            if (!collided)
            {
                groundLevel = 9999;
                wallRight = 9999;
                wallLeft = 0;
            }
            //hit.Update(gameTime);

            
            collided = false;

            switch (action)
            {
                case Action.run:
                    Fall();
                    Move();
                    UpdateBoundingBox();
                    run.Update(gameTime);
                    break;
                case Action.attack1:
                    attack.Update(gameTime);
                    break;
                case Action.death:
                    if (!(death.nLoops >= 1))
                    {
                        death.Update(gameTime);
                    }
                    break;
                case Action.hit:
                    if (!(hit.nLoops >= 6))
                    {
                        hit.Update(gameTime);
                    }
                    else
                    {
                        hit.nLoops = 0;
                        if (health > 0)
                        {
                            action = Action.run;
                        }
                        else
                        {
                            Die();
                        }
                        
                    }
                    break;
            }
        }

        private void Move()
        {
            if (action != Action.death && action != Action.hit)
            {
           
                switch (state)
                {
                    case State.Right:
                        snelheid.X = 5;
                        break;
                    case State.Left:
                        snelheid.X = -5;
                        break;
                }
                if (snelheid != Vector2.Zero)
                {
                    position += snelheid;
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
        }

        public override void CheckForHero(Hero target)
        {
            if (action != Action.death && action != Action.hit)
            {
                
            
                if (state == State.Right)
                {
                    if (target.positie.X >= position.X && target.positie.X <= position.X + 250)
                    {
                        action = Action.attack1;
                    }
                    else
                    {
                        action = Action.run;
                    }
                }

                if (state == State.Left)
                {
                    if (target.positie.X <= position.X && target.positie.X >= position.X - 250)
                    {
                        action = Action.attack1;
                    }
                    else
                    {
                        action = Action.run;
                    }
                }
            }
        }
        private void Fall()
        {
            if (position.Y == groundLevel - height * 3)
            {

            }
            else if (position.Y > groundLevel - height * 3)
            {
                gravity = new Vector2(0, 1);
                position.Y = groundLevel - height * 3;
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
        private void UpdateBoundingBox()
        {
            boundingBox = new Rectangle(Convert.ToInt16(position.X), Convert.ToInt16(position.Y), width * 3, height * 3);

            if (state == State.Left)
            {
                attackHitbox = new Rectangle(boundingBox.Left - 50, boundingBox.Y, 130, 130);
            }
            else
            {
                attackHitbox = new Rectangle(boundingBox.Right - 60, boundingBox.Y, 130, 130);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (action)
            {
                case Action.run:
                    //spriteBatch.Draw(blokTexture2D, boundingBox, Color.Red);
                    spriteBatch.Draw(texture, position, run.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), spriteEffect, 0);
                    break;
                case Action.attack1:
                    //spriteBatch.Draw(blokTexture2D, attackHitbox, Color.Red);
                    if (state == State.Left)
                    {
                        spriteBatch.Draw(texture, new Vector2(position.X - 40, position.Y), attack.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), spriteEffect, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(texture, position, attack.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), spriteEffect, 0);
                    }
                    break;
                case Action.death:
                    if (state == State.Left)
                    {
                        spriteBatch.Draw(texture, new Vector2(position.X - 50, position.Y), death.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), spriteEffect, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(texture, position, death.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), spriteEffect, 0);
                    }
                    break;
                case Action.hit:
                    if (state == State.Left)
                    {
                        spriteBatch.Draw(texture, new Vector2(position.X - 50, position.Y), hit.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), spriteEffect, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(texture, position, hit.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), spriteEffect, 0);
                    }
                    
                    break;
            }
        }

        public override void CollisionWithBlock(Block target)
        {
            //landen op
            if (position.Y + height * 3 - 50 <= target.Position.Y)
            {
                position.Y = target.Position.Y - height * 3 + 1;
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
            else if (boundingBox.Left <= target.boundingBox.Right && position.X >= target.boundingBox.Right - 20)
            {
                state = State.Right;
                spriteEffect = SpriteEffects.None;
            }


        }
        public override bool CheckForAttackCollision(Hero target)
        {
            bool intersects = attackHitbox.Intersects(target.boundingBox);
            if (intersects)
            {
                collided = true;
            }
            return intersects;
        }
        public override void AttackHero(Hero target)
        {
            if (action == Action.attack1)
            {
                target.TakeDamage();
            }

        }

        public override void Reset()
        {
            health = 3;
            action = Action.run;
        }
    }
}
