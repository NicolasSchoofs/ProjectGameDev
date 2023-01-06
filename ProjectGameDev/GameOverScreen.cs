using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct2D1.Effects;

namespace ProjectGameDev
{
    internal class GameOverScreen:IGameObject
    {
        private Texture2D texture;
        private SpriteFont font;
        private Rectangle mainMenu;
        private Rectangle menuItem;
        private SpriteEffects spriteEffect = SpriteEffects.None;
        private Rectangle hitbox1, hitbox2, hitbox3;


        private int width = 280, height = 380;
        private Vector2 position;
        private Vector2 positionItem1, positionItem2, positionItem3;

        public GameOverScreen(Texture2D menuTexture, SpriteFont font)
        {
            positionItem1 = new Vector2(Game1.screenwidth / 2 - width / 2 + 40, Game1.screenHeight / 2 - height / 2 + 50);
            positionItem2 = new Vector2(Game1.screenwidth / 2 - width / 2 + 40, Game1.screenHeight / 2 - height / 2 + 150);
            positionItem3 = new Vector2(Game1.screenwidth / 2 - width / 2 + 40, Game1.screenHeight / 2 - height / 2 + 250);

            hitbox1 = new Rectangle(Convert.ToInt16(positionItem1.X), Convert.ToInt16(positionItem1.Y), 195, 80);
            hitbox2 = new Rectangle(Convert.ToInt16(positionItem2.X), Convert.ToInt16(positionItem2.Y), 195, 80);
            hitbox3 = new Rectangle(Convert.ToInt16(positionItem3.X), Convert.ToInt16(positionItem3.Y), 195, 80);
            //positionItem1 = new Vector2(0, 0);
            menuItem = new Rectangle(310, 283, 195, 80);
            //
            this.font = font;
            texture = menuTexture;
            //
            position = new Vector2(Game1.screenwidth / 2 - width / 2, Game1.screenHeight / 2 - height / 2);
            mainMenu = new Rectangle(30, 220, width, height);
            //
        }

        public void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            var mousePosition = new Point(mouseState.X, mouseState.Y);

            if (hitbox1.Contains(mousePosition))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    Game1.start = true;
                }
            }



            if (hitbox3.Contains(mousePosition))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    System.Environment.Exit(0);
                }

            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, position, mainMenu, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), spriteEffect, 0);
            spriteBatch.Draw(texture, position, mainMenu, Color.White * 0.9f, 0, new Vector2(1, 1), new Vector2(1, 1), spriteEffect, 0);
            spriteBatch.Draw(texture, positionItem1, menuItem, Color.White * 0.9f, 0, new Vector2(1, 1), new Vector2(1, 1), spriteEffect, 0);
            spriteBatch.Draw(texture, positionItem2, menuItem, Color.White * 0.9f, 0, new Vector2(1, 1), new Vector2(1, 1), spriteEffect, 0);
            spriteBatch.Draw(texture, positionItem3, menuItem, Color.White * 0.9f, 0, new Vector2(1, 1), new Vector2(1, 1), spriteEffect, 0);

            //public void DrawString(SpriteFont spriteFont, StringBuilder text, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
            spriteBatch.DrawString(font, "GAME OVER", new Vector2(positionItem1.X - 140, positionItem1.Y - 150), Color.Red, 0, Vector2.Zero, new Vector2(3,3), spriteEffect, 0);
            spriteBatch.DrawString(font, "RETRY", new Vector2(positionItem1.X + 55, positionItem1.Y + 25), Color.Black);
            spriteBatch.DrawString(font, "CREDITS", new Vector2(positionItem2.X + 35, positionItem2.Y + 25), Color.Black);
            spriteBatch.DrawString(font, "EXIT", new Vector2(positionItem3.X + 65, positionItem3.Y + 25), Color.Black);

        }
    }
}
