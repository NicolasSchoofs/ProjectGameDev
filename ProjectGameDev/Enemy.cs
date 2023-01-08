using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ProjectGameDev
{
    internal class Enemy:Sprite, IGameObject
    {
        public Action action;
        public int health = 3;
        public void Die()
        {
            action = Action.death;
        }

        public void TakeDamage()
        {
            if (action != Action.hit && action != Action.death)
            {
                health--;
                action = Action.hit;
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
        public virtual void CollisionWithBlock(Block target)
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
           
        }

        public virtual void Reset()
        {

        }
        public virtual void CheckForHero(Hero target)
        {

        }

        public virtual bool CheckForAttackCollision(Hero target)
        {
            return false;
        }
        public virtual void AttackHero(Hero target)
        {

        }
    }
}
