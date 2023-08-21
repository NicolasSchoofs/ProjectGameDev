using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        public EnemyManager enemyManager;
        public Texture2D Texture;
        public int End;
        public List<Block> Blocks;
        public Background Bg;
        public Song Song;
        public Hero hero;

        public int level;
        private ContentManager content;


        public Level(int[,] gameBoard, List<Enemy> enemies, int end, int level, ContentManager content, Hero hero)
        {
            this.End = end;
            Bg = new Background(content, level);
            Blocks = new List<Block>();
            this.GameBoard = gameBoard;
            this.level = level;
            this.content = content;
            this.hero = hero;
            CreateBlocks();
            enemyManager = new EnemyManager();
            enemyManager.AddEnemies(enemies);
        }

        private void CreateBlocks()
        {
            for (int l = 0; l < GameBoard.GetLength(0); l++)
            {
                for (int c = 0; c < GameBoard.GetLength(1); c++)
                {
                    Blocks.Add(new Block(content, GameBoard[l, c], new Vector2(75 * c, 75 * l)));
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            if (CheckLevelOver(hero))
            {
                LevelOver(hero, hero.Healthbar);
                
            }

            foreach (var block in Blocks)
            {
                if (hero.Collision(block))
                {
                    hero.CollisionWithBlock(block);
                }

            }
            foreach (var block in Blocks)
            {
                enemyManager.CollisionWithBlock(block);
            }
            enemyManager.CheckForHero(hero);

            enemyManager.Attack(hero);
        
            hero.Update(gameTime);
            enemyManager.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var block in Blocks)
            {
                block.Draw(spriteBatch);
            }

          enemyManager.Draw(spriteBatch);

        }
        public void DrawBg(SpriteBatch spriteBatch)
        {
            Bg.Draw(spriteBatch);
        }

        public void PlayMusic()
        {
            MusicPlayer.Initialize();
            MusicPlayer.PlaySong(content, level);
            MusicPlayer.Paused(true);
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
                Game1.GameState = GameState.next;
            }
            else
            {
                Game1.GameState = GameState.won;
            }

        }

        internal void ResetLevel()
        {
            enemyManager.ResetEnemies();
            hero.Reset(hero.Healthbar);
        }
    }
}
