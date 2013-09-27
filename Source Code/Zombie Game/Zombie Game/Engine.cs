using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Zombie_Game
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Engine : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Game States
        enum GameState
        {
            MainMenu,
            Options,
            Playing,
        }

        GameState currentGameState = GameState.MainMenu;

        // Screen Adjustments
        int screenWidth = 1360, screenHeight = 768;

        // Buttons
        Button buttonPlay;

        // Player
        Player player;

        // Zombies
        List<Zombie> zombies = new List<Zombie>();

        // Bullets
        List<Bullet> bullets = new List<Bullet>();

        // Mouse Clearing
        bool leftClicked = false;
        bool rightClicked = false;

        public Engine()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            // Makes the mouse visible
            IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Screen Adjustments
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            //graphics.IsFullScreen = true;

            // Loads the button for the main menu
            buttonPlay = new Button(Content.Load<Texture2D>("Button"), graphics.GraphicsDevice);
            buttonPlay.setPosition(350, 300);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here

            MouseState mouse = Mouse.GetState();

            // Game State Managing
            switch (currentGameState)
            {
                case GameState.MainMenu:

                    if (buttonPlay.isClicked == true)
                    {
                        currentGameState = GameState.Playing;

                        // Player Initialization
                        player = new Player(Content.Load<Texture2D>("res/player/playerRifle"));
                        player.setPosition(50, 50);
                    }

                    buttonPlay.Update(mouse);
                    break;

                case GameState.Playing:

                    player.Update(Keyboard.GetState(), Mouse.GetState(), this);

                    foreach (Zombie z in zombies)
                    {
                        z.Update(player, Keyboard.GetState(), mouse, this);
                    }

                    // Shooting The Weapon
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        if (!leftClicked)
                        {
                            bullets.Add(new Bullet(Content.Load<Texture2D>("res/bullets/bullet2"),
                            mouse, player.position, 20));
                            leftClicked = true;
                        }
                    }
                    // Clearing The Mouse Button
                    else
                    {
                        leftClicked = false;
                    }

                    // Making Zombies
                    if (mouse.RightButton == ButtonState.Pressed)
                    {
                        if (!rightClicked)
                        {
                            zombies.Add(new Zombie(Content.Load<Texture2D>("res/ZomTest/zTest"),Mouse
                                .GetState().X, Mouse.GetState().Y));
                            rightClicked = true;
                        }
                    }
                    // Clearing The Mouse Button
                    else
                    {
                        rightClicked = false;
                    }

                    // Bullet Updating
                    foreach (Bullet b in bullets)
                    {
                        b.Update();
                        if (Vector2.Distance(b.position, player.position) > 750)
                            b.isVisible = false;
                    }

                    // Clearing Lost Bullets
                    for (int x = 0; x < bullets.Count; x++)
                    {
                        if (!bullets[x].isVisible)
                            bullets.RemoveAt(x);
                    }

                        break;

                case GameState.Options:

                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            switch (currentGameState)
            {
                case GameState.MainMenu:
                    spriteBatch.Draw(Content.Load<Texture2D>("MainMenu"), 
                        new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    buttonPlay.Draw(spriteBatch);
                    break;

                case GameState.Playing:
                    player.Draw(spriteBatch);

                    foreach (Zombie z in zombies)
                    {
                        z.Draw(spriteBatch);
                    }

                    foreach (Bullet b in bullets)
                    {
                        b.Draw(spriteBatch);
                    }
                    break;

                case GameState.Options:

                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
