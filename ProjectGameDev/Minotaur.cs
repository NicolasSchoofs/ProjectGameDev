﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX;
using SharpDX.Direct3D9;
using SharpDX.MediaFoundation;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ProjectGameDev
{
 
    internal class Minotaur: Enemy, IGameObject
    {
        private Texture2D texture;
        private Animation run;
        private Animation attack;
        private Animation hit;
        private Animation death;

        private int width = 55, height = 45;
        private State state = State.Right;
        private int groundLevel = 9999;
        private int wallRight = 9999;
        private int wallLeft = 0;

        private int startX = 25, startY = 115;
        private int schuifOp_X = 96, schuifOp_Y = 0;

        private Texture2D blokTexture2D;
        private int attackStartX = 5, attackStartY = 290;
        private int hitStartX = 5, hitStartY = 788;
        private int deathStartX = 5, deathStartY = 885;

        private Rectangle attackHitbox;

        private Vector2 position;

        private Vector2 snelheid;
        private Vector2 gravity;
        private Vector2 gravityAcceleration;
        private Vector2 terminalVelocity;

        private SpriteEffects spriteEffect = SpriteEffects.None;


        public Minotaur(Texture2D minotaurTexture, Texture2D blokTexture, Vector2 spawnLocation)
        {
            blokTexture2D = blokTexture;
            boundingBox = new Rectangle(Convert.ToInt16(position.X), Convert.ToInt16(position.Y), width * 3, height * 3);
            attackHitbox = new Rectangle(Convert.ToInt16(boundingBox.Right), Convert.ToInt16(boundingBox.Y), 30, 50);

            position = spawnLocation;
            snelheid = new Vector2(1, 1);
            gravity = new Vector2(0, 1);
            gravityAcceleration = new Vector2(0, 0.1f);
            terminalVelocity = new Vector2(0, 6);

            action = Action.run;
            texture = minotaurTexture;



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

            //attack
            attack = new Animation();
            attack.AddFrame(new AnimationFrame(new Rectangle(attackStartX, attackStartY, 90, 70)));
            attack.AddFrame(new AnimationFrame(new Rectangle(attackStartX + schuifOp_X, attackStartY, 90, 70)));
            attack.AddFrame(new AnimationFrame(new Rectangle(attackStartX + schuifOp_X * 2, attackStartY, 90, 70)));
            attack.AddFrame(new AnimationFrame(new Rectangle(attackStartX + schuifOp_X * 3, attackStartY, 90, 70)));
            attack.AddFrame(new AnimationFrame(new Rectangle(attackStartX + schuifOp_X * 4, attackStartY, 90, 70)));
            attack.AddFrame(new AnimationFrame(new Rectangle(attackStartX + schuifOp_X * 5, attackStartY, 90, 70)));
            attack.AddFrame(new AnimationFrame(new Rectangle(attackStartX + schuifOp_X * 6, attackStartY, 90, 70)));
            attack.AddFrame(new AnimationFrame(new Rectangle(attackStartX + schuifOp_X * 7, attackStartY, 90, 70)));
            attack.AddFrame(new AnimationFrame(new Rectangle(attackStartX + schuifOp_X * 8, attackStartY, 90, 70)));

            //hit
            hit = new Animation();
            hit.AddFrame(new AnimationFrame(new Rectangle(hitStartX, hitStartY, 90, 70)));
            hit.AddFrame(new AnimationFrame(new Rectangle(hitStartX + schuifOp_X, hitStartY, 90, 70)));
            hit.AddFrame(new AnimationFrame(new Rectangle(hitStartX + schuifOp_X * 2, hitStartY, 90, 70)));
            //death
            death = new Animation();
            death.AddFrame(new AnimationFrame(new Rectangle(deathStartX, deathStartY, 90, 70)));
            death.AddFrame(new AnimationFrame(new Rectangle(deathStartX + schuifOp_X, deathStartY, 90, 70)));
            death.AddFrame(new AnimationFrame(new Rectangle(deathStartX + schuifOp_X * 2, deathStartY, 90, 70)));
            death.AddFrame(new AnimationFrame(new Rectangle(deathStartX + schuifOp_X * 3, deathStartY, 90, 70)));
            death.AddFrame(new AnimationFrame(new Rectangle(deathStartX + schuifOp_X * 4, deathStartY, 90, 70)));
            death.AddFrame(new AnimationFrame(new Rectangle(deathStartX + schuifOp_X * 5, deathStartY, 90, 70)));

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
                case Action.death:
                    if (!(death.nLoops >= 1))
                    {
                        death.Update(gameTime);
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
                        snelheid.X = 10;
                        break;
                    case State.Left:
                        snelheid.X = -10;
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
                attackHitbox = new Rectangle(boundingBox.Left - 30, boundingBox.Y- 40, 130, 170);
            }
            else
            {
                attackHitbox = new Rectangle(boundingBox.Right - 100, boundingBox.Y - 40, 130, 170);
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
                    spriteBatch.Draw(texture, new Vector2(position.X - 50, position.Y- 50), attack.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), spriteEffect, 0);
                    break;
                case Action.hit:
                    spriteBatch.Draw(texture, new Vector2(position.X - 50, position.Y), hit.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), spriteEffect, 0);
                    break;
                case Action.death:
                    spriteBatch.Draw(texture, position, death.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), spriteEffect, 0);
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
