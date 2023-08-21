using LinqToTwitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Entities.Movement
{
    internal interface IMovable
    {
        void Move();
        void Fall();
        void Update();

        bool Collision(Block target);

        void CollisionWithBlock(Block target);
    }
}
