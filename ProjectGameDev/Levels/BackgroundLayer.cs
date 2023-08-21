using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameDev.Levels
{
    internal class BackgroundLayer
    {
        private Texture2D _texture;
        private Rectangle _sourceRectangle;
        private Vector2 _position;
        private float _scrollSpeed;

        public BackgroundLayer(Texture2D texture, Rectangle sourceRectangle, float scrollSpeed)
        {
            _texture = texture;
            _sourceRectangle = sourceRectangle;
            _scrollSpeed = scrollSpeed;
            _position = Vector2.Zero;
        }

        public void Update(GameTime gameTime)
        {
            _position.X += _scrollSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_position.X > _texture.Width)
            {
                _position.X -= _texture.Width;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, _sourceRectangle, Color.White);
            spriteBatch.Draw(_texture, _position + new Vector2(_texture.Width, 0), _sourceRectangle, Color.White);
        }
    }
}
