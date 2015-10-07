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
    class GameScreen
    {
        
        private Game1 game;
        private int screenWidth;
        private int screenHeight;
        private SpriteFont font;

        private Texture2D quitButton;
        private Vector2 quitButtonPosition;
        private Rectangle quitButtonRectangle;
        private Texture2D spaceTexture;

        private MouseState mouseState;
        private Point mousePosition;
        private ButtonState prevState = ButtonState.Released;

        
        private Player playerShip;
        private Texture2D playerTexture;
        private Texture2D playerAttackBallTexture;

        private List<AttackBall> playerAttackBallList = new List<AttackBall>();
        private float buttonFireDelay = 0.0f;
        private float enemyDelay = 0.0f;
        private float enemySpawnRate = 3.0f;

        private  List<AttackBall> enemyAttackBallList = new List<AttackBall>();

        
        List<Enemy> enemyShipList = new List<Enemy>();
        Texture2D enemyTexture;
        Texture2D enemyAttackBallTexture;
        Random rnd = new Random();

        float timer = 0.0f;
        Boolean runTimer;

        public GameScreen(Game1 game)
        {
            runTimer = true;
            this.game = game;
            screenWidth = game.GraphicsDevice.Viewport.Width;
            screenHeight = game.GraphicsDevice.Viewport.Height;

            quitButton = game.Content.Load<Texture2D>("quit");
            quitButtonPosition = new Vector2(1050, 650);
            quitButtonRectangle = new Rectangle((int)quitButtonPosition.X, (int)quitButtonPosition.Y, quitButton.Width, quitButton.Height);

            spaceTexture = game.Content.Load<Texture2D>("spaceBackground");
            playerTexture = game.Content.Load<Texture2D>("ship3");
            playerShip = new Player(game.GraphicsDevice, new Vector2(400, 300), playerTexture);

            playerAttackBallTexture = game.Content.Load<Texture2D>("playerAttack");
            enemyAttackBallTexture = game.Content.Load<Texture2D>("enemyAttack");
            enemyTexture = game.Content.Load<Texture2D>("enemy");

            font = game.Content.Load<SpriteFont>("myFont");

        }
        public void Update(GameTime gameTime)
        {
            if (runTimer)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            playerShip.Update(gameTime);
            
            UpdateInput();

            if (playerShip.Lives == 0) // game over
            {
                runTimer = false;
                Score playerScore = new Score(timer);
                game.gameOver(playerScore);
                saveScore(playerScore);

            }

            for (int i = 0; i < playerAttackBallList.Count; i++)
            {
                playerAttackBallList[i].Update(gameTime);
                if (playerAttackBallList[i].Position.Y < -100.0f)
                    playerAttackBallList.RemoveAt(i);
            }

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            buttonFireDelay -= elapsed;
            enemyDelay -= elapsed;


            for (int i = 0; i < enemyShipList.Count; i++) 
            { 
                enemyShipList[i].Update(gameTime); 
 
                if (enemyShipList[i].Firing) 
                { 
                    AttackBall attackBall = new AttackBall(enemyAttackBallTexture, new Vector2(enemyShipList[i].Position.X + 10.0f, enemyShipList[i].Position.Y + 30.0f), -300.0f); 
                    enemyAttackBallList.Add(attackBall); 
                    enemyShipList[i].Firing = false; 
                }


                int collide = enemyShipList[i].CollisionBall(playerAttackBallList);

                if (collide != -1)
                {

                    enemyShipList.RemoveAt(i);

                    playerAttackBallList.RemoveAt(collide);
                }
                else 
                    if (playerShip.CollisionTest(enemyShipList[i].Position,enemyShipList[i].Radius))
                    {
                        enemyShipList.RemoveAt(i);
                    }
                    else if (enemyShipList[i].Position.Y > 2000.0f)
                    {
                        enemyShipList.RemoveAt(i);
                    }
                        
                       
             } 
 
            for (int i = 0; i < enemyAttackBallList.Count; i++) 
            { 
                enemyAttackBallList[i].Update(gameTime);

                if (enemyAttackBallList[i].Position.Y > 900)
                {
                    enemyAttackBallList.RemoveAt(i); 
                }
                else if (playerShip.CollisionTest(enemyAttackBallList[i].Position, 20.0f))
                {
                    enemyAttackBallList.RemoveAt(i); 
                }
                       
            }
 

            if (enemyDelay <= 0.0f)
            {
                CreateEnemy();
                enemyDelay = enemySpawnRate;
                buttonFireDelay = 0.25f;

            }


            mouseState = Mouse.GetState();

            ButtonState currState = mouseState.LeftButton;
            if (currState == ButtonState.Pressed && currState != prevState)
            {
                mousePosition = new Point(mouseState.X, mouseState.Y);
                if (quitButtonRectangle.Contains(mousePosition))
                {
                    game.quitGame();
                }

                prevState = currState;

            } if (currState == ButtonState.Released)
            {
                prevState = currState;
            }

            
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(spaceTexture, Vector2.Zero, Color.White);

            String lives = playerShip.Lives.ToString() + " Lives ";
            spriteBatch.DrawString(font,lives, new Vector2(50,600),Color.White);

            spriteBatch.DrawString(font, timer.ToString("0.00"), new Vector2(1100, 600), Color.PeachPuff);

            spriteBatch.Draw(quitButton, quitButtonPosition, Color.White);
            playerShip.Draw(spriteBatch);

            foreach (AttackBall a in playerAttackBallList)
                a.Draw(spriteBatch);

            foreach (Enemy e in enemyShipList)
                e.Draw(spriteBatch);

            foreach (AttackBall a in enemyAttackBallList)
                a.Draw(spriteBatch);
           
            
        }
        private void UpdateInput()
        {
            KeyboardState keyState = Keyboard.GetState();


            if (keyState.IsKeyDown(Keys.Up) || keyState.IsKeyDown(Keys.W))
            {
                playerShip.Accelerate();
            }
            if (keyState.IsKeyDown(Keys.Down) || keyState.IsKeyDown(Keys.S))
            {
                playerShip.MoveReverse();
            }
            if (keyState.IsKeyDown(Keys.Left) || keyState.IsKeyDown(Keys.A))
            {
                playerShip.TurnLeft();
            }
            if (keyState.IsKeyDown(Keys.Right) || keyState.IsKeyDown(Keys.D))
            {
                playerShip.TurnRight();
            }
            if (keyState.IsKeyDown(Keys.Space))
            {
                if (buttonFireDelay <= 0.0f)
                {
                    AttackBall shot = new AttackBall(playerAttackBallTexture, new Vector2(playerShip.Position.X, playerShip.Position.Y - 50.0f), 300.0f);

                    playerAttackBallList.Add(shot);
                    buttonFireDelay = 0.45f;
                }

            }
        }

        public void CreateEnemy()
        {
            Random r = new Random();
            int startX = r.Next(1000);
            
            Enemy enemy = new Enemy(enemyTexture, new Vector2(startX, 0), 10.0f);
            enemy.SetAcrossMovement((float)startX / 800.0f * 250.0f, 100.0f);
            enemy.Firing = true;
            enemyShipList.Add(enemy);
            
            
        }
        public void saveScore(Score s)
        {
            List<Score> scores = loadScores();
            scores.Add(s);

            //String path = System.IO.Path.GetTempPath() + @"\scoreboard.xml";
            //String path = @"C:\score.xml";
            String path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\scoreboard.xml";

            FileStream stream = File.Open(path, FileMode.Create);
            try
            {
                // Convert the object to XML data and put it in the stream
                XmlSerializer serializer = new XmlSerializer(typeof(List<Score>));
                serializer.Serialize(stream, scores);


            }
            /*
        catch (IOException e)
        {

        }*/
            finally
            {

                stream.Close();
            }
        }
        public List<Score> loadScores()
        {
            List<Score> scores = new List<Score>();
            //String path = System.IO.Path.GetTempPath() + @"\scoreboard.xml";
            String path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\scoreboard.xml";
           // String path = @"C:\score.xml";

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
