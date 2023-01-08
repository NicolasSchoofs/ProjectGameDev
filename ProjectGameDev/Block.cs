using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectGameDev
{
    internal class Block:Sprite, IGameObject
    {
        public Rectangle boundingBox { get; set; }
        public Texture2D blokTexture;
        public bool Passable { get; set; }
        public Color Color { get; set; }
        public Texture2D Texture { get; set; }
        public Rectangle SourceRectangle { get; set; }

        public Vector2 Position;
        //public CollideWithEvent CollideWithEvent { get; set; }

        public Block(Texture2D texture, Rectangle sourceRectangle, Vector2 position)
        {
            //this.blokTexture = blokTexture;
            Texture = texture;
            SourceRectangle = sourceRectangle;
            Position = position;
            boundingBox = new Rectangle(Convert.ToInt16(position.X),Convert.ToInt16(position.Y) , sourceRectangle.Width * 5, sourceRectangle.Height * 5);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(blokTexture, boundingBox, Color.Red);
            spriteBatch.Draw(Texture, Position, SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(5, 5), SpriteEffects.None, 0);
            //spriteBatch.Draw(textureIdle, positie, idle.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), spriteEffect, 0);
            
        }

        public void Update(GameTime gameTime)
        {
            
        }


    }
}
