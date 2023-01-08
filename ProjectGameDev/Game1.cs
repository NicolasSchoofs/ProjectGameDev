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
        private Texture2D _bgTexture;
        private Texture2D _minotaurTexture;
        private Texture2D _skeletonTexture;

        private Song plainsSong;
        private Song CastleSong;
        public static int screenwidth = 1280;
        public static int screenHeight = 720;

        public static int currentLevel = 1;

        public static bool pauze = false;
        public static bool start = false;
        public static bool death = false;
        public static bool reset = false;

        public static GameState gameState = GameState.start;

       


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

        private int[,] level1Board = new int[,]
        {
            { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2 },
            { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2 },
            { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2 },
            { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2 },
            { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2 },
            { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2 },
            { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2 },
            { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 2, 2, 2, 2, 2, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2 },
            { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2 },
            { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
            { 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }
        };

        private int[,] level2Board = new int[,]
         {
             { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 0, 0, 3, 3, 0, 0, 3, 3, 0, 0, 3, 3, 0, 0, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 0, 0, 4, 4, 0, 0, 4, 4, 0, 0, 4, 4, 0, 0, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 4, 4, 4, 4, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 0, 0, 0, 0, 0, 0, 3, 3, 4, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 },
             { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 }
         };

        private Level level1;
        private Level level2;


        private List<Enemy> enemiesLevel1 = new List<Enemy>();
        private List<Enemy> enemiesLevel2 = new List<Enemy>();
        private List<Level>levels = new List<Level>();



        private Hero hero;
        private Slime slime1;
        private Slime slime2;
        private Slime slime3;
        private Slime slime4;
        private HealthBar healthbar;

        private Menu menu;
        private StartScreen startScreen;
        private GameOverScreen gameOverScreen;
        private VictoryScreen victoryScreen;

        private Background bg;

        private Minotaur minotaur1;
        private Minotaur minotaur2;
        private Minotaur minotaur3;

        private Skeleton skeleton1;
        private Skeleton skeleton2;
        private Skeleton skeleton3;
        private Skeleton skeleton4;
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
            _heroTextureIdle = Content.Load<Texture2D>("Hero/Idle");
            _heroTextureRun = Content.Load<Texture2D>("Hero/Run");
            _heroTextureJump = Content.Load<Texture2D>("Hero/Jump");
            _heroTextureFall = Content.Load<Texture2D>("Hero/Fall");
            _heroTextureAttack1 = Content.Load<Texture2D>("Hero/Attack1");
            _heroTextureAttack2 = Content.Load<Texture2D>("Hero/Attack2");
            _heroTextureAttack3 = Content.Load<Texture2D>("Hero/Attack3");
            _heroTextureAttack4 = Content.Load<Texture2D>("Hero/Attack4");
            _heroTextureDeath = Content.Load<Texture2D>("Hero/Death");
            _heroTextureTakeHit = Content.Load<Texture2D>("Hero/Take Hit");

            _minotaurTexture = Content.Load<Texture2D>("Minotaur");
            _skeletonTexture = Content.Load<Texture2D>("Skeleton");

            plainsSong = Content.Load<Song>("Music/Funky Forest");
            CastleSong = Content.Load<Song>("Music/Sen's Fortress");
            



            _HeartTexture = Content.Load<Texture2D>("Heart");
            _menuTexture = Content.Load<Texture2D>("Menu");
            _bgTexture = Content.Load<Texture2D>("Background");

            font = Content.Load<SpriteFont>("Font");

            blokTexture = new Texture2D(GraphicsDevice, 1, 1);
            blokTexture.SetData(new[] { Color.White });





            //slime
            _slimeIdle = Content.Load<Texture2D>("slime_idle2");

            //tiles
            _tilesetMarioWorld = Content.Load<Texture2D>("tilesMarioWorld");


     





            _graphics.PreferredBackBufferWidth = screenwidth;
            _graphics.PreferredBackBufferHeight = screenHeight;
            _graphics.ApplyChanges();


            base.Initialize();
            healthbar = new HealthBar(_HeartTexture, 5);
            hero = new Hero(_heroTextureIdle, _heroTextureRun, _heroTextureJump, _heroTextureFall, _heroTextureAttack1, _heroTextureAttack2, _heroTextureAttack3, _heroTextureAttack4, _heroTextureDeath, _heroTextureTakeHit, blokTexture, healthbar);


            menu = new Menu(_menuTexture, font);
            startScreen = new StartScreen(_menuTexture, font);
            gameOverScreen = new GameOverScreen(_menuTexture, font);
            victoryScreen = new VictoryScreen(_menuTexture, font);

            bg = new Background(_bgTexture, 1);


           

            //enemies level1

            slime1 = new Slime(_slimeIdle, blokTexture, new Vector2(900, 100));
            slime2 = new Slime(_slimeIdle, blokTexture, new Vector2(1900, 100));
            slime3 = new Slime(_slimeIdle, blokTexture, new Vector2(4000, 100));
            slime4 = new Slime(_slimeIdle, blokTexture, new Vector2(4500, 100));

            minotaur1 = new Minotaur(_minotaurTexture, blokTexture, new Vector2(3200, 100));
            

            enemiesLevel1.Add(slime1);
            enemiesLevel1.Add(slime2);
            enemiesLevel1.Add(slime3);
            enemiesLevel1.Add(slime4);
            enemiesLevel1.Add(minotaur1);

            //enemies level2

            skeleton1 = new Skeleton(_skeletonTexture, blokTexture, new Vector2(900, 100));
            skeleton2 = new Skeleton(_skeletonTexture, blokTexture, new Vector2(1100, 400));
            skeleton3 = new Skeleton(_skeletonTexture, blokTexture, new Vector2(1200, 400));
            skeleton4 = new Skeleton(_skeletonTexture, blokTexture, new Vector2(3500, 100));

            minotaur2 = new Minotaur(_minotaurTexture, blokTexture, new Vector2(3200, 100));
            minotaur3 = new Minotaur(_minotaurTexture, blokTexture, new Vector2(5000, 100));

            enemiesLevel2.Add(skeleton1);
            enemiesLevel2.Add(skeleton2);
            enemiesLevel2.Add(skeleton3);
            enemiesLevel2.Add(skeleton4);
            enemiesLevel2.Add(minotaur2);
            enemiesLevel2.Add(minotaur3);

            level1 = new Level(_tilesetMarioWorld, _bgTexture, level1Board, 1, plainsSong, enemiesLevel1, 5500);
            
            level2 = new Level(_tilesetMarioWorld, _bgTexture, level2Board, 2, CastleSong, enemiesLevel2, 5300);
            levels.Add(level1);
            levels.Add(level2);

            levels[currentLevel - 1].PlayMusic();







        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _camera = new Camera();

            // TODO: use this.Content to load your game content here

        }

        protected override void Update(GameTime gameTime)
        {
            // || Keyboard.GetState().IsKeyDown(Keys.Escape)
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    Exit();

            // TODO: Add your update logic here
            switch (gameState)
            {
                case GameState.death:
                    MediaPlayer.Pause();
                    gameOverScreen.Update(gameTime);
                    break;
                case GameState.start:
                    MediaPlayer.Pause();
                    startScreen.Update(gameTime);
                    break;
                case GameState.pauze:
                    MediaPlayer.Pause();
                    menu.Update(gameTime);
                    break;
                case GameState.reset:
                    hero.Reset(new HealthBar(_HeartTexture, 5));
                    foreach (var enemy in enemiesLevel1)
                    {
                        enemy.Reset();
                    }
                    foreach (var enemy in enemiesLevel2)
                    {
                        enemy.Reset();
                    }
                    currentLevel = 1;
                    levels[currentLevel - 1].PlayMusic();
                    gameState = GameState.playing;
                    break;
                case GameState.won:
                    MediaPlayer.Stop();
                    victoryScreen.Update(gameTime);
                    break;
                case GameState.playing:
                    MediaPlayer.Resume();
                    if (levels[currentLevel - 1].CheckLevelOver(hero))
                    {
                        levels[currentLevel - 1].LevelOver(hero, healthbar);
                        levels[currentLevel - 1].PlayMusic();
                    }

                    foreach (var block in levels[currentLevel - 1].blocks)
                    {
                        if (hero.Collision(block))
                        {
                            hero.CollisionWithBlock(block);

                        }

                    }
                    foreach (var block in levels[currentLevel - 1].blocks)
                    {
                        foreach (var enemy in levels[currentLevel - 1].enemies)
                        {
                            if (enemy.Collision(block))
                            {
                                enemy.CollisionWithBlock(block);
                            }
                        }

                    }


                    foreach (var enemy in levels[currentLevel - 1].enemies)
                    {
                        enemy.CheckForHero(hero);
                    }
                   



                    foreach (var enemy in levels[currentLevel - 1].enemies)
                    {
                        if (hero.CheckForAttackCollision(enemy))
                        {
                            hero.AttackEnemy(enemy);
                        }
                        if (hero.Collision(enemy))
                        {
                            hero.CollisionEnemy(enemy);
                        }
                        if (enemy.CheckForAttackCollision(hero))
                        {
                            enemy.AttackHero(hero);
                        }
                        if (enemy.CheckForAttackCollision(hero))
                        {
                            enemy.AttackHero(hero);
                        }

                    }

                    


                    _camera.Follow(hero);
                    hero.Update(gameTime);
                    levels[currentLevel - 1].Update(gameTime);
                    hero.healthbar.Update(gameTime);
                    break;
                
            }
            //if (!death)
            //{
            //    if (start)
            //    {
            //        if (!pauze)
            //        {
                        
            //            MediaPlayer.Resume();
            //            if (levels[currentLevel - 1].CheckLevelOver(hero))
            //            {
            //                levels[currentLevel - 1].LevelOver(hero);
            //            }
                        
            //            foreach (var block in levels[currentLevel - 1].blocks)
            //            {
            //                if (hero.Collision(block))
            //                {
            //                    hero.CollisionWithBlock(block);
                                
            //                }

            //            }
            //            foreach (var block in levels[currentLevel - 1].blocks)
            //            {
            //                foreach (var enemy in enemies)
            //                {
            //                    if (enemy.Collision(block))
            //                    {
            //                        enemy.CollisionWithBlock(block);
            //                    }
            //                }
                           
            //            }

            //            minotaur.CheckForHero(hero);
            //            skeleton.CheckForHero(hero);

                      

            //            foreach (var enemy in enemies)
            //            {
            //                if (hero.CheckForAttackCollision(enemy))
            //                {
            //                    hero.AttackEnemy(enemy);
            //                }
            //                if (hero.Collision(enemy))
            //                {
            //                    hero.CollisionEnemy(enemy);
            //                }
            //            }

            //            if (skeleton.CheckForAttackCollision(hero))
            //            {
            //                skeleton.AttackHero(hero);
            //            }
            //            if (minotaur.CheckForAttackCollision(hero))
            //            {
            //                minotaur.AttackHero(hero);
            //            }


            //            _camera.Follow(hero);
            //            hero.Update(gameTime);
            //            levels[currentLevel - 1].Update(gameTime);
            //            healthbar.Update(gameTime);
                        
            //        }
            //        else
            //        {
            //            MediaPlayer.Pause();
            //            menu.Update(gameTime);
            //        }
            //    }
            //    else
            //    {
            //        MediaPlayer.Pause();
            //        startScreen.Update(gameTime);
                    
            //    }
            //}
            //else
            //{
            //    MediaPlayer.Pause();
            //    gameOverScreen.Update(gameTime);
            //}
            bg.Update(gameTime);
            
            

            base.Update(gameTime);
            
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(sortMode: default, null, SamplerState.PointClamp);
            levels[currentLevel - 1].DrawBg(_spriteBatch);
            _spriteBatch.End();
            switch (gameState)
            {
                case GameState.death:
                    _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                    gameOverScreen.Draw(_spriteBatch);
                    _spriteBatch.End();
                    break;
                case GameState.start:
                    _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                    startScreen.Draw(_spriteBatch);
                    _spriteBatch.End();
                    break;
                case GameState.pauze:
                    _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                    menu.Draw(_spriteBatch);
                    _spriteBatch.End();
                    break;
                case GameState.reset:
                    break;
                case GameState.won:
                    _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                    victoryScreen.Draw(_spriteBatch);
                    _spriteBatch.End();
                    break;
                case GameState.playing:
                    // TODO: Add your drawing code here
                    _spriteBatch.Begin(sortMode: default, null, SamplerState.PointClamp, transformMatrix: _camera.Transform);
                    hero.Draw(_spriteBatch);
                    levels[currentLevel - 1].Draw(_spriteBatch);

                    _spriteBatch.End();

                    _spriteBatch.Begin(sortMode: default, null, SamplerState.PointClamp);
                    hero.healthbar.Draw(_spriteBatch);
                    _spriteBatch.End();

                    base.Draw(gameTime);
                    break;
            }
            //if (!death)
            //{
            //    if (start)
            //    {
            //        if (!pauze)
            //        {
                       
                        

            //            // TODO: Add your drawing code here
            //            _spriteBatch.Begin(sortMode: default, null, SamplerState.PointClamp, transformMatrix: _camera.Transform);
            //            hero.Draw(_spriteBatch);
            //            levels[currentLevel - 1].Draw(_spriteBatch);

            //            _spriteBatch.End();

            //            _spriteBatch.Begin(sortMode: default, null, SamplerState.PointClamp);
            //            healthbar.Draw(_spriteBatch);
            //            _spriteBatch.End();

            //            base.Draw(gameTime);
            //        }
            //        else
            //        {
            //            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            //            menu.Draw(_spriteBatch);
            //            _spriteBatch.End();
            //        }
            //    }
            //    else
            //    {
            //        _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            //        startScreen.Draw(_spriteBatch);
                    
            //        _spriteBatch.End();
            //    }

            //}
            //else
            //{
            //    _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            //    gameOverScreen.Draw(_spriteBatch);
            //    _spriteBatch.End();
            //}
            


        }

    }
}