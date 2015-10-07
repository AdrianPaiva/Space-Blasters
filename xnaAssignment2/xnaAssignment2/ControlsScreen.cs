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
    class ControlsScreen
    {
        private Game1 game;
        int screenWidth;
        int screenHeight;
        private Texture2D backgroundTexture;

        private Texture2D homeButton;
        private Vector2 homeButtonPosition;

        private MouseState mouseState;
        private Point mousePosition;

        private Rectangle homeButtonRectangle;
        ButtonState prevState = ButtonState.Released;
        private SpriteFont font;
        private SpriteFont smallfont;

        public ControlsScreen(Game1 game)
        { 

            this.game = game;
            screenWidth = game.GraphicsDevice.Viewport.Width;
            screenHeight = game.GraphicsDevice.Viewport.Height;

            font = game.Content.Load<SpriteFont>("myFont");
            smallfont = game.Content.Load<SpriteFont>("scoreFont");
 
            screenWidth = game.GraphicsDevice.Viewport.Width;
            screenHeight = game.GraphicsDevice.Viewport.Height;
            backgroundTexture = game.Content.Load<Texture2D>("spaceBackground");

            homeButton = game.Content.Load<Texture2D>("mainMenu");
            homeButtonPosition = new Vector2(1050, 650);
            homeButtonRectangle = new Rectangle((int)homeButtonPosition.X, (int)homeButtonPosition.Y, homeButton.Width, homeButton.Height);
            

        }
        public void Update()
        {

            mouseState = Mouse.GetState();

            ButtonState currState = mouseState.LeftButton;
            if (currState == ButtonState.Pressed && currState != prevState)
            {
                mousePosition = new Point(mouseState.X, mouseState.Y);
                if (homeButtonRectangle.Contains(mousePosition))
                {
                    game.quitGame();
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

            spriteBatch.Draw(homeButton, homeButtonPosition, Color.White);

            spriteBatch.DrawString(font, "CONTROLS", new Vector2(550, 5), Color.PeachPuff);

            spriteBatch.DrawString(smallfont, "Move Left", new Vector2(200, 100), Color.PeachPuff);
            spriteBatch.DrawString(smallfont, "Move Right", new Vector2(200, 200), Color.PeachPuff);
            spriteBatch.DrawString(smallfont, "Move Up", new Vector2(200, 300), Color.PeachPuff);
            spriteBatch.DrawString(smallfont, "Move Down", new Vector2(200, 400), Color.PeachPuff);
            spriteBatch.DrawString(smallfont, "Fire", new Vector2(200, 500), Color.PeachPuff);

            spriteBatch.DrawString(smallfont, "A or Left arrow", new Vector2(500, 100), Color.Tomato);
            spriteBatch.DrawString(smallfont, "D or Right arrow", new Vector2(500, 200), Color.Tomato);
            spriteBatch.DrawString(smallfont, "W or Up arrow", new Vector2(500, 300), Color.Tomato);
            spriteBatch.DrawString(smallfont, "S or down arrow", new Vector2(500, 400), Color.Tomato);
            spriteBatch.DrawString(smallfont, "Spacebar", new Vector2(500, 500), Color.Tomato);
            
            
           
            
        }
    }
}
