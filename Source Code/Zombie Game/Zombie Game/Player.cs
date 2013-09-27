using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Zombie_Game
{
    class Player
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 origin;
        public float rotation = 0;

        public Player(Texture2D newTexture)
        {
            texture = newTexture;
        }

        public void Update(KeyboardState keyboard, MouseState mouse, Engine engine)
        {
            // Rortational Origin
            Rectangle originRect = new Rectangle((int) position.X, (int) position.Y, 
                (int) texture.Width, (int) texture.Height);
            origin = new Vector2(originRect.Width / 2, originRect.Height / 2);

            int playerSpeed = 2;

            if (keyboard.IsKeyDown(Keys.A))
                position.X -= playerSpeed;

            if (keyboard.IsKeyDown(Keys.D))
                position.X += playerSpeed;

            if (keyboard.IsKeyDown(Keys.W))
                position.Y -= playerSpeed;

            if (keyboard.IsKeyDown(Keys.S))
                position.Y += playerSpeed;

            float deltaY = mouse.Y - position.Y;
            float deltaX = mouse.X - position.X;
            float radians = (float) Math.Atan2(deltaY, deltaX);
            setRotation(radians);
        }

        public void setPosition(float x, float y)
        {
            position = new Vector2(x,y);
        }

        public void setRotation(float rot)
        {
            rotation = rot;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,position,null,Color.White,rotation,origin,1f,SpriteEffects.None,0);
        }
    }
}
