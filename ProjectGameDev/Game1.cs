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
        won
    }
    public class Game1 : Game
    {
        //private Texture2D _heroTextureIdle;
        //private Texture2D _heroTextureRun;
        //private Texture2D _heroTextureJump;
        //private Texture2D _heroTextureFall;
        //private Texture2D _heroTextureAttack1;
        //private Texture2D _heroTextureAttack2;
        //private Texture2D _heroTextureAttack3;
        //private Texture2D _heroTextureAttack4;
        //private Texture2D _heroTextureDeath;
        //private Texture2D _heroTextureTakeHit;
        private Texture2D _heartTexture;
        private Texture2D _menuTexture;
        private Texture2D _bgTexture;
        private Texture2D _minotaurTexture;
        private Texture2D _skeletonTexture;

        private Song _plainsSong;
        private Song _castleSong;
        public static int ScreenWidth = 1280;
        public static int ScreenHeight = 720;

        public static int CurrentLevel = 1;


        public static GameState GameState = GameState.start;

       


        private SpriteFont _font;
        

        private Camera _camera;



        private Texture2D _slimeIdle;
        private Texture2D _tilesetMarioWorld;

       

        private int[,] _level1Board = new int[,]
        {
            { 2,2,2,2,2,2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2 },
            { 2,2,2,2,2,2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2 },
            { 2,2,2,2,2,2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2 },
            { 2,2,2,2,2,2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2 },
            { 2,2,2,2,2,2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2 },
            { 2,2,2,2,2,2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2 },
            { 2,2,2,2,2,2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2 },
            { 2,2,2,2,2,2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 2, 2, 2, 2, 2, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2 },
            { 2,2,2,2,2,2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2 },
            { 2,2,2,2,2,2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
            { 2,2,2,2,2,2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
            { 2,2,2,2,2,2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
            { 2,2,2,2,2,2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
            { 2,2,2,2,2,2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
            { 2,2,2,2,2,2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
            { 2,2,2,2,2,2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }
        };

        private int[,] _level2Board = new int[,]
         {
             { 4,4,4,4,4,4,4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4,4,4,4,4,4,4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4,4,4,4,4,4,4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4,4,4,4,4,4,4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4,4,4,4,4,4,4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 0, 0, 3, 3, 0, 0, 3, 3, 0, 0, 3, 3, 0, 0, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4,4,4,4,4,4,4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 0, 0, 4, 4, 0, 0, 4, 4, 0, 0, 4, 4, 0, 0, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4,4,4,4,4,4,4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4,4,4,4,4,4,4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4,4,4,4,4,4,4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 4, 4, 4, 4, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 0, 0, 0, 0, 0, 0, 3, 3, 4, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4,4,4,4,4,4,4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4,4,4,4,4,4,4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4,4,4,4,4,4,4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4,4,4,4,4,4,4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4,4,4,4,4,4,4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4,4,4,4,4,4,4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4,4,4,4,4,4,4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 }
         };

        private Level _level1;
        private Level _level2;


        private List<Enemy> _enemiesLevel1 = new List<Enemy>();
        private List<Enemy> _enemiesLevel2 = new List<Enemy>();
        private List<Level>_levels = new List<Level>();



        private Hero _hero;
        private Slime _slime1;
        private Slime _slime2;
        private Slime _slime3;
        private Slime _slime4;
        private HealthBar _healthbar;

        private Menu _menu;
        private StartScreen _startScreen;
        private GameOverScreen _gameOverScreen;
        private VictoryScreen _victoryScreen;

        private Background _bg;

        private Minotaur _minotaur1;
        private Minotaur _minotaur2;
        private Minotaur _minotaur3;

        private Skeleton _skeleton1;
        private Skeleton _skeleton2;
        private Skeleton _skeleton3;
        private Skeleton _skeleton4;
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


            _plainsSong = Content.Load<Song>("Music/Funky Forest");
            _castleSong = Content.Load<Song>("Music/Sen's Fortress");
            

            _heartTexture = Content.Load<Texture2D>("Hero/Heart");
            _menuTexture = Content.Load<Texture2D>("Menu/Menu");
            _bgTexture = Content.Load<Texture2D>("Levels/Background");

            _font = Content.Load<SpriteFont>("Font");

         




            //slime
            _slimeIdle = Content.Load<Texture2D>("Enemies/slime_idle2");

            //tiles
            _tilesetMarioWorld = Content.Load<Texture2D>("Levels/tilesMarioWorld");


            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();


            base.Initialize();
            _healthbar = new HealthBar(_heartTexture, 3);
            _hero = new Hero(_healthbar,Content);


            _menu = new Menu(_menuTexture, _font);
            _startScreen = new StartScreen(_menuTexture, _font);
            _gameOverScreen = new GameOverScreen(_menuTexture, _font);
            _victoryScreen = new VictoryScreen(_menuTexture, _font);

            _bg = new Background(_bgTexture, 1);
             

           

            //enemies level1

            _slime1 = new Slime(new Vector2(1500, 100), Content);
            _slime2 = new Slime(new Vector2(2500, 100), Content);
            _slime3 = new Slime(new Vector2(5000, 100), Content);
            _slime4 = new Slime(new Vector2(5500, 100), Content);

            _minotaur1 = new Minotaur(new Vector2(3200, 100), Content);
            

            _enemiesLevel1.Add(_slime1);
            _enemiesLevel1.Add(_slime2);
            _enemiesLevel1.Add(_slime3);
            _enemiesLevel1.Add(_slime4);
            _enemiesLevel1.Add(_minotaur1);

            //enemies level2

            _skeleton1 = new Skeleton(new Vector2(1300, 100), Content);
            _skeleton2 = new Skeleton(new Vector2(1800, 400), Content);
            _skeleton3 = new Skeleton(new Vector2(1900, 400), Content);
            _skeleton4 = new Skeleton(new Vector2(3500, 100), Content);

            _minotaur2 = new Minotaur(new Vector2(3200, 100), Content);
            _minotaur3 = new Minotaur(new Vector2(5000, 100), Content);

            _enemiesLevel2.Add(_skeleton1);
            _enemiesLevel2.Add(_skeleton2);
            _enemiesLevel2.Add(_skeleton3);
            _enemiesLevel2.Add(_skeleton4);
            _enemiesLevel2.Add(_minotaur2);
            _enemiesLevel2.Add(_minotaur3);

            _level1 = new Level(_tilesetMarioWorld, _bgTexture, _level1Board, 1, _plainsSong, _enemiesLevel1, 5900);
            
            _level2 = new Level(_tilesetMarioWorld, _bgTexture, _level2Board, 2, _castleSong, _enemiesLevel2, 5700);
            _levels.Add(_level1);
            _levels.Add(_level2);

            _levels[CurrentLevel - 1].PlayMusic();







        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _camera = new Camera();

            // TODO: use this.Content to load your game content here

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    Exit();

            // TODO: Add your update logic here
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
                    _hero.Reset(new HealthBar(_heartTexture, 3));
                    foreach (var enemy in _enemiesLevel1)
                    {
                        enemy.Reset();
                    }
                    foreach (var enemy in _enemiesLevel2)
                    {
                        enemy.Reset();
                    }
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
                    if (_levels[CurrentLevel - 1].CheckLevelOver(_hero))
                    {
                        _levels[CurrentLevel - 1].LevelOver(_hero, _healthbar);
                        _levels[CurrentLevel - 1].PlayMusic();
                    }

                    foreach (var block in _levels[CurrentLevel - 1].Blocks)
                    {
                        if (_hero.Collision(block))
                        {
                            _hero.CollisionWithBlock(block);

                        }

                    }
                    foreach (var block in _levels[CurrentLevel - 1].Blocks)
                    {
                        foreach (var enemy in _levels[CurrentLevel - 1].Enemies)
                        {
                            if (enemy.Collision(block))
                            {
                                enemy.CollisionWithBlock(block);
                            }
                        }

                    }


                    foreach (var enemy in _levels[CurrentLevel - 1].Enemies)
                    {
                        enemy.CheckForHero(_hero);
                    }
                   



                    foreach (var enemy in _levels[CurrentLevel - 1].Enemies)
                    {
                        if (_hero.CheckForAttackCollision(enemy))
                        {
                            _hero.AttackEnemy(enemy);
                        }
                        if (enemy.CheckForAttackCollision(_hero))
                        {
                            enemy.AttackHero(_hero);
                        }

                    }

                    


                    _camera.Follow(_hero);
                    _hero.Update(gameTime);
                    _levels[CurrentLevel - 1].Update(gameTime);
                    _hero.Healthbar.Update(gameTime);
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
                    // TODO: Add your drawing code here
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