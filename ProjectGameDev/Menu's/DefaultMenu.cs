using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Reflection.Metadata;

namespace ProjectGameDev.Menu_s
{
    internal abstract class DefaultMenu
    {
        public Texture2D Texture;
        public SpriteFont Font;
        public Rectangle MainMenu;
        public Rectangle MenuItem;
        public SpriteEffects SpriteEffect = SpriteEffects.None;
        public Rectangle Hitbox1, Hitbox2, Hitbox3;


        public int Width = 280, Height = 380;
        public Vector2 Position;
        public Vector2 PositionItem1, PositionItem2, PositionItem3;
        public DefaultMenu(ContentManager content)
        {
            PositionItem1 = new Vector2(Game1.ScreenWidth / 2 - Width / 2 + 40, Game1.ScreenHeight / 2 - Height / 2 + 50);
            PositionItem2 = new Vector2(Game1.ScreenWidth / 2 - Width / 2 + 40, Game1.ScreenHeight / 2 - Height / 2 + 150);
            PositionItem3 = new Vector2(Game1.ScreenWidth / 2 - Width / 2 + 40, Game1.ScreenHeight / 2 - Height / 2 + 250);

            Hitbox1 = new Rectangle(Convert.ToInt16(PositionItem1.X), Convert.ToInt16(PositionItem1.Y), 195, 80);
            Hitbox2 = new Rectangle(Convert.ToInt16(PositionItem2.X), Convert.ToInt16(PositionItem2.Y), 195, 80);
            Hitbox3 = new Rectangle(Convert.ToInt16(PositionItem3.X), Convert.ToInt16(PositionItem3.Y), 195, 80);
            MenuItem = new Rectangle(310, 283, 195, 80);
            
            Font = content.Load<SpriteFont>("Font"); ;
            Texture = content.Load<Texture2D>("Menu/Menu");
            
            Position = new Vector2(Game1.ScreenWidth / 2 - Width / 2, Game1.ScreenHeight / 2 - Height / 2);
            MainMenu = new Rectangle(30, 220, Width, Height);
        }
    }
}
