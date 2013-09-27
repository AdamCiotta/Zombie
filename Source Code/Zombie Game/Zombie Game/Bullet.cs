using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Zombie_Game
{
    class Bullet
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 origin;
        public Vector2 velocity;
        public float rotation = 0;
        public bool isVisible = true;

        public Bullet(Texture2D newTexture, MouseState mouse, Vector2 pos, float speed)
        {
            texture = newTexture;
            position = pos;

            Rectangle originRect = new Rectangle((int)position.X, (int)position.Y,
               (int)texture.Width, (int)texture.Height);
            origin = new Vector2(originRect.Width / 2, originRect.Height / 2);

            float deltaY = mouse.Y - position.Y;
            float deltaX = mouse.X - position.X;
            float radians = (float)Math.Atan2(deltaY, deltaX);
            rotation = radians;

            velocity = new Vector2((float) Math.Cos(rotation) * speed, (float) Math.Sin(rotation) * speed);
        }

        public void Update()
        {
            position += velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0);
        }
    }
}
