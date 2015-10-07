using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    class StartScreen
    {
        private Texture2D backgroundTexture;
        private Game1 game;

        int screenWidth;
        int screenHeight;

        private Texture2D startButton;
        private Texture2D scoreboardButton;
        private Texture2D controlsButton;

        private Vector2 startButtonPosition;
        private Vector2 scoreboardButtonPosition;
        private Vector2 controlsButtonPosition;

        private MouseState mouseState;
        private Point mousePosition;

        private SpriteFont font;

        private Rectangle startButtonRectangle;
        private Rectangle scoreboardButtonRectangle;
        private Rectangle controlsButtonRectangle;

        ButtonState prevState = ButtonState.Released;

        public StartScreen(Game1 game)
        {
            this.game = game;
            screenWidth = game.GraphicsDevice.Viewport.Width;
            screenHeight = game.GraphicsDevice.Viewport.Height;


            backgroundTexture = game.Content.Load<Texture2D>("spaceBackground");
            font = game.Content.Load<SpriteFont>("myFont");

            startButton = game.Content.Load<Texture2D>("startButton");
            startButtonPosition = new Vector2((screenWidth / 2) - 100, (screenHeight / 2)-100);

            scoreboardButton = game.Content.Load<Texture2D>("scoreboard");
            scoreboardButtonPosition = new Vector2((screenWidth / 2) - 100, (screenHeight / 2));

            controlsButton = game.Content.Load<Texture2D>("controls");
            controlsButtonPosition = new Vector2((screenWidth / 2) - 100, (screenHeight / 2) + 100);

            startButtonRectangle = new Rectangle((int)startButtonPosition.X, (int)startButtonPosition.Y, startButton.Width, startButton.Height);
            scoreboardButtonRectangle = new Rectangle((int)scoreboardButtonPosition.X, (int)scoreboardButtonPosition.Y, scoreboardButton.Width, scoreboardButton.Height);
            controlsButtonRectangle = new Rectangle((int)controlsButtonPosition.X, (int)controlsButtonPosition.Y, controlsButton.Width, controlsButton.Height);


        }
        public void Update()
        {
            


            mouseState = Mouse.GetState();

            ButtonState currState = mouseState.LeftButton;
            if (currState == ButtonState.Pressed && currState != prevState)
            {
                mousePosition = new Point(mouseState.X, mouseState.Y);

                if (startButtonRectangle.Contains(mousePosition))
                {
                    game.startGame();
                }
                else if (scoreboardButtonRectangle.Contains(mousePosition))
                {
                    game.startScoreBoard();
                }
                else if (controlsButtonRectangle.Contains(mousePosition))
                {
                    game.startControls();
                }
                prevState = currState;

            } if (currState == ButtonState.Released)
            {
                prevState = currState;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (backgroundTexture != null)
            {
                Rectangle screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
                spriteBatch.Draw(backgroundTexture, screenRectangle, Color.White);
            }
            spriteBatch.Draw(startButton, startButtonPosition, Color.White);
            spriteBatch.Draw(scoreboardButton, scoreboardButtonPosition, Color.White);
            spriteBatch.Draw(controlsButton, controlsButtonPosition, Color.White);
            spriteBatch.DrawString(font, "SPACE BLASTER!", new Vector2(450, 5), Color.PeachPuff);
            spriteBatch.DrawString(font, "See How Long You Can Survive!", new Vector2(275,600), Color.PeachPuff);

        }
    }
}
