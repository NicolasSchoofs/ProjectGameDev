using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Entities;
using ProjectGameDev.Interfaces;

namespace ProjectGameDev
{
    internal class Background: IGameObject
    {
        private Texture2D _texture;
        private int _width = 513, _height = 430;

        private Rectangle _rect;

        private float scrollSpeed = 5.0f;
        private Vector2 _position = new Vector2(0,-300);

        private SpriteEffects _spriteEffect = SpriteEffects.None;

        public Background(ContentManager content, int level)
        {
            switch (level)
            {
                case 1:
                    _rect = new Rectangle(9, 20, _width, _height);
                    break;
                case 2:
                    _rect = new Rectangle(9 + _width * 2, 20 + _height * 5, _width, _height);
                    break;
                default:
                    break;
            }
           
            _texture = content.Load<Texture2D>("Levels/Background");

        }
        public void Update(GameTime gameTime)
        {
            _position.X += scrollSpeed;
            if (_position.X > _width * 2.5f) 
            {
                _position.X = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, _rect, Color.White, 0, new Vector2(1, 1), new Vector2(2.5f, 2.5f), _spriteEffect, 0);

        }
    }
}
