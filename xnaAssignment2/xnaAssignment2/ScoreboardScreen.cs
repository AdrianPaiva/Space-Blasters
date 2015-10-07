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
using System.Xml.Serialization;
using System.IO;

/**************************************************
* Adrian Paiva, Patrick Gomes Sanches
* 100864588 
* created: Nov 26 2014
* lastEdit: December 3 2014
**************************************************/

namespace xnaAssignment2
{
    class ScoreboardScreen
    {
        private Texture2D backgroundTexture;
        private Game1 game;
        int screenWidth;
        int screenHeight;

        private Texture2D homeButton;
        private Vector2 homeButtonPosition;

        private MouseState mouseState;
        private Point mousePosition;

        private Rectangle homeButtonRectangle;
        ButtonState prevState = ButtonState.Released;
        private SpriteFont font;
        private SpriteFont scoreFont;

        private List<Score> scores;

        public ScoreboardScreen(Game1 game)
        {
            
            scores = loadScores();

            scores.Sort(delegate(Score x, Score y)
            {
                return y.time.CompareTo(x.time);
            });
            
            font = game.Content.Load<SpriteFont>("myFont");
            scoreFont = game.Content.Load<SpriteFont>("scoreFont");

            this.game = game;
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

            spriteBatch.DrawString(font,"NAME", new Vector2(450, 5), Color.PeachPuff);
            spriteBatch.DrawString(font, "SCORE", new Vector2(700, 5), Color.PeachPuff);

            
            
                foreach (Score s in scores)
                {
                    int a = scores.IndexOf(s) + 2;
                    spriteBatch.DrawString(scoreFont, s.name.ToString(), new Vector2(450, (25 * a)), Color.OrangeRed);
                    spriteBatch.DrawString(scoreFont, s.time.ToString(), new Vector2(700, (25 * a)), Color.OrangeRed);
                }
            
            
             
            
        }
        public List<Score> loadScores()
        {
            List<Score> scores = new List<Score>();
            //String path = System.IO.Path.GetTempPath() + @"\scoreboard.xml";
            //String path = @"C:\score.xml";
            String path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\scoreboard.xml";

            try
            {

                if (File.Exists(path))
                {
                    FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read);
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Score>));
                    scores = (List<Score>)serializer.Deserialize(stream);
                    
                    stream.Close();
                }

            }
            catch (Exception e)
            {

            }

            return scores;
        }
    }
    
}
