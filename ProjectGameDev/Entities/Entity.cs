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
using ProjectGameDev.Entities.Movement;
using SharpDX.MediaFoundation;
using ProjectGameDev.Entities.Combat;

namespace ProjectGameDev.Entities
{
    internal class Entity
    {
        public ActionState Action;
        public int Health = 3;
        public int Height, Width;
        public SpriteEffects SpriteEffect;
        public Vector2 Position;
        public Rectangle BoundingBox;
        public bool Collided;

        private CombatManager _combatManager;

        public MovementManager movementManager;

        public Rectangle AttackHitbox;

        public int GroundLevel, WallLeft, WallRight;
        public Vector2 Speed;

        public int RunningSpeed;

        public Vector2 Gravity;
        public Vector2 GravityAcceleration;
        public Vector2 TerminalVelocity;

        public Vector2 offset;
        public AnimationManager _animationManager;
        public Entity()
        {
            movementManager = new MovementManager(this);
            _combatManager = new CombatManager();
            _animationManager = new AnimationManager();
        }

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
            movementManager.CollisionWithBlock(target);
        }

        public virtual void TakeDamage()
        {
            if (Action != ActionState.hit && Action != ActionState.death)
            {
                Health--;
                Action = ActionState.hit;
            }
        }

        public virtual bool CheckForAttackCollision(Entity target)
        {
            return _combatManager.CheckForAttackCollision(this, target);
        }

        public virtual void Die()
        {
            Action = ActionState.death;
        }

        public virtual void Fall()
        {
            movementManager.Fall();
        }

        public virtual void UpdateBoundingBox()
        {
            BoundingBox = new Rectangle(Convert.ToInt16(Position.X) + Convert.ToInt16(offset.X), Convert.ToInt16(Position.Y) + Convert.ToInt16(offset.Y), Width * 3, Height * 3);
        }

        public virtual void Update(GameTime gameTime)
        {
            movementManager.Update();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_animationManager.currentAnimation.texture, Position, _animationManager.currentAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
        }

        public virtual void Move()
        {
            movementManager.Move();
        }

        public virtual void Attack(Entity target)
        {
            _combatManager.Attack(this, target);
        }


    }
}
