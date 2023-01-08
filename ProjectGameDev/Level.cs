using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SharpDX.Direct2D1;
using SharpDX.Direct3D9;
using SamplerState = Microsoft.Xna.Framework.Graphics.SamplerState;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace ProjectGameDev
{
    internal class Level: IGameObject
    {
        public int[,] gameBoard;
        public Texture2D texture;
        public int end;
        public List<Block> blocks;
        public Background bg;
        public Song song;

        public List<Enemy> enemies;

        Rectangle sourceRectangleGrass = new Rectangle(444, 203, 15, 15);
        Rectangle sourceRectangleGround = new Rectangle(444, 219, 15, 15);
        Rectangle sourceRectangleCastle = new Rectangle(444, 460, 15, 15);
        Rectangle sourceRectangleCastleGround = new Rectangle(444, 476, 15, 15);

        public Level(Texture2D bloktexture, Texture2D bgTexture, int[,] gameBoard, int typeBg, Song song, List<Enemy> enemies, int end)
        {
            this.end = end;
            this.enemies = enemies;
            this.song = song;
            bg = new Background(bgTexture, typeBg);
            texture = bloktexture;
            blocks = new List<Block>();
            this.gameBoard = gameBoard;
            

            CreateBlocks();
        }

        private void CreateBlocks()
        {
            for (int l = 0; l < gameBoard.GetLength(0); l++)
            {
                for (int c = 0; c < gameBoard.GetLength(1); c++)
                {
                    if (gameBoard[l, c] == 1)
                    {
                        blocks.Add(new Block(texture, sourceRectangleGrass, new Vector2(75 * c, 75 * l)));
                    }

                    if (gameBoard[l, c] == 2)
                    {
                        blocks.Add(new Block(texture, sourceRectangleGround, new Vector2(75 * c, 75 * l)));
                    }
                    if (gameBoard[l, c] == 3)
                    {
                        blocks.Add(new Block(texture, sourceRectangleCastle, new Vector2(75 * c, 75 * l)));
                    }
                    if (gameBoard[l, c] == 4)
                    {
                        blocks.Add(new Block(texture, sourceRectangleCastleGround, new Vector2(75 * c, 75 * l)));
                    }
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var enemy in enemies)
            {
                enemy.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var block in blocks)
            {
                block.Draw(spriteBatch);
            }

            foreach (var enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }

        }
        public void DrawBg(SpriteBatch spriteBatch)
        {
            bg.Draw(spriteBatch);
        }

        public void PlayMusic()
        {
            MediaPlayer.Stop();
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Pause();
        }

        public bool CheckLevelOver(Hero target)
        {
            if (target.positie.X >= end)
            {
                return true;
            }
            return false;
        }
        public void LevelOver(Hero target, HealthBar healthbar)
        {
            if (Game1.currentLevel <  2)
            {
                Game1.currentLevel++;
                target.Reset(healthbar);
            }
            else
            {
                Game1.gameState = GameState.won;
            }
            
        }

      

     
    }
}
