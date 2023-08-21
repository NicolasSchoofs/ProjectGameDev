using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Entities;
using ProjectGameDev.Interfaces;

namespace ProjectGameDev
{
    internal class Block:IGameObject
    {
        public Rectangle BoundingBox { get; set; }
        public Texture2D Texture { get; set; }
    

        private int _type;

        public Vector2 Position;
        

        private Rectangle _sourceRectangleGrass = new Rectangle(444, 203, 15, 15);
        private Rectangle _sourceRectangleGround = new Rectangle(444, 219, 15, 15);
        private Rectangle _sourceRectangleCastle = new Rectangle(444, 460, 15, 15);
        private Rectangle _sourceRectangleCastleGround = new Rectangle(444, 476, 15, 15);

        private int _width = 15;
        private int _height = 15;

        public Block(ContentManager content, int typeBlock, Vector2 positionBlock)
        {
            _type = typeBlock;
            Texture = content.Load<Texture2D>("Levels/tilesMarioWorld");
            Position = positionBlock;
            if (_type != 0)
            {
                BoundingBox = new Rectangle(Convert.ToInt16(Position.X), Convert.ToInt16(Position.Y), _width * 5, _height * 5);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            switch (_type)
            {
                case 0:
                    break;
                case 1:
                    spriteBatch.Draw(Texture, Position, _sourceRectangleGrass, Color.White, 0, new Vector2(1, 1), new Vector2(5, 5), SpriteEffects.None, 0);
                    break;
                case 2:
                    spriteBatch.Draw(Texture, Position, _sourceRectangleGround, Color.White, 0, new Vector2(1, 1), new Vector2(5, 5), SpriteEffects.None, 0);
                    break;
                case 3:
                    spriteBatch.Draw(Texture, Position, _sourceRectangleCastle, Color.White, 0, new Vector2(1, 1), new Vector2(5, 5), SpriteEffects.None, 0);
                    break;
                case 4:
                    spriteBatch.Draw(Texture, Position, _sourceRectangleCastleGround, Color.White, 0, new Vector2(1, 1), new Vector2(5, 5), SpriteEffects.None, 0);
                    break;
            }
            
        }

        public void Update(GameTime gameTime)
        {
            
        }


    }
}
