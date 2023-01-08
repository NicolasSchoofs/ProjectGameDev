using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectGameDev.Animations;
using ProjectGameDev.Enemies;
using ProjectGameDev.Entities.HeroDesign;
using ProjectGameDev.Levels;
using SharpDX.XAudio2;

namespace ProjectGameDev
{
    internal class Skeleton: Enemy
    {
        private Texture2D _texture;
        private Animation _run;
        private Animation _attack;
        private Animation _hit;
        private Animation _death;


        private int _startX = 0, _startY = 140;
        private int _schuifOpX = 64, _schuifOpY = 0;

        
        private int _attackStartX = 5, _attackStartY = 10;



        public Skeleton(Texture2D skeletonTexture, Vector2 spawnLocation)
        {
            Width = 45;
            Height = 37;
            State = State.right;
            GroundLevel = 9999;
            WallRight = 9999;
            WallLeft = 0;
            

            SpriteEffect = SpriteEffects.FlipHorizontally;
            RunningSpeed =  5;

            Position = spawnLocation;
            BoundingBox = new Rectangle(Convert.ToInt16(Position.X), Convert.ToInt16(Position.Y), Width * 3, Height * 3);
            AttackHitbox = new Rectangle(Convert.ToInt16(BoundingBox.Right), Convert.ToInt16(BoundingBox.Y), 30, 50);
            Speed = new Vector2(1, 1);
            Gravity = new Vector2(0, 1);
            GravityAcceleration = new Vector2(0, 0.1f);
            TerminalVelocity = new Vector2(0, 6);

            Action = Action.run;
            _texture = skeletonTexture;



            //run
            _run = new Animation();
            _run.AddFrame(new AnimationFrame(new Rectangle(_startX, _startY, Width, Height)));
            _run.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX, _startY, Width, Height)));
            _run.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 2, _startY, Width, Height)));
            _run.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 3, _startY, Width, Height)));
            _run.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 4, _startY, Width, Height)));
            _run.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 5, _startY, Width, Height)));
            _run.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 6, _startY, Width, Height)));
            _run.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 7, _startY, Width, Height)));
            _run.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 8, _startY, Width, Height)));
            _run.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 9, _startY, Width, Height)));
            _run.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 10, _startY, Width, Height)));
            _run.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 11, _startY, Width, Height)));

            //attack
            _attack = new Animation();
            _attack.AddFrame(new AnimationFrame(new Rectangle(_startX, _attackStartY, 65, Height)));
            _attack.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX, _attackStartY, 65, Height)));
            _attack.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 2, _attackStartY, 65, Height)));
            _attack.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 3, _attackStartY, 65, Height)));
            _attack.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 4, _attackStartY, 65, Height)));
            _attack.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 5, _attackStartY, 65, Height)));
            _attack.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 6, _attackStartY, 65, Height)));
            _attack.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 7, _attackStartY, 65, Height)));
            _attack.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 8, _attackStartY, 65, Height)));

            //hit
            _hit = new Animation();
            _hit.AddFrame(new AnimationFrame(new Rectangle(_startX, 268, 65, Height)));
            _hit.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX, 268, 65, Height)));
            _hit.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 2, 268, 65, Height)));

            //death
            _death = new Animation();
            _death.AddFrame(new AnimationFrame(new Rectangle(_startX, 75, 70, Height)));
            _death.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX, 75, 70, Height)));
            _death.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 2, 75, 70, Height)));
            _death.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 3, 75, 70, Height)));
            _death.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 4, 75, 70, Height)));
            _death.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 5, 75, 70, Height)));
            _death.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 6, 75, 70, Height)));
            _death.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 7, 75, 70, Height)));
            _death.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 8, 75, 70, Height)));
            _death.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 9, 75, 70, Height)));
            _death.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 10, 75, 70, Height)));
            _death.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 11, 75, 70, Height)));
        }
        public override void Update(GameTime gameTime)
        {

            if (!Collided)
            {
                GroundLevel = 9999;
                WallRight = 9999;
                WallLeft = 0;
            }
            
            Collided = false;

            switch (Action)
            {
                case Action.run:
                    Fall();
                    Move();
                    UpdateBoundingBox();
                    _run.Update(gameTime);
                    break;
                case Action.attack:
                    _attack.Update(gameTime);
                    break;
                case Action.death:
                    if (!(_death.NLoops >= 1))
                    {
                        _death.Update(gameTime);
                    }
                    break;
                case Action.hit:
                    if (!(_hit.NLoops >= 6))
                    {
                        _hit.Update(gameTime);
                    }
                    else
                    {
                        _hit.NLoops = 0;
                        if (Health > 0)
                        {
                            Action = Action.run;
                        }
                        else
                        {
                            Die();
                        }
                        
                    }
                    break;
            }
        }


   
        public override void UpdateBoundingBox()
        {

            base.UpdateBoundingBox();

            if (State == State.left)
            {
                AttackHitbox = new Rectangle(BoundingBox.Left - 50, BoundingBox.Y, 130, 130);
            }
            else
            {
                AttackHitbox = new Rectangle(BoundingBox.Right - 60, BoundingBox.Y, 130, 130);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (Action)
            {
                case Action.run:
                    spriteBatch.Draw(_texture, Position, _run.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    break;
                case Action.attack:
                    if (State == State.left)
                    {
                        spriteBatch.Draw(_texture, new Vector2(Position.X - 40, Position.Y), _attack.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(_texture, Position, _attack.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    }
                    break;
                case Action.death:
                    if (State == State.left)
                    {
                        spriteBatch.Draw(_texture, new Vector2(Position.X - 50, Position.Y), _death.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(_texture, Position, _death.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    }
                    break;
                case Action.hit:
                    if (State == State.left)
                    {
                        spriteBatch.Draw(_texture, new Vector2(Position.X - 50, Position.Y), _hit.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    }
                    else
                    {
                        spriteBatch.Draw(_texture, Position, _hit.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    }
                    
                    break;
            }
        }



        public override void AttackHero(Hero target)
        {
            if (Action == Action.attack)
            {
                base.AttackHero(target);
            }

        }

    }
}
