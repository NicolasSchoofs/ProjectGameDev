using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Entities;
using ProjectGameDev.Interfaces;

namespace ProjectGameDev
{
    internal class Background: IGameObject
    {
        private Texture2D _texture;
        private int _width = 513, _height = 430;

        private int _type;

        private Vector2 _position = new Vector2(0,-300);

        private SpriteEffects _spriteEffect = SpriteEffects.None;

        private Rectangle _bgPlains, _bgCastle;
        public Background(Texture2D textureBackground, int type)
        {
            this._type = type;
            _bgPlains = new Rectangle(9, 20, _width, _height);
            _bgCastle = new Rectangle(9 + _width * 2, 20 + _height * 5, _width, _height);
            _texture = textureBackground;

        }
        public void Update(GameTime gameTime)
        {
            //
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_type == 1)
            {
                spriteBatch.Draw(_texture, _position, _bgPlains, Color.White, 0, new Vector2(1, 1), new Vector2(2.5f, 2.5f), _spriteEffect, 0);
            }
            else if (_type == 2)
            {
                spriteBatch.Draw(_texture, _position, _bgCastle, Color.White, 0, new Vector2(1, 1), new Vector2(2.5f, 2.5f), _spriteEffect, 0);
            }
            
        }
    }
}
