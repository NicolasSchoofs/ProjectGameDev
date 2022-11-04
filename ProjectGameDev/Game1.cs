using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using SamplerState = Microsoft.Xna.Framework.Graphics.SamplerState;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace ProjectGameDev
{
    public class Game1 : Game
    {
        private Texture2D _heroTextureIdle;
        private Texture2D _heroTextureRun;
        private Texture2D _heroTextureJump;
        private Texture2D _heroTextureFall;
        private Texture2D _heroTextureAttack1;
        private Texture2D _heroTextureAttack2;
        private Texture2D _heroTextureAttack3;
        private Texture2D _heroTextureAttack4;
        private Texture2D _heroTextureDeath;
        private Texture2D _heroTextureTakeHit;

        private Texture2D _slimeIdle;
        private Texture2D _tileset;

        private int blockx = 40;
        private int blocky = 21;
        private int blockWidth = 30;
        private int blockHeight = 30;

        private int[,] gameboard = new int[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
        };





        private Hero hero;
        private Slime slime;
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
            // TODO: Add your initialization logic here
            //hero
            _heroTextureIdle = Content.Load<Texture2D>("Idle");
            _heroTextureRun = Content.Load<Texture2D>("Run");
            _heroTextureJump = Content.Load<Texture2D>("Jump");
            _heroTextureFall = Content.Load<Texture2D>("Fall");
            _heroTextureAttack1 = Content.Load<Texture2D>("Attack1");
            _heroTextureAttack2 = Content.Load<Texture2D>("Attack2");
            _heroTextureAttack3 = Content.Load<Texture2D>("Attack3");
            _heroTextureAttack4 = Content.Load<Texture2D>("Attack4");
            _heroTextureDeath = Content.Load<Texture2D>("Death");
            _heroTextureTakeHit = Content.Load<Texture2D>("Take Hit");


            //slime
            _slimeIdle = Content.Load<Texture2D>("slime_idle2");


            _tileset = Content.Load<Texture2D>("tileset");




            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();


            base.Initialize();
            hero = new Hero(_heroTextureIdle, _heroTextureRun, _heroTextureJump, _heroTextureFall, _heroTextureAttack1, _heroTextureAttack2, _heroTextureAttack3, _heroTextureAttack4, _heroTextureDeath, _heroTextureTakeHit);

            slime = new Slime(_slimeIdle);


        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            hero.Update(gameTime);
            slime.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(sortMode:default,null,SamplerState.PointClamp);
            hero.Draw(_spriteBatch);
            slime.Draw(_spriteBatch);
            _spriteBatch.Draw(_tileset, new Vector2(0,0),new Rectangle(blockx, blocky, blockWidth, blockHeight), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}