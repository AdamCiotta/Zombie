using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Zombie_Game
{
    class Zombie
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 origin;
        public float rotation = 0;

        public Zombie(Texture2D newTexture, int x, int y)
        {
            texture = newTexture;
            position = new Vector2(x, y);
        }

        public void Update(Player player, KeyboardState keyboard, MouseState mouse, Engine engine)
        {
            // Rortational Origin
            Rectangle originRect = new Rectangle((int)position.X, (int)position.Y,
                (int)texture.Width, (int)texture.Height);
            origin = new Vector2(originRect.Width / 2, originRect.Height / 2);

            int viewDistance = 200;

            double distance = Math.Sqrt(Math.Pow(player.position.X - position.X, 2) + 
                Math.Pow(player.position.Y - position.Y, 2));

            if (distance < viewDistance)
            {
                float deltaX = player.position.X - position.X;
                float deltaY = player.position.Y - position.Y;
                float radians = (float)Math.Atan2(deltaY, deltaX);
                setRotation(radians);

                float zombieSpeed = 1.5f;
                float dx = (float) Math.Cos(rotation) * zombieSpeed;
                float dy = (float) Math.Sin(rotation) * zombieSpeed;
                position.X += dx;
                position.Y += dy;
            }
        }

        public void setPosition(float x, float y)
        {
            position = new Vector2(x, y);
        }

        public void setRotation(float rot)
        {
            rotation = rot;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0);
        }
    }
}
