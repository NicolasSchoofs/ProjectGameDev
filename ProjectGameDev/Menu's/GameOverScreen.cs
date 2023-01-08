using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct2D1.Effects;
using ProjectGameDev.Interfaces;
using ProjectGameDev.Menu_s;

namespace ProjectGameDev
{
    internal class GameOverScreen: DefaultMenu, IGameObject
    {

        public GameOverScreen(Texture2D menuTexture, SpriteFont font) : base(menuTexture,font)
        {

        }

        public void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            var mousePosition = new Point(mouseState.X, mouseState.Y);

            if (Hitbox1.Contains(mousePosition))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    Game1.GameState = GameState.reset;
                }
            }



            if (Hitbox3.Contains(mousePosition))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    System.Environment.Exit(0);
                }

            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, Position, mainMenu, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
            spriteBatch.Draw(Texture, Position, MainMenu, Color.White * 0.9f, 0, new Vector2(1, 1), new Vector2(1, 1), SpriteEffect, 0);
            spriteBatch.Draw(Texture, PositionItem1, MenuItem, Color.White * 0.9f, 0, new Vector2(1, 1), new Vector2(1, 1), SpriteEffect, 0);
            spriteBatch.Draw(Texture, PositionItem2, MenuItem, Color.White * 0.9f, 0, new Vector2(1, 1), new Vector2(1, 1), SpriteEffect, 0);
            spriteBatch.Draw(Texture, PositionItem3, MenuItem, Color.White * 0.9f, 0, new Vector2(1, 1), new Vector2(1, 1), SpriteEffect, 0);

            //public void DrawString(SpriteFont spriteFont, StringBuilder text, Vector2 Position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
            spriteBatch.DrawString(Font, "GAME OVER", new Vector2(PositionItem1.X - 140, PositionItem1.Y - 150), Color.Red, 0, Vector2.Zero, new Vector2(3,3), SpriteEffect, 0);
            spriteBatch.DrawString(Font, "RETRY", new Vector2(PositionItem1.X + 55, PositionItem1.Y + 25), Color.Black);
            spriteBatch.DrawString(Font, "CREDITS", new Vector2(PositionItem2.X + 35, PositionItem2.Y + 25), Color.Black);
            spriteBatch.DrawString(Font, "EXIT", new Vector2(PositionItem3.X + 65, PositionItem3.Y + 25), Color.Black);

        }
    }
}
