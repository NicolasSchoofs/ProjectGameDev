using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ProjectGameDev.Enemies;
using ProjectGameDev.Entities.Animation;
using SharpDX.XAudio2;

namespace ProjectGameDev.Entities
{
    internal abstract class Entity
    {
        public ActionState Action;
        public int Health = 3;
        public int Height, Width;
        public SpriteEffects SpriteEffect;
        public Vector2 Position;
        public Rectangle BoundingBox;
        public bool Collided;

        public Rectangle AttackHitbox;

        public int GroundLevel, WallLeft, WallRight;
        public Vector2 Speed;

        public int RunningSpeed;

        public Vector2 Gravity;
        public Vector2 GravityAcceleration;
        public Vector2 TerminalVelocity;


        public virtual bool Collision(Block target)
        {
            bool intersects = BoundingBox.Intersects(target.BoundingBox);
            if (intersects)
            {
                Collided = true;
            }
            return intersects;
        }
        public virtual void CollisionWithBlock(Block target)
        {

            if (Position.Y + Height * 3 - 80 <= target.Position.Y)
            {
                if (!(BoundingBox.Right > WallRight) && !(BoundingBox.Left < WallLeft))
                {
                    Position.Y = target.Position.Y - Height * 3 + 1;
                    GroundLevel = Convert.ToInt16(target.Position.Y);
                }

            }

            else if (BoundingBox.Right >= target.BoundingBox.Left && BoundingBox.Right <= target.BoundingBox.Left + 30)
            {
                WallRight = Convert.ToInt16(target.BoundingBox.Left);

            }


            else if (BoundingBox.Left <= target.BoundingBox.Right && Position.X >= target.BoundingBox.Right - 20)
            {
                WallLeft = Convert.ToInt16(target.BoundingBox.Right);
            }



            else if (Position.Y + 50 <= target.Position.Y + target.BoundingBox.Y)
            {
                Position.Y += 3;
                Speed.Y = 1;

            }
        }

        public virtual void TakeDamage()
        {
            if (Action != ActionState.hit && Action != ActionState.death)
            {
                Health--;
                Action = ActionState.hit;
            }
        }

        public virtual bool CheckForAttackCollision(Enemy target)
        {
            bool intersects = AttackHitbox.Intersects(target.BoundingBox);
            if (intersects)
            {
                Collided = true;
            }
            return intersects;
        }

        public virtual void Die()
        {
            Action = ActionState.death;
        }

        public virtual void Fall()
        {
            if (Position.Y > 3000)
            {
                Action = ActionState.death;
            }

            if (Position.Y == GroundLevel - Height * 3) return;

            if (Position.Y > GroundLevel - Height * 3)
            {
                Gravity = new Vector2(0, 1);
                Position.Y = GroundLevel - Height * 3;
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

        public virtual void UpdateBoundingBox()
        {
            BoundingBox = new Rectangle(Convert.ToInt16(Position.X), Convert.ToInt16(Position.Y), Width * 3, Height * 3);

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public virtual void Move()
        {
            if (Action != ActionState.death && Action != ActionState.attack)
            {

                if (Action != ActionState.hit)
                {
                    if (Speed != Vector2.Zero)
                    {
                        if (Speed.Y < 0)
                        {
                            Action = ActionState.jump;
                        }
                        else if (Speed.Y > 0)
                        {
                            Action = ActionState.fall;
                        }
                        else
                        {
                            Action = ActionState.run;
                        }


                    }

                    else if (Action != ActionState.attack)
                    {
                        Action = ActionState.idle;
                    }

                }
                Position += Speed;
                if (BoundingBox.Right > WallRight)
                {
                    Position.X = WallRight - Width * 3;
                }

                if (BoundingBox.Left < WallLeft)
                {
                    Position.X = WallLeft;
                }

            }
        }

        public virtual void AttackEnemy(Enemy target)
        {
            target.TakeDamage();
        }
    }
}
