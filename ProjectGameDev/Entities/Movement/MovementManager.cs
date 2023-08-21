using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Entities.Animation;
using SharpDX.Direct3D9;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Entities.Movement
{
    internal class MovementManager : IMovable
    {
        private Entity movableEntity;

        public MovementManager(Entity movableEntity)
        {
            this.movableEntity = movableEntity;
        }

 

        public void Update()
        {
            Fall();
            Move();
        }

        public void Fall()
        {
            if (movableEntity.Position.Y > 3000)
            {
                movableEntity.Action = ActionState.death;
            }

            if (movableEntity.BoundingBox.Bottom == movableEntity.GroundLevel) return;

            if (movableEntity.BoundingBox.Bottom > movableEntity.GroundLevel)
            {
                movableEntity.Gravity = new Vector2(0, 1);
                movableEntity.Position.Y = movableEntity.GroundLevel - movableEntity.offset.Y - movableEntity.Height * 3 + 1;
                movableEntity.Speed.Y = 0;
                movableEntity.GravityAcceleration = new Vector2(0, 0.1f);
            }
            else
            {
                if (movableEntity.Gravity.Y < movableEntity.TerminalVelocity.Y)
                {
                    movableEntity.Gravity += movableEntity.GravityAcceleration;
                }

                movableEntity.Speed += movableEntity.Gravity;


            }
        }

        public void Move()
        {
            if (movableEntity.Action != ActionState.death && movableEntity.Action != ActionState.attack)
            {

                if (movableEntity.Action != ActionState.hit)
                {
                    if (movableEntity.Speed.Y < 0)
                    {
                        movableEntity.Action = ActionState.jump;
                    }
                    else if (movableEntity.Speed.Y > 0)
                    {
                        movableEntity.Action = ActionState.fall;
                    }
                    else if (movableEntity.Speed.X != 0)
                    {
                        movableEntity.Action = ActionState.run;
                    }
                    else
                    {
                        movableEntity.Action = ActionState.idle;
                    }
                }
                movableEntity.Position += movableEntity.Speed;
                movableEntity.UpdateBoundingBox();
                if (movableEntity.BoundingBox.Right == movableEntity.WallRight) return;
                if (movableEntity.BoundingBox.Left == movableEntity.WallLeft) return;

                if (movableEntity.BoundingBox.Right > movableEntity.WallRight)
                {
                    movableEntity.Position.X = movableEntity.WallRight - movableEntity.offset.X - movableEntity.Width * 3;
                }

                if (movableEntity.BoundingBox.Left < movableEntity.WallLeft)
                {
                    movableEntity.Position.X = movableEntity.WallLeft - movableEntity.offset.X;
                }

            }
        }

        public bool Collision(Block target)
        {
            bool intersects = movableEntity.BoundingBox.Intersects(target.BoundingBox);
            if (intersects)
            {
                movableEntity.Collided = true;
            }
            return intersects;
        }

        public void CollisionWithBlock(Block target)
        {

            if (movableEntity.BoundingBox.Bottom >= target.BoundingBox.Top && movableEntity.BoundingBox.Bottom < target.BoundingBox.Top + 100)
            {
                if (movableEntity.BoundingBox.Right > target.BoundingBox.Left + 20 && movableEntity.BoundingBox.Left < target.BoundingBox.Right - 20)
                {
                    movableEntity.GroundLevel = Convert.ToInt16(target.BoundingBox.Top);
                }


            }
            else if (movableEntity.BoundingBox.Right > target.BoundingBox.Left && movableEntity.BoundingBox.Right <= target.BoundingBox.Left + 50)
            {
                if (movableEntity.BoundingBox.Bottom >= target.BoundingBox.Top + 80)
                {
                    movableEntity.WallRight = Convert.ToInt16(target.BoundingBox.Left) - 5;
                }

            }


            else if (movableEntity.BoundingBox.Left <= target.BoundingBox.Right && movableEntity.BoundingBox.Left >= target.BoundingBox.Right - 50)
            {
                if (movableEntity.BoundingBox.Bottom >= target.BoundingBox.Top + 80)
                {
                    movableEntity.WallLeft = Convert.ToInt16(target.BoundingBox.Right) + 5;

                }
            }




            else if (movableEntity.BoundingBox.Top <= target.BoundingBox.Bottom)
            {
                movableEntity.Position.Y += 3;
                movableEntity.Speed.Y = 1;

            }
        }

        
    }
}
