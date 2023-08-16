using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToTwitter;
using Microsoft.Xna.Framework;
using System.IO;
using ProjectGameDev.Levels;
using ProjectGameDev.Entities;
using ProjectGameDev.Entities.Animation;
using ProjectGameDev.Entities.HeroDesign;
using ProjectGameDev.Interfaces;

namespace ProjectGameDev.Enemies
{
    internal abstract class Enemy : Entity, IGameObject
    {

        public State State;
        public override bool Collision(Block target)
        {
            bool intersects = BoundingBox.Intersects(target.BoundingBox);
            if (intersects)
            {
                Collided = true;
            }
            return intersects;
        }
        public override void CollisionWithBlock(Block target)
        {
            //floor collision
            if (Position.Y + Height * 3 - 50 <= target.Position.Y)
            {
                Position.Y = target.Position.Y - Height * 3 + 1;
                GroundLevel = Convert.ToInt16(target.Position.Y);

            }
            //right wall collision
            else if (BoundingBox.Right >= target.BoundingBox.Left && BoundingBox.Right <= target.BoundingBox.Left + 30)
            {
                State = State.left;
                SpriteEffect = SpriteEffects.FlipHorizontally;
            }
            //left wall collision
            else if (BoundingBox.Left <= target.BoundingBox.Right && Position.X >= target.BoundingBox.Right - 20)
            {
                State = State.right;
                SpriteEffect = SpriteEffects.None;
            }
            //ceiling collision
            else if (Position.Y + 50 <= target.Position.Y + target.BoundingBox.Y)
            {
                Position.Y += 3;
                Speed.Y = 3;

            }
        }

        public virtual void Reset()
        {
            Health = 3;
            Action = ActionState.run;
        }
        public virtual void CheckForHero(Hero target)
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

        public virtual bool CheckForAttackCollision(Hero target)
        {
            if (Action == ActionState.death || Action == ActionState.hit)
            {
                return false;
            }
            bool intersects = AttackHitbox.Intersects(target.BoundingBox);
            if (intersects)
            {
                Collided = true;
            }
            return intersects;
        }
        public virtual void AttackHero(Hero target)
        {
            target.TakeDamage();
        }

        public override void Move()
        {
            if (Action != ActionState.death && Action != ActionState.hit)
            {
                switch (State)
                {
                    case State.right:
                        Speed.X = RunningSpeed;
                        break;
                    case State.left:
                        Speed.X = -RunningSpeed;
                        break;
                }
                if (Speed != Vector2.Zero)
                {
                    Position += Speed;
                    if (BoundingBox.Right > WallRight)
                    {
                        State = State.left;
                    }

                    if (BoundingBox.Left < WallLeft)
                    {
                        State = State.right;
                        SpriteEffect = SpriteEffects.None;
                    }

                }
            }
        }


    }
}
