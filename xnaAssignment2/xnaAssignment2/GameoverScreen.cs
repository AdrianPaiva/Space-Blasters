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
using System.IO;
using System.Xml.Serialization;

/**************************************************
* Adrian Paiva, Patrick Gomes Sanches
* 100864588 
* created: Nov 26 2014
* lastEdit: December 3 2014
**************************************************/

namespace xnaAssignment2
{
    class GameoverScreen
    {
        private Texture2D backgroundTexture;
        private Game1 game;
        int screenWidth;
        int screenHeight;

        private Texture2D scoreButton;
        private Vector2 scoreButtonPosition;

        private MouseState mouseState;
        private Point mousePosition;

        private Rectangle scoreButtonRectangle;
        ButtonState prevState = ButtonState.Released;
        private SpriteFont font;
        private Score score;

        public GameoverScreen(Game1 game, Score score)
        {
            
            font = game.Content.Load<SpriteFont>("myFont");

            //saveScore(score);

            this.score = score;
            this.game = game;
            screenWidth = game.GraphicsDevice.Viewport.Width;
            screenHeight = game.GraphicsDevice.Viewport.Height;
            backgroundTexture = game.Content.Load<Texture2D>("spaceBackground");

            scoreButton = game.Content.Load<Texture2D>("scoreboard");
            scoreButtonPosition = new Vector2(1000, 550);
            scoreButtonRectangle = new Rectangle((int)scoreButtonPosition.X, (int)scoreButtonPosition.Y, scoreButton.Width, scoreButton.Height);
        }
        public void Update()
        {

            mouseState = Mouse.GetState();

            ButtonState currState = mouseState.LeftButton;
            if (currState == ButtonState.Pressed && currState != prevState)
            {
                mousePosition = new Point(mouseState.X, mouseState.Y);
                if (scoreButtonRectangle.Contains(mousePosition))
                {
                    game.startScoreBoard();
                }

                prevState = currState;

            } if (currState == ButtonState.Released)
            {
                prevState = currState;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Draw(backgroundTexture, screenRectangle, Color.White);

            spriteBatch.Draw(scoreButton, scoreButtonPosition, Color.White);

            spriteBatch.DrawString(font, "GAME OVER " + score.name, new Vector2((screenWidth /2) - 300, 5), Color.PeachPuff);
            spriteBatch.DrawString(font, "Your Time is: " + score.time, new Vector2((screenWidth / 2) - 300, 200), Color.PeachPuff);  

        }
       

    }
}
