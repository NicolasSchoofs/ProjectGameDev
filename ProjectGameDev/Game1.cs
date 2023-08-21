using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using SamplerState = Microsoft.Xna.Framework.Graphics.SamplerState;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.Xna.Framework.Media;
using ProjectGameDev.Levels;
using ProjectGameDev.Enemies;
using ProjectGameDev.Entities.HeroDesign;
using ProjectGameDev.Extra;

namespace ProjectGameDev
{
    public enum GameState
    {
        pauze,
        start,
        death,
        reset,
        playing,
        next,
        won
    }
    public class Game1 : Game
    {
 
            
        private LevelFactory _levelFactory = new LevelFactory();

        public static int ScreenWidth = 1280;
        public static int ScreenHeight = 720;

        public static int CurrentLevel = 1;


        public static GameState GameState = GameState.start;

        private SpriteFont _font;
        

        private Camera _camera;

        private Level _level1;
        private Level _level2;

        private List<Level> _levels = new List<Level>();


        private Hero _hero;

        private HealthBar _healthbar;

        private Menu _menu;
        private StartScreen _startScreen;
        private GameOverScreen _gameOverScreen;
        private VictoryScreen _victoryScreen;

        private Background _bg;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();


            base.Initialize();
            _healthbar = new HealthBar(Content, 3);
            _hero = new Hero(_healthbar,Content);


            _menu = new Menu(Content);
            _startScreen = new StartScreen(Content);
            _gameOverScreen = new GameOverScreen(Content);
            _victoryScreen = new VictoryScreen(Content);

            _bg = new Background(Content, 1);


            _level1 = LevelFactory.Level1(Content, _hero, GraphicsDevice);
            _level2 = LevelFactory.Level2(Content, _hero, GraphicsDevice);
       
            _levels.Add(_level1);
            _levels.Add(_level2);

            _levels[CurrentLevel - 1].PlayMusic();

        }

        protected override void LoadContent()
        {
       
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _camera = new Camera();


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    Exit();

            switch (GameState)
            {
                case GameState.death:
                    MediaPlayer.Pause();
                    _gameOverScreen.Update(gameTime);
                    break;
                case GameState.start:
                    MediaPlayer.Pause();
                    _startScreen.Update(gameTime);
                    break;
                case GameState.pauze:
                    MediaPlayer.Pause();
                    _menu.Update(gameTime);
                    break;
                case GameState.reset:
                    _hero.Reset(new HealthBar(Content, 3));
                    _level1.ResetLevel();
                    _level2.ResetLevel();
                    CurrentLevel = 1;
                    _levels[CurrentLevel - 1].PlayMusic();
                    GameState = GameState.playing;
                    break;
                case GameState.won:
                    MediaPlayer.Stop();
                    _victoryScreen.Update(gameTime);
                    break;
                case GameState.playing:
                    MediaPlayer.Resume();
                    _levels[CurrentLevel - 1].Update(gameTime);
                    _camera.Follow(_hero);
                    
                    break;
                case GameState.next:
                    _levels[CurrentLevel - 1].PlayMusic();
                    GameState = GameState.playing;
                    break;

            }
            
            _bg.Update(gameTime);
            
            

            base.Update(gameTime);
            
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(sortMode: default, null, SamplerState.PointClamp);
            _levels[CurrentLevel - 1].DrawBg(_spriteBatch);
            _spriteBatch.End();
            switch (GameState)
            {
                case GameState.death:
                    _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                    _gameOverScreen.Draw(_spriteBatch);
                    _spriteBatch.End();
                    break;
                case GameState.start:
                    _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                    _startScreen.Draw(_spriteBatch);
                    _spriteBatch.End();
                    break;
                case GameState.pauze:
                    _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                    _menu.Draw(_spriteBatch);
                    _spriteBatch.End();
                    break;
                case GameState.reset:
                    break;
                case GameState.won:
                    _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                    _victoryScreen.Draw(_spriteBatch);
                    _spriteBatch.End();
                    break;
                case GameState.playing:
                    _spriteBatch.Begin(sortMode: default, null, SamplerState.PointClamp, transformMatrix: _camera.Transform);
                    _hero.Draw(_spriteBatch);
                 
                    _levels[CurrentLevel - 1].Draw(_spriteBatch);
                    _spriteBatch.End();

                    _spriteBatch.Begin(sortMode: default, null, SamplerState.PointClamp);
                    _hero.Healthbar.Draw(_spriteBatch);
                    _spriteBatch.End();

                    base.Draw(gameTime);
                    break;
            }


        }

    }
}