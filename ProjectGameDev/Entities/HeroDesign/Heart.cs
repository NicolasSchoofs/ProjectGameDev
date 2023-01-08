using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Interfaces;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace ProjectGameDev.Entities.HeroDesign
{
    enum HeartState
    {
        full,
        half,
        empty
    }
    internal class Heart : IGameObject
    {
        private HeartState State;

        private Texture2D texture;

        private int Width = 32, Height = 28;
        private int schijfOp = 31;

        private SpriteEffects SpriteEffect = SpriteEffects.None;

        private Vector2 Position;

        private Rectangle full;
        private Rectangle half;
        private Rectangle empty;

        public Heart(Texture2D texture, Vector2 positie)
        {
            this.texture = texture;
            Position = positie;

            full = new Rectangle(0, 0, Width, Height);
            half = new Rectangle(schijfOp, 0, Width, Height);
            empty = new Rectangle(schijfOp * 2, 0, Width, Height);


            State = HeartState.full;
        }
        public void Update(GameTime gameTime)
        {

        }




        public void Draw(SpriteBatch spriteBatch)
        {
            switch (State)
            {
                case HeartState.full:
                    spriteBatch.Draw(texture, Position, full, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    break;
                case HeartState.half:
                    spriteBatch.Draw(texture, Position, half, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    break;
                case HeartState.empty:
                    spriteBatch.Draw(texture, Position, empty, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    break;
            }
        }

        public bool LowerHealth()
        {
            switch (State)
            {
                case HeartState.full:
                    State = HeartState.half;
                    return true;
                case HeartState.half:
                    State = HeartState.empty;
                    return true;

                case HeartState.empty:
                    return false;

            }

            return false;


        }
    }
}
