using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using SamplerState = Microsoft.Xna.Framework.Graphics.SamplerState;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using System;
using System.Collections.Generic;
using System.Net.Mime;


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
        private Texture2D _HeartTexture;
        private Texture2D _menuTexture;

        public static int screenwidth = 1280;
        public static int screenHeight = 720;

        public static bool pauze = false;


        private SpriteFont font;
        private int score = 0;

        private Camera _camera;

        private Texture2D blokTexture;

        private Texture2D _slimeIdle;
        private Texture2D _tilesetMarioWorld;

        private int blockx = 40;
        private int blocky = 21;
        private int blockWidth = 30;
        private int blockHeight = 30;

        private int[,] gameboard = new int[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 0, 0, 1, 1, 1, 1, 1, 1 },
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 2, 2, 2, 2, 2, 2 },
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 2, 2, 2, 2, 2, 2 },
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 2, 2, 2, 2, 2, 2 },
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 2, 2, 2, 2, 2, 2 },
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 2, 2, 2, 2, 2, 2 },
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 2, 2, 2, 2, 2, 2 },
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0, 2, 2, 2, 2, 2, 2 }
        };

        private List<Block> blocks = new List<Block>();

        



        private Hero hero;
        private Slime slime;
        private HealthBar healthbar;
        private Menu menu;
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
            _HeartTexture = Content.Load<Texture2D>("Heart");
            _menuTexture = Content.Load<Texture2D>("Menu");

            font = Content.Load<SpriteFont>("Font");

            blokTexture = new Texture2D(GraphicsDevice, 1, 1);
            blokTexture.SetData(new[] { Color.White });

            //camera = new Camera(new Viewport(0, 0, 720, 1280));




            //slime
            _slimeIdle = Content.Load<Texture2D>("slime_idle2");

            //tiles
            _tilesetMarioWorld = Content.Load<Texture2D>("tilesMarioWorld");

            //source rectangles mario world 
            Rectangle sourceRectangleGrass = new Rectangle(444, 203, 15, 15);
            Rectangle sourceRectangleGround = new Rectangle(444, 219, 15, 15);

            //level





            _graphics.PreferredBackBufferWidth = screenwidth;
            _graphics.PreferredBackBufferHeight = screenHeight;
            _graphics.ApplyChanges();


            base.Initialize();
            healthbar = new HealthBar(_HeartTexture, 5);
            hero = new Hero(_heroTextureIdle, _heroTextureRun, _heroTextureJump, _heroTextureFall, _heroTextureAttack1, _heroTextureAttack2, _heroTextureAttack3, _heroTextureAttack4, _heroTextureDeath, _heroTextureTakeHit, blokTexture, healthbar);

            slime = new Slime(_slimeIdle, blokTexture);

            menu = new Menu(_menuTexture, font);

            

            

            void CreateBlocks()
            {
                for (int l = 0; l < gameboard.GetLength(0); l++)
                {
                    for (int c = 0; c < gameboard.GetLength(1); c++)
                    {
                        if (gameboard[l,c] == 1)
                        {
                            blocks.Add(new Block(_tilesetMarioWorld, sourceRectangleGrass, new Vector2(75*c,75*l),  blokTexture));
                        }

                        if (gameboard[l, c] == 2)
                        {
                            blocks.Add(new Block(_tilesetMarioWorld, sourceRectangleGround, new Vector2(75 * c, 75 * l), blokTexture));
                        }


                    }
                }
            }
            CreateBlocks();


        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _camera = new Camera();

            // TODO: use this.Content to load your game content here

        }

        protected override void Update(GameTime gameTime)
        {
           
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

            // TODO: Add your update logic here
            if (!pauze)
            {
                foreach (var block in blocks)
                {
                    if (hero.Collision(block))
                    {
                        hero.CollisionWithBlock(block);
                        break;
                    }

                }

                foreach (var block in blocks)
                {
                    if (slime.Collision(block))
                    {
                        slime.CollisionWithBlock(block);
                        break;
                    }
                }

                if (hero.Collision(slime))
                {
                    hero.CollisionWithSlime(slime);
                }
                _camera.Follow(hero);
                hero.Update(gameTime);

                slime.Update(gameTime);
                healthbar.Update(gameTime);
                //camera.UpdateCamera(new Viewport(0,0, 1000,1000));
            }

            base.Update(gameTime);
            
            
        }

        protected override void Draw(GameTime gameTime)
        {
            if (!pauze)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);

                // TODO: Add your drawing code here
                _spriteBatch.Begin(sortMode: default, null, SamplerState.PointClamp, transformMatrix: _camera.Transform);
                hero.Draw(_spriteBatch);
                slime.Draw(_spriteBatch);
                
                _spriteBatch.DrawString(font, "YO", new Vector2(500,500), Color.Red);

                foreach (var block in blocks)
                {
                    block.Draw(_spriteBatch);
                }
                //menu.Draw(_spriteBatch);
                _spriteBatch.End();

                _spriteBatch.Begin(sortMode: default, null, SamplerState.PointClamp);
                healthbar.Draw(_spriteBatch);
                _spriteBatch.End();

                base.Draw(gameTime);
            }
            else
            {
                _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                menu.Draw(_spriteBatch);
                _spriteBatch.End();
            }
            
        }

    }
}