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
    class Player
    {
        private Vector2 position;
        private Texture2D shipSprite;
        private Vector2 spriteOrigin;
        private int windowWidth, windowHeight;
        private float velocity = 0.0f;
        private int lives = 5;
        private float radius = 50.0f;
        private  Boolean recovering = false;
        private float recoverTime = 0.1f;
        private float total = 0.0f;


        public Player(GraphicsDevice device, Vector2 position, Texture2D sprite)
        {
            
            this.position = position;
            shipSprite = sprite;
            spriteOrigin.X = (float)shipSprite.Width / 2.0f;
            spriteOrigin.Y = (float)shipSprite.Height / 2.0f;
            windowHeight = device.Viewport.Height;
            windowWidth = device.Viewport.Width;

        }
        public void Draw(SpriteBatch batch)
        {
            batch.Draw(shipSprite, position, null, Color.White,
                                0.0f, spriteOrigin, 1.0f, SpriteEffects.None, 0.0f);

        }
        public void Update(GameTime gameTime)
        {
            position.Y += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            velocity *= 0.95f;

            if (position.Y < 0) position.Y = 0.0f;
            if (position.Y > windowHeight) position.Y = windowHeight;
            if (position.X < 0) position.X = 0.0f;
            if (position.X > windowWidth) position.X = windowWidth;


            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            total += elapsed;

            if (total > recoverTime)
            {
                recovering = false;
            }
            
        }
        public void Accelerate()
        {
            position.Y -= 3.0f;
            velocity -= 20.0f; 
        }

        public void MoveReverse()
        {
            position.Y += 3.0f;
            velocity += 20.0f;
        }

        public void TurnRight()
        {
            position.X += 3.0f;
        }

        public void TurnLeft()
        {
            position.X -= 3.0f;
        }
        public Vector2 Position
        {
            get
            {
                return position;
            }
        }
        public int Lives
        {
            set { lives = value; }
            get { return lives; }
        }

        public bool CollisionTest(Vector2 point, float radius)
        {
            if ((point - position).Length() < this.radius + radius)
            {
                if (!recovering)
                {
                    lives--;
                    total = 0.0f;
                    recovering = true;
                }
                           
                
                return true;
            }
            return false;
        } 
    }
}
