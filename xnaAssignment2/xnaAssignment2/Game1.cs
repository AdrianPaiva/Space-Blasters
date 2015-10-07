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

/**************************************************
* Adrian Paiva, Patrick Gomes Sanches
* 100864588 
* created: Nov 26 2014
* lastEdit: December 3 2014
**************************************************/

namespace xnaAssignment2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        enum Screen
        {
            StartScreen,
            GameScreen,
            ScoreboardScreen,
            ControlsScreen,
            GameoverScreen
        }
        Screen currentScreen;

        StartScreen startScreen;
        GameScreen gameScreen;
        ScoreboardScreen scoreboardScreen;
        ControlsScreen controlsScreen;
        GameoverScreen gameoverScreen;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
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

            startScreen = new StartScreen(this);
            currentScreen = Screen.StartScreen;


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
            switch (currentScreen)
            {
                case Screen.StartScreen:
                    if (startScreen != null)
                        startScreen.Update();
                    break;
                case Screen.GameScreen:
                    if (gameScreen != null)
                        gameScreen.Update(gameTime);
                    break;
                case Screen.ScoreboardScreen:
                    if (scoreboardScreen != null)
                        scoreboardScreen.Update();
                    break;
                case Screen.ControlsScreen:
                    if (controlsScreen != null)
                        controlsScreen.Update();
                    break;
                case Screen.GameoverScreen:
                    if (gameoverScreen != null)
                        gameoverScreen.Update();
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
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch (currentScreen)
            {
                case Screen.StartScreen:
                    if (startScreen != null)
                        startScreen.Draw(spriteBatch);
                    break;
                case Screen.GameScreen:
                    if (gameScreen != null)
                        gameScreen.Draw(gameTime,spriteBatch);
                    break;
                case Screen.ScoreboardScreen:
                    if (scoreboardScreen != null)
                        scoreboardScreen.Draw(spriteBatch);
                    break;
                case Screen.ControlsScreen:
                    if (controlsScreen != null)
                        controlsScreen.Draw(spriteBatch);
                        break;
                case Screen.GameoverScreen:
                        if (gameoverScreen != null)
                            gameoverScreen.Draw(spriteBatch);
                        break;

            }


            spriteBatch.End(); 

            base.Draw(gameTime);
        }
        public void startGame()
        {
            gameScreen = new GameScreen(this);
            currentScreen = Screen.GameScreen;
            startScreen = null;
            controlsScreen = null; 
            gameoverScreen = null;
        }
        public void startScoreBoard()
        {
            scoreboardScreen = new ScoreboardScreen(this);
            currentScreen = Screen.ScoreboardScreen;
            startScreen = null;
            gameScreen = null;
            controlsScreen = null; 
            gameoverScreen = null;
        }
        public void quitGame()
        {
            startScreen = new StartScreen(this);
            currentScreen = Screen.StartScreen;
            gameScreen = null;
            scoreboardScreen = null;
            controlsScreen = null; 
            gameoverScreen = null;
        }
        public void startControls()
        {
            controlsScreen = new ControlsScreen(this);
            currentScreen = Screen.ControlsScreen;
            gameScreen = null;
            scoreboardScreen = null;
            startScreen = null;
            gameoverScreen = null;
        }
        public void gameOver(Score score)
        {
            gameoverScreen = new GameoverScreen(this, score);
            currentScreen = Screen.GameoverScreen;
            gameScreen = null;
            scoreboardScreen = null;
            startScreen = null;
            controlsScreen = null;
        }
       
    }
}
