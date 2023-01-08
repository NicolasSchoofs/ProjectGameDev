using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectGameDev.Animations;
using ProjectGameDev.Enemies;
using ProjectGameDev.Entities.HeroDesign;
using ProjectGameDev.Levels;
using SharpDX;
using SharpDX.Direct3D9;
using SharpDX.MediaFoundation;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ProjectGameDev
{

    internal class Minotaur: Enemy
    {
        private Texture2D _texture;
        private Animation _run;
        private Animation _attack;
        private Animation _hit;
        private Animation _death;


        private int _startX = 25, _startY = 115;
        private int _schuifOpX = 96, _schuifOpY = 0;

        private Texture2D _blokTexture2D;
        private int _attackStartX = 5, _attackStartY = 290;
        private int _hitStartX = 5, _hitStartY = 788;
        private int _deathStartX = 5, _deathStartY = 885;


        public Minotaur(Texture2D minotaurTexture, Texture2D blokTexture, Vector2 spawnLocation)
        {
            Width = 55;
            Height = 45;
            State = State.right;
            GroundLevel = 9999;
            WallRight = 9999;
            WallLeft = 0;
            AttackHitbox = new Rectangle(Convert.ToInt16(BoundingBox.Right), Convert.ToInt16(BoundingBox.Y), 30, 50);
            Position = spawnLocation;
            Speed = new Vector2(1, 1);
            SpriteEffect = SpriteEffects.None;
            RunningSpeed = 10;




            _blokTexture2D = blokTexture;
            BoundingBox = new Rectangle(Convert.ToInt16(Position.X), Convert.ToInt16(Position.Y), Width * 3, Height * 3);
            

            RunningSpeed = 10;

            
            
            Gravity = new Vector2(0, 1);
            GravityAcceleration = new Vector2(0, 0.1f);
            TerminalVelocity = new Vector2(0, 6);

            Action = Action.run;
            _texture = minotaurTexture;



            //Run
            _run = new Animation();
            _run.AddFrame(new AnimationFrame(new Rectangle(_startX, _startY, Width, Height)));
            _run.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX, _startY, Width, Height)));
            _run.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 2, _startY, Width, Height)));
            _run.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 3, _startY, Width, Height)));
            _run.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 4, _startY, Width, Height)));
            _run.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 5, _startY, Width, Height)));
            _run.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 6, _startY, Width, Height)));
            _run.AddFrame(new AnimationFrame(new Rectangle(_startX + _schuifOpX * 7, _startY, Width, Height)));

            //attack
            _attack = new Animation();
            _attack.AddFrame(new AnimationFrame(new Rectangle(_attackStartX, _attackStartY, 90, 70)));
            _attack.AddFrame(new AnimationFrame(new Rectangle(_attackStartX + _schuifOpX, _attackStartY, 90, 70)));
            _attack.AddFrame(new AnimationFrame(new Rectangle(_attackStartX + _schuifOpX * 2, _attackStartY, 90, 70)));
            _attack.AddFrame(new AnimationFrame(new Rectangle(_attackStartX + _schuifOpX * 3, _attackStartY, 90, 70)));
            _attack.AddFrame(new AnimationFrame(new Rectangle(_attackStartX + _schuifOpX * 4, _attackStartY, 90, 70)));
            _attack.AddFrame(new AnimationFrame(new Rectangle(_attackStartX + _schuifOpX * 5, _attackStartY, 90, 70)));
            _attack.AddFrame(new AnimationFrame(new Rectangle(_attackStartX + _schuifOpX * 6, _attackStartY, 90, 70)));
            _attack.AddFrame(new AnimationFrame(new Rectangle(_attackStartX + _schuifOpX * 7, _attackStartY, 90, 70)));
            _attack.AddFrame(new AnimationFrame(new Rectangle(_attackStartX + _schuifOpX * 8, _attackStartY, 90, 70)));

            //hit
            _hit = new Animation();
            _hit.AddFrame(new AnimationFrame(new Rectangle(_hitStartX, _hitStartY, 90, 70)));
            _hit.AddFrame(new AnimationFrame(new Rectangle(_hitStartX + _schuifOpX, _hitStartY, 90, 70)));
            _hit.AddFrame(new AnimationFrame(new Rectangle(_hitStartX + _schuifOpX * 2, _hitStartY, 90, 70)));
            //death
            _death = new Animation();
            _death.AddFrame(new AnimationFrame(new Rectangle(_deathStartX, _deathStartY, 90, 70)));
            _death.AddFrame(new AnimationFrame(new Rectangle(_deathStartX + _schuifOpX, _deathStartY, 90, 70)));
            _death.AddFrame(new AnimationFrame(new Rectangle(_deathStartX + _schuifOpX * 2, _deathStartY, 90, 70)));
            _death.AddFrame(new AnimationFrame(new Rectangle(_deathStartX + _schuifOpX * 3, _deathStartY, 90, 70)));
            _death.AddFrame(new AnimationFrame(new Rectangle(_deathStartX + _schuifOpX * 4, _deathStartY, 90, 70)));
            _death.AddFrame(new AnimationFrame(new Rectangle(_deathStartX + _schuifOpX * 5, _deathStartY, 90, 70)));

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
                case Action.death:
                    if (!(_death.NLoops >= 1))
                    {
                        _death.Update(gameTime);
                    }
                    break;
            }
        }

      

        public override void CheckForHero(Hero target)
        {
            if (Action != Action.death && Action != Action.hit)
            {
            
                if (State == State.right)
                {
                    if (target.Position.X >= Position.X && target.Position.X <= Position.X + 250)
                    {
                        Action = Action.attack;
                    }
                    else
                    {
                        Action = Action.run;
                    }
                }

                if (State == State.left)
                {
                    if (target.Position.X <= Position.X && target.Position.X >= Position.X - 250)
                    {
                        Action = Action.attack;
                    }
                    else
                    {
                        Action = Action.run;
                    }
                }
            }
        }
       
        public override void UpdateBoundingBox()
        {
            base.UpdateBoundingBox();
            if (State == State.left)
            {
                AttackHitbox = new Rectangle(BoundingBox.Left - 30, BoundingBox.Y- 40, 130, 170);
            }
            else
            {
                AttackHitbox = new Rectangle(BoundingBox.Right - 100, BoundingBox.Y - 40, 130, 170);
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
                    //spriteBatch.Draw(blokTexture2D, AttackHitbox, Color.Red);
                    spriteBatch.Draw(_texture, new Vector2(Position.X - 50, Position.Y- 50), _attack.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    break;
                case Action.hit:
                    spriteBatch.Draw(_texture, new Vector2(Position.X - 50, Position.Y), _hit.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
                    break;
                case Action.death:
                    spriteBatch.Draw(_texture, Position, _death.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(1, 1), new Vector2(3, 3), SpriteEffect, 0);
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
        public override void Reset()
        {
            Health = 3;
            Action = Action.run;
        }

    }
}
