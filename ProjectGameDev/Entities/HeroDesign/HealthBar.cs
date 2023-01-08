using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Interfaces;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace ProjectGameDev.Entities.HeroDesign
{
    internal class HealthBar:IGameObject
    {
        private List<Heart> hearts = new List<Heart>();
        private int schijfOp = 100;
        private int Health;
        
        public HealthBar(Texture2D HeartTexture, int nHearts)
        {
            Health = nHearts * 2;
            for (int i = 0; i < nHearts; i++)
            {
                hearts.Add(new Heart(HeartTexture, new Vector2(20 + schijfOp * i,20)));
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var heart in hearts)
            {
                heart.Update(gameTime);
            }
        }

      

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var heart in hearts)
            {
                heart.Draw(spriteBatch);
            }
        }

        public bool LowerHealth()
        {
            
            for (int i = hearts.Count - 1; i >= 0; i--)
            {
                if (hearts[i].LowerHealth())
                {
                    Health--;
                    break;
                }

            }
            if (Health <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
