using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Enemies;
using ProjectGameDev.Entities.HeroDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Levels
{
    internal class EnemyManager
    {
        public List<Enemy> _enemies;

        public EnemyManager()
        {
            _enemies = new List<Enemy>();
        }

        public void AddEnemy(Enemy enemy)
        {
            _enemies.Add(enemy);
        }

        public void AddEnemies(List<Enemy> enemies)
        {
            foreach (Enemy enemy in enemies)
            {
                _enemies.Add(enemy);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var enemy in _enemies)
            {
                enemy.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var enemy in _enemies)
            {
                enemy.Draw(spriteBatch);
            }
        }

        internal void CollisionWithBlock(Block block)
        {
            foreach (var enemy in _enemies)
            {
                if (enemy.Collision(block))
                {
                    enemy.CollisionWithBlock(block);
                }
            }
        }

        internal void CheckForHero(Hero hero)
        {
            foreach (var enemy in _enemies)
            {
                enemy.CheckForHero(hero);
            }
        }

        internal void Attack(Hero hero)
        {
            foreach (var enemy in _enemies)
            {
                if (hero.CheckForAttackCollision(enemy))
                {
                    hero.Attack(enemy);
                }
                if (enemy.CheckForAttackCollision(hero))
                {
                    enemy.AttackHero(hero);
                }

            }
        }

        internal void ResetEnemies()
        {
            foreach(var enemy in _enemies)
            {
                enemy.Reset();
            }
        }
    }
}
