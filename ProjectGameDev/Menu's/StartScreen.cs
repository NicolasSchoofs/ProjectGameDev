﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectGameDev.Interfaces;
using ProjectGameDev.Menu_s;
using Microsoft.Xna.Framework.Content;

namespace ProjectGameDev
{
    internal class StartScreen: DefaultMenu,  IGameObject
    {

        public StartScreen(ContentManager content): base(content)
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
                    Game1.GameState = GameState.playing;
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
            spriteBatch.Draw(Texture, Position, MainMenu, Color.White * 0.9f, 0, new Vector2(1, 1), new Vector2(1, 1), SpriteEffect, 0);
            spriteBatch.Draw(Texture, PositionItem1, MenuItem, Color.White * 0.9f, 0, new Vector2(1, 1), new Vector2(1, 1), SpriteEffect, 0);
            spriteBatch.Draw(Texture, PositionItem3, MenuItem, Color.White * 0.9f, 0, new Vector2(1, 1), new Vector2(1, 1), SpriteEffect, 0);

            spriteBatch.DrawString(Font, "START", new Vector2(PositionItem1.X + 55, PositionItem1.Y + 25), Color.Black);
            spriteBatch.DrawString(Font, "EXIT", new Vector2(PositionItem3.X + 65, PositionItem3.Y + 25), Color.Black);

        }
    }
}
