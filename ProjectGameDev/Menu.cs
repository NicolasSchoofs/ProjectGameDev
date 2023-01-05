using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace ProjectGameDev
{
    internal class Menu
    {
        private Texture2D texture;
        private SpriteFont font;
        private Rectangle mainMenu;
        private Rectangle menuItem;
        private SpriteEffects spriteEffect = SpriteEffects.None;

        private int width = 280, height = 380;
        private Vector2 position;
        public Menu(Texture2D menuTexture, SpriteFont font)
        {
            position = new Vector2(Game1.screenwidth / 2 - width/2, Game1.screenHeight/2 - height/2);
            texture = menuTexture;
            this.font = font;

            mainMenu = new Rectangle(30, 220, width, height);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, position, mainMenu, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), spriteEffect, 0);
            spriteBatch.Draw(texture, position,mainMenu, Color.White * 0.9f, 0, new Vector2(1,1), new Vector2(1,1), spriteEffect, 0);
        }
    }
}
