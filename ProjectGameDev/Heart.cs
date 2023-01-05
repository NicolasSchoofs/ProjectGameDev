using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace ProjectGameDev
{
    enum HeartState
    {
        full,
        half,
        empty
    }
    internal class Heart:Sprite, IGameObject
    {
        private HeartState state;

        private Texture2D texture;

        private int width= 32, height=28;
        private int schijfOp = 31;

        private SpriteEffects spriteEffect = SpriteEffects.None;

        private Vector2 position;

        private Rectangle full;
        private Rectangle half;
        private Rectangle empty;
         
        public Heart(Texture2D texture, Vector2 positie)
        {
            this.texture = texture;
            position = positie;

            full = new Rectangle(0, 0, width, height);
            half = new Rectangle(schijfOp, 0, width, height);
            empty = new Rectangle(schijfOp * 2, 0, width, height);


            state = HeartState.full;
        }
        public void Update(GameTime gameTime)
        {
            
        }

      


        public void Draw(SpriteBatch spriteBatch)
        {
            switch (state)
            {
                case HeartState.full:
                    spriteBatch.Draw(texture, position, full, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), spriteEffect, 0);
                    break;
                case HeartState.half:
                    spriteBatch.Draw(texture, position, half, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), spriteEffect, 0);
                    break;
                case HeartState.empty:
                    spriteBatch.Draw(texture, position, empty, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), spriteEffect, 0);
                    break;
            }
        }

        public bool LowerHealth()
        {
            switch (state)
            {
                case HeartState.full:
                    state = HeartState.half;
                    return true;
                case HeartState.half:
                    state = HeartState.empty;
                    return true;
                    
                case HeartState.empty:
                    return false;
                    
            }

            return false;


        }
    }
}
