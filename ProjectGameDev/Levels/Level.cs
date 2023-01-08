using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using ProjectGameDev.Enemies;
using ProjectGameDev.Entities.HeroDesign;
using ProjectGameDev.Interfaces;
using SharpDX.Direct2D1;
using SharpDX.Direct3D9;
using SamplerState = Microsoft.Xna.Framework.Graphics.SamplerState;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace ProjectGameDev.Levels
{
    internal class Level : IGameObject
    {
        public int[,] GameBoard;
        public Texture2D Texture;
        public int End;
        public List<Block> Blocks;
        public Background Bg;
        public Song Song;

        public List<Enemy> Enemies;


        public Level(Texture2D bloktexture, Texture2D bgTexture, int[,] gameBoard, int typeBg, Song song, List<Enemy> enemies, int end)
        {
            this.End = end;
            this.Enemies = enemies;
            this.Song = song;
            Bg = new Background(bgTexture, typeBg);
            Texture = bloktexture;
            Blocks = new List<Block>();
            this.GameBoard = gameBoard;


            CreateBlocks();
        }

        private void CreateBlocks()
        {
            for (int l = 0; l < GameBoard.GetLength(0); l++)
            {
                for (int c = 0; c < GameBoard.GetLength(1); c++)
                {
                    Blocks.Add(new Block(Texture, GameBoard[l, c], new Vector2(75 * c, 75 * l)));
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var enemy in Enemies)
            {
                enemy.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var block in Blocks)
            {
                block.Draw(spriteBatch);
            }

            foreach (var enemy in Enemies)
            {
                enemy.Draw(spriteBatch);
            }

        }
        public void DrawBg(SpriteBatch spriteBatch)
        {
            Bg.Draw(spriteBatch);
        }

        public void PlayMusic()
        {
            MediaPlayer.Stop();
            MediaPlayer.Play(Song);
            MediaPlayer.Volume = 0.3f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Pause();
        }

        public bool CheckLevelOver(Hero target)
        {
            if (target.Position.X >= End)
            {
                return true;
            }
            return false;
        }
        public void LevelOver(Hero target, HealthBar healthbar)
        {
            if (Game1.CurrentLevel < 2)
            {
                Game1.CurrentLevel++;
                target.Reset(healthbar);
            }
            else
            {
                Game1.GameState = GameState.won;
            }

        }




    }
}
