using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectGameDev
{
    internal class Background:IGameObject
    {
        //6,3
        private Texture2D texture;
        private int width = 513, height = 430;

        private int type;

        private Vector2 position = new Vector2(0,-300);

        private SpriteEffects spriteEffect = SpriteEffects.None;

        private Rectangle bgPlains, bgCastle;
        public Background(Texture2D textureBackground, int type)
        {
            this.type = type;
            bgPlains = new Rectangle(9, 20, width, height);
            bgCastle = new Rectangle(9 + width * 2, 20 + height * 5, width, height);
            texture = textureBackground;

        }
        public void Update(GameTime gameTime)
        {
            //
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (type == 1)
            {
                spriteBatch.Draw(texture, position, bgPlains, Color.White, 0, new Vector2(1, 1), new Vector2(2.5f, 2.5f), spriteEffect, 0);
            }
            else if (type == 2)
            {
                spriteBatch.Draw(texture, position, bgCastle, Color.White, 0, new Vector2(1, 1), new Vector2(2.5f, 2.5f), spriteEffect, 0);
            }
            
        }
    }
}
