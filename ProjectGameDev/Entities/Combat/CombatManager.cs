using Microsoft.Xna.Framework;
using ProjectGameDev.Enemies;
using ProjectGameDev.Entities.Animation;
using ProjectGameDev.Entities.HeroDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Entities.Combat
{
    internal class CombatManager
    {
        public bool CheckForAttackCollision(Entity attacker, Entity target)
        {
            if (attacker.Action == ActionState.death || attacker.Action == ActionState.hit)
            {
                return false;
            }

            Rectangle attackHitbox = attacker.AttackHitbox;

            bool intersects = attackHitbox.Intersects(target.BoundingBox);

            if (intersects)
            {
                attacker.Collided = true;
            }

            return intersects;
        }

        public void Attack(Entity attacker, Entity target)
        {
            target.TakeDamage();
        }


    }
}
